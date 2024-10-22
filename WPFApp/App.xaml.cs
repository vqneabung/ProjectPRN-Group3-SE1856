using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Configuration;
using System.Data;
using System.IO;
using System.Text;
using System.Windows;

namespace WPFApp
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    /// 


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
            //configuration = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("appsettings.json").Build();

            //services.AddTransient<LoginPage>();
            //services.AddTransient<HomePage>();
            //services.AddTransient<AdminPage>();
            //services.AddTransient<ProfilePage>();
            //services.AddTransient<BookingReservationPage>();
            //services.AddTransient<ManageRoomPage>();
            //services.AddTransient<ManageRoomTypePage>();
            //services.AddTransient<ManageCustomerPage>();
            //services.AddTransient<Booking>();
            //services.AddTransient<BookingReservationPageForCustomer>();

            //services.AddSingleton<IConfiguration>(configuration);

            //services.AddSingleton<BookingReservationDAO>();
            //services.AddSingleton<CustomerDAO>();
            //services.AddSingleton<RoomInformationDAO>();
            //services.AddSingleton<RoomTypeDAO>();
            //services.AddSingleton<BookingDetailDAO>();

            //services.AddSingleton<IRoomInformationRepository, RoomInformationRepository>();
            //services.AddSingleton<IRoomInformationService, RoomInformationService>();
            //services.AddSingleton<ICustomerRepository, CustomerRepository>();
            //services.AddSingleton<ICustomerService, CustomerService>();
            //services.AddSingleton<IRoomTypeRepository, RoomTypeRepository>();
            //services.AddSingleton<IRoomTypeService, RoomTypeService>();
            //services.AddSingleton<IBookingReservationService, BookingReservationService>();
            //services.AddSingleton<IBookingReservationRepository, BookingReservationRepository>();
            //services.AddSingleton<IBookingDetailRepository, BookingDetailRepository>();
            //services.AddSingleton<IBookingDetailService, BookingDetailService>();

            //services.AddDbContext<FuminiHotelManagementContext>();



        }
    }

}
