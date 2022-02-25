using ApiProject.Data;
using ApiProject.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProjectMember = ApiProject.Data.ProjectMember;

namespace ApiProject.Controllers
{
    [Route("api/")]
    [Produces("application/json")]
    [ApiController]
    public class ProjectMemberController : ControllerBase
    {
        private ApiProjectDbContext _dbContext;

        public ProjectMemberController(ApiProjectDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        [Authorize]
        /// <summary>
        /// Get memberships by user
        /// </summary>
        /// <param name="member"></param>
        /// <returns>User memberships</returns>
        [HttpGet("member/{UserId}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult GetProjectUsers([FromRoute] int UserId)
        {
            try
            {
                var memberships = _dbContext.ProjectMembers.Where(x => x.UserId == UserId).ToList();
                if (memberships.Count == 0)
                {
                    return StatusCode(404, "User has not been associated with any project");
                }
                else
                {
                    return StatusCode(200, memberships);
                }
            }
            catch (Exception)
            {
                return StatusCode(500, "Unexpected system error");
            }
        }

        [Authorize]
        /// <summary>
        /// Get all memberships
        /// </summary>
        [HttpGet("member")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]

        public IActionResult Get()
        {
            try
            {
                var projects = _dbContext.ProjectMembers.ToList();
                if (projects.Count == 0)
                {
                    return StatusCode(404, "No members not found");
                }
                else
                {
                    return StatusCode(200, projects);
                }
            }
            catch (Exception)
            {
                return StatusCode(500, "Unexpected system error");
            }
        }

        [Authorize]
        /// <summary>
        /// Add membership for user in a specific project
        /// </summary>
        [HttpPost("member")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult Create([FromBody] ProjectMemberRequest request)
        {
            ProjectMember projectMember = new ProjectMember();
            projectMember.ProjectId = request.ProjectId;
            projectMember.UserId = request.UserId;
            projectMember.RoleName = request.RoleName;

            try
            {
                _dbContext.ProjectMembers.Add(projectMember);
                _dbContext.SaveChanges();
                StatusCode(201, "Member added to project succesfully");
            }
            catch (Exception)
            {
                return StatusCode(500, "Unexpected error");
            }

            var members = _dbContext.ProjectMembers.ToList();
            return Ok(members);
        }

        [Authorize]
        /// <summary>
        /// Update existing membership
        /// </summary>
        [HttpPut("member")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult Update([FromBody] ProjectMemberRequest request)
        {
            try
            {
                var membership = _dbContext.ProjectMembers.FirstOrDefault(x => x.ProjectId == request.ProjectId && x.UserId == request.UserId);
                if (membership == null)
                {
                    return StatusCode(404, "Membership not found");
                }

                membership.RoleName = request.RoleName;

                _dbContext.Entry(membership).State = EntityState.Modified;
                _dbContext.SaveChanges();
            }
            catch (Exception)
            {
                return StatusCode(500, "Unexpected error");
            }

            var members = _dbContext.ProjectMembers.ToList();
            return Ok(members);
        }

        [Authorize]
        /// <summary>
        /// Delete membership by user and project related to it
        /// </summary>
        [HttpDelete("member/{UserId}/Project/{ProjectId}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult Delete([FromRoute] int UserId, [FromRoute] int ProjectId)
        {
            try
            {
                var membership = _dbContext.ProjectMembers.FirstOrDefault(x => x.ProjectId == ProjectId && x.UserId == UserId);
                if (membership == null)
                {
                    return StatusCode(404, "Membership not found");
                }
                _dbContext.Entry(membership).State = EntityState.Deleted;
                _dbContext.SaveChanges();
            }
            catch (Exception)
            {
                return StatusCode(500, "Unexpected error");
            }

            var members = _dbContext.ProjectMembers.ToList();
            return Ok(members);
        }
    }
}
