using GalaSoft.MvvmLight.Command;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Reflection;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;
using Xamarin.Model;
using Xamarin.Views;

namespace Xamarin.ViewModels
{
    public class LandViewModel : BaseViewModel
    {
        #region Atributos
        private ObservableCollection<string> borders;
        private ObservableCollection<Currency> currency;
        private ObservableCollection<Language> language;

        #endregion

        #region Propiedades
        public Land Land { get; set; }

        public ObservableCollection<string> Borders
        {
            get { return borders; }
            set { this.SetValue(ref borders, value); }
        }

        public ObservableCollection<Language> Language
        {
            get { return language; }
            set { this.SetValue(ref language, value); }
        }

        public ObservableCollection<Currency> Currency
        {
            get { return currency; }
            set { this.SetValue(ref currency, value); }
        }

        #endregion

        #region Constructor
        public LandViewModel(Land land)
        {
            this.Land = land;
            this.Borders = new ObservableCollection<string>(land.Borders);
            this.Currency = new ObservableCollection<Currency>(land.Currencies);
            this.Language = new ObservableCollection<Language>(land.Languages);
        } 
        #endregion
    }
}
