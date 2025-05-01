namespace SlimWaist.Extentions
{
    public class TranslateExtention : IMarkupExtension
    {
        public string Key { get; set; }

        public object ProvideValue(IServiceProvider serviceProvider)
        {
            var binding = new Binding()
            {
                Mode = BindingMode.OneWay,
                Path = $"[{Key}]",
                Source = Translator.instance
            };
            return binding;

        }
    }
}
