using BussinessObjects;
using DataAccessObjects;
using Microsoft.Extensions.DependencyInjection;
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

namespace WPFApp.Login_and_home_page_of_each_role
{
    /// <summary>
    /// Interaction logic for Dashboard_for_Head_of_Department.xaml
    /// </summary>
    public partial class Dashboard_for_Head_of_Department : Window
    {
        private readonly UserDAO _userDAO;
        private readonly BlogNewsDAO _BlogNews;
        private readonly CourseDAO _courseDAO;
        private readonly DepartmentDAO _departmentDAO;
        private readonly LmsContext _context;
        private readonly int _headId;

        public Dashboard_for_Head_of_Department(int headId)
        {
            InitializeComponent();
            _context = new LmsContext();
            _userDAO = new UserDAO();
            _BlogNews = new BlogNewsDAO(_context);
            _courseDAO = new CourseDAO(_context);
            _departmentDAO = new DepartmentDAO(_context);
            _headId = headId;

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

        private void DepartmentManagement_Click(object sender, RoutedEventArgs e)
        {

        }

        private void CourseManagement_Click(object sender, RoutedEventArgs e)
        {
            CourseManagementWindowForHead courseManagementWindow = new CourseManagementWindowForHead(_headId);
            courseManagementWindow.Show();
            this.Close();
        }
    }
}
