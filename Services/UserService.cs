using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TaimeApi.Enums;
using TaimeApi.Models;

namespace TaimeApi.Services
{
    public class UserService : BaseService
    {
        private readonly Context _context;
        public UserService([FromServices] Context context)
        {
            _context = context;
        }

        public async Task<ResultData> GetAll()
        {
            return SuccessData(await _context.Users.Include(x=> x.AddressList).ToListAsync());
        }

        public async Task<ResultData> GetById(int id)
        {
            if(id < 1)
                return ErrorData(TaimeApiErrors.TaimeApi_Post_400_Invalid_Id);
                
            return SuccessData(await _context.Users.Include(x=> x.AddressList)
                .SingleOrDefaultAsync(x=> x.Id == id));
        }

        public async Task<ResultData> Create(UserModel request)
        {
            _context.Users.Add(request);
            request.AddressList.ForEach(x=> {
                _context.Addresses.Add(x);
            });
            await _context.SaveChangesAsync();
            return SuccessData(request);
        }
    }
}