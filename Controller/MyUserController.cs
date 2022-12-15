using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using UsuarioAPI.Models;
using UsuarioAPI.ObjectReverse;
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
            var usuario = await dc.MyUsers.Where(x => x.UserName == user.UserName && x.Password == user.Password).FirstOrDefaultAsync();

            if (usuario == null)
            {
                return NotFound(new { message = "Usuário ou Senha invalidos!" });
            }

            Settings.Settings configuracoes = new Settings.Settings();
            var token = configuracoes.GenerateToken(user);

            return new { user = user, token = token, data = DateTime.UtcNow};
        }


    }
}
