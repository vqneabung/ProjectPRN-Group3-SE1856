﻿using BussinessObjects;
using DataAccessObjects;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Repositories;
using Services;
using System.Configuration;
using System.Data;
using System.Text;
using System.Windows;
using WPFApp.Blog___News.BlogNewsData;
using WPFApp.Blog___News;
using WPFApp.Course_Overview;
using WPFApp.Enrollment_Manager;
using WPFApp.Forum;
using WPFApp.Course_Overview;
using Repositories;
using DataAccessObjects;
using Microsoft.Extensions.DependencyInjection;
using System.Text;
using Microsoft.Extensions.Configuration;
using BussinessObjects;
using Services;
using WPFApp.Enrollment_Manager;
using WPFApp.Document_Management;
using WPFApp.Department_Management;


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
            var login = ServiceProvider.GetRequiredService<MainWindow>();
            login.Show();


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


            services.AddScoped<IRepository<BussinessObjects.Forum>, ForumRespository>();
            services.AddScoped<IRepository<Department>, DepartmentRepository>();
            services.AddScoped<IRepository<Document>, DocumentRepository>();
            services.AddScoped<IRepository<Course>, CourseRepository>();
            services.AddScoped<IRepository<Assignment>, AssignmentRepository>();

            services.AddScoped<IService<Department>, DepartmentService>();
            services.AddScoped<IService<Document>, DocumentService>();
            services.AddScoped<IService<Course>, CourseService>();
            services.AddScoped<IService<Assignment>, AssignmentService>();

            services.AddDbContext<LmsContext>();

            services.AddScoped<CourseOverviewWindow>();
            services.AddScoped<AssignmentManagementWindow>();

            //Repository
            services.AddScoped<IRepository<BussinessObjects.Enrollment>, EnrollmentRepository>();
            services.AddScoped<IRepository<BlogNews>, BlogNewsRepository>();
            services.AddScoped<IRepository<BussinessObjects.Forum>, ForumRespository>();
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
            services.AddSingleton<CourseTypeDAO>();

            //services.AddScoped<IRepository<Forum>, ForumRespository>();
            services.AddScoped<IRepository<Department>, DepartmentRepository>();
            services.AddScoped<IRepository<Document>, DocumentRepository>();
            services.AddScoped<IRepository<Course>, CourseRepository>();
            services.AddScoped<IRepository<CourseType>, CourseTypeRepository>();
            services.AddScoped<IRepository<User>, UserRepository>();

            services.AddScoped<IService<Department>, DepartmentService>();
            services.AddScoped<IService<Document>, DocumentService>();
            services.AddScoped<IService<Course>, CourseService>();
            services.AddScoped<IService<CourseType>, CourseTypeService>();
            services.AddScoped<IService<User>, UserService>();

            //Service
            services.AddScoped<IService<BussinessObjects.Forum>, ForumService>();
            services.AddScoped<IService<BussinessObjects.Enrollment>, EnrollmentService>();
            services.AddScoped<IEnrollmentService, EnrollmentService>();
            services.AddScoped<IService<BlogNews>, BlogNewsService>();

            //Data
            services.AddScoped<IBlogNewsData, BlogNewsData>();

            //Windows
            services.AddScoped<CreateBlogNews>();
            services.AddScoped<MainWindow>();
            services.AddScoped<EnrollmentManagementWindow>();
            services.AddScoped<ForumWindow>();
            services.AddScoped<BlogNewsManagementWindow>();
            services.AddScoped<UpdateBlogNews>();
            services.AddScoped<MainWindow>();
            //services.AddSingleton<IRoomInformationRepository, RoomInformationRepository>();
            services.AddDbContext<LmsContext>();

            services.AddScoped<CourseOverviewWindow>();
            services.AddScoped<CourseOverviewPopup>();
            services.AddScoped<DocumentManagementWindow>();
            services.AddScoped<DocumentManagePopup>();
            services.AddScoped<DepartmentManagementWindow>();
            services.AddScoped<DepartmentManagementPopup>();

        }
    }

}
