namespace QuanLySinhVien.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Violate")]
    public partial class Violate
    {
        public int id { get; set; }

        [StringLength(10)]
        public string idStudent { get; set; }

        [Required]
        [StringLength(500)]
        public string ContentViolate { get; set; }

        public DateTime? CreatedAt { get; set; }

        public DateTime? UpdatedAt { get; set; }

        public virtual Student Student { get; set; }
    }
}
