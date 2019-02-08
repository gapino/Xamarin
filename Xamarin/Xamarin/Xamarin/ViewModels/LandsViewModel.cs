using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using Xamarin.Forms;
using Xamarin.Model;
using Xamarin.Services;


namespace Xamarin.ViewModels
{
    class LandsViewModel : BaseViewModel 
    {

        private ObservableCollection<Lands> lands;
        private ApiService apiService;

        public ObservableCollection<Lands> Lands
        {
            get { return lands; }
            set { SetValue(ref lands, value); }
        }


        public LandsViewModel()
        {
            this.apiService = new ApiService();
            this.LoadLands();
        }

        private async void LoadLands()
        {
            var conect = await this.apiService.CheckConnection();

            if (!conect.IsSucces)
            {
                await Application.Current.MainPage.DisplayAlert("Error", conect.Message, "Aceitar");
                await Application.Current.MainPage.Navigation.PopAsync();
                return;
            }
            var response = await this.apiService.GetList<Lands>("http://restcountries.eu", "/rest", "/v2/all");

            if (!response.IsSucces)
            {
                await Application.Current.MainPage.DisplayAlert("Error", response.Message,"Aceitar");
                return;
            }

            var list = (List<Lands>)response.Result;
           // this.Lands = new ObservableCollection<Lands>(list); 
        }
    }

}
