using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using SQLite.Net.Interop;
using Xamarin.Interface;

[assembly: Xamarin.Forms.Dependency(typeof(Xamarin.Droid.Implementation.Config))]
namespace Xamarin.Droid.Implementation
{
    public class Config : IConfig
    {
        private string directoryBD;

        private ISQLitePlatform plataform;

        public string DirectoryBD
        {
            get
            {
                if (string.IsNullOrEmpty(directoryBD))
                {
                    directoryBD = System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal);
                }
                return directoryBD;
            }
        }

        public ISQLitePlatform Plataform
        {
            get
            {
                if (plataform == null)
                {
                    plataform = new SQLite.Net.Platform.XamarinAndroid.SQLitePlatformAndroid();
                }

                return plataform;
            }
        }
    }
}