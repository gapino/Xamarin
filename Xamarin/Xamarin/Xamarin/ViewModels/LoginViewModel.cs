using GalaSoft.MvvmLight.Command;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;

namespace Xamarin.ViewModels
{
    class LoginViewModel : BaseViewModel
    {

        private string email;
        private string pass;
        private bool isRunning;
        private bool isEnabled;

        public string Email
        {
            get { return email; }
            set { SetValue(ref email, value); }
        }

        public string Pass
        {
            get { return pass; }
            set { SetValue(ref pass, value); }
        }

        public bool IsRunning
        {
            get { return isRunning; }
            set { SetValue(ref isRunning, value); }
        }

        public bool IsEnabled
        {
            get { return isEnabled; }
            set { SetValue(ref isEnabled, value); }
        }

        public bool IsRemember { get; set; }

        public ICommand LoginComand
        {
            get
            {
                return new RelayCommand(Login);
            }
        }

        private async void Login()
        {
            if (string.IsNullOrEmpty(this.Email) || string.IsNullOrEmpty(this.Pass))
            {
                await Application.Current.MainPage.DisplayAlert("Error", "não pode deixar campos vazios", "Aceitar");
            }
            else
            {
                if (this.Email == "guille.pino24@gmail.com" && this.Pass == "123")
                {
                    await Application.Current.MainPage.DisplayAlert("OK", "Seja Benvindo", "Aceitar");
                    this.IsRunning = true;
                    this.IsEnabled = false;
                }
                else
                {
                    await Application.Current.MainPage.DisplayAlert("Error", "Usuario ou password incorrecto", "Aceitar");
                    this.Pass = string.Empty;
                    this.IsRunning = false;
                    this.IsEnabled = true;
                }
            }
        }

        public LoginViewModel()
        {
            this.IsRemember = true;
            this.IsEnabled = true;
        }
    }
}
