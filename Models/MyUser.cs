using System;
using System.Collections.Generic;

namespace UsuarioAPI.Models;

public partial class MyUser
{
    public int Id { get; set; }

    public string UserName { get; set; } = null!;

    public string Password { get; set; } = null!;

    public string Role { get; set; } = null!;

    public string Empresa { get; set; } = null!;

    // Método para criação de usuário
    public static MyUser CreateUser(string userName, string password, string role, string empresa)
    {
        return new MyUser
        {
            UserName = userName,
            Password = password,
            Role = role,
            Empresa = empresa
        };
    }
}
