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

        }

        private void btnSearch_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btnUpdate_Click_1(object sender, RoutedEventArgs e)
        {
            UpdateBlogNews updateBlogNews = App.ServiceProvider.GetRequiredService<UpdateBlogNews>();
        }

        private void btnDelete_Click_2(object sender, RoutedEventArgs e)
        {

        }
    }
}
