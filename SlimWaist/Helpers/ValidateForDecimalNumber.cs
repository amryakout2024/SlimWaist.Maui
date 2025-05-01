using InputKit.Shared.Validations;
using System.Text.RegularExpressions;

namespace SlimWaist.Helpers
{
    class ValidateForDecimalNumber : IValidation
    {
        // work only for advanced entry

        Regex regex = new Regex(@"^[0-9]+(.[0-9])?$");

        public string Message { get; set; } = " ادخل رقم صحيح او عشري واحد فقط";

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
