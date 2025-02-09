using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SlimWaist.Helpers
{
    public class Navigation
    {
        public static async Task GoToAsync(string pageName)
        {
            await Shell.Current.GoToAsync(pageName, animate: true);
        }
    }
}

