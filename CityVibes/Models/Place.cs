namespace CityVibes.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Place
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Place()
        {
            Favorites = new HashSet<Favorite>();
            Opening_Schedule = new HashSet<Opening_Schedule>();
            Reviews = new HashSet<Review>();
        }

        [Key]
        public int Id_Place { get; set; }

        [Required]
        [StringLength(255)]
        public string Name { get; set; }

        public string Description { get; set; }

        public int? Id_Category { get; set; }

        [StringLength(255)]
        public string Address { get; set; }

        [StringLength(100)]
        public string City { get; set; }

        public decimal? Latitude { get; set; }

        public decimal? Longitude { get; set; }

        public decimal? Rating { get; set; }

        public int? Id_Price_Range { get; set; }

        public DateTime? Creation_Date { get; set; }

        public virtual Category Category { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Favorite> Favorites { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Opening_Schedule> Opening_Schedule { get; set; }

        public virtual Price_Ranges Price_Ranges { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Review> Reviews { get; set; }
    }
}
