using SQLite;

namespace SlimWaist.Models
{
    public class Setting
    {
        [PrimaryKey]
        public int Id { get; set; }

        public int SavedMembershipId { get; set; } = 0;

        public string? CultureInfo { get; set; } = "en-US";

    }
}
