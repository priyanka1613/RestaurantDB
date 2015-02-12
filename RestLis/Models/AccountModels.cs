using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using System.Web.Mvc;
using System.Web.Security;

namespace RestLis.Models
{

    #region Models

    public class ChangePasswordModel
    {
        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Current password")]
        public string OldPassword { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "New password")]
        public string NewPassword { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Confirm new password")]
        [Compare("NewPassword", ErrorMessage = "The new password and confirmation password do not match.")]
        public string ConfirmPassword { get; set; }
    }

    public class LogOnModel
    {
        [Required]
        [Display(Name = "Användarnamn:")]
        public string UserName { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Lösenord:")]
        public string Password { get; set; }

        [Display(Name = "kom ihåg mig")]
        public bool RememberMe { get; set; }
    }


    public class RegisterModel
    {

        [Required]
        [Display(Name = "Användarnamn:")]
        public string UserName { get; set; }


        [Required]
        [DataType(DataType.EmailAddress)]
        [Display(Name = "E-postadress:")]
        public string Email { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Lösenord:")]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Skriv lösenord igen:")]
        [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
        public string ConfirmPassword { get; set; }
    }
}
    #endregion  