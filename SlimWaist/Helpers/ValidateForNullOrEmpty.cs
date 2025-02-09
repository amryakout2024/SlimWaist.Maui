using InputKit.Shared.Validations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace SlimWaist.Helpers
{
    class ValidateForNullOrEmpty : IValidation
    {      
        // work only for advanced entry


        public string Message { get; set; } = "ادخل القيمة";

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
