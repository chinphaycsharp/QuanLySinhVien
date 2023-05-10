using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace QuanLySinhVien.ViewModel.Violate
{
    public class EditViolateViewModel
    {
        public int Id { get; set; }
        public string idStudent { get; set; }

        [Required]
        [StringLength(500)]
        public string ContentViolate { get; set; }

        public DateTime? UpdatedAt { get; set; }
    }
}