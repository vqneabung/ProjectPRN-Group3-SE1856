using BussinessObjects;
using DataAccessObjects;
using Microsoft.Extensions.DependencyInjection;
using Repositories;
using Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Formats.Asn1;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using WPFApp.Course_Overview;
using WPFApp.Login_and_home_page_of_each_role;
using WPFApp.Submission_Grading;

namespace WPFApp
{
    /// <summary>
    /// Interaction logic for AssignmentManagementWindow.xaml
    /// </summary>
    public partial class AssignmentManagementWindow : Window
    {
        private readonly IService<Assignment> _assignmentService;
        private readonly IService<Class> _classService;

        public AssignmentManagementWindow(IService<Assignment> service, IService<Class> classService)
        {
            var context = new LmsContext();
            InitializeComponent();
            _assignmentService = service;
            _classService = classService;
        }

        private void Course_Overview_btn(object sender, RoutedEventArgs e)
        {
            this.Close();
            CourseOverviewWindow overviewWindow = App.ServiceProvider.GetRequiredService<CourseOverviewWindow>();
            overviewWindow.ShowDialog();
        }

        private void Main_btn(object sender, RoutedEventArgs e)
        {
            var DashBoard_for_lecture = new Dashboard_for_lecturer();
            DashBoard_for_lecture.Show();
            this.Close();
        }

        private void Add_btn(object sender, RoutedEventArgs e)
        {
            var assignmentList = _assignmentService.GetAll();

            Assignment obj = new();

            try
            {
                if (!string.IsNullOrWhiteSpace(txtAssignmentId.Text))
                {
                    obj.AssignmentId = int.Parse(txtAssignmentId.Text.Trim());
                }
                else
                {
                    MessageBox.Show("Assignment ID is required", "Input Error", MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }
            }
            catch (Exception)
            {
                MessageBox.Show("Please enter a valid number for Assignment ID", "Wrong Format", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            if (assignmentList.FirstOrDefault(a => a.AssignmentId == obj.AssignmentId) != null)
            {
                MessageBox.Show("Already have this assignment ID", "Doublicated", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            if (ClassSelector.SelectedValue != null)
            {
                obj.ClassId = int.Parse(ClassSelector.SelectedValue.ToString());
                obj.Class = _classService.GetAll().FirstOrDefault(c => c.ClassId == obj.ClassId);
            }
            else { MessageBox.Show("Please choose class"); }
            obj.Title = txtTitle.Text;
            obj.Description = txtDescription.Text;
            obj.Password = txtPassword.Text;
            obj.UnlockState = chkUnlockState.IsChecked;
            try
            {
                obj.DueDate = dateDueDate.SelectedDate.HasValue
                ? DateOnly.FromDateTime(dateDueDate.SelectedDate.Value)
                : DateOnly.FromDateTime(DateTime.Now);
            }
            catch (Exception) { MessageBox.Show("Choose date correctly"); }
            obj.Submissions = new List<Submission>();

            _assignmentService.Add(obj);

            Window_Loaded(sender, e);
        }

        private void Delete_btn(object sender, RoutedEventArgs e)
        {
            if (AssignmentData.SelectedItem != null)
            {
                var assign = AssignmentData.SelectedItem as Assignment;
                _assignmentService.Delete(assign);

                Window_Loaded(sender, e);
                MessageBox.Show("Delete finish");
            }
            else { MessageBox.Show("Please choose an assignment to do!!"); }
        }

        private void Update_btn(object sender, RoutedEventArgs e)
        {
            if (AssignmentData.SelectedItem != null)
            {
                var assign = AssignmentData.SelectedItem as Assignment;

                Assignment obj = new();

                obj.AssignmentId = assign.AssignmentId;
                if (ClassSelector.SelectedValue != null)
                {
                    obj.ClassId = int.Parse(ClassSelector.SelectedValue.ToString());
                    obj.Class = _classService.GetAll().FirstOrDefault(c => c.ClassId == obj.ClassId);
                }
                else { MessageBox.Show("Please choose class"); }
                obj.Title = txtTitle.Text;
                obj.Description = txtDescription.Text;
                obj.Password = txtPassword.Text;
                obj.UnlockState = chkUnlockState.IsChecked;
                obj.DueDate = dateDueDate.SelectedDate.HasValue
                    ? DateOnly.FromDateTime(dateDueDate.SelectedDate.Value)
                    : null;
                obj.Submissions = assign.Submissions;

                _assignmentService.Update(obj);

                Window_Loaded(sender, e);
            }
            else { MessageBox.Show("Please choose an assignment to do!!"); }
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            try
            {
                var assignmentList = _assignmentService.GetAll();
                var observableAssignmentList = new ObservableCollection<Assignment>();
                if (assignmentList != null)
                {
                    foreach (var assignment in assignmentList)
                    {
                        observableAssignmentList.Add(assignment);
                    }
                }

                var classList = _classService.GetAll();
                var observableClassList = new ObservableCollection<Class>();
                if (classList != null)
                {
                    foreach (var c in classList)
                    {
                        observableClassList.Add(c);
                    }
                }

                ClassSelector.ItemsSource = observableClassList;
                AssignmentData.ItemsSource = observableAssignmentList;

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        private void AssignmentData_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (AssignmentData.SelectedItem != null)
            {

                var assign = AssignmentData.SelectedItem as Assignment;

                if (assign != null)
                {
                    txtAssignmentId.Text = assign.AssignmentId.ToString();
                    ClassSelector.SelectedValue = assign.ClassId;
                    txtTitle.Text = assign.Title;
                    txtDescription.Text = assign.Description;
                    txtPassword.Text = assign.Password;
                    chkUnlockState.IsChecked = assign.UnlockState;
                    dateDueDate.SelectedDate = assign.DueDate.GetValueOrDefault().ToDateTime(TimeOnly.MinValue);
                    lstSubmissions.ItemsSource = new ObservableCollection<Submission>(assign.Submissions);

                }
                else { MessageBox.Show("Please chose which Assignment to display!"); }
            }
        }

        private void SubissionManage_btn(object sender, RoutedEventArgs e)
        {
            var list = lstSubmissions.SelectedItem as List<Submission>;
            IService<Submission> service = App.ServiceProvider.GetService<IService<Submission>>();

            SubmissionGradingWindow submissionGradingWindow = new(service);
            submissionGradingWindow.ListSubmissionGrading = list;
            submissionGradingWindow.Show();
            this.Close();
        }
    }
}
