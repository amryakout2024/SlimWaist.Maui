using InputKit.Shared.Validations;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace SlimWaist.Helpers
{
    class ValidateForNullOrEmpty : IValidation
    {
        // work only for advanced entry

        public string Message => AppShell.ValidateForNullOrEmptyMessage;

        public bool Validate(object value)
        {
            if (value is string text)
            {
                return !string.IsNullOrEmpty(text);
            }

            return false;
        }
    }
}
