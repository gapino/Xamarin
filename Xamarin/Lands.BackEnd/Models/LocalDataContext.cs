using Lands.Domain;
using System.Data.Entity;

namespace Lands.BackEnd.Models
{
    public class LocalDataContext: DataContext
    {
        public DbSet<User> Users { get; set; }
    }
}