using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using UsuarioAPI.Models;
using UsuarioAPI.ObjectReverse;
using UsuarioAPI.Repositories;
using UsuarioAPI.Settings;

namespace UsuarioAPI.Controller
{
    [ApiController]
    [Route("[controller]")]
    public class MyUserController : ControllerBase
    {
        private readonly DataContext dc;
        public MyUserController(DataContext context)
        { 
            this.dc = context;
        }

        [HttpPost("login")]
        public async Task<ActionResult<dynamic>> AuthenticateUser([FromBody] MyUser user)
        {
            var usuario = await dc.MyUsers.Where(x => x.UserName == user.UserName && x.Password == user.Password).ToListAsync();

            if (usuario == null || usuario.Count == 0)
            {
                return NotFound(new { message = "Usuário ou Senha invalidos!" });
            }

            var token = Settings.Settings.GenerateToken(user);

            return new { user = user, token = token };
        }
    }
}
