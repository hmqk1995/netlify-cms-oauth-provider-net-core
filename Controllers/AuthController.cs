using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;

namespace netlify_cms_oauth_provider_aspnet_core.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        public AuthController (IConfiguration configuration)
        {

        }

        [HttpGet("github")]
        public IActionResult GithubLogin()
        {
            return Challenge(new AuthenticationProperties{ RedirectUri = "/auth/github/callback" }, "Github");
        }

        [HttpGet("github/callback")]
        public IActionResult GithubSuccess()
        {
            return new EmptyResult();
        }
    }
}
