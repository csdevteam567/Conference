using System;
using System.Data.Entity;
using System.Linq;

namespace Conference.Models
{
    public class ConferenceModel : DbContext
    {
        public ConferenceModel()
            : base("name=ConferenceModel")
        {
            if (!Database.Exists())
            {
                Database.Create();
            }
        }

        public virtual DbSet<User> Users { get; set; }

        public virtual DbSet<City> Cities { get; set; }
    }
}