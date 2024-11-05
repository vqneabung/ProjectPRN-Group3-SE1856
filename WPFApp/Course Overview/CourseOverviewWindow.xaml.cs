using BussinessObjects;
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

namespace WPFApp.Course_Overview
{
    /// <summary>
    /// Interaction logic for CourseOverviewWindow.xaml
    /// </summary>
    public partial class CourseOverviewWindow : Window
    {
        private readonly IService<Course> _service;
        public CourseOverviewWindow(IService<Course> service)
        {
            InitializeComponent();
            _service = service;
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            var courseList = _service.GetAll();
            dgCourse.ItemsSource = courseList;
        }

        private void Search_Button(object sender, RoutedEventArgs e)
        {
            string courseName = txtSearch.Text.Trim();

            if (!string.IsNullOrEmpty(courseName))
            {
                var searchResults = _service.GetAll().Where(c => c.CourseName.Contains(courseName, StringComparison.OrdinalIgnoreCase)).ToList();
                dgCourse.ItemsSource = searchResults;

                if (searchResults.Count == 0)
                {
                    MessageBox.Show("No courses found with the specified name.");
                }
            }
            else
            {
                MessageBox.Show("Please enter a course name.");
            }
        }

        private void Create_Button(object sender, RoutedEventArgs e)
        {
            CourseOverviewPopup courseOverviewPopup = App.ServiceProvider.GetRequiredService<CourseOverviewPopup>();
            courseOverviewPopup.Show();
        }

        private void Update_Button(object sender, RoutedEventArgs e)
        {

        }

        private void Delete_Button(object sender, RoutedEventArgs e)
        {

        }
    }
}
