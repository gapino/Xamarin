using Lands.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Lands.BackEnd.Models
{
    public class LocalDataContext: DataContext
    {
        public System.Data.Entity.DbSet<Lands.Domain.User> Users { get; set; }
    }
}