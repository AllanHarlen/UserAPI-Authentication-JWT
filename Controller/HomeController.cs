using Microsoft.AspNetCore.Authorization;
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
    public class HomeController : ControllerBase
    {
        private readonly DataContext dc;
        public HomeController(DataContext context)
        { 
            this.dc = context;
        }

        [HttpGet("authenticated")]
        [Authorize]
        public string Authenticated() => $"Autenticado: " + User.Identity.Name;
    }
}
