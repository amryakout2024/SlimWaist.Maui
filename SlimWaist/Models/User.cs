using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SlimWaist.Models
{
    public class User
    {
        [PrimaryKey,AutoIncrement]

        public int Id { get; set; }

        public string UserKey { get; set; }="0";

        public string? Email { get; set; }

    }
}
