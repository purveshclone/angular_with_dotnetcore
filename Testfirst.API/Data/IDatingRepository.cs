using System.Collections.Generic;
using System.Threading.Tasks;
using Testfirst.API.Helpers;
using Testfirst.API.Models;

namespace Testfirst.API.Data
{
    public interface IDatingRepository
    {
         void Add<T>(T entity) where T:class;
         void delete<T>(T entity) where T:class;
        Task<bool> SaveAll(); 
        Task<PageList<Users>> GetUsers(UserParams userParams);
        Task<Users> GetUser(int Id);
        Task<Photo> GetPhoto(int id);
        Task<Photo> GetMainPhotoForUser(int userId);
        Task<Like> GetLike(int userId, int recipientId);

    }
}