using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Linq;

namespace CityVibes.Models
{
    public partial class Model1 : DbContext
    {
        public Model1()
            : base("name=DB_Model")
        {
        }

        public virtual DbSet<Category> Categories { get; set; }
        public virtual DbSet<Favorite> Favorites { get; set; }
        public virtual DbSet<Opening_Schedule> Opening_Schedule { get; set; }
        public virtual DbSet<Place> Places { get; set; }
        public virtual DbSet<Price_Ranges> Price_Ranges { get; set; }
        public virtual DbSet<Review> Reviews { get; set; }
        public virtual DbSet<User> Users { get; set; }
        public bool Status { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Category>()
                .HasMany(e => e.Users)
                .WithMany(e => e.Categories)
                .Map(m => m.ToTable("User_Categories").MapLeftKey("Id_Category").MapRightKey("Id_User"));

            modelBuilder.Entity<Place>()
                .Property(e => e.Latitude)
                .HasPrecision(9, 6);

            modelBuilder.Entity<Place>()
                .Property(e => e.Longitude)
                .HasPrecision(9, 6);

            modelBuilder.Entity<Place>()
                .Property(e => e.Rating)
                .HasPrecision(3, 2);
        }
    }
}
