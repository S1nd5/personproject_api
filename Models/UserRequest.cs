using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace ApiProject.Models
{
    public class UserRequest
    {
        [Required]
        public int Id { get; set; }

        [Required]
        public String? UserName { get; set; }

        [Required]
        public String? Password { get; set; }

        [Required]
        public String? FirstName { get; set; }

        [Required]
        public String? LastName { get; set; }

        [Required]
        public String? Occupation { get; set; }

        [Required]
        public decimal? Salary { get; set; }

        [Required]
        public String? Country { get; set; }

        [Required]
        public String? City { get; set; }
    }
}
