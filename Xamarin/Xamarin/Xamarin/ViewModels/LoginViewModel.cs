using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;

namespace Xamarin.ViewModels
{
    class LoginViewModel
    {
        public string Email { get; set; }

        public string Pass { get; set; }

        public bool IsRunning { get; set; }

        public bool IsRemember { get; set; }

        public ICommand LoginComand { get; set; }

        public LoginViewModel()
        {
            this.IsRemember = true;
        }
    }
}
