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
using WPFApp.Login_and_home_page_of_each_role;

namespace WPFApp.Data
{
    public class ReturnToDashboard
    {
        private readonly UserDAO _dao;
        private readonly IEnrollmentService _enrollmentService;

        public ReturnToDashboard(UserDAO dao, IEnrollmentService enrollmentService)
        {
            _dao = dao;
            _enrollmentService = enrollmentService;
        }

        public void Return()
        {
            if (Data.user == null)
            {
                MessageBox.Show("You are not logged in. Please log in to access the dashboard.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            switch (Data.user.Role)
            {
                case "Student":
                    new Dashboard_for_student(Data.user.UserId, _enrollmentService).ShowDialog();
                    break;
                case "Lecturer":
                    new Dashboard_for_lecturer().ShowDialog();
                    break;
                case "Staff":
                    new Dashboard_for_staff().ShowDialog();
                    break;
                case "Head of Department":
                    new Dashboard_for_Head_of_Department(Data.user.UserId).ShowDialog();
                    break;
                case "Admin":
                    new Dashboard_for_admin().ShowDialog();
                    break;
                default:
                    MessageBox.Show("Role is undefined.");
                    return;
            }
        }
    }
}
