using BussinessObjects;
using Microsoft.Extensions.DependencyInjection;
using Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Windows.Xps.Packaging;
using System.Xml;
using WPFApp.Document_Management;
using WPFApp.Login_and_home_page_of_each_role;

namespace WPFApp.Department_Management
{
    /// <summary>
    /// Interaction logic for DepartmentManagementWindow.xaml
    /// </summary>
    public partial class DepartmentManagementWindow : Window
    {
        private readonly IService<Department> departmentService;
        private readonly IService<Course> courseService;
        private Department selectedDepartment;
        private string UserRole;
        private int? userID;
        public DepartmentManagementWindow(IService<Department> dservice, IService<Course> cservice, string role, int? Id = null)
        {
            InitializeComponent();
            UserRole = role;
            userID = Id;
            departmentService = dservice;
            courseService = cservice;
        }
        private void LoadData()
        {
            if (UserRole == "HoD" && userID.HasValue)
            {
                UserTabControl.SelectedIndex = 1;
                AdminTab.Visibility = Visibility.Collapsed;
                dgCourse.ItemsSource = courseService.GetAll();

            }
            else if (UserRole == "Admin")
            {
                UserTabControl.SelectedIndex = 0;
                HoDTab.Visibility = Visibility.Collapsed;
                dgDepartment.ItemsSource = departmentService.GetAll();
            }
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            LoadData();
        }

        private void Home_Button(object sender, RoutedEventArgs e)
        {
            if (UserRole == "HoD")
            {
                this.Hide();
                System.Windows.Application.Current.Windows.OfType<Dashboard_for_Head_of_Department>().FirstOrDefault()?.Show();
            }
            else if (UserRole == "Admin")
            {
                this.Hide();
                Dashboard_for_admin dashboard_For_Admin = new Dashboard_for_admin();
                dashboard_For_Admin.Show();
            }
        }

        private void Search_Button(object sender, RoutedEventArgs e)
        {
            string title = null;
            if (!string.IsNullOrWhiteSpace(txtASearch.Text)) title = txtASearch.Text;

            if (!string.IsNullOrWhiteSpace(txtHSearch.Text)) title = txtHSearch.Text;

            if (!string.IsNullOrEmpty(title))
            {
                if (UserRole == "HoD")
                {
                    var searchResults = courseService.GetAll().Where(c => c.CourseName.Contains(title, StringComparison.OrdinalIgnoreCase)).ToList();
                    dgCourse.ItemsSource = searchResults;
                    if (searchResults.Count == 0)
                    {
                        System.Windows.MessageBox.Show("No courses found with the specified name.");
                    }
                }
                if (UserRole == "Admin")
                {
                    var searchResults = departmentService.GetAll().Where(c => c.DepartmentName.Contains(title, StringComparison.OrdinalIgnoreCase)).ToList();
                    dgDepartment.ItemsSource = searchResults;
                    if (searchResults.Count == 0)
                    {
                        System.Windows.MessageBox.Show("No departments found with the specified name.");
                    }
                }
            }
            else
            {
                System.Windows.MessageBox.Show("Please enter a name.");
            }
        }

        private void Create_Button(object sender, RoutedEventArgs e)
        {
            selectedDepartment = null;
            DepartmentManagementPopup departmentManagementPopup = App.ServiceProvider.GetRequiredService<DepartmentManagementPopup>();
            departmentManagementPopup.ClearData();
            departmentManagementPopup.ShowDialog();
            LoadData();
        }

        private void Update_Button(object sender, RoutedEventArgs e)
        {
            selectedDepartment = dgDepartment.SelectedItem as Department;
            if (selectedDepartment != null)
            {
                DepartmentManagementPopup departmentManagementPopup = App.ServiceProvider.GetRequiredService<DepartmentManagementPopup>();
                departmentManagementPopup.SetDepartment(selectedDepartment);
                departmentManagementPopup.ShowDialog();
                LoadData();
            }
            else
            {
                System.Windows.MessageBox.Show("Please select a department to update.");
            }
        }

        private void Delete_Button(object sender, RoutedEventArgs e)
        {
            selectedDepartment = dgDepartment.SelectedItem as Department;
            var result = MessageBox.Show("Are you sure you want to delete this department?",
                                 "Delete Confirmation",
                                 MessageBoxButton.YesNo,
                                 MessageBoxImage.Warning);
            if (result == MessageBoxResult.Yes)
            {
                if (selectedDepartment != null && selectedDepartment.DepartmentId > 0)
                {
                    departmentService.Delete(selectedDepartment);
                    MessageBox.Show("Department deleted successfully.");
                }
                else
                {
                    MessageBox.Show("No department selected to delete.");
                }
                LoadData();
            }
            else
            {
                MessageBox.Show("Department deletion canceled.");
            }
        }

        private void Link_Button(object sender, RoutedEventArgs e)
        {
            if (userID.HasValue)
            {
                var department = departmentService.GetAll().FirstOrDefault(d => d.HeadId == userID.Value);
                if (department != null)
                {
                    var selectedCourse = dgCourse.SelectedItem as Course;
                    if (selectedCourse != null)
                    {
                        selectedCourse.DepartmentId = department.DepartmentId;
                        courseService.Update(selectedCourse);
                        LoadData();
                        System.Windows.MessageBox.Show("Course linked to department successfully.");
                    }
                    else
                    {
                        System.Windows.MessageBox.Show("Please select a course to link.");
                    }
                }
                else
                {
                    System.Windows.MessageBox.Show("No department found for your role.");
                }
            }
            else
            {
                System.Windows.MessageBox.Show("User ID is not available.");
            }
        }
    }
}
