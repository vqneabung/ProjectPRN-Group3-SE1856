using BussinessObjects;
using DataAccessObjects;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using WPFApp.Login_and_home_page_of_each_role;

namespace WPFApp.Course_Management
{
    public partial class CourseManagementWindowForHead : Window
    {
        private readonly int _headId;
        private readonly CourseDAO _courseDAO;
        private readonly DepartmentDAO _departmentDAO;

        public CourseManagementWindowForHead(int headId)
        {
            InitializeComponent();
            _headId = headId;

            // Initialize DAOs
            var context = new LmsContext();
            _courseDAO = new CourseDAO(context);
            _departmentDAO = new DepartmentDAO(context);

            LoadCourses();
        }

        private void LoadCourses()
        {
            try
            {
                // Get the department associated with the head ID
                var department = _departmentDAO.GetAll().FirstOrDefault(d => d.HeadId == _headId);

                if (department == null)
                {
                    MessageBox.Show("No department found associated with the current head ID.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }

                // Load courses associated with the department
                List<Course> courses = _courseDAO.GetAll()
                                                 .Where(c => c.DepartmentId == department.DepartmentId)
                                                 .ToList();

                if (courses == null || courses.Count == 0)
                {
                    MessageBox.Show("No courses found for the current department.", "Information", MessageBoxButton.OK, MessageBoxImage.Information);
                }
                else
                {
                    CourseDataGrid.ItemsSource = courses;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred while loading courses: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void Home_Click(object sender, RoutedEventArgs e)
        {
            Dashboard_for_Head_of_Department dashboard_For_Head_Of_Department = new Dashboard_for_Head_of_Department(_headId);
            dashboard_For_Head_Of_Department.Show();
            this.Close();
        }

        private void Logout_Click(object sender, RoutedEventArgs e)
        {
            MainWindow mainWindow = new MainWindow();
            mainWindow.Show();
            this.Close();
        }
    }
}
