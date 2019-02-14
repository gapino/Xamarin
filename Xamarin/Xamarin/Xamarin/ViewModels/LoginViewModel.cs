﻿using GalaSoft.MvvmLight.Command;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;
using Xamarin.Services;
using Xamarin.Views;

namespace Xamarin.ViewModels
{
    class LoginViewModel : BaseViewModel
    {
        private ApiService apiService;
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
                    var token = await apiService.GetToken("http://guille.somee.com/api/", this.Email, this.Pass);

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

                var mainViewModel = MainViewModel.GetInstance();
                mainViewModel.Token = token;
                mainViewModel.Lands = new LandsViewModel();
                await Application.Current.MainPage.Navigation.PushAsync(new LandsPage());
                this.Email = string.Empty;
                this.Pass = string.Empty;

                this.IsRunning = false;
                this.IsEnabled = true;
            }
        }

        public LoginViewModel()
        {
            this.apiService = new ApiService();
            this.IsRemember = true;
            this.IsEnabled = true;
        }
    }
}
