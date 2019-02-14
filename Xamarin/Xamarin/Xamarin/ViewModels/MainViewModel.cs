using System;
using System.Collections.Generic;
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

        public MainViewModel()
        {
            instance = this;
            this.Login = new LoginViewModel();
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
