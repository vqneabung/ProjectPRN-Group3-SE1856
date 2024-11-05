using System.Configuration;
using System.Data;
using System.Windows;
using WPFApp.Blog___News;
using WPFApp.Blog___News.BlogNewsData;
using WPFApp.Enrollment;
using WPFApp.Forum;

namespace WPFApp
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        public static ServiceProvider? ServiceProvider { get; private set; }
        public IConfiguration? configuration;

        protected override void OnStartup(StartupEventArgs e)
        {

            var serviceCollection = new ServiceCollection();
            ConfigureService(serviceCollection);
            ServiceProvider = serviceCollection.BuildServiceProvider();
            Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);
            //var login = ServiceProvider.GetRequiredService<LoginPage>();
            //login.Show();

            var mainWindow = ServiceProvider.GetRequiredService<MainWindow>();
            mainWindow.Show();


        }


        private void ConfigureService(IServiceCollection services)
        {

            //DAO
            services.AddScoped<AssignmentDAO>();
            services.AddScoped<BlogNewsDAO>();
            services.AddScoped<CourseDAO>();
            services.AddScoped<DepartmentDAO>();
            services.AddScoped<DocumentDAO>();
            services.AddScoped<EnrollmentDAO>();
            services.AddScoped<ForumDAO>();
            services.AddScoped<PostDAO>();
            services.AddScoped<SubmissionDAO>();
            services.AddScoped<UserDAO>();   

            services.AddScoped<IRepository<BussinessObjects.Forum>, ForumRespository>();
            //Repository
            services.AddScoped<IRepository<BussinessObjects.Enrollment>, EnrollmentRepository>();
            services.AddScoped<IRepository<BlogNews>, BlogNewsRepository>();
            services.AddScoped<IRepository<BussinessObjects.Forum>, ForumRespository>();


            //Service
            services.AddScoped<IService<BussinessObjects.Forum>, ForumService>();
            services.AddScoped<IService<BussinessObjects.Enrollment>, EnrollmentService>();
            services.AddScoped<IService<BlogNews>, BlogNewsService>();

            //Context
            services.AddDbContext<LmsContext>();

            //Data
            services.AddScoped<IBlogNewsData,BlogNewsData>();

            //Windows
            services.AddScoped<CreateBlogNews>();
            services.AddScoped<MainWindow>();
            services.AddScoped<EnrollmentManagementWindow>();
            services.AddScoped<ForumWindow>();
            services.AddScoped<BlogNewsManagementWindow>();
            services.AddScoped<UpdateBlogNews>();


        }
    }

}
