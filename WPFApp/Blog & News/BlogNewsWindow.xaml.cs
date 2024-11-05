using BussinessObjects;
using DataAccessObjects;
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

namespace WPFApp.Blog___News
{
    /// <summary>
    /// Interaction logic for BlogNewsWindow.xaml
    /// </summary>
    public partial class BlogNewsWindow : Window
    {
        private readonly IService<Course> cservice;
        private readonly IService<Department> dservice;
        private readonly IService<CourseType> ctype;
        public CourseOverviewPopup(IService<Course> Cservice, IService<Department> Dservice, IService<CourseType> Ctype)
        public BlogNewsWindow()
        {
            InitializeComponent();
            cservice = Cservice;
            dservice = Dservice;
            ctype = Ctype;
        }
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            cmbDepartment.ItemsSource = dservice.GetAll();
            cmbType.ItemsSource = ctype.GetAll();
        }

        private void Create_Button(object sender, RoutedEventArgs e)
        {
            try
            {
                var newCourse = new Course
                {
                    CourseName = txtName.Text,
                    Description = txtDescription.Text,
                    Credits = int.TryParse(txtCredit.Text, out var credits) ? credits : (int?)null,
                    DepartmentId = (int?)cmbDepartment.SelectedValue,
                    CourseTypeId = (int?)cmbType.SelectedValue
                };

                cservice.Add(newCourse);
                MessageBox.Show("Course created successfully!", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

    }
}
