using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Xamarin.Helpers;
using Xamarin.Model;
using Xamarin.Services;
using Xamarin.ViewModels;
using Xamarin.Views;

namespace Xamarin
{
    public partial class App : Application
    {
        public static NavigationPage Navigator { get; internal set; }
        public static MasterPage Master { get; internal set; }

        public App()
        {
            InitializeComponent();
            if (string.IsNullOrEmpty(Settings.Token))
            {
                MainPage = new NavigationPage(new LoginPage());
            }
            else
            {
                var dataService = new DataService();
                var user = dataService.First<UserLocal>(false);

                var main = MainViewModel.GetInstance();
                main.User = user;
                main.Token = Settings.Token;
                main.TokenType = Settings.TokenType;
                main.Lands = new LandsViewModel();
                MainPage = new MasterPage();
            }
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
