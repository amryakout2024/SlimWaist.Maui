using InputKit.Shared.Validations;

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
