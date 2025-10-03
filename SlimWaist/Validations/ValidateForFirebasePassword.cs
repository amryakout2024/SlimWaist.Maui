using InputKit.Shared.Validations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace SlimWaist.Validations
{
    public class ValidateForFirebasePassword : IValidation
    {
        public string Message => App.ValidateForFirebasePasswordMessage;

        public bool Validate(object value)
        {
            if (value is string text)
            {
                if (text.Trim().Length >= 6)
                    return true;
            }
            return false;
        }

    }
}
