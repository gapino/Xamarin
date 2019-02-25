using Plugin.Media.Abstractions;
using Plugin.Media;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Model;
using Xamarin.Services;
using Xamarin.Forms;
using GalaSoft.MvvmLight.Command;
using System.Windows.Input;
using Xamarin.Helpers;

namespace Xamarin.ViewModels
{
    public class ProfileViewModel : BaseViewModel
    {

        #region atribbutos
        private ApiService apiService;

        private bool isRunning;
        private bool isEnabled;
        private ImageSource imageSource;
        private MediaFile file;
        #endregion

        #region Propertys
        public UserLocal User { get; set; }

        public ImageSource ImageSource
        {
            get { return this.imageSource; }
            set { SetValue(ref this.imageSource, value); }
        }

        public bool IsEnabled
        {
            get { return this.isEnabled; }
            set { SetValue(ref this.isEnabled, value); }
        }

        public bool IsRunning
        {
            get { return this.isRunning; }
            set { SetValue(ref this.isRunning, value); }
        }

        #endregion

        #region Construct
        public ProfileViewModel()
        {
            this.apiService = new ApiService();
            this.User = MainViewModel.GetInstance().User;
            this.ImageSource = this.User.ImageFullPath;
            this.isEnabled = true; 
        }
        #endregion

        #region Comandos

        public ICommand ChangeImageCommand
        {
            get
            {
                return new RelayCommand(ChangeImage);
            }
        }

        public ICommand SaveCommand
        {
            get
            {
                return new RelayCommand(Save);
            }
        }

        #endregion

        #region Metodos
        private async void Save()
        {
            if (string.IsNullOrEmpty(this.User.FirstName))
            {
                await Application.Current.MainPage.DisplayAlert(
                    "Error",
                    "Não pode dejar o Nome vazio",
                    "Aceitar");
                return;
            }

            if (string.IsNullOrEmpty(this.User.LastName))
            {
                await Application.Current.MainPage.DisplayAlert(
                    "Error",
                    "Não pode dejar o Sobrenome vazio",
                    "Aceitar");
                return;
            }

            if (string.IsNullOrEmpty(this.User.Email))
            {
                await Application.Current.MainPage.DisplayAlert(
                    "Error",
                    "Não pode dejar o Email vazio",
                    "Aceitar");
                return;
            }

            if (!RegexUtilities.IsValidEmail(this.User.Email))
            {
                await Application.Current.MainPage.DisplayAlert(
                    "Error",
                    "Não é o Email valido",
                    "Aceitar");
                return;
            }

            if (string.IsNullOrEmpty(this.User.Telephone))
            {
                await Application.Current.MainPage.DisplayAlert(
                  "Error",
                    "Não pode dejar o Telephone vazio",
                    "Aceitar");
                return;
            }

            this.IsRunning = true;
            this.IsEnabled = false;

            var checkConnetion = await this.apiService.CheckConnection();
            if (!checkConnetion.IsSucces)
            {
                this.IsRunning = false;
                this.IsEnabled = true;
                await Application.Current.MainPage.DisplayAlert(
                    "Error",
                    "Problemas de coneçao",
                    "Aceitar");
                return;
            }

            byte[] imageArray = null;
            if (this.file != null)
            {
                imageArray = FilesHelper.ReadFully(this.file.GetStream());
            }

            var userDomain = Converter.ToUserDomain(this.User, imageArray);
            

            var apiSecurity = Application.Current.Resources["APISecurity"].ToString();
            var response = await this.apiService.Put(
                apiSecurity,
                "/api",
                "/Users",
                MainViewModel.GetInstance().TokenType,
                MainViewModel.GetInstance().Token,
                userDomain);

            if (!response.IsSucces)
            {
                this.IsRunning = false;
                this.IsEnabled = true;
                await Application.Current.MainPage.DisplayAlert(
                    "Error",
                    response.Message,
                    "Aceitar");
                return;
            }

            this.IsRunning = false;
            this.IsEnabled = true;

           
            await App.Navigator.PopAsync();
        }


        private async void ChangeImage()
        {
            await CrossMedia.Current.Initialize();

            if (CrossMedia.Current.IsCameraAvailable &&
                CrossMedia.Current.IsTakePhotoSupported)
            {
                var source = await Application.Current.MainPage.DisplayActionSheet(
                    "De onde vai procurar a foto",
                    "Cancelar",
                    null,
                    "Galeria",
                    "Camara");

                if (source == "Cancelar")
                {
                    this.file = null;
                    return;
                }

                if (source == "Camara")
                {
                    this.file = await CrossMedia.Current.TakePhotoAsync(
                        new StoreCameraMediaOptions
                        {
                            Directory = "Sample",
                            Name = "test.jpg",
                            PhotoSize = PhotoSize.Small,
                        }
                    );
                }
                else
                {
                    this.file = await CrossMedia.Current.PickPhotoAsync();
                }
            }
            else
            {
                this.file = await CrossMedia.Current.PickPhotoAsync();
            }

            if (this.file != null)
            {
                this.ImageSource = ImageSource.FromStream(() =>
                {
                    var stream = file.GetStream();
                    return stream;
                });
            }
        } 
        #endregion
    }
}
