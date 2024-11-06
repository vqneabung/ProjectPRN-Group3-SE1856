using BussinessObjects;
using DataAccessObjects;
using Repositories;
using Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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

namespace WPFApp
{
    /// <summary>
    /// Interaction logic for AssignmentManagementWindow.xaml
    /// </summary>
    public partial class AssignmentManagementWindow : Window
    {
        private readonly IService<Assignment> _assignmentService;
        private readonly IService<Class> _classService;

        public AssignmentManagementWindow(IService<Assignment> service, IService<Class> service1)
        {
            _assignmentService = service;
            _classService = service1;
            InitializeComponent();
            LoadData();
        }

        private void Course_Overview_btn(object sender, RoutedEventArgs e)
        {
            //this.Close();
            //CourseOverviewWindow overviewWindow = new CourseOverviewWindow();
            //overviewWindow.Show();
            MessageBox.Show("Open course over view window");
        }

        private void Main_btn(object sender, RoutedEventArgs e)
        {
            var DashBoard_for_lecture = new Dashboard_for_lecturer();
            DashBoard_for_lecture.Show();
            this.Close();
        }

        public void LoadData()
        {
            try
            {
                var assignmentList = _assignmentService.GetAll();
                var observableAssignmentList = new ObservableCollection<Assignment>(assignmentList);
                var classList = _classService.GetAll();
                var observableClassList = new ObservableCollection<Class>(classList);

                ClassSelector.ItemsSource = observableClassList;
                AssignmentData.ItemsSource = observableAssignmentList;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        private void LoadRowInformation(object sender, RoutedEventArgs e)
        {
            try
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
                        lstSubmissions.ItemsSource = assign.Submissions.ToList();
                    }
                    else { MessageBox.Show("Please chose which Assignment to display!"); }
                }
            } catch (Exception ex) { MessageBox.Show(ex.Message); }
        }

        private void Add_btn(object sender, RoutedEventArgs e)
        {

        }

        private void Search_btn(object sender, RoutedEventArgs e)
        {

        }

        private void Delete_btn(object sender, RoutedEventArgs e)
        {

        }

        private void Update_btn(object sender, RoutedEventArgs e)
        {

        }

    }
}
