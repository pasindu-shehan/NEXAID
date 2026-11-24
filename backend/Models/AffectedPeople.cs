using System.ComponentModel.DataAnnotations;

namespace backend.Models
{
    public class AffectedPeople
    {
        public int Id { get; set; }
        public string Name { get; set; }


      
        public int Age { get; set; }
        public string Gender { get; set; } = string.Empty;
        public string? ContactNumber { get; set; }
        public string? Email { get; set; }
        public string Address { get; set; } = string.Empty;
        public string Country { get; set; }
        //public double? Longitude { get; set; }
        public string? NeedsDescription { get; set; }
        //public UrgencyLevel Urgency { get; set; } = UrgencyLevel.Medium;
        //[Required] public int CrisisId { get; set; }
    }
}
