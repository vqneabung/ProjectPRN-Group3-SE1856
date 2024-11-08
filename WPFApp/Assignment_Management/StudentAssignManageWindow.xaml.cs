using BussinessObjects;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.VisualBasic.ApplicationServices;
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
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using WPFApp.Course_Overview;
using WPFApp.Login_and_home_page_of_each_role;

namespace WPFApp.Assignment_Management
{
    /// <summary>
    /// Interaction logic for StudentAssignManageWindow.xaml
    /// </summary>
    public partial class StudentAssignManageWindow : Window
    {
        private readonly IService<Assignment> _assignmentService;
        private readonly IService<BussinessObjects.User> _userService;
        private readonly IService<Class> _classService;
        private readonly IService<Submission> _submissionService;
        private readonly IEnrollmentService _enrollmentService;
        private readonly int _studentId;

        private readonly LmsContext _context;

        private readonly int _classId; 
        public StudentAssignManageWindow(int studentId, IService<Assignment> service)
        {
            InitializeComponent();
            _context = new();
            _assignmentService = service;
            _studentId = studentId;
            _userService = App.ServiceProvider.GetService<IService<BussinessObjects.User>>();
            _classService = App.ServiceProvider.GetService<IService<BussinessObjects.Class>>();
            _submissionService = App.ServiceProvider.GetService<IService<BussinessObjects.Submission>>();
        }

        private void Main_btn(object sender, RoutedEventArgs e)
        {
            this.Close();
            new Dashboard_for_student(_studentId, _enrollmentService).ShowDialog();
        }

        private void Update_btn(object sender, RoutedEventArgs e)
        {
            if (AssignmentData.SelectedItem != null)
            {

                var assign = AssignmentData.SelectedItem as Assignment;
                int lstSubmis = _submissionService.GetAll().First().SubmissionId;
                int newId = lstSubmis + 1;
                Submission submission = new();
                submission.SubmissionId = newId;
                submission.StudentId = _studentId;
                submission.AssignmentId = assign.AssignmentId;
                submission.Assignment = assign;
                submission.SubmissionDate = DateOnly.FromDateTime(DateTime.Now);
                submission.Grade = null;

                _submissionService.Add(submission);

                Window_Loaded(sender, e);
            }
        }

        private void CourseOverview_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
            CourseOverviewWindow courseOverviewWindow = new CourseOverviewWindow(_enrollmentService);
            courseOverviewWindow.Show();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            try
            {
                var assignments = _context.Assignments
                .Join(
                    _context.Classes,                      // Join với bảng Classes
                    assignment => assignment.ClassId,      // Điều kiện join: ClassId từ bảng Assignments
                    cls => cls.ClassId,                    // Điều kiện join: ClassId từ bảng Classes
                    (assignment, cls) => new { assignment, cls } // Lấy kết quả join (Assignment và Class)
                )
                .Join(
                    _context.Enrollments,                  // Join với bảng Enrollments
                    ac => ac.cls.CourseId,                 // Điều kiện join: CourseId từ bảng Classes (ac.cls.CourseId)
                    enrollment => enrollment.CourseId,     // Điều kiện join: CourseId từ bảng Enrollments
                    (ac, enrollment) => new { ac.assignment, ac.cls, enrollment } // Lấy kết quả join (Assignment, Class, Enrollment)
                )
                .Where(x => x.enrollment.StudentId == _studentId) // Lọc theo StudentId
                .Select(x => new
                {
                    x.assignment.AssignmentId,
                    x.assignment.Title,
                    x.assignment.Password,
                    x.assignment.UnlockState,
                    x.assignment.Description,
                    x.assignment.DueDate,
                    x.assignment.Class,
                    x.assignment.ClassId,
                    x.assignment.Submissions
                })
                .ToList();

                // Chuyển đổi kết quả assignments thành danh sách Assignment
                var assignmentList = assignments.Select(a => new Assignment
                {
                    AssignmentId = a.AssignmentId,
                    Title = a.Title,
                    Password = a.Password,
                    UnlockState = a.UnlockState,
                    Description = a.Description,
                    DueDate = a.DueDate,
                    Class = a.Class,
                    ClassId = a.ClassId,
                    Submissions = a.Submissions,
                }).ToList();

                var list = new ObservableCollection<Assignment>(assignmentList);

                // Cập nhật ItemsSource cho AssignmentData
                AssignmentData.ItemsSource = list;

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
                    txtClass.Text = assign.Class.ClassName;
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
    }
}
