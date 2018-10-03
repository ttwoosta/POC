using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace POC_Web.ViewModel
{
    public class RegisterViewModel
    {
        [Required]
        [DataType(DataType.EmailAddress)]
        [RegularExpression(@"^[\d\w\._\-]+@([\d\w\._\-]+\.)+[\w]+$", ErrorMessage = "Email is invalid")]
        public string Email { get; set; }

        [Required]
        public string Name { get; set; }


        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }


        [Required(ErrorMessage = "Confirm Password is required")]
        [DataType(DataType.Password)]
        [Compare("Password")]
        public string ConfirmPassword { get; set; }


    }
}