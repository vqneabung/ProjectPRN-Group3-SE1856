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

namespace WPFApp.Forum
{
    /// <summary>
    /// Interaction logic for ForumWindow.xaml
    /// </summary>
    public partial class ForumWindow : Window
    {
        private readonly ForumService _forumService;


        public ForumWindow(ForumService forumService)
        {
            InitializeComponent();
            _forumService = forumService;
        }

        public void LoadForums()
        {
            var forums = _forumService.GetAll();
            dgForum.ItemsSource = forums;
            LoadForums();
        }

        private void btnSearch_Click(object sender, RoutedEventArgs e)
        {
            var searchText = txtSearch.Text;
            var forums = _forumService.GetAll()?.Where(f => f.Title?.Contains(searchText, StringComparison.OrdinalIgnoreCase) == true);
            dgForum.ItemsSource = forums;
        }

        private void btnCreate_Click(object sender, RoutedEventArgs e)
        {
            var newForum = new Forum
            {
                Title = txtTitle.Text,
                CreateDate = DateOnly.FromDateTime(DateTime.Now)
            };
            _forumService.Add(newForum);
            LoadForums();
        }

        private void btnUpdate_Click(object sender, RoutedEventArgs e)
        {
            if (dgForum.SelectedItem is BussinessObjects.Forum selectedForum)
            {
                selectedForum.Title = txtTitle.Text;
                _forumService.Update(selectedForum);
                LoadForums();
            }
        }

        private void btnDelete_Click(object sender, RoutedEventArgs e)
        {
            if (dgForum.SelectedItem is BussinessObjects.Forum selectedForum)
            {
                _forumService.Delete(selectedForum);
                LoadForums();
            }
        }
    }
}
