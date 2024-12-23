﻿using BussinessObjects;
using DataAccessObjects;
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
using WPFApp.Course_Management;
using WPFApp.Document_Management;
using WPFApp.Forum;
using WPFApp.Submission_Grading;

namespace WPFApp.Login_and_home_page_of_each_role
{
    /// <summary>
    /// Interaction logic for Dashboard_for_lecturer.xaml
    /// </summary>
    public partial class Dashboard_for_lecturer : Window
    {
        private readonly UserDAO _userDAO;
        private readonly BlogNewsDAO _BlogNews;
        private readonly CourseDAO _courseDAO;
        private readonly DepartmentDAO _departmentDAO;
        private readonly ClassDAO _classDAO;
        private readonly LmsContext _context;

        public Dashboard_for_lecturer()
        {
            _context = new LmsContext();
            _userDAO = new UserDAO();
            _BlogNews = new BlogNewsDAO(_context);
            _courseDAO = new CourseDAO(_context);
            _departmentDAO = new DepartmentDAO(_context);
            _classDAO = new ClassDAO(_context);
            InitializeComponent();
            LoadBlogNewsList();
            LoadStatisticalList();
            DataContext = this;
        }
        private void LogoutButton_Click(object sender, RoutedEventArgs e)
        {
            MainWindow login = App.ServiceProvider.GetRequiredService<MainWindow>();
            login.Show();
            this.Close();
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

        private void CourseManagement_Click(object sender, RoutedEventArgs e)
        {
            CourseManagementWindow courseManagementWindow = new CourseManagementWindow();
            courseManagementWindow.Show();
            this.Close();
        }

        private void AssignmentManagement_Click(object sender, RoutedEventArgs e)
        {
            IService<Assignment> service = App.ServiceProvider.GetRequiredService<IService<Assignment>>();
            IService<Class> service1 = App.ServiceProvider.GetRequiredService<IService<Class>>();
            AssignmentManagementWindow assignmentWindow = new(service, service1);

            //AssignmentManagementWindow assignmentWindow = App.ServiceProvider.GetRequiredService<AssignmentManagementWindow>();
            this.Close();
            assignmentWindow.Show();

        }

        private void SubmissionGrading_Click(object sender, RoutedEventArgs e)
        {
            SubmissionGradingWindow submissionGradingWindow = App.ServiceProvider.GetRequiredService<SubmissionGradingWindow>();
            submissionGradingWindow.Show();
            this.Hide();
        }

        private void Forum_Discussion_Click(object sender, RoutedEventArgs e)
        {
            ForumWindow forumWindow = App.ServiceProvider.GetRequiredService<ForumWindow>();
            forumWindow.LoadForums();
            forumWindow.Show();
            this.Hide();

        }

        private void DocumentManagement_Click(object sender, RoutedEventArgs e)
        {
            string role = "Lecturer";
            IService<Document> documentService = App.ServiceProvider.GetRequiredService<IService<Document>>();
            DocumentManagementWindow documentManagementWindow = new DocumentManagementWindow(documentService, role);
            documentManagementWindow.Show();
            this.Hide();
        }
    }
}
