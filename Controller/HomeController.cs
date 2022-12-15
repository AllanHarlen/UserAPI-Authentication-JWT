using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace UsuarioAPI.Controller
{
    [ApiController]
    [Route("[controller]")]
    public class HomeController : ControllerBase
    {
        [HttpGet("anonymous")]
        [AllowAnonymous]
        public string Anonymous() => "Anonymous";

        [HttpGet("master")]
        [Authorize(Roles = "Master")]
        public string Master() => "Master";

        [HttpGet("authenticated")]
        [Authorize]
        public string Authenticated() => $"Autenticado: " + User.Identity.Name;
    }
}
