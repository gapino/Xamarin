using GalaSoft.MvvmLight.Command;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;
using Xamarin.Helpers;
using Xamarin.Views;

namespace Xamarin.ViewModels
{
    public class MenuItemViewModel
    {
        public string Icon { get; set; }

        public string Title { get; set; }

        public string PageName { get; set; }

        public ICommand NavegationCommand
        {
            get
            {
                return new RelayCommand(Navegate);
            }
        }

        private void Navegate()
        {
            App.Master.IsPresented = false;
            if (this.PageName == "LoginPage")
            {
                Settings.Token = string.Empty;
                Settings.TokenType = string.Empty;

                var main = MainViewModel.GetInstance();
                main.Token = string.Empty;
                main.TokenType = string.Empty;
                Application.Current.MainPage = new NavigationPage( new LoginPage());
            }

            if (this.PageName == "ProfilePage")
            {

                MainViewModel.GetInstance().Profile = new ProfileViewModel();
                App.Navigator.PushAsync(new ProfilePage());
            }
        }
    }
}

