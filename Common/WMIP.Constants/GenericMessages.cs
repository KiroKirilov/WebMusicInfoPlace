using System;
using System.Collections.Generic;
using System.Text;

namespace WMIP.Constants
{
    public static class GenericMessages
    {
        public const string InvalidDataProvided = "Invalid data provided.";
        
        public const string FillOutForm = "Please provide a title and a body.";

        public const string NotFound = "{0} not found.";

        public const string CouldntDoSomething = "Couldn't {0}.";

        public const string SuccessfullyDidSomething = "Successfully {0}.";

        public const string InputStringLengthMinAndMaxErrorMessage = "The {0} must be at least {2} and at max {1} characters long.";

        public const string InputStringLengthMinOnlyErrorMessage = "The {0} must be at least {2} characters long.";
    }
}
