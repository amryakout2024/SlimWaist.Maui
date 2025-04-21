using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SlimWaist.Extentions
{
    public class ChangeDirections
    {
        public FlowDirection this[string key]=>FlowDirection;

        public FlowDirection FlowDirection { get; set; }

        public static ChangeDirections instance {get;set;}=new ChangeDirections();
    }
}
