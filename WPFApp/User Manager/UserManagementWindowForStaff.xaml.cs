﻿using BussinessObjects;
using DataAccessObjects;
using Microsoft.Extensions.DependencyInjection;
using System.Windows;
using System.Windows.Controls;
using WPFApp.Login_and_home_page_of_each_role;

namespace WPFApp.User_Manager
{
    /// <summary>
    /// Interaction logic for UserManagementWindowForStaff.xaml
    /// </summary>
    public partial class UserManagementWindowForStaff : Window
    {
        private readonly UserDAO userDao = new UserDAO();

        public UserManagementWindowForStaff()
        {
            InitializeComponent();
            LoadUserData();
        }

        private void dgUser_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var selectedUser = dgUser.SelectedItem as User;
            if (selectedUser != null)
            {
                txtUserName.Text = selectedUser.UserName;
                txtPassword.Password = selectedUser.Password; // You might not want to show the password
                txtFullName.Text = selectedUser.FullName;
                txtEmail.Text = selectedUser.Email;
                RoleComboBox.SelectedItem = RoleComboBox.Items
                    .Cast<ComboBoxItem>()
                    .FirstOrDefault(item => item.Content.ToString() == selectedUser.Role);
            }
        }

        private void LoadUserData()
        {
            // Get all users and filter by role
            var users = userDao.GetAll()
                               .Where(user => user.Role == "Student" || user.Role == "Lecturer")
                               .ToList();

            // Bind the filtered list to the DataGrid
            dgUser.ItemsSource = users;
        }

        private bool ValidateFields()
        {
            // Check if any field is empty
            if (string.IsNullOrWhiteSpace(txtUserName.Text) ||
                string.IsNullOrWhiteSpace(txtPassword.Password) ||
                string.IsNullOrWhiteSpace(txtFullName.Text) ||
                string.IsNullOrWhiteSpace(txtEmail.Text) ||
                string.IsNullOrWhiteSpace(txtStatus.Text) ||
                RoleComboBox.SelectedItem == null)
            {
                MessageBox.Show("All fields must be filled.");
                return false;
            }

            // Validate email format (must be ...@gmail.com)
            if (!System.Text.RegularExpressions.Regex.IsMatch(txtEmail.Text, @"^[^@\s]+@gmail\.com$"))
            {
                MessageBox.Show("Email must be in the format ...@gmail.com.");
                return false;
            }

            return true;
        }

        private void btnAddUser_Click(object sender, RoutedEventArgs e)
        {
            if (!ValidateFields()) return;
            int newUserId = userDao.GetNextUserId();
            var user = new User
            {
                UserId = newUserId,
                UserName = txtUserName.Text,
                Password = txtPassword.Password,
                FullName = txtFullName.Text,
                Email = txtEmail.Text,
                Status = txtStatus.Text,
                Role = (RoleComboBox.SelectedItem as ComboBoxItem)?.Content.ToString()
            };

            userDao.Add(user);
            LoadUserData();
            ClearFields();
        }

        private void btnUpdateUser_Click(object sender, RoutedEventArgs e)
        {
            if (!ValidateFields()) return;
            var selectedUser = dgUser.SelectedItem as User;
            if (selectedUser == null)
            {
                MessageBox.Show("Please select a user to update.");
                return;
            }

            selectedUser.UserName = txtUserName.Text;
            selectedUser.Password = txtPassword.Password;
            selectedUser.FullName = txtFullName.Text;
            selectedUser.Email = txtEmail.Text;
            selectedUser.Status = txtStatus.Text;
            selectedUser.Role = (RoleComboBox.SelectedItem as ComboBoxItem)?.Content.ToString();

            userDao.Update(selectedUser);
            LoadUserData();
            ClearFields();
        }

        private void btnDeleteUser_Click(object sender, RoutedEventArgs e)
        {
            var selectedUser = dgUser.SelectedItem as User;
            if (selectedUser == null)
            {
                MessageBox.Show("Please select a user to delete.");
                return;
            }

            userDao.Delete(selectedUser);
            LoadUserData();
            ClearFields();
        }

        private void btnReadUser_Click(object sender, RoutedEventArgs e)
        {
            var selectedUser = dgUser.SelectedItem as User;
            if (selectedUser == null)
            {
                MessageBox.Show("Please select a user to read.");
                return;
            }

            txtUserName.Text = selectedUser.UserName;
            txtPassword.Password = selectedUser.Password; // You might not want to show the password
            txtFullName.Text = selectedUser.FullName;
            txtEmail.Text = selectedUser.Email;
            RoleComboBox.SelectedItem = RoleComboBox.Items
                .Cast<ComboBoxItem>()
                .FirstOrDefault(item => item.Content.ToString() == selectedUser.Role);
        }

        private void ClearFields()
        {
            txtUserName.Clear();
            txtPassword.Clear();
            txtFullName.Clear();
            txtEmail.Clear();
            RoleComboBox.SelectedIndex = -1;
        }

        private void Home_Click(object sender, RoutedEventArgs e)
        {
            Dashboard_for_staff dashboard_For_Staff = new Dashboard_for_staff();
            dashboard_For_Staff.Show();
            this.Close();
        }

        private void Logout_Click(object sender, RoutedEventArgs e)
        {
            MainWindow login = App.ServiceProvider.GetRequiredService<MainWindow>();
            login.Show();
            this.Close();
        }
    }
}
