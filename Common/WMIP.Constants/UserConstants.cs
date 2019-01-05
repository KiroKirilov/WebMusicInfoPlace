using System;
using System.Collections.Generic;
using System.Text;

namespace WMIP.Constants
{
    public static class UserConstants
    {
        public static readonly string[] Roles = { "User", "Admin", "Artist", "Critic" };

        public const int UsernameMinLength = 3;

        public const int UsernameMaxLength = 30;

        public const int PasswordMinLength = 4;

        public const int PasswordMaxLength = 25;

        public const string DoNotMatchErrorMessage = "The password and confirmation password do not match.";

        public const string InvalidCharactersErrorMessage = "Only latic characters, digits and the symbols '-', '_', '.', '*', '~' are allowed".
    }
}
