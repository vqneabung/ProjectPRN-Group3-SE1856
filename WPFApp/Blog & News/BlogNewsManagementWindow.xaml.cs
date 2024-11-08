using BussinessObjects;
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
using WPFApp.Blog___News.BlogNewsData;
using WPFApp.Data;

namespace WPFApp.Blog___News
{
    /// <summary>
    /// Interaction logic for BlogNewsManagementWindow.xaml
    /// </summary>
    public partial class BlogNewsManagementWindow : Window
    {
        private readonly IService<BlogNews> _blogNewsService;
        private readonly IBlogNewsData _blogNewsData;



        public BlogNewsManagementWindow(IService<BlogNews> blogNewsService, IBlogNewsData blogNewsData)
        {
            InitializeComponent();
            _blogNewsService = blogNewsService;
            _blogNewsData = blogNewsData;
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

        private void btnUpdate_Click(object sender, RoutedEventArgs e)
        {
            if (dgBlogNews.SelectedItem is BlogNews selectedBlogNews)
            {
                _blogNewsData.postID = selectedBlogNews.PostId;
                UpdateBlogNews updateBlogNews = App.ServiceProvider.GetRequiredService<UpdateBlogNews>();
                updateBlogNews.Show();
                this.Hide();

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

        private void btnReturn_Click(object sender, RoutedEventArgs e)
        {
            ReturnToDashboard returnToDashboard = App.ServiceProvider.GetRequiredService<ReturnToDashboard>();
            returnToDashboard.Return();
            this.Hide();
        }
    }
}
