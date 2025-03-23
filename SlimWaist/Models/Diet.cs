using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SlimWaist.Models
{
    public class Diet
    {
        [PrimaryKey,AutoIncrement]
        public int DietId { get; set; }

        public string? DietName { get; set; }

        public string? DietDescription { get; set; }

        public bool IsDefault { get; set; }=false;
    }
}
