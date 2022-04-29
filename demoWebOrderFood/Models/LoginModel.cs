using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace demoWebOrderFood.Models
{
    public class LoginModel
    {

        [Key]
        [Display(Name = "Username")]
        [Required(ErrorMessage = "Bạn phải nhập tên đăng nhập.")]
        public string UserName { set; get; }

        [Required(ErrorMessage = "Bạn phải nhập mật khẩu.")]
        [Display(Name = "Password")]
        public string Password { set; get; }
    }
}