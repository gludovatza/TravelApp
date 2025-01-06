using System.ComponentModel.DataAnnotations;

namespace TravelApp.Models
{
    public class Trip
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "The destination is required.")]
        [StringLength(100, ErrorMessage = "The destination must be less than 100 characters")]
        public string Destination { get; set; }

        [Required(ErrorMessage = "The description is required.")]
        [StringLength(500, ErrorMessage = "The description must be less than 500 characters")]
        public string Description { get; set; }

        public ICollection<UserTrip>? UserTips { get; set; }
    }
}
