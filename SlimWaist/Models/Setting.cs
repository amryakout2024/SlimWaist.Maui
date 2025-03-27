using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SlimWaist.Models
{
    public class Setting
    {
        [PrimaryKey]
        public int Id { get; set; }

        public int CurrentMemberShipId { get; set; }=0;

        public int SavedMemberShipId { get; set; }=0;

    }
}
