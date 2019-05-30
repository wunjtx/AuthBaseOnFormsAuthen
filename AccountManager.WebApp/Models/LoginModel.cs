using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace AccountManager.WebApp.Models
{
    public class LoginModel
    {
        [DisplayName("User Name")]
        [Required]
        [MaxLength(length:50, ErrorMessage ="max length is 50")]
        [MinLength(length:5, ErrorMessage = "max length is 5")]
        public string UserName { get; set; }
        [DisplayName("User Password")]
        [Required]
        [MaxLength(length: 50, ErrorMessage = "max length is 50")]
        [MinLength(length: 5, ErrorMessage = "max length is 5")]
        public string UserPassword { get; set; }
        [DisplayName("Remember Me")]

        public bool RememberMe { get; set; }
    }
}