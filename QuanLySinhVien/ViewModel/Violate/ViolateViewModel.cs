using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace QuanLySinhVien.ViewModel.Violate
{
    public class ViolateViewModel
    {
        public int Id { get; set; }
        public string idStudent { get; set; }
        public string StudentName { get; set; }
        public string ContentViolate { get; set; }
        public DateTime? CreatedAt { get; set; }
    }
}