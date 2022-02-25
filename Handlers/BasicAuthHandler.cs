using Microsoft.AspNetCore.Authentication;
using Microsoft.Extensions.Options;
using System.Text.Encodings.Web;
using System.Net.Http.Headers;
using System.Text;
using ApiProject.Data;
using System.Security.Claims;

namespace ApiProject.Handlers
{
    public class BasicAuthHandler : AuthenticationHandler<AuthenticationSchemeOptions>
    {
        private readonly ApiProjectDbContext _dbContext;
        public BasicAuthHandler(
            IOptionsMonitor<AuthenticationSchemeOptions> options,
            ILoggerFactory logger,
            UrlEncoder encoder,
            ISystemClock clock,
            ApiProjectDbContext context)
            : base(options,logger, encoder, clock)
            {
            _dbContext = context;
            }
#pragma warning disable CS1998 // Async method lacks 'await' operators and will run synchronously
        protected override async Task<AuthenticateResult> HandleAuthenticateAsync()
#pragma warning restore CS1998 // Async method lacks 'await' operators and will run synchronously
        {
            if (!Request.Headers.ContainsKey("Authorization"))
                return AuthenticateResult.Fail("No authorization header provided");

            try
            {
                var authenticationHeaderValue = AuthenticationHeaderValue.Parse(Request.Headers["Authorization"]);
                if ( authenticationHeaderValue == null )
                {
                    return AuthenticateResult.Fail("No authorization header value provided");
                }
                byte[]? bytes = Convert.FromBase64String(authenticationHeaderValue.Parameter);
                string[] credentials = Encoding.UTF8.GetString(bytes).Split(":");
                string email = credentials[0];
                string password = credentials[1];

                User? user = _dbContext.Users.Where(user => user.UserName == email).FirstOrDefault();

                if (user == null )
                {
                    return AuthenticateResult.Fail("Invalid authentication data (username,password)");

                } else
                {
                    bool verifyPassword = BCrypt.Net.BCrypt.Verify(password, user.Password);

                    if ( verifyPassword )
                    {
                        Claim[]? claims = new[] { new Claim(ClaimTypes.Name, user.UserName) };
                        var identity = new ClaimsIdentity(claims, Scheme.Name);
                        var principal = new ClaimsPrincipal(identity);
                        var ticket = new AuthenticationTicket(principal, Scheme.Name);

                        return AuthenticateResult.Success(ticket);

                    } else
                    {
                        return AuthenticateResult.Fail("Invalid username or password");
                    }
                }
            }
            catch ( Exception ex )
            {
                return AuthenticateResult.Fail("Error has occured during authentication: " + ex);
            }
        }
    }
}
