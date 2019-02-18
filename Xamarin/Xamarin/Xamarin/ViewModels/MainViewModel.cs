using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using Xamarin.Model;

namespace Xamarin.ViewModels
{
    class MainViewModel
    {
        public LoginViewModel Login { get; set; }

        public LandsViewModel Lands { get; set; }

        public LandViewModel Land { get; set; }

        public TokenResponse Token { get; set; }

        public ObservableCollection<MenuItemViewModel> Menus { get; set; }

        public MainViewModel()
        {
            instance = this;
            this.Login = new LoginViewModel();
            this.LoadMenu();
        }

        private void LoadMenu()
        {
            this.Menus = new ObservableCollection<MenuItemViewModel>();
            this.Menus.Add(new MenuItemViewModel { Icon = "ic_chevron_right", Title = "Profile", PageName = "ProfilePage" });
            this.Menus.Add(new MenuItemViewModel { Icon = "ic_chevron_right", Title = "Logout", PageName = "LoginPage" });
        }

        private static MainViewModel instance;

        public static MainViewModel GetInstance()
        {
            if (instance == null)
            {
                return new MainViewModel();
            }
            return instance;
        }
    }
}
