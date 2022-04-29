using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace demoWebOrderFood.Models
{
    public class RegisterModel
    {
        [Key]
        public long ID { set; get; }

        [Display(Name="Your UserName")]
        [StringLength(16, MinimumLength = 8, ErrorMessage = "Tên đăng nhập độ dài từ 8-16 ký tự.")]
        [Required(ErrorMessage ="Yêu cầu nhập tên đăng nhập.")]
        public string UserName { set; get; }

        [Display(Name = "Create a password")]
        [StringLength(20,MinimumLength = 6,ErrorMessage ="Độ dài mật khẩu ít nhất 6 ký tự.")]
        [Required(ErrorMessage = "Yêu cầu nhập mật khẩu.")]
        public string Password { set; get; }

        [Display(Name = "Confirm your password")]
        [Compare("Password", ErrorMessage ="Xác nhận mật khẩu không đúng.")]
        public string ConfirmPassword { set; get; }

        [Display(Name="Name")]
        [Required(ErrorMessage = "Yêu cầu nhập họ tên.")]
        public string Name { set; get; }

        [Required(ErrorMessage = "Yêu cầu nhập email.")]
        [Display(Name = "Your email")]
        public string Email { set; get; }
    }
}