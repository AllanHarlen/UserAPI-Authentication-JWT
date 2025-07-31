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
        public async Task<ActionResult<dynamic>> AuthenticateUser([FromBody] LoginModel login)
        {
            var usuario = await dc.MyUsers.Where(x => x.UserName == login.UserName && x.Password == login.Password).FirstOrDefaultAsync();
            if (usuario == null)            
                return NotFound(new { message = "Usuário ou Senha invalidos!" });
            
            Settings.Settings configuracoes = new Settings.Settings();
            var token = configuracoes.GenerateToken(usuario);

            return new { user = usuario, token = token, data = DateTime.UtcNow};
        }

        [HttpPost("create")]
        public async Task<ActionResult<MyUser>> CreateUser([FromBody] LoginModel login)
        {
            if (string.IsNullOrWhiteSpace(login.UserName) || string.IsNullOrWhiteSpace(login.Password))
                return BadRequest(new { message = "UserName e Password são obrigatórios." });

            var exists = await dc.MyUsers.AnyAsync(x => x.UserName == login.UserName);
            if (exists)
                return Conflict(new { message = "Usuário já existe." });

            var newUser = MyUser.CreateUser(login.UserName, login.Password, string.Empty, string.Empty);
            dc.MyUsers.Add(newUser);
            await dc.SaveChangesAsync();
            return CreatedAtAction(nameof(CreateUser), new { id = newUser.Id }, newUser);
        }
    }
}
