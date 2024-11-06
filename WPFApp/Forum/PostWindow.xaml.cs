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
    /// Interaction logic for PostWindow.xaml
    /// </summary>
    public partial class PostWindow : Window
    {
        private readonly IService<Post> _postService;
        private readonly LmsContext _context;

        public PostWindow(IService<Post> postService, LmsContext context)
        {
            InitializeComponent();
            _postService = postService;
            _context = context;
            LoadPosts();
        }

        private void LoadPosts()
        {
            dgForum.ItemsSource = _postService.GetAll();
        }

        private void btnCreate_Click(object sender, RoutedEventArgs e)
        {
            var newPost = new Post
            {
                Content = "New Post Content",
                PostDate = DateOnly.FromDateTime(DateTime.Now)
            };
            _postService.Add(newPost);
            LoadPosts();
        }

        private void btnDelete_Click(object sender, RoutedEventArgs e)
        {
            if (dgForum.SelectedItem is Post selectedPost)
            {
                _postService.Delete(selectedPost);
                LoadPosts();
            }
        }
    }
}
