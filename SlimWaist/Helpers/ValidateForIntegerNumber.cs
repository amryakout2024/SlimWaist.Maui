using InputKit.Shared.Validations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace SlimWaist.Helpers
{
    public class ValidateForIntegerNumber : IValidation
    {
        // work only for advanced entry

        Regex regex = new Regex(@"^[0-9]+$");

        public string Message{ get; set; } = "ادخل رقم صحيح";

        public bool Validate(object value)
        {
            if (value is string text)
            {
                return regex.IsMatch(text);
            }
            return false;
        }
    }
}
