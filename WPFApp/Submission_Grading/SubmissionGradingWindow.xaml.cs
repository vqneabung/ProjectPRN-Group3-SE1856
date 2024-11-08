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
using WPFApp.Course_Overview;
using WPFApp.Login_and_home_page_of_each_role;

namespace WPFApp.Submission_Grading
{
    /// <summary>
    /// Interaction logic for SubmissionGradingWindow.xaml
    /// </summary>
    public partial class SubmissionGradingWindow : Window
    {
        private readonly IService<Submission> _submissionService;
        public SubmissionGradingWindow(IService<Submission> service)
        {
            InitializeComponent();
            _submissionService = service;
        }

        public List<Submission> ListSubmissionGrading { get; set; }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {


            if (ListSubmissionGrading.Any()) { }


        }

        private void AssignmentWindow_btn(object sender, RoutedEventArgs e)
        {
            this.Close();
            IService<Assignment> service = App.ServiceProvider.GetRequiredService<IService<Assignment>>();
            IService<Class> service1 = App.ServiceProvider.GetRequiredService<IService<Class>>();
            AssignmentManagementWindow assignmentWindow = new(service, service1);

            assignmentWindow.Show();
            this.Close();
        }

        private void Course_Overview_btn(object sender, RoutedEventArgs e)
        {
            this.Close();
            CourseOverviewWindow overviewWindow = App.ServiceProvider.GetRequiredService<CourseOverviewWindow>();
            overviewWindow.Show();
        }

        private void Main_btn(object sender, RoutedEventArgs e)
        {
            var DashBoard_for_lecture = new Dashboard_for_lecturer();
            DashBoard_for_lecture.Show();
            this.Close();
        }
    }
}
