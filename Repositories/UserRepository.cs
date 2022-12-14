using UsuarioAPI.Models;
using UsuarioAPI.ObjectReverse;

namespace UsuarioAPI.Repositories
{
    public class UserRepository
    {
        public MyUser Get()
        {
            using (var context = new DataContext())
            {
                var user = context.MyUsers.ToList().FirstOrDefault();
                return user;
            }
        }
    }
}
