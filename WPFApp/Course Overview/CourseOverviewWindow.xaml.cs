using BussinessObjects;
using DataAccessObjects;
using Microsoft.Extensions.DependencyInjection;
using Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using WPFApp.Forum;
using WPFApp.Login_and_home_page_of_each_role;

namespace WPFApp.Course_Overview
{
    public partial class CourseOverviewWindow : Window
    {
        private readonly CourseDAO courseDAO;
        private readonly EnrollmentDAO enrollmentDAO;
        private ObservableCollection<CourseData> courses;
        private readonly IEnrollmentService _enrollmentService;

        public int _studentId = Data.Data.user.UserId;

        public CourseOverviewWindow(IEnrollmentService enrollmentService)
        {
            InitializeComponent();
            courseDAO = new CourseDAO(new LmsContext());
            enrollmentDAO = new EnrollmentDAO(new LmsContext());
            _enrollmentService = enrollmentService;
            LoadCourses();
        }

        // A class to hold course information with enrollment status
        public class CourseData
        {
            public int CourseId { get; set; }
            public string CourseName { get; set; }
            public DateOnly? EnrollmentDate { get; set; }
            public bool IsEnrolled { get; set; }
        }

        private void LoadCourses(string search = "")
        {
            // Retrieve all courses
            var allCourses = courseDAO.GetAll();
            // Get the student's current enrollments
            var enrolledCourses = _enrollmentService.GetEnrollmentByStudentId(_studentId).ToList();

            // Combine course data with enrollment information
            var courseDataList = allCourses
                .Where(c => string.IsNullOrWhiteSpace(search) || c.CourseName.Contains(search, StringComparison.OrdinalIgnoreCase))
                .Select(c => new CourseData
                {
                    CourseId = c.CourseId,
                    CourseName = c.CourseName,
                    EnrollmentDate = enrolledCourses.FirstOrDefault(e => e.CourseId == c.CourseId)?.EnrollmentDate,
                    IsEnrolled = enrolledCourses.Any(e => e.CourseId == c.CourseId && e.Status == true)
                })
                .ToList();

            // Bind data to the DataGrid
            courses = new ObservableCollection<CourseData>(courseDataList);
            DataGridCourses.ItemsSource = courses;
        }

        private void Search_Click(object sender, RoutedEventArgs e)
        {
            LoadCourses(txtSearch.Text);
        }

        private void Enroll_Click(object sender, RoutedEventArgs e)
        {
            var selectedCourseData = DataGridCourses.SelectedItem as CourseData;
            if (selectedCourseData != null && !selectedCourseData.IsEnrolled)
            {
                // Create a new enrollment
                var enrollment = new Enrollment
                {
                    StudentId = _studentId,
                    CourseId = selectedCourseData.CourseId,
                    EnrollmentDate = DateOnly.FromDateTime(DateTime.Now),
                    Status = true
                };
                _enrollmentService.Add(enrollment);
                LoadCourses();
            }
            else
            {
                MessageBox.Show("You are already enrolled in this course or no course is selected.", "Enrollment Error", MessageBoxButton.OK, MessageBoxImage.Warning);
            }


        }

        private void Unenroll_Click(object sender, RoutedEventArgs e)
        {
            var selectedCourseData = DataGridCourses.SelectedItem as CourseData;
            if (selectedCourseData != null && selectedCourseData.IsEnrolled)
            {
                // Find the enrollment to delete
                var enrollment = _enrollmentService.GetEnrollmentByStudentId(_studentId)
                                              .FirstOrDefault(e => e.CourseId == selectedCourseData.CourseId && e.Status == true);
                if (enrollment != null)
                {
                    _enrollmentService.Delete(enrollment);
                    LoadCourses();
                }
            }
            else
            {
                MessageBox.Show("You are not enrolled in this course or no course is selected.", "Unenrollment Error", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }

        private void Home_Click(object sender, RoutedEventArgs e)
        {
            Dashboard_for_student home = new Dashboard_for_student(_studentId, _enrollmentService);
            home.Show();
            this.Hide();
        }

        private void Logout_Click(object sender, RoutedEventArgs e)
        {
            MainWindow mainWindow = App.ServiceProvider.GetRequiredService<MainWindow>();
            mainWindow.Show();
            this.Hide();
        }

        private void DataGridCourses_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void Forum_Click(object sender, RoutedEventArgs e)
        {
            var selectedCourseData = DataGridCourses.SelectedItem as CourseData;
            if (selectedCourseData != null && selectedCourseData.IsEnrolled)
            {
                // Find the enrollment to delete
                var enrollment = _enrollmentService.GetEnrollmentByStudentId(_studentId)
                                              .FirstOrDefault(e => e.CourseId == selectedCourseData.CourseId && e.Status == true);
                if (enrollment != null)
                {
                    Data.Data.courseId = selectedCourseData.CourseId;
                    ForumWindow forum = ForumWindow.Create(App.ServiceProvider);
                    forum.Show();
                    this.Hide();
                }
            }
            else
            {
                MessageBox.Show("You are not enrolled in this course or no course is selected.", "Unenrollment Error", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }
    }
}
