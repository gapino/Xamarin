using GalaSoft.MvvmLight.Command;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;
using Xamarin.Model;
using Xamarin.Services;


namespace Xamarin.ViewModels
{
    class LandsViewModel : BaseViewModel 
    {

        private ObservableCollection<LandViewItemModel> lands;
        private bool isRefresh;
        private string filter;
        private List<LandViewItemModel> landList;

        private ApiService apiService;

        public ObservableCollection<LandViewItemModel> Lands
        {
            get { return lands; }
            set { SetValue(ref lands, value); }
        }

        public bool IsRefresh
        {
            get { return isRefresh; }
            set { SetValue(ref isRefresh, value); }
        }
        public string Filter
        {
            get { return filter; }
            set
            {
                SetValue(ref filter, value);
                this.Search();
            }
        }

        public LandsViewModel()
        {
            this.apiService = new ApiService();
            this.LoadLands();
        }

        public ICommand RefreshCommand
        {
            get
            {
                return new RelayCommand(LoadLands);
            } 
        }

        public ICommand FilterCommand
        {
            get
            {
                return new RelayCommand(Search);
            }
        }

        private void Search()
        {
            if (string.IsNullOrEmpty(this.Filter))
            {
                this.Lands = new ObservableCollection<LandViewItemModel>(this.landList);
            }
            else
            {
                this.Lands = new ObservableCollection<LandViewItemModel>(this.landList.Where(
                    l => l.Name.ToLower().Contains(this.Filter.ToLower()) ||
                         l.Capital.ToLower().Contains(this.Filter.ToLower())));
            }
        }

        private async void LoadLands()
        {
            this.IsRefresh = true;
            var conect = await this.apiService.CheckConnection();

            if (!conect.IsSucces)
            {
                this.IsRefresh = false;
                await Application.Current.MainPage.DisplayAlert("Error", conect.Message, "Aceitar");
                await Application.Current.MainPage.Navigation.PopAsync();
                return;
            }
            var response = await this.apiService.GetList<LandViewItemModel>("http://restcountries.eu", "/rest", "/v2/all");

            if (!response.IsSucces)
            {
                this.IsRefresh = false;
                await Application.Current.MainPage.DisplayAlert("Error", response.Message,"Aceitar");
                return;
            }
            this.IsRefresh = false;
            this.landList = (List<LandViewItemModel>)response.Result;
            this.Lands = new ObservableCollection<LandViewItemModel>(this.landList);
        }
    }

}
