using BussinessObjects;
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

namespace WPFApp.Department_Management
{
    /// <summary>
    /// Interaction logic for DepartmentManagementPopup.xaml
    /// </summary>
    public partial class DepartmentManagementPopup : Window
    {
        private readonly IService<Department> departmentService;
        private readonly IService<User> userService;
        private Department selectedDepartment;
        public DepartmentManagementPopup(IService<Department> Dservice, IService<User> Uservice)
        {
            InitializeComponent();
            departmentService = Dservice;
            userService = Uservice;
        }
        public void SetDepartment(Department department)
        {
            selectedDepartment = department;
            if (selectedDepartment != null)
            {
                cmbHead.SelectedValue = selectedDepartment.HeadId;
                txtName.Text = selectedDepartment.DepartmentName;
            }
        }
        public void ClearData()
        {
            selectedDepartment = null;
            txtName.Clear();
            cmbHead.SelectedIndex = -1;
        }
        private void Close_Button(object sender, RoutedEventArgs e)
        {
            this.Hide();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            cmbHead.ItemsSource = userService.GetAll().Where( u => u.Role != "Head of Department");
        }

        private void Submit_Button(object sender, RoutedEventArgs e)
        {
            if (selectedDepartment == null)
            {
                selectedDepartment = new Department();
            }
            selectedDepartment.DepartmentName = txtName.Text;
            selectedDepartment.HeadId = (int?)cmbHead.SelectedValue;

            if (selectedDepartment.DepartmentId == 0)
            {
                int maxId = departmentService.GetAll().Max(d => (int?)d.DepartmentId) ?? 0;
                selectedDepartment.DepartmentId = maxId + 1;

                departmentService.Add(selectedDepartment);
                System.Windows.MessageBox.Show("Department created successfully.");
            }
            else
            {
                departmentService.Update(selectedDepartment);
                System.Windows.MessageBox.Show("Department updated successfully.");
            }

            this.Hide();
        }
    }
}
