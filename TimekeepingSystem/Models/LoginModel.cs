using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace TimekeepingSystem.Models
{
    public class LoginModel
    {
        [Required(ErrorMessage = "Please re-enter username")]
        public string UserName { get; set; }

        [Required(ErrorMessage = "Please re-enter password")]
        public string Password { get; set; }

        public bool RememberMe { get; set; }
    }
}