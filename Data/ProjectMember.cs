using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ApiProject.Data
{
    public class ProjectMember
    {
        [Column(Order = 0)]
        public int ProjectId { get; set; }

        public int UserId { get; set; }

        public String? RoleName { get; set; }
    }
}
