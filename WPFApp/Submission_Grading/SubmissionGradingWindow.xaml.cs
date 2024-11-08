using BussinessObjects;
using DataAccessObjects;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.VisualBasic;
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
        private Class SearchClass=new();
        private Assignment SearchAssignment =new();
        private User SearchStudent = new();
        private Submission SearchSubmission = new();

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            listSubmission.ItemsSource =  _submissionService.GetAll();
        }

        private void AssignmentWindow_btn(object sender, RoutedEventArgs e)
        {
            this.Hide();
            IService<Assignment> service = App.ServiceProvider.GetRequiredService<IService<Assignment>>();
            IService<Class> service1 = App.ServiceProvider.GetRequiredService<IService<Class>>();
            AssignmentManagementWindow assignmentWindow = new(service, service1);

            assignmentWindow.Show();
            this.Hide();
        }

        private void Course_Overview_btn(object sender, RoutedEventArgs e)
        {
            this.Hide();
            CourseOverviewWindow overviewWindow = App.ServiceProvider.GetRequiredService<CourseOverviewWindow>();
            overviewWindow.Show();
        }

        private void Main_btn(object sender, RoutedEventArgs e)
        {
            var DashBoard_for_lecture = new Dashboard_for_lecturer();
            DashBoard_for_lecture.Show();
            this.Hide();
        }

        private void Submission_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var submission = listSubmission.SelectedItem as Submission;
            if(submission != null)
            {

                txtSubmissionId.Text = submission.SubmissionId.ToString();
                txtAssignmentId.Text = submission.AssignmentId.ToString();
                txtTitle.Text = submission.Assignment.Title.ToString();
                SubmissionDateDatePicker.SelectedDate = submission.SubmissionDate.GetValueOrDefault().ToDateTime(TimeOnly.MinValue);
                txtGrade.Text = submission.Grade.ToString();
            }
            else { MessageBox.Show("Please chose which submission to display!"); }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (listSubmission.SelectedItem != null)
            {
                var submission = (Submission)listSubmission.SelectedItem;

                try
                {
                    if (!string.IsNullOrWhiteSpace(txtGrade.Text))
                    {
                        submission.Grade = decimal.Parse(txtGrade.Text.Trim());
                        if(submission.Grade < 0 || submission.Grade > 100)
                        {
                            MessageBox.Show("Grade is not negative and below 100", "Input Error", MessageBoxButton.OK, MessageBoxImage.Warning);
                            return;
                        }
                        
                    }
                    else
                    {
                        MessageBox.Show("Grade is required", "Input Error", MessageBoxButton.OK, MessageBoxImage.Warning);
                        return;
                    }
                }
                catch (Exception)
                {
                    MessageBox.Show("Please enter a valid grade ", "Wrong Format", MessageBoxButton.OK, MessageBoxImage.Error);
                }

                _submissionService.Update(submission);



                Window_Loaded(sender, e);
            }
            else { MessageBox.Show("Please choose an assignment to do!!"); }
        }
    }
}
