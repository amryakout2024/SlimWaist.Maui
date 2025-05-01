using SQLite;

namespace SlimWaist.Models
{
    public class Membership
    {
        [PrimaryKey, AutoIncrement]

        public int Id { get; set; }

        public string? Email { get; set; }

        public string? Password { get; set; }

        public string? Name { get; set; }

        public double Height { get; set; }

        public double Weight { get; set; }

        public string? WeightDate { get; set; }

        public string? BirthDate { get; set; }

        public string? BodyActivity { get; set; }

        public string? Gender { get; set; }

        public string? BMI { get; set; }

        public string? IdealWeight { get; set; }

        public string? ModifiedWeight { get; set; }

        public string? TotalEnergy { get; set; }

        public string? CultureInfo { get; set; } = "ar-SA";
    }

}
