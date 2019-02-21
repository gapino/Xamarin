using System;
using System.Collections.Generic;
using System.Text;
using SQLite.Net.Interop;

namespace Xamarin.Interface
{
    public interface IConfig
    {
        string DirectoryBD { get; }

        ISQLitePlatform Plataform { get; } 
    }
}
