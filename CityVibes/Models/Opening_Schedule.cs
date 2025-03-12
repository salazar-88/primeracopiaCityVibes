namespace CityVibes.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Opening_Schedule
    {
        [Key]
        public int Id_Schedule { get; set; }

        public int? Id_Place { get; set; }

        [Required]
        [StringLength(50)]
        public string Day { get; set; }

        public TimeSpan Opening_Time { get; set; }

        public TimeSpan Closing_Time { get; set; }

        public virtual Place Place { get; set; }
    }
}
