using backend.Services;
using Google.Apis.Auth;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace backend.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        public class AuthenticateRequest
        {
            [Required]
            public string IdToken { get; set; }
        }

        private readonly JwtGenerator _jwtGenerator;

        public UserController(IConfiguration configuration)
        {
            _jwtGenerator = new JwtGenerator(configuration["JwtPrivateSigningKey"]);
        }

        [AllowAnonymous]
        [HttpPost("authenticate")]
        public IActionResult Authenticate([FromBody] AuthenticateRequest data)
        {
            GoogleJsonWebSignature.ValidationSettings settings = new GoogleJsonWebSignature.ValidationSettings();
            settings.Audience = new List<string> { "950688783434-unpse286sph97v9mvjo1djgmn6k5ajpb.apps.googleusercontent.com" };
            GoogleJsonWebSignature.Payload payload = GoogleJsonWebSignature.ValidateAsync(data.IdToken, settings).Result;

            return Ok(new { AuthToken = _jwtGenerator.CreateUserAuthToken(payload.Email)});

        }
    }
}
