using Microsoft.Extensions.DependencyInjection;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using WPFApp.Blog___News;
using WPFApp.Enrollment;
using WPFApp.Forum;
using WPFApp.Forum.Data;

namespace WPFApp
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void ForumTest(object sender, RoutedEventArgs e)
        {
            ForumWindow forumWindow = App.ServiceProvider.GetRequiredService<ForumWindow>();
            forumWindow.Show(); 
            this.Close();
        }

        private void EnrollmentTest(object sender, RoutedEventArgs e)
        {
            EnrollmentManagementWindow enrollmentManagementWindow = App.ServiceProvider.GetRequiredService<EnrollmentManagementWindow>();
            enrollmentManagementWindow.Show();
            this.Close();

        }

        private void BlogNewsTest(object sender, RoutedEventArgs e)
        {
            CreateBlogNews createBlogNews = App.ServiceProvider.GetRequiredService<CreateBlogNews>();
            createBlogNews.Show();
            this.Close();

        }
    }
}