using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace ApiProject.Data
{
    public class User
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        public String? UserName { get; set; }

        [JsonIgnore]
        public String? Password { get; set; }

        public String? FirstName { get; set; }

        public String? LastName { get; set; }

        public String? Occupation { get; set; }

        public decimal? Salary { get; set; }

        public String? Country { get; set; }

        public String? City { get; set; }
    }
}
