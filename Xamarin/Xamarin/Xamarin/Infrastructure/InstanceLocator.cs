using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.ViewModels;

namespace Xamarin.Infrastructure
{
    class InstanceLocator
    {
        public MainViewModel Main { get; set; }

        public InstanceLocator()
        {
            this.Main = new MainViewModel();
        }
    }
}
