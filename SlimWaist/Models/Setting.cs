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
        [PrimaryKey,AutoIncrement]
        public int Id { get; set; }

        public string? MemberShipId { get; set; }

        public bool IsLoginSaved { get; set; }=false;

        public string? Email { get; set; }

        public string? Password { get; set; }
    }
}
