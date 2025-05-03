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

        public int WeightDateDay { get; set; }

        public int WeightDateMounth { get; set; }

        public int WeightDateYear { get; set; }

        public int BirthDateDay { get; set; }

        public int BirthDateMounth { get; set; }

        public int BirthDateYear { get; set; }

        public string? BodyActivity { get; set; }

        public string? Gender { get; set; }

        public string? BMI { get; set; }

        public string? IdealWeight { get; set; }

        public string? ModifiedWeight { get; set; }

        public string? TotalEnergy { get; set; }

        public int ObesityDegreeId { get; set; }

        public double WaistCircumferenceMeasurement { get; set; }

        public int WaistCircumferenceId { get; set; }

        public string? CultureInfo { get; set; } = "ar-SA";
    }

}
