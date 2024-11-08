using BussinessObjects;
using DataAccessObjects;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using Services;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Forms;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using WPFApp.Login_and_home_page_of_each_role;

namespace WPFApp.Document_Management
{
    /// <summary>
    /// Interaction logic for DocumentManagementWindow.xaml
    /// </summary>
    public partial class DocumentManagementWindow : Window
    {
        private readonly IService<Document> service;
        private Document selectedDocument;  
        private readonly string UserRole;

        public DocumentManagementWindow(IService<Document> Dservice, string role)
        {
            InitializeComponent();
            UserRole = role;
            service = Dservice;
        }
        private void LoadDocuments()
        {
            if (UserRole == "Lecturer")
            {
                UserTabControl.SelectedIndex = 0;
                StudentTab.Visibility = Visibility.Collapsed;
                dgLDocument.ItemsSource = service.GetAll();

            }
            else if (UserRole == "Student")
            {
                UserTabControl.SelectedIndex = 1;
                LecturerTab.Visibility = Visibility.Collapsed;
                dgSDocument.ItemsSource = service.GetAll();
            }
        }
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            LoadDocuments();
        }
        private void Search_Button(object sender, RoutedEventArgs e)
        {
            string title = null;
            if (!string.IsNullOrWhiteSpace(txtLSearch.Text)) title = txtLSearch.Text;

            if (!string.IsNullOrWhiteSpace(txtSSearch.Text)) title = txtSSearch.Text;

            if (!string.IsNullOrEmpty(title))
            {
                var searchResults = service.GetAll().Where(c => c.Title.Contains(title, StringComparison.OrdinalIgnoreCase)).ToList();
                if(UserRole == "Lecturer") dgLDocument.ItemsSource = searchResults;
                if(UserRole == "Student") dgSDocument.ItemsSource = searchResults;

                if (searchResults.Count == 0)
                {
                    System.Windows.MessageBox.Show("No Document found with the specified title.");
                }
            }
            else
            {
                System.Windows.MessageBox.Show("Please enter a Document title.");
            }
        }
        private void Upload_Button(object sender, RoutedEventArgs e)
        {
            IService<Document> documentService = App.ServiceProvider.GetRequiredService<IService<Document>>();
            IService<Course> courseService = App.ServiceProvider.GetRequiredService<IService<Course>>();
            DocumentManagePopup documentManagePopup = new DocumentManagePopup(documentService, courseService);
            documentManagePopup.ShowDialog();
            LoadDocuments();
        }
        private void Update_Button(object sender, RoutedEventArgs e)
        {
            selectedDocument = dgLDocument.SelectedItem as Document;
            if (selectedDocument != null)
            {
                DocumentManagePopup documentManagePopup = App.ServiceProvider.GetRequiredService<DocumentManagePopup>();
                documentManagePopup.SetDocument(selectedDocument);
                documentManagePopup.ShowDialog();
                LoadDocuments();
            }
            else
            {
                System.Windows.MessageBox.Show("Please select a document to update.");
            }
        }
        private void Delete_Button(object sender, RoutedEventArgs e)
        {
            selectedDocument = dgLDocument.SelectedItem as Document;
            var result = System.Windows.MessageBox.Show("Are you sure you want to delete this Document?",
                                 "Delete Confirmation",
                                 MessageBoxButton.YesNo,
                                 MessageBoxImage.Warning);

            if (result == MessageBoxResult.Yes)
            {
                if (selectedDocument != null)
                {
                    service.Delete(selectedDocument);
                    System.Windows.MessageBox.Show("Document deleted successfully.");
                }
                else
                {
                    System.Windows.MessageBox.Show("No document selected to delete.");
                }
                LoadDocuments();
            }
            else
            {
                System.Windows.MessageBox.Show("Document deleted successfully.");
            }
        }
        private void Home_Button(object sender, RoutedEventArgs e)
        {
            if (UserRole == "Lecturer")
            {
                this.Hide();
                Dashboard_for_lecturer dashboard_For_Lecturer = new Dashboard_for_lecturer();
                dashboard_For_Lecturer.Show();
            }
            else if (UserRole == "Student")
            {
                this.Hide();
                System.Windows.Application.Current.Windows.OfType<Dashboard_for_student>().FirstOrDefault()?.Show();
            }
        }
        private void Download_Button(object sender, RoutedEventArgs e)
        {
            selectedDocument = dgSDocument.SelectedItem as Document;
            if (selectedDocument != null)
            {
                try
                {
                    byte[] fileData = selectedDocument.FileAttachment;
                    if (fileData == null || fileData.Length == 0)
                    {
                        System.Windows.MessageBox.Show("File data is empty.");
                        return;
                    }

                    SaveFileDialog saveFileDialog = new SaveFileDialog
                    {
                        FileName = selectedDocument.Title,
                        Filter = "All files (*.*)|*.*"
                    };

                        File.WriteAllBytes(saveFileDialog.FileName, fileData);
                        System.Windows.MessageBox.Show("File downloaded successfully.");
                }
                catch (Exception ex)
                {
                    System.Windows.MessageBox.Show($"Error downloading file: {ex.Message}");
                }
            }
            else
            {
                System.Windows.MessageBox.Show("Please select a document to download.");
            }
        }
    }
}
