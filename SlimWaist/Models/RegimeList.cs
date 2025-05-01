using SQLite;

namespace SlimWaist.Models
{
    public class RegimeList
    {
        [PrimaryKey, AutoIncrement]
        public int RegimeId { get; set; }

        public int MembershipId { get; set; }

        public string? RegimeName { get; set; }

        public string? Datestart { get; set; }

        public string? DateEnd { get; set; }

        public string? Weight { get; set; }
    }
}
