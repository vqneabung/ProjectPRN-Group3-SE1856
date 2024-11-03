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

namespace WPFApp.Enrollment
{
    /// <summary>
    /// Interaction logic for EnrollmentManagementWindow.xaml
    /// </summary>
    public partial class EnrollmentManagementWindow : Window
    {
        
        private readonly EnrollmentService _enrollmentService;

        public EnrollmentManagementWindow(EnrollmentService enrollmentService)
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

        }

        private void UpdateEnrollment_Click(object sender, RoutedEventArgs e)
        {

        }

        private void DeleteEnrollment_Click(object sender, RoutedEventArgs e)
        {

        }

        private void dgEnrollment_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
