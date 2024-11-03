using BussinessObjects;
using DataAccessObjects;
using Repositories;
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

namespace WPFApp
{
    /// <summary>
    /// Interaction logic for AssignmentManagementWindow.xaml
    /// </summary>
    public partial class AssignmentManagementWindow : Window
    {
        private readonly AssignmentService _assignmentService;

        public AssignmentManagementWindow(AssignmentService assignmentService)
        {
            InitializeComponent();
            _assignmentService = assignmentService;
        }

        public void loadData()
        {
            try
            {
                var assignmentList = _assignmentService.GetAll();
                AssignmentData.ItemsSource = assignmentList;
            } catch (Exception ex) { throw new Exception(ex.Message); }
        }

        private void Add_btn(object sender, RoutedEventArgs e)
        {

        }

        private void Search_btn(object sender, RoutedEventArgs e)
        {

        }
    }
}
