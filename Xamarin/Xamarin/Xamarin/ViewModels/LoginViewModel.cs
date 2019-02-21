using GalaSoft.MvvmLight.Command;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;
using Xamarin.Helpers;
using Xamarin.Services;
using Xamarin.Views;

namespace Xamarin.ViewModels
{
    class LoginViewModel : BaseViewModel
    {
        private ApiService apiService;
        private DataService dataServices;
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

        public ICommand RegisterCommand
        {
            get
            {
                return new RelayCommand(Register);
            }
        }

        private async void Register()
        {
            this.IsRunning = true;
            this.IsEnabled = false;
            MainViewModel.GetInstance().Register = new RegisterViewModel();
            await Application.Current.MainPage.Navigation.PushAsync(new RegisterPage());
            this.IsRunning = false;
            this.IsEnabled = true;
        }

        private async void Login()
        {
            this.IsRunning = true;
            this.IsEnabled = false;
            if (string.IsNullOrEmpty(this.Email) || string.IsNullOrEmpty(this.Pass))
            {
                this.IsRunning = true;
                this.IsEnabled = false;
                await Application.Current.MainPage.DisplayAlert("Error", "não pode deixar campos vazios", "Aceitar");
                this.IsRunning = false;
                this.IsEnabled = true;

            }
            else
            {
                this.IsRunning = true;
                this.IsEnabled = false;
                var connection = await this.apiService.CheckConnection();
                if (!connection.IsSucces)
                {
                    this.IsRunning = false;
                    this.IsEnabled = true;
                    await Application.Current.MainPage.DisplayAlert("Error", connection.Message, "Aceitar");
                    return;
                }
                    var token = await apiService.GetToken("http://guille.somee.com/Token", this.Email, this.Pass);

                if (token == null)
                {
                    this.IsRunning = false;
                    this.IsEnabled = true;
                    await Application.Current.MainPage.DisplayAlert("Error", "Algo aconteceu mal, por favor tente de novo", "Aceitar");
                    return;
                }

                if (string.IsNullOrEmpty(token.Accesstoken))
                {
                    this.IsRunning = false;
                    this.IsEnabled = true;
                    await Application.Current.MainPage.DisplayAlert("Error", "Usuario ou password incorrecto", "Aceitar");
                    return;
                }

                var user = await apiService.GetUserByEmail(Application.Current.Resources["APISecurity"].ToString(),
                                                           "/api", "/Users/GetUserByEmail", this.Email);

                var userLocal = Converter.ToUserLocal(user);
                
                var mainViewModel = MainViewModel.GetInstance();

                mainViewModel.User = userLocal;
                mainViewModel.Token = token.Accesstoken;
                mainViewModel.TokenType = token.Tokentype;
                if (this.IsRemember)
                {
                    Settings.Token = token.Accesstoken;
                    Settings.TokenType = token.Tokentype;
                    this.dataServices.DeleteAllAndInsert(userLocal);
                }
                mainViewModel.Lands = new LandsViewModel();
                Application.Current.MainPage = new MasterPage();
                this.Email = string.Empty;
                this.Pass = string.Empty;

                this.IsRunning = false;
                this.IsEnabled = true;
            }
        }

        public LoginViewModel()
        {
            this.apiService = new ApiService();
            this.dataServices = new DataService();
            this.IsRemember = true;
            this.IsEnabled = true;
            
        }
    }
}
