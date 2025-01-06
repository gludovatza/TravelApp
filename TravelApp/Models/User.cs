using System.ComponentModel.DataAnnotations;

namespace TravelApp.Models
{
    public class User
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "The name is required.")]
        [StringLength(50, ErrorMessage = "The name must be less than 50 characters.")]
        public string Name { get; set; }

        public ICollection<UserTrip>? UserTips { get; set; }
    }
}
