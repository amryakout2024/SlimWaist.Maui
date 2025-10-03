using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SlimWaist.Models.FirebaseDto
{
    public class FirebaseMembership
    {
        public int Id { get; set; }

        public string? UserKey { get; set; }

        public string? Email { get; set; }

        public Membership? Membership { get; set; }
    }
}
