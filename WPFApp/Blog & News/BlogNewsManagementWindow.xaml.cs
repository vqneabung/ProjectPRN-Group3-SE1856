using Microsoft.Extensions.DependencyInjection;
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
    /// Interaction logic for BlogNewsManagementWindow.xaml
    /// </summary>
    public partial class BlogNewsManagementWindow : Window
    {
        private readonly BlogNewsService _blogNewsService;



        public BlogNewsManagementWindow(BlogNewsService blogNewsService)
        {
            InitializeComponent();
            _blogNewsService = blogNewsService;
            LoadBlogNews();
        }

        public void LoadBlogNews()
        {
            var blogNews = _blogNewsService.GetAll();
            dgBlogNews.ItemsSource = blogNews;
        }

        private void btnCreate_Click(object sender, RoutedEventArgs e)
        {
            CreateBlogNews createBlogNews = App.ServiceProvider.GetRequiredService<CreateBlogNews>();
            createBlogNews.ShowDialog();
            LoadBlogNews();
        }

        private void btnSearch_Click(object sender, RoutedEventArgs e)
        {
            string searchTerm = txtSearch.Text;
            var filteredBlogNews = _blogNewsService.GetAll()
                .Where(bn => bn.Title.Contains(searchTerm, StringComparison.OrdinalIgnoreCase) ||
                             bn.Content.Contains(searchTerm, StringComparison.OrdinalIgnoreCase))
                .ToList();
            dgBlogNews.ItemsSource = filteredBlogNews;
        }

        private void btnUpdate_Click(object sender, RoutedEventArgs e)
        {
            if (dgBlogNews.SelectedItem is BlogNews selectedBlogNews)
            {
                UpdateBlogNews updateBlogNews = App.ServiceProvider.GetRequiredService<UpdateBlogNews>();
                updateBlogNews.LoadBlogNews(selectedBlogNews);
                updateBlogNews.ShowDialog();
                LoadBlogNews();
            }
            else
            {
                MessageBox.Show("Please select a blog news item to update.");
            }
        }

        private void btnDelete_Click(object sender, RoutedEventArgs e)
        {
            if (dgBlogNews.SelectedItem is BlogNews selectedBlogNews)
            {
                _blogNewsService.Delete(selectedBlogNews);
                LoadBlogNews();
            }
            else
            {
                MessageBox.Show("Please select a blog news item to delete.");
            }
        }
    }
}
