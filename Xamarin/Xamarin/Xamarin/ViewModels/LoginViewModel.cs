using GalaSoft.MvvmLight.Command;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;
using Xamarin.Views;

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
                if (this.Email == "guille" && this.Pass == "123")
                {
                   
                    this.IsRunning = true;
                    this.IsEnabled = false;

                    this.Email = string.Empty;
                    this.Pass = string.Empty;

                    MainViewModel.GetInstance().Lands = new LandsViewModel();
                    await Application.Current.MainPage.Navigation.PushAsync(new LandsPage());

                    this.IsRunning = false;
                    this.IsEnabled = true;

                    
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
