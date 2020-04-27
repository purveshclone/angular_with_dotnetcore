using System.Collections.Generic;
using System.Threading.Tasks;
using Testfirst.API.Models;

namespace Testfirst.API.Data
{
    public interface IDatingRepository
    {
         void Add<T>(T entity) where T:class;
         void delete<T>(T entity) where T:class;
        Task<bool> SaveAll(); 
        Task<IEnumerable<Users>> GetUsers();
        Task<Users> GetUser(int Id);

    }
}