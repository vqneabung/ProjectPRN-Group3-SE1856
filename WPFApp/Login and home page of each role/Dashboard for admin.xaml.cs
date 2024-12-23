﻿using BussinessObjects;
using DataAccessObjects;
using Microsoft.EntityFrameworkCore;
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
using WPFApp.Blog___News;
using WPFApp.Department_Management;
using WPFApp.User_Manager;

namespace WPFApp.Login_and_home_page_of_each_role
{
    /// <summary>
    /// Interaction logic for Dashboard_for_admin.xaml
    /// </summary>
    public partial class Dashboard_for_admin : Window
    {
        private readonly UserDAO _userDAO;
        private readonly BlogNewsDAO _BlogNews;
        private readonly CourseDAO _courseDAO;
        private readonly DepartmentDAO _departmentDAO;
        private readonly LmsContext _context;
        
        public Dashboard_for_admin()
        {
            _context = new LmsContext();
            _userDAO = new UserDAO();
            _BlogNews = new BlogNewsDAO(_context);
            _courseDAO = new CourseDAO(_context);
            _departmentDAO = new DepartmentDAO(_context);
            InitializeComponent();
            LoadBlogNewsList();
            LoadStatisticalList();
            DataContext = this;
        }

        private void LoadBlogNewsList()
        {
            BlogNewsDataGrid.ItemsSource = _BlogNews.GetAll();
        }

        private void LoadStatisticalList()
        {
            statisticalDataGrid.ItemsSource = LoadCount();
        }

        public class StatisticsItem
        {
            public int StudentCount { get; set; }
            public int CourseCount { get; set; }
            public int DepartmentCount { get; set; }
        }

        private List<StatisticsItem> LoadCount()
        {
            int StudentCount = _userDAO.GetAll().Count(u => u.Role == "Student");
            int CourseCount = _courseDAO.GetAll().Count();
            int DepartmentCount = _departmentDAO.GetAll().Count();

            // Returning as a list to bind with DataGrid
            return new List<StatisticsItem>
            {
                new StatisticsItem
                {
                    StudentCount = StudentCount,
                    CourseCount = CourseCount,
                    DepartmentCount = DepartmentCount
                }
            };
        }
        private void LogoutButton_Click(object sender, RoutedEventArgs e)
        {
            MainWindow login = App.ServiceProvider.GetRequiredService<MainWindow>();
            login.Show();
            this.Close();
        }

        private void AdminPanel_Click(object sender, RoutedEventArgs e)
        {

        }

        private void UserManagement_Click(object sender, RoutedEventArgs e)
        {
            UserManagementWindowForAdmin userManagementWindowForAdmin = new UserManagementWindowForAdmin();
            userManagementWindowForAdmin.Show();
            this.Close();
        }

        private void DepartmentManagement_Click(object sender, RoutedEventArgs e)
        {
            string role = "Admin";
            IService<Department> deparmentService = App.ServiceProvider.GetRequiredService<IService<Department>>();
            IService<Course> courseService = App.ServiceProvider.GetRequiredService<IService<Course>>();
            DepartmentManagementWindow departmentManagementWindow = new DepartmentManagementWindow(deparmentService, courseService, role);
            departmentManagementWindow.Show();
            this.Hide();
        }

        private void BlogNews_Click(object sender, RoutedEventArgs e)
        {
            BlogNewsManagementWindow blogNewsManagementWindow = App.ServiceProvider.GetRequiredService<BlogNewsManagementWindow>();
            blogNewsManagementWindow.LoadBlogNews();
            blogNewsManagementWindow.Show();
            this.Hide();
        }
    }
}
