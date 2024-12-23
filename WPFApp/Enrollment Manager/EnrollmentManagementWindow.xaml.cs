﻿using BussinessObjects;
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
using WPFApp.Data;

namespace WPFApp.Enrollment_Manager
{
    /// <summary>
    /// Interaction logic for EnrollmentManagementWindow.xaml
    /// </summary>
    public partial class EnrollmentManagementWindow : Window
    {
        
        private readonly IService<BussinessObjects.Enrollment> _enrollmentService;

        public EnrollmentManagementWindow(IService<BussinessObjects.Enrollment> enrollmentService)
        {
            _enrollmentService = enrollmentService;
            InitializeComponent();
            LoadEnrollments();
        }


        public void LoadEnrollments()
        {
            var enrollments = _enrollmentService.GetAll();
            dgEnrollment.ItemsSource = enrollments;

        }   

  private void CreateEnrollment_Click(object sender, RoutedEventArgs e)
        {
            var newEnrollment = new BussinessObjects.Enrollment
            {
                // Set properties for the new enrollment
                StudentId = 1, // Example value
                CourseId = 1, // Example value
                EnrollmentDate = DateOnly.FromDateTime(DateTime.Now)
            };
            _enrollmentService.Add(newEnrollment);
            LoadEnrollments();
        }

        private void DeleteEnrollment_Click(object sender, RoutedEventArgs e)
        {
            if (dgEnrollment.SelectedItem is BussinessObjects.Enrollment selectedEnrollment)
            {
                _enrollmentService.Delete(selectedEnrollment);
                LoadEnrollments();
            }
        }

        private void dgEnrollment_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (dgEnrollment.SelectedItem is BussinessObjects.Enrollment selectedEnrollment)
            {
                tbEnrollmentId.Text = selectedEnrollment.EnrollmentId.ToString();
                tbStudent.Text = selectedEnrollment.StudentId?.ToString() ?? string.Empty;
                tbCourse.Text = selectedEnrollment.CourseId?.ToString() ?? string.Empty;
                tbStatus.Text = selectedEnrollment.Course?.CourseName ?? string.Empty; // Assuming status is course name
                tbEnrollmentDate.Text = selectedEnrollment.EnrollmentDate?.ToString("yyyy-MM-dd") ?? string.Empty;
            }
        }

        private void btnReturn_Click(object sender, RoutedEventArgs e)
        {
            ReturnToDashboard returnToDashboard = App.ServiceProvider.GetRequiredService<ReturnToDashboard>();
            returnToDashboard.Return();
            this.Hide();
        }
    }
}
