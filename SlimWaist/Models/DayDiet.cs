using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SlimWaist.Models
{
    public class DayDiet
    {
        [PrimaryKey,AutoIncrement]
        public int DayDietId { get; set; }

        public int MembershipId { get; set; }

        public int DietId { get; set; }

        public DateTime DayDietDate { get; set; }

        //public int DateMonth { get; set; }

        //public int DateYear { get; set; }

        public bool IsExistsInDb { get; set; } = false;

    }
}
