using BussinessObjects;
using Microsoft.Win32;
using Services;
using System;
using System.Collections.Generic;
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

namespace WPFApp.Document_Management
{
    /// <summary>
    /// Interaction logic for DocumentManagePopup.xaml
    /// </summary>
    public partial class DocumentManagePopup : Window
    {
        private readonly IService<Document> documentService;
        private readonly IService<Course> courseService;
        private Document selectedDocument;
        public DocumentManagePopup(IService<Document> Dservice, IService<Course> Cservice)
        {
            InitializeComponent();
            documentService = Dservice;
            courseService = Cservice;

        }
        public void SetDocument(Document document)
        {
            selectedDocument = document;
            if (selectedDocument != null)
            {
                cmbCourse.SelectedValue = selectedDocument.CourseId;
                txtTitle.Text = selectedDocument.Title;
                txtDescription.Text = selectedDocument.Description;
            }
        }
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            cmbCourse.ItemsSource = courseService.GetAll(); 
        }

        private void Upload_Button(object sender, RoutedEventArgs e)
        {
            Microsoft.Win32.OpenFileDialog openFileDialog = new Microsoft.Win32.OpenFileDialog();
            if (openFileDialog.ShowDialog() == true)
            {
                selectedDocument.FileAttachment = File.ReadAllBytes(openFileDialog.FileName);
                System.Windows.MessageBox.Show("File uploaded successfully.");
            }
        }

        private void Submit_Button(object sender, RoutedEventArgs e)
        {
            if (selectedDocument == null)
            {
                selectedDocument = new Document();
            }

            selectedDocument.CourseId = (int?)cmbCourse.SelectedValue;
            selectedDocument.Title = txtTitle.Text;
            selectedDocument.Description = txtDescription.Text;

            if (selectedDocument.DocumentId == 0)
            {
                int maxId = documentService.GetAll().Max(d => (int?)d.DocumentId) ?? 0;
                selectedDocument.DocumentId = maxId + 1;
                documentService.Add(selectedDocument);
            }
            else
            {
                documentService.Update(selectedDocument);
            }

            System.Windows.MessageBox.Show("Document saved successfully.");
            this.Hide();
        }


        private void Close_Button(object sender, RoutedEventArgs e)
        {
            this.Hide();
        }
    }
}
