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
using WPFApp.Course_Overview;
using WPFApp.Data;


namespace WPFApp.Forum
{
    /// <summary>
    /// Interaction logic for ForumWindow.xaml
    /// </summary>
    public partial class ForumWindow : Window
    {
        private readonly IService<BussinessObjects.Forum> _forumService;
        private readonly ReturnToDashboard _returnToDashboard;

        public static ForumWindow Create(IServiceProvider serviceProvider)
        {
            var forumService = serviceProvider.GetRequiredService<IService<BussinessObjects.Forum>>();
            var returnToDashboard = serviceProvider.GetRequiredService<ReturnToDashboard>();

            // Khởi tạo ForumWindow với các dependency và sử dụng giá trị `a` nếu cần
            return new ForumWindow(forumService, returnToDashboard);
        }


        public ForumWindow(IService<BussinessObjects.Forum> forumService, ReturnToDashboard returnToDashboard)
        {
            InitializeComponent();
            _forumService = forumService;
            _returnToDashboard = returnToDashboard;
            LoadForums();
        }

        public void LoadForums()
        {
            var courseId = WPFApp.Data.Data.courseId;
            var forums = _forumService.GetAll()?.Where(f => f.CourseId == courseId);
            dgForum.ItemsSource = forums;
        }


        private void btnCreate_Click(object sender, RoutedEventArgs e)
        {
            var newForum = new BussinessObjects.Forum
            {
                Title = tbTitle.Text,
                CreateDate = DateOnly.FromDateTime(DateTime.Now)
            };
            _forumService.Add(newForum);
            LoadForums();
        }

        private void btnUpdate_Click(object sender, RoutedEventArgs e)
        {
            if (dgForum.SelectedItem is BussinessObjects.Forum selectedForum)
            {
                selectedForum.Title = tbTitle.Text;
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

        private void btnEnter_Click(object sender, RoutedEventArgs e)
        {
            if (dgForum.SelectedItem is BussinessObjects.Forum selectedForum)
            {
                Forum.Data.ForumData.forum = selectedForum;
                PostWindow postWindow = PostWindow.Create(App.ServiceProvider);
                postWindow.Show();
                this.Hide();
            }

        }

        private void btnHome_Click(object sender, RoutedEventArgs e)
        {
            CourseOverviewWindow courseOverviewWindow = App.ServiceProvider.GetRequiredService<CourseOverviewWindow>();
            courseOverviewWindow.Show();
            this.Hide();
        }

        private void dgForum_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var forum = dgForum.SelectedItem as BussinessObjects.Forum;

            if (forum != null)
            {
                tbCourse.Text = forum.CourseId.ToString();
                tbTitle.Text = forum.Title;
            }
        }
    }
}
