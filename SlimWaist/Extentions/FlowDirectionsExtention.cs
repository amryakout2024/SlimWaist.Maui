namespace SlimWaist.Extentions
{
    public class FlowDirectionsExtention : IMarkupExtension
    {
        public object ProvideValue(IServiceProvider serviceProvider)
        {
            var binding = new Binding()
            {
                Path = "[keyName]",
                Source = ChangeDirections.instance
            };

            return binding;
        }
    }
}
