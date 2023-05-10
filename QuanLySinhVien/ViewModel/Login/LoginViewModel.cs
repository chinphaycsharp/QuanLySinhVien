using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace QuanLySinhVien.ViewModel.Login
{
    public class LoginViewModel
    {
        [Required(ErrorMessage = "Bạn chưa nhập trường này")]
        [StringLength(50)]
        public string UserName { get; set; }

        [Required(ErrorMessage = "Bạn chưa nhập trường này")]
        [StringLength(100)]
        public string PassWord { get; set; }

        public DateTime? CreatedAt { get; set; }

        public DateTime? UpdatedAt { get; set; }
    }
}