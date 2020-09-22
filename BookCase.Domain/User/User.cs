using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace BookCase.Domain.User
{
    public class User
    {
        public Guid Id { get; set; }
        [Display(Name = "Mail")]
        public string Mail { get; set; }
        [Display(Name = "Password")]
        public string Password { get; set; }
        public Role Role { get; set; }

    }
}
