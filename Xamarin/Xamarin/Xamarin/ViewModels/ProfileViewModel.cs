using Plugin.Media.Abstractions;
using Plugin.Media;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Model;
using Xamarin.Services;
using Xamarin.Forms;

namespace Xamarin.ViewModels
{
    class ProfileViewModel : BaseViewModel
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
    }
}
