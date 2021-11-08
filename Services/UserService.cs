using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TaimeApi.Models;

namespace TaimeApi.Services
{
    public class UserService
    {
        private readonly Context _context;
        public UserService([FromServices] Context context)
        {
            _context = context;
        }

        public async Task<List<UserModel>> GetAll()
        {
            return await _context.Users.Include(x=> x.AddressList).ToListAsync();
        }

        public async Task<UserModel> GetById(int id)
        {
            return await _context.Users.Include(x=> x.AddressList)
                .SingleOrDefaultAsync(x=> x.Id == id);
        }

        public async Task<UserModel> Create(UserModel request)
        {
            _context.Users.Add(request);
            request.AddressList.ForEach(x=> {
                _context.Addresses.Add(x);
            });
            await _context.SaveChangesAsync();
            return request;
        }
    }
}