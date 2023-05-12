using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace QuanLySinhVien.ViewModel.Student
{
    public class AddStudentViewModel
    {
        [Required(ErrorMessage = "Bạn chưa nhập trường này")]
        [StringLength(255)]
        public string Id { get; set; }
        [Required(ErrorMessage = "Bạn chưa nhập trường này")]
        [StringLength(255)]
        public string Name { get; set; }

        [Required(ErrorMessage = "Bạn chưa nhập trường này")]
        [StringLength(255)]
        public string Phone { get; set; }

        [Required(ErrorMessage = "Bạn chưa nhập trường này")]
        [StringLength(255)]
        public string Address { get; set; }

        [Required(ErrorMessage = "Bạn chưa nhập trường này")]
        [StringLength(255)]
        public string Email { get; set; }


        public DateTime BirthDay { get; set; }
        public string BirthDayStr { get; set; }

        [Required(ErrorMessage = "Bạn chưa nhập trường này")]
        [StringLength(10)]
        public string ClassName { get; set; }

        [Required(ErrorMessage = "Bạn chưa nhập trường này")]
        [StringLength(255)]
        public string MajorName { get; set; }

        public DateTime? CreatedAt { get; set; }
    }
}