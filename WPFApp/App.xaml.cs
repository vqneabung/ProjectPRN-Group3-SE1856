using BussinessObjects;
using DataAccessObjects;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Repositories;
using Services;
using System.Configuration;
using System.Data;
using System.Text;
using System.Windows;
using WPFApp.Course_Overview;

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


        }


        private void ConfigureService(IServiceCollection services)
        {

            //services.AddTransient<LoginPage>();
            //services.AddSingleton<IConfiguration>(configuration);

            services.AddSingleton<AssignmentDAO>();
            services.AddSingleton<BlogNewsDAO>();
            services.AddSingleton<CourseDAO>();
            services.AddSingleton<DepartmentDAO>();
            services.AddSingleton<DocumentDAO>();
            services.AddSingleton<EnrollmentDAO>();
            services.AddSingleton<ForumDAO>();
            services.AddSingleton<PostDAO>();
            services.AddSingleton<SubmissionDAO>();
            services.AddSingleton<UserDAO>();

            //services.AddScoped<IRepository<Forum>, ForumRespository>();
            //services.AddSingleton<IRoomInformationRepository, RoomInformationRepository>();
            services.AddScoped<IRepository<Department>, DepartmentRepository>();
            services.AddScoped<IRepository<Document>, DocumentRepository>();
            services.AddScoped<IRepository<Course>, CourseRepository>();
            services.AddScoped<IRepository<Assignment>, AssignmentRepository>();

            services.AddScoped<IService<Department>, DepartmentService>();
            //services.AddScoped<IService<Document>, DocumentService>();
            //services.AddScoped<IService<Course>, CourseService>();
            services.AddScoped<IService<Assignment>, AssignmentService>();



            services.AddDbContext<LmsContext>();

            services.AddScoped<CourseOverviewWindow>();
            services.AddScoped<AssignmentManagementWindow>();



        }
    }

}
