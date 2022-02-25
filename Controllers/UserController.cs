using ApiProject.Data;
using ApiProject.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using User = ApiProject.Data.User;

namespace ApiProject.Controllers
{
    [Route("api/")]
    [Produces("application/json")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private ApiProjectDbContext _dbContext;

        public UserController(ApiProjectDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        /*[Authorize]
        [HttpGet("test")]
        public IActionResult Test()
        {
            string user = HttpContext.User.Identity.Name;
            return StatusCode(200, "Authorized with: " + user);
        }*/

        /// <response code="200">Returns all users</response>
        /// <response code="404">Users not found</response>
        [HttpGet("user")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]

        public IActionResult Get()
        {
            try
            {
                var users = _dbContext.Users.ToList();
                if (users.Count == 0)
                {
                    return StatusCode(404, "Users not found");
                }
                else
                {
                    return StatusCode(200, users);
                }
            }
            catch ( Exception )
            {
                return StatusCode(500, "Unexpected system error");
            }
        }

        [HttpPost("user")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult Create([FromBody] UserRequest request)
        {
            User user = new User();
            user.UserName = request.UserName;
            user.Password = BCrypt.Net.BCrypt.HashPassword(request.Password);
            user.FirstName = request.FirstName;
            user.LastName = request.LastName;
            user.Occupation = request.Occupation;
            user.Salary = request.Salary;
            user.Country = request.Country;
            user.City = request.City;

            try
            {
                _dbContext.Users.Add(user);
                _dbContext.SaveChanges();
                StatusCode(201, "User created succesfully");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                return StatusCode(500, "Unexpected error: " + ex.ToString());
            }

            var users = _dbContext.Users.ToList();
            return Ok(users);
        }

        [HttpPut("user")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult Update([FromBody] UserRequest request )
        {
            try
            {
                var user = _dbContext.Users.FirstOrDefault(x => x.Id == request.Id);
                if ( user == null )
                {
                    return StatusCode(404, "User not found");
                }

                user.UserName = request.UserName;
                user.FirstName = request.FirstName;
                user.LastName = request.LastName;
                user.Occupation = request.Occupation;
                user.Salary = request.Salary;
                user.Country = request.Country;
                user.City = request.City;

                _dbContext.Entry(user).State = EntityState.Modified;
                _dbContext.SaveChanges();
            }
            catch (Exception ex)
            {

                Console.WriteLine(ex);
                return StatusCode(500, "Unexpected error");
            }

            var users = _dbContext.Users.ToList();
            return Ok(users);
        }

        [HttpDelete("user/{UserId}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult Delete([FromRoute] int UserId)
        {
            try
            {
                var user = _dbContext.Users.FirstOrDefault(x => x.Id == UserId);
                if (user == null)
                {
                    return StatusCode(404, "User not found");
                }
                _dbContext.Entry(user).State = EntityState.Deleted;
                _dbContext.SaveChanges();
            }
            catch (Exception ex)
            {

                Console.WriteLine(ex);
                return StatusCode(500, "Unexpected error");
            }

            var users = _dbContext.Users.ToList();
            return Ok(users);
        }
    }
}
