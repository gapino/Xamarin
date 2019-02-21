using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Model;

namespace Xamarin.ViewModels
{
    class ProfileViewModel
    {
        public UserLocal User { get; set; }

        public ProfileViewModel()
        {
            this.User = MainViewModel.GetInstance().User;
        }
    }
}
