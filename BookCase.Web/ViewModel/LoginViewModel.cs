using System;
using System.ComponentModel.DataAnnotations;

namespace BookCase.Web.ViewModel
{
    public class LoginViewModel
    {
        [Display(Name = "Email")]
        public string Email { get; set; }

        [Display(Name = "Password")]
        public string Password { get; set; }
    }
}
