using System.ComponentModel.DataAnnotations;

namespace ApiProject.Models
{
    public class ProjectRequest
    {
        [Required]
        public int ProjectId { get; set; }

        [Required]
        public String? ProjectName { get; set; }
    }
}
