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
    public class LandViewModel : BaseViewModel
    {
        public Lands Land { get; set; }

        public LandViewModel(Lands land)
        {
            this.Land = land;
        }
    }
}
