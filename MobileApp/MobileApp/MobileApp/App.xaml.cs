using MobileApp.Handlers;
using MobileApp.Models;
using MobileApp.Views;
using Xamarin.Forms;

namespace MobileApp
{
    public partial class App : Application
    {
        static Database DB;
        public static Database Database { get { if (DB == null) DB = new Database(); return DB; } }

        public static User CurrentUser { get; set; }

        public App()
        {
            InitializeComponent();

            MainPage = new LoginPage();
        }

        protected override void OnStart()
        {
            // Handle when your app starts
        }

        protected override void OnSleep()
        {
            // Handle when your app sleeps
        }

        protected override void OnResume()
        {
            // Handle when your app resumes
        }
    }
}
