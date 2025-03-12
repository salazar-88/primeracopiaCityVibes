namespace CityVibes.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Review
    {
        [Key]
        public int Id_Review { get; set; }

        public int? Id_User { get; set; }

        public int? Id_Place { get; set; }

        public int? Rating { get; set; }

        public string Comment { get; set; }

        public DateTime? Creation_Date { get; set; }

        public virtual Place Place { get; set; }

        public virtual User User { get; set; }
    }
}
