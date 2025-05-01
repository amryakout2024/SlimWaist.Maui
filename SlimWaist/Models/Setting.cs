using SQLite;

namespace SlimWaist.Models
{
    public class Setting
    {
        [PrimaryKey]
        public int Id { get; set; }

        public int CurrentMemberShipId { get; set; } = 0;

        public int SavedMemberShipId { get; set; } = 0;

        public string? CultureInfo { get; set; } = "ar-SA";

    }
}
