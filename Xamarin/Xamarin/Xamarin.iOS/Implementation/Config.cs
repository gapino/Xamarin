using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Foundation;
using SQLite.Net.Interop;
using UIKit;
using Xamarin.Interface;

[assembly: Xamarin.Forms.Dependency(typeof(Xamarin.iOS.Implementation.Config))]
namespace Xamarin.iOS.Implementation
{
    public class Config: IConfig
    {
        private string directoryBD;

        private ISQLitePlatform plataform;

        public string DirectoryBD
        {
            get
            {
                if (string.IsNullOrEmpty(directoryBD))
                {
                    var directory = System.Environment.GetFolderPath(Environment.SpecialFolder.Personal);
                    directoryBD = System.IO.Path.Combine(directory,"..","Library");
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
                    plataform = new SQLite.Net.Platform.XamarinIOS.SQLitePlatformIOS();
                }

                return plataform;
            }
        }
    }
}