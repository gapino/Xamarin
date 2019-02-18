using GalaSoft.MvvmLight.Command;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;
using Xamarin.Model;
using Xamarin.Views;

namespace Xamarin.ViewModels
{
    public class LandViewItemModel : Land
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
            Land obj = new Land
            {
                Name = this.Name,
                TopLevelDomain = this.TopLevelDomain,
                Alpha2Code = this.Alpha2Code,
                Alpha3Code = this.Alpha3Code,
                CallingCodes = this.CallingCodes,
                Capital = this.Capital,
                AltSpellings = this.AltSpellings,
                Region = this.Region,
                Subregion = this.Subregion,
                Population = this.Population,
                Latlng = this.Latlng,
                Demonym = this.Demonym,
                Area = this.Area,
                Gini = this.Gini,
                Timezones = this.Timezones,
                Borders = this.Borders,
                NativeName = this.NativeName,
                NumericCode = this.NumericCode,
                Currencies = this.Currencies,
                Languages = this.Languages,
                Translations = this.Translations,
                Flag = this.Flag,
                RegionalBlocs = this.RegionalBlocs,
                Cioc = this.Cioc
            };
            MainViewModel.GetInstance().Land = new LandViewModel(obj);
            App.Navigator.PushAsync(new LandTabbedPage());
        }
    }
}
