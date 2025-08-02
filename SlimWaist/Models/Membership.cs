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

        public DateTime WeightDate { get; set; }

        //public int WeightDateDay { get; set; }

        //public int WeightDateMonth { get; set; }

        //public int WeightDateYear { get; set; }

        public DateTime BirthDate { get; set; }

        //public int BirthDateDay { get; set; }

        //public int BirthDateMonth { get; set; }

        //public int BirthDateYear { get; set; }

        public int BodyActivityId { get; set; }

        //0 means male ---- 1 means female
        public int GenderId { get; set; }

        public double WaistCircumferenceMeasurement { get; set; }

        public string? CultureInfo { get; set; } = "en-US";

        public bool IsExistsInDb { get; set; } = false;

    }

}
