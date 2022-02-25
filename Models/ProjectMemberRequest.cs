using System.ComponentModel.DataAnnotations;

namespace ApiProject.Models
{
    public class ProjectMemberRequest
    {
        [Required]
        public int ProjectId { get; set; }

        [Required]
        public int UserId { get; set; }

        public String? RoleName { get; set; }
    }
}
