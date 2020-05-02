using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Testfirst.API.Models;

namespace Testfirst.API.Data
{
    public class DatingRepository : IDatingRepository
    {
        private readonly DataContext _context;

        public DatingRepository(DataContext context)
        {
            _context = context;
        }
        public void Add<T>(T entity) where T : class
        {
            _context.Add(entity);
        }

        public void delete<T>(T entity) where T : class
        {
            _context.Remove(entity);
        }

        public async Task<IEnumerable<Users>> GetUsers()
        {
            var users = await _context.Users.Include(p=>p.Photos).ToListAsync();
            return users;
        }

        public async Task<Users> GetUser(int Id)
        {
            var user = await _context.Users.Include(p=>p.Photos).FirstOrDefaultAsync(u=>u.Id==Id);
            return user;
        }

        public async Task<bool> SaveAll()
        {
           return await _context.SaveChangesAsync()>0;
        }

        public async Task<Photo> GetPhoto(int id)
        {
           var photo = await _context.Photo.FirstOrDefaultAsync(u=>u.PhotoId==id);
            return photo;
        }

        public async Task<Photo> GetMainPhotoForUser(int userId)
        {
           var photo = await _context.Photo.Where(p=>p.UsersId==userId).FirstOrDefaultAsync(u=>u.IsMain);
            return photo;
        }
    }
}