using BussinessObjects;
using DataAccessObjects;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.VisualBasic.ApplicationServices;
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
using WPFApp.Course_Overview;
using WPFApp.Forum;
using WPFApp.Document_Management;
using WPFApp.Assignment_Management;

namespace WPFApp.Login_and_home_page_of_each_role
{
    /// <summary>
    /// Interaction logic for Dashboard_for_student.xaml
    /// </summary>
    public partial class Dashboard_for_student : Window
    {
        private readonly UserDAO _userDAO;
        private readonly BlogNewsDAO _BlogNews;
        private readonly CourseDAO _courseDAO;
        private readonly DepartmentDAO _departmentDAO;
        private readonly EnrollmentDAO _enrollmentDAO;
        private readonly LmsContext _context;
        private readonly int _studentId;
        private readonly IEnrollmentService _enrollmentService;


        public Dashboard_for_student(int studentId, IEnrollmentService enrollmentService)
        {
            _studentId = studentId;
            _context = new LmsContext();
            _userDAO = new UserDAO();
            _BlogNews = new BlogNewsDAO(_context);
            _courseDAO = new CourseDAO(_context);
            _departmentDAO = new DepartmentDAO(_context);
            _enrollmentDAO = new EnrollmentDAO(_context);
            DataContext = this;
            _enrollmentService = enrollmentService;
            InitializeComponent();
            LoadBlogNewsList();
            LoadStatisticalList();
            LoadEnrolledCourses();

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
        /// <summary>
        /// /////////////////////////////////////////////////////////////////////
        /// </summary>
        public class EnrolledCourses
        {
            public string CourseName { get; set; }
            public DateOnly EnrollmentDate { get; set; }
        }

        private List<EnrolledCourses> LoadCourses()
        {
            // Fetching enrollments for the current student
            var enrollments = _enrollmentDAO.GetAll();
            // Converting to a list of EnrolledCourses to bind to DataGrid
            return enrollments.Select(enrollment => new EnrolledCourses
            {
                CourseName = enrollment.Course?.CourseName ?? "Unknown",
                EnrollmentDate = enrollment.EnrollmentDate ?? default
            }).ToList();
        }

        private void LoadEnrolledCourses()
        {
            coursesDataGrid.ItemsSource = LoadCourses();
        }

        private void CourseOverview_Click(object sender, RoutedEventArgs e)
        {
            CourseOverviewWindow courseOverviewWindow = App.ServiceProvider.GetRequiredService<CourseOverviewWindow>();
            courseOverviewWindow.LoadCourses();
            courseOverviewWindow.Show();
            this.Close();
        }

        private void AssignmentSubmission_Click(object sender, RoutedEventArgs e)
        {
            IService<Assignment> service = App.ServiceProvider.GetRequiredService<IService<Assignment>>();
            StudentAssignManageWindow assignmentWindow = new(_studentId, service);

            //AssignmentManagementWindow assignmentWindow = App.ServiceProvider.GetRequiredService<AssignmentManagementWindow>();
            this.Close();
            assignmentWindow.Show();
        }

        private void ForumDiscussion_Click(object sender, RoutedEventArgs e)
        {
            ForumWindow forumWindow = App.ServiceProvider.GetRequiredService<ForumWindow>();
            forumWindow.ShowDialog();
            this.Hide();
        }

        private void DocumentManagement_Click(object sender, RoutedEventArgs e)
        {
            string role = "Student";
            IService<Document> documentService = App.ServiceProvider.GetRequiredService<IService<Document>>();
            DocumentManagementWindow documentManagementWindow = new DocumentManagementWindow(documentService, role);
            documentManagementWindow.Show();
            this.Hide();
        }
    }
}
