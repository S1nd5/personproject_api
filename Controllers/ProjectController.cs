using ApiProject.Data;
using ApiProject.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Project = ApiProject.Data.Project;

namespace ApiProject.Controllers
{
    [Route("api/")]
    [Produces("application/json")]
    [ApiController]
    public class ProjectController : ControllerBase
    {
        private ApiProjectDbContext _dbContext;

        public ProjectController(ApiProjectDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        [Authorize]
        [HttpGet("project/{Id}/users")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult GetProjectUsers([FromRoute] int Id)
        {
            try
            {
                var projectMembers = _dbContext.ProjectMembers.Where(x => x.ProjectId == Id).ToList();
                if (projectMembers.Count == 0)
                {
                    return StatusCode(404, "Project not found or there is no members in the requested project");
                }
                else
                {
                    return StatusCode(200, projectMembers);
                }
            }
            catch (Exception)
            {
                return StatusCode(500, "Unexpected system error");
            }
        }

        [Authorize]
        [HttpGet("project")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]

        public IActionResult Get()
        {
            try
            {
                var projects = _dbContext.Projects.ToList();
                if (projects.Count == 0)
                {
                    return StatusCode(404, "Projects not found");
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
        [HttpPost("project")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult Create([FromBody] ProjectRequest request)
        {
            Project project = new Project();
            project.ProjectName = request.ProjectName;

            try
            {
                _dbContext.Projects.Add(project);
                _dbContext.SaveChanges();
                StatusCode(201, "Project created succesfully");
            }
            catch (Exception)
            {
                return StatusCode(500, "Unexpected error");
            }

            var projects = _dbContext.Projects.ToList();
            return Ok(projects);
        }

        [Authorize]
        [HttpPut("project")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult Update([FromBody] ProjectRequest request)
        {
            try
            {
                var project = _dbContext.Projects.FirstOrDefault(x => x.ProjectId == request.ProjectId);
                if (project == null)
                {
                    return StatusCode(404, "Project not found");
                }

                project.ProjectName = request.ProjectName;

                _dbContext.Entry(project).State = EntityState.Modified;
                _dbContext.SaveChanges();
            }
            catch (Exception)
            {
                return StatusCode(500, "Unexpected error");
            }

            var projects = _dbContext.Projects.ToList();
            return Ok(projects);
        }

        [Authorize]
        [HttpDelete("project/{ProjectId}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult Delete([FromRoute] int ProjectId)
        {
            try
            {
                var project = _dbContext.Projects.FirstOrDefault(x => x.ProjectId == ProjectId);
                if (project == null)
                {
                    return StatusCode(404, "Project not found");
                }
                _dbContext.Entry(project).State = EntityState.Deleted;
                _dbContext.SaveChanges();
            }
            catch (Exception)
            {
                return StatusCode(500, "Unexpected error");
            }

            var projects = _dbContext.Projects.ToList();
            return Ok(projects);
        }
    }
}
