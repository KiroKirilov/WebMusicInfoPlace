using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using WMIP.Constants;

namespace WMIP.Web.Models.Users
{
    public class RegisterViewModel
    {
        [Required]
        [DataType(DataType.Text)]
        [StringLength(maximumLength: UserConstants.UsernameMaxLength, 
            ErrorMessage = GenericMessages.InputStringLengthMinAndMaxErrorMessage,
            MinimumLength = UserConstants.UsernameMinLength)]
        [RegularExpression("[a-zA-Z0-9-_.*~]+", ErrorMessage = UserConstants.InvalidCharactersErrorMessage)]
        [Display(Name = "Username")]
        public string Username { get; set; }

        [Required]
        [EmailAddress]
        [Display(Name = "Email")]
        public string Email { get; set; }

        [Required]
        [StringLength(maximumLength: UserConstants.PasswordMaxLength,
            ErrorMessage = GenericMessages.InputStringLengthMinAndMaxErrorMessage,
            MinimumLength = UserConstants.PasswordMinLength)]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = UserConstants.DoNotMatchErrorMessage)]
        [Display(Name = "Confirm Password")]
        public string ConfirmPassword { get; set; }
    }
}
