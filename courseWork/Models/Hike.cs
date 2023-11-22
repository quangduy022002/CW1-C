using SQLite;
using System;
namespace courseWork.Models
{
    public class Hike
    {
        public Hike ()
        {
            Date = DateTime.Now;
        }

        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        public string Title { get; set; }
        public string Location { get; set; }
        public DateTime Date { get; set; }
        public bool IsPacking { get; set; }
        public double Length { get; set; }
        public string Level { get; set; }
        public string Description { get; set; }

        public Hike Clone() => MemberwiseClone() as Hike;

        public (bool IsValid, string? ErrorMessage) Validate()
        {
            if (string.IsNullOrWhiteSpace(Title))
            {
                return (false, $"{nameof(Title)} is required.");
            }
            else if (string.IsNullOrWhiteSpace(Location))
            {
                return (false, $"{nameof(Location)} is required.");
            }
            else if (Length <= 0)
            {
                return (false, $"{nameof(Length)} should be greater than 0.");
            }
            else if (Date == DateTime.MinValue)
            {
                return (false, $"{nameof(Date)} is required.");
            }
            else if (string.IsNullOrWhiteSpace(Level))
            {
                return (false, $"{nameof(Level)} is required.");
            }
         
            return (true, null);

        }
    }
}
