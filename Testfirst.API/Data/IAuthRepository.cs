using System.Threading.Tasks;
using Testfirst.API.Models;

namespace Testfirst.API.Data
{
    public interface IAuthRepository
    {
         Task<Users> Register(Users user, string password);
         Task<Users> Login(string username, string password);
         Task<bool> UserExist(string username);
    }
}