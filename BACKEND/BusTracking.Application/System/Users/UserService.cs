using BusTracking.Data.Entities;
using BusTracking.ViewModels.System.Users;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using System.Threading.Tasks;
using System.Linq;
using BusTracking.Data.Enum;
using System.Collections.Generic;
using BusTracking.Data.EF;
using Microsoft.EntityFrameworkCore;

namespace BusTracking.Application.System.Users
{
    public class UserService : IUserService
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;
        private readonly BusTrackingDbContext _context;
        
        public UserService(BusTrackingDbContext dbContext,UserManager<AppUser> userManager, SignInManager<AppUser> signInManager, RoleManager<AppRole> roleManager, IConfiguration config)
        {
            this._userManager = userManager;
            this._signInManager = signInManager;
            this._context = dbContext;
        }

        public async Task<List<UserDto>> GetAllMonitorUnAssignAsync()
        {
            var query = from m in _context.AppUsers
                        where m.TypeAccount == TypeAccount.MonitorAcc
                        join b in _context.Buses on m.Id equals b.MonitorId into gb
                        from sub in gb.DefaultIfEmpty()
                        where sub.DriverId == null
                        select m;

            var monitor = await query.Select(x => new UserDto()
            {
                Id = x.Id,
                UserName = x.UserName,
                FullName = x.FullName,
                Dob = x.Dob,
                Email = x.Email,
                TypeAccount = (int)x.TypeAccount,
                PhoneNumber = x.PhoneNumber,
                Status = (int)x.Status
            }).ToListAsync();
            return monitor;
        }

        public async Task<List<UserDto>> GetAllByType(int type)
        {
            var query = from x in _context.AppUsers
                        where x.TypeAccount == (TypeAccount)type
                        select x;
            var parents = await query.Select(x => new UserDto()
            {
                Id = x.Id,
                Dob = x.Dob,
                TypeAccount = (int)x.TypeAccount,
                Email = x.Email,
                FullName = x.FullName,
                PhoneNumber = x.PhoneNumber,
                Status = (int)x.Status,
                UserName = x.UserName
            }).ToListAsync();
            return parents;
        }

        public async Task<bool> CreateUser(CreateUserRequestDto request)
        {
            var user = new AppUser()
            {
                FullName = request.FullName,
                Email = request.Email,
                PhoneNumber = request.PhoneNumber,
                UserName = request.UserName,
                TypeAccount = (TypeAccount)request.TypeAccount,
                Status = (Status)request.Status,

            };
            var result = await _userManager.CreateAsync(user, request.Password);
            if (result.Succeeded) return true;
            return false;
        }
    }
}
