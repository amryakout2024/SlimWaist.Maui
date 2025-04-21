using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SlimWaist.Extentions
{
    public class FlowDirectionsExtention : IMarkupExtension
    {
        public object ProvideValue(IServiceProvider serviceProvider)
        {
            var binding=new Binding()
            {
                Path="[keyName]",
                Source=ChangeDirections.instance
            };

            return binding;
        }
    }
}
