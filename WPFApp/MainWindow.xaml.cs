using DataAccessObjects;
using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Client.NativeInterop;
using Repositories;
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
using WPFApp.Login_and_home_page_of_each_role;

namespace WPFApp
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public readonly UserDAO _userDAO;

        public MainWindow()
        {
            InitializeComponent();
            _userDAO = new UserDAO();
        }

        private void btnLogin_Click(object sender, RoutedEventArgs e)
        {
            string username = txtUserName.Text;
            string password = txtPassword.Password;

            var user = _userDAO.GetByUserName(username);

            if(user != null && user.Password == password)
            {
                switch (user.Role)
                {
                    case "Student":
                        new Dashboard_for_student(user.UserId).Show();
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
                this.Close();
            }
            else
            {
                MessageBox.Show("Invalid UserName or password.");
            }
        }

        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void Course_Button(object sender, RoutedEventArgs e)
        {
            this.Hide();
            CourseOverviewWindow courseOverviewWindow = App.ServiceProvider.GetRequiredService<CourseOverviewWindow>();
            courseOverviewWindow.Show();
        }
    }
}