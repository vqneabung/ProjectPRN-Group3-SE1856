using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using DataAccessObjects;
using BussinessObjects;
using System.Windows.Controls;
using WPFApp.Login_and_home_page_of_each_role;

namespace WPFApp.Course_Management
{
    public partial class CourseManagementWindow : Window
    {
        private readonly CourseDAO courseDAO;
        private readonly EnrollmentDAO enrollmentDAO;
        private readonly DepartmentDAO departmentDAO;
        private readonly CourseTypeDAO courseTypeDAO;
        public CourseManagementWindow()
        {
            InitializeComponent();
            var context = new LmsContext();
            courseDAO = new CourseDAO(context);
            enrollmentDAO = new EnrollmentDAO(context);
            departmentDAO = new DepartmentDAO(context);
            courseTypeDAO = new CourseTypeDAO(context);

            LoadCourses();
            LoadDepartments();
            LoadCourseTypes();
            LoadCourseComboBox();
        }

        private void LoadCourses()
        {
            dgCourses.ItemsSource = courseDAO.GetAll();
        }

        private void LoadDepartments()
        {
            cbDepartments.ItemsSource = departmentDAO.GetAll(); // Get all departments
        }

        private void LoadCourseTypes()
        {
            cbCourseTypes.ItemsSource = courseTypeDAO.GetAll(); // Get all course types
        }

        private void LoadCourseComboBox()
        {
            cbCourses.ItemsSource = courseDAO.GetAll();
        }

        // CRUD Operations
        private void BtnAddCourse_Click(object sender, RoutedEventArgs e)
        {
            // Input validation
            if (string.IsNullOrWhiteSpace(txtCourseName.Text) ||
                string.IsNullOrWhiteSpace(txtCourseDescription.Text) ||
                string.IsNullOrWhiteSpace(txtCredits.Text) ||
                cbDepartments.SelectedValue == null ||
                cbCourseTypes.SelectedValue == null)
            {
                MessageBox.Show("Please fill in all fields.");
                return;
            }

            // Check if txtCredits is a valid integer
            if (!int.TryParse(txtCredits.Text, out int credits))
            {
                MessageBox.Show("Credits must be a valid number.");
                return;
            }

            // Create a new course with CourseId set to max + 1
            var newCourse = new Course
            {
                CourseId = courseDAO.GetAll().Max(c => c.CourseId) + 1, // Assuming CourseId is an integer
                CourseName = txtCourseName.Text,
                Description = txtCourseDescription.Text,
                Credits = credits,
                DepartmentId = (int)cbDepartments.SelectedValue,
                CourseTypeId = (int)cbCourseTypes.SelectedValue
            };

            courseDAO.Add(newCourse);
            LoadCourses();
            ClearCourseFields();
        }

        private void BtnUpdateCourse_Click(object sender, RoutedEventArgs e)
        {
            // Input validation
            if (string.IsNullOrWhiteSpace(txtCourseName.Text) ||
                string.IsNullOrWhiteSpace(txtCourseDescription.Text) ||
                string.IsNullOrWhiteSpace(txtCredits.Text) ||
                cbDepartments.SelectedValue == null ||
                cbCourseTypes.SelectedValue == null)
            {
                MessageBox.Show("Please fill in all fields.");
                return;
            }

            // Check if txtCredits is a valid integer
            if (!int.TryParse(txtCredits.Text, out int credits))
            {
                MessageBox.Show("Credits must be a valid number.");
                return;
            }

            // Update the selected course
            if (dgCourses.SelectedItem is Course selectedCourse)
            {
                selectedCourse.CourseName = txtCourseName.Text;
                selectedCourse.Description = txtCourseDescription.Text;
                selectedCourse.Credits = credits;
                selectedCourse.DepartmentId = (int)cbDepartments.SelectedValue;
                selectedCourse.CourseTypeId = (int)cbCourseTypes.SelectedValue;

                courseDAO.Update(selectedCourse);
                LoadCourses();
                ClearCourseFields();
            }
        }


        private void BtnDeleteCourse_Click(object sender, RoutedEventArgs e)
        {
            if (dgCourses.SelectedItem is Course selectedCourse)
            {
                courseDAO.Delete(selectedCourse);
                LoadCourses();
            }
        }

        private void DgCourses_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (dgCourses.SelectedItem is Course selectedCourse)
            {
                // Populate the text boxes and combo boxes with the selected course details
                txtCourseName.Text = selectedCourse.CourseName;
                txtCourseDescription.Text = selectedCourse.Description;
                txtCredits.Text = selectedCourse.Credits.ToString();

                // Set the selected value of the ComboBox for Departments and Course Types
                cbDepartments.SelectedValue = selectedCourse.DepartmentId;
                cbCourseTypes.SelectedValue = selectedCourse.CourseTypeId;
            }
        }


        // Show list of enrolled students
        private void BtnSearchStudents_Click(object sender, RoutedEventArgs e)
        {
            if (cbCourses.SelectedValue is int selectedCourseId)
            {
                var enrolledStudents = enrollmentDAO.GetAll()
                    .Where(e => e.CourseId == selectedCourseId && e.Status == true)
                    .ToList();
                dgEnrolledStudents.ItemsSource = enrolledStudents;
            }
            else
            {
                MessageBox.Show("Please select a course.");
            }
        }

        private void Home_Click(object sender, RoutedEventArgs e)
        {
            Dashboard_for_staff home = new Dashboard_for_staff();
            home.Show();
            this.Close();
        }

        private void Logout_Click(object sender, RoutedEventArgs e)
        {
            MainWindow mainWindow = new MainWindow();
            mainWindow.Show();
            this.Close();
        }

        private void ClearCourseFields()
        {
            txtCourseName.Clear();
            txtCourseDescription.Clear();
            txtCredits.Clear();
            cbDepartments.SelectedIndex = -1;
            cbCourseTypes.SelectedIndex = -1;

        }
    }
}
