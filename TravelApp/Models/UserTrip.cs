using System.ComponentModel.DataAnnotations;

namespace TravelApp.Models
{
    public class UserTrip
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "The User Id is required.")]
        public int UserId { get; set; }
        
        [Required(ErrorMessage = "The Trip Id is required.")]
        public int TripId { get; set; }
        
        [Required(ErrorMessage = "The date is required.")]
        [DataType(DataType.Date, ErrorMessage = "The date must be a valid date.")]
        public DateTime Date { get; set; }

        public User? User { get; set; }

        public Trip? Trip { get; set; }
    }
}
