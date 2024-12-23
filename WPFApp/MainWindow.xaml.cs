using BussinessObjects;
using DataAccessObjects;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Identity.Client.NativeInterop;
using Repositories;
using Services;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using WPFApp.Data;
using WPFApp.Course_Overview;
using WPFApp.Login_and_home_page_of_each_role;

namespace WPFApp
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public readonly UserDAO _userDAO;
       
        private readonly IEnrollmentService _enrollmentService;

        public MainWindow(UserDAO userDAO, IEnrollmentService enrollmentService)
        {
            InitializeComponent();
            _userDAO = userDAO;
            _enrollmentService = enrollmentService;
        }

        private void btnLogin_Click(object sender, RoutedEventArgs e)
        {
            //forTest
            //new Dashboard_for_lecturer().Show();
            //this.Close();

            string username = txtUserName.Text;
            string password = txtPassword.Password;

            var user = _userDAO.GetByUserName(username);

            Data.Data.user = user;

            if (user != null && user.Password == password)
            {
                switch (user.Role)
                {
                    case "Student":
                        new Dashboard_for_student(user.UserId, _enrollmentService).Show();
                        break;
                    case "Lecturer":
                        new Dashboard_for_lecturer().Show();
                        break;
                    case "Staff":
                        new Dashboard_for_staff().Show();
                        break;
                    case "Head of Department":
                        new Dashboard_for_Head_of_Department(user.UserId).Show();
                        break;
                    case "Admin":
                        new Dashboard_for_admin().Show();
                        break;
                    default:
                        MessageBox.Show("Role is undefined.");
                        return;
                }
                this.Hide();
            }
            else
            {
                MessageBox.Show("Invalid UserName or password.");
            }
        }

        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            this.Hide();
        }

        private void Course_Button(object sender, RoutedEventArgs e)
        {
            this.Hide();
            CourseOverviewWindow courseOverviewWindow = App.ServiceProvider.GetRequiredService<CourseOverviewWindow>();
            courseOverviewWindow.Show();
        }
    }
}