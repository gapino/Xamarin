using GalaSoft.MvvmLight.Command;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;
using Xamarin.Model;
using Xamarin.Views;

namespace Xamarin.ViewModels
{
    public class LandViewItemModel : Lands
    {
        public ICommand SelectLandCommand
        {
            get
            {
                return new RelayCommand(SelectItem);
            }
        }

        private void SelectItem()
        {
            Lands obj = new Lands
            {
                Name = this.Name,
                TopLevelDomain = this.TopLevelDomain,
                Alpha2Code = this.Alpha2Code,
                Alpha3Code = this.Alpha3Code,
                CallingCodes = this.CallingCodes,
                Capital = this.Capital
            };
            MainViewModel.GetInstance().Land = new LandViewModel(obj);
            Application.Current.MainPage.Navigation.PushAsync(new LandPage());
        }
    }
}
