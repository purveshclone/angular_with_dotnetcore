using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Testfirst.API.Helpers;
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

        public async Task<PageList<Users>> GetUsers(UserParams userparams)
        {
            var users = _context.Users.Include(p => p.Photos).OrderByDescending(u => u.LastActive).AsQueryable();
            users = users.Where(u => u.Id != userparams.UserId);
            users = users.Where(u => u.Gender == userparams.Gender);
            if (userparams.Likers)
            {
                var userLikers = await GetUserLikes(userparams.UserId, userparams.Likers);
                users = users.Where(u => userLikers.Contains(u.Id));
            }
            if (userparams.Likees)
            {
                var userLikees = await GetUserLikes(userparams.UserId, userparams.Likers);
                users = users.Where(u => userLikees.Contains(u.Id));
            }

            if (userparams.MinAge != 18 || userparams.MaxAge != 99)
            {
                var minDob = DateTime.Today.AddYears(-userparams.MaxAge - 1);
                var maxDob = DateTime.Today.AddYears(-userparams.MinAge);
                users = users.Where(u => u.DateOfBirth >= minDob && u.DateOfBirth <= maxDob);
            }
            if (!string.IsNullOrEmpty(userparams.OrderBy))
            {
                switch (userparams.OrderBy.ToLower())
                {
                    case "created":
                        users.OrderByDescending(u => u.Created);
                        break;
                    default:
                        users = users.OrderByDescending(u => u.LastActive);
                        break;
                }
            }
            users = users.Where(u => u.Gender == userparams.Gender);

            return await PageList<Users>.CreateAsync(users, userparams.PageNumber, userparams.PageSize);
        }

        private async Task<IEnumerable<int>> GetUserLikes(int id, bool likers)
        {
            var user = await _context.Users.Include(u => u.Likees).Include(u => u.Likers).FirstOrDefaultAsync(u => u.Id == id);
            if (likers)
            {
                return _context.Likes.Where(l => l.LikeeId == id).Select(i => i.LikerId);
            }
            else
            {
                return _context.Likes.Where(l => l.LikerId == id).Select(i => i.LikeeId);
            }
        }
        public async Task<Users> GetUser(int Id)
        {
            var user = await _context.Users.Include(p => p.Photos).FirstOrDefaultAsync(u => u.Id == Id);
            return user;
        }

        public async Task<bool> SaveAll()
        {
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<Photo> GetPhoto(int id)
        {
            var photo = await _context.Photo.FirstOrDefaultAsync(u => u.PhotoId == id);
            return photo;
        }

        public async Task<Photo> GetMainPhotoForUser(int userId)
        {
            var photo = await _context.Photo.Where(p => p.UsersId == userId).FirstOrDefaultAsync(u => u.IsMain);
            return photo;
        }

        public async Task<Like> GetLike(int userId, int recipientId)
        {
            return await _context.Likes.FirstOrDefaultAsync(a =>
                 a.LikerId == userId && a.LikeeId == recipientId);
        }
    }
}