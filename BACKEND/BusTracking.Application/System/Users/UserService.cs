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
using BusTracking.ViewModels.Common;
using BusTracking.Utilities.Constants;
using System;

namespace BusTracking.Application.System.Users
{
    public class UserService : IUserService
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly RoleManager<AppRole> _roleManager;
        private readonly SignInManager<AppUser> _signInManager;
        private readonly BusTrackingDbContext _context;
        
        public UserService(
            BusTrackingDbContext dbContext,
            UserManager<AppUser> userManager,
            RoleManager<AppRole> roleManager,
            SignInManager<AppUser> signInManager,
            IConfiguration config)
        {
            this._userManager = userManager;
            this._roleManager = roleManager;
            this._signInManager = signInManager;
            this._context = dbContext;
        }

        public async Task<List<UserDto>> GetAllMonitorUnAssignAsync()
        {
            var query = from m in _context.AppUsers
                        where m.TypeAccount == TypeAccount.MonitorAcc && m.IsDeleted == false
                        join b in _context.Buses on m.Id equals b.MonitorId into gb
                        from sub in gb.DefaultIfEmpty()
                        where sub.MonitorId == null
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
                        where x.IsDeleted == false
                        select x;
            var users = await query.Select(x => new UserDto()
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
            return users;
        }

        public async Task<PageResultDto<UserDto>> GetAllPaging(GetUserPagingRequestDto request)
        {
            var query = _userManager.Users.Where(x=>x.IsDeleted==false).AsQueryable();
            if (!string.IsNullOrEmpty(request.UserName))
            {
                query = query.Where(x => x.UserName.Contains(request.UserName));
            }
            if (!string.IsNullOrEmpty(request.FullName))
            {
                query = query.Where(x => x.FullName.Contains(request.FullName));
            }
            if (!string.IsNullOrEmpty(request.PhoneNumber))
            {
                query = query.Where(x => x.PhoneNumber.Contains(request.PhoneNumber));
            }
            if (request.TypeAccount>=0)
            {
                query = query.Where(x => x.TypeAccount == (TypeAccount)request.TypeAccount);
            }
            //3. Paging
            int totalRow = await query.CountAsync();
            var data = await query.Skip((request.PageIndex - 1) * request.PageSize)
                .Take(request.PageSize)
                .Select(x => new UserDto()
                {
                    Email = x.Email,
                    PhoneNumber = x.PhoneNumber,
                    UserName = x.UserName,
                    Id = x.Id,
                    TypeAccount = (int)x.TypeAccount,
                    FullName = x.FullName,
                    Dob = x.Dob,
                    Status = (int)x.Status,
                    Roles = null
                }).ToListAsync();

            //4. Select and projection
            var pagedResult = new PageResultDto<UserDto>()
            {
                StatusCode = ResponseCode.Success,
                Message = "Thành công",
                TotalRecord = totalRow,
                Items = data
            };
            return pagedResult;
        }

        public async Task<ResultDto<UserDto>> GetById(Guid id)
        {
            var user = await _userManager.FindByIdAsync(id.ToString());
            if (user == null)
            {
                return new ResultDto<UserDto>(ResponseCode.Validate,"User không tồn tại",null);
            }
            var roles = await _userManager.GetRolesAsync(user);
            var userDto = new UserDto()
            {
                Email = user.Email,
                PhoneNumber = user.PhoneNumber,
                UserName = user.UserName,
                Id = user.Id,
                TypeAccount = (int)user.TypeAccount,
                FullName = user.FullName,
                Dob = user.Dob,
                Status = (int)user.Status,
                Roles = roles
            };
            return new ResultDto<UserDto>(ResponseCode.Success,"Thành công",userDto);
        }

        public async Task<ResponseDto> Create(CreateUserRequestDto request)
        {
            if (await _userManager.FindByNameAsync(request.UserName) != null)
            {
                return new ResponseDto(ResponseCode.Validate,"Tài khoản đã tồn tại");
            }
            if (await _userManager.FindByEmailAsync(request.Email) != null)
            {
                return new ResponseDto(ResponseCode.Validate, "Email đã tồn tại");
            }
            var createUser = new AppUser()
            {
                FullName = request.FullName,
                Email = request.Email,
                PhoneNumber = request.PhoneNumber,
                Dob = request.Dob,
                UserName = request.UserName,
                TypeAccount = (TypeAccount)request.TypeAccount,
                Status = (Status)request.Status,
            };
            var result = await _userManager.CreateAsync(createUser, request.Password);
            if (result.Succeeded)
            {
                foreach (var item in request.RolesName)
                {
                    var kq = await _userManager.AddToRoleAsync(createUser,item);
                    if (!kq.Succeeded)
                    {
                        return new ResponseDto(ResponseCode.LogicError, "Tạo mới thất bại");
                    }
                }
            }
            else
            {
                return new ResponseDto(ResponseCode.LogicError, "Tạo mới thất bại");
            }
            return new ResponseDto(ResponseCode.Success,"Tạo mới thành công");
        }

        public async Task<ResponseDto> Update(UpdateUserRequestDto request)
        {
            var user = await _userManager.FindByIdAsync(request.Id.ToString());
            if (user == null)
            {
                return new ResponseDto(ResponseCode.NotFound, "User không tồn tại");
            }
            if (await _userManager.Users.AnyAsync(x => x.Email == request.Email && x.Id != request.Id))
            {
                return new ResponseDto(ResponseCode.Validate,"Emai đã tồn tại");
            }
            
            user.FullName = request.FullName;
            user.PhoneNumber = request.PhoneNumber;
            user.TypeAccount = (TypeAccount)request.TypeAccount;
            user.Status = (Status)request.Status;
            user.Dob = request.Dob;
            user.Email = request.Email;

            var result = await _userManager.UpdateAsync(user);

            if (result.Succeeded)
            {
                foreach (var item in request.RemovedRoles)
                {
                    if (await _userManager.IsInRoleAsync(user, item) == true)
                    {
                        var kq = await _userManager.RemoveFromRoleAsync(user, item);
                        if (!kq.Succeeded)
                        {
                            return new ResponseDto(ResponseCode.LogicError, "Cập nhật thất bại do thêm role thất bại");
                        }
                    }
                }
                foreach (var roleName in request.AddedRoles)
                {
                    if (await _userManager.IsInRoleAsync(user, roleName) == false)
                    {
                        var kq = await _userManager.AddToRoleAsync(user, roleName);
                        if (!kq.Succeeded)
                        {
                            return new ResponseDto(ResponseCode.LogicError, "Cập nhật thất bại do thêm role thất bại");
                        }
                    }
                }
            }
            else
            {
                return new ResponseDto(ResponseCode.LogicError, "Cập nhật người dùng thất bại");
            }
            return new ResponseDto(ResponseCode.Success, "Cập nhật người dùng thành công");
           
        }

        public async Task<ResponseDto> Delete(Guid id)
        {
            var user = await _context.AppUsers.Where(x => x.IsDeleted == false).FirstOrDefaultAsync(x => x.Id == id);
            if (user == null)
            {
                return new ResponseDto(ResponseCode.NotFound,"Không tìm thấy đối tượng cần xóa");
            }
            user.IsDeleted = true;
            _context.AppUsers.Update(user);
            var result = await _context.SaveChangesAsync();
            if(result>0) return new ResponseDto(ResponseCode.Success, "Xóa thành công");
            return new ResponseDto(ResponseCode.LogicError, "Xóa thất bại");
        }

        public async Task<ResponseDto> AssignRoles(RoleAssignRequest request)
        {
            var user = await _userManager.Users.Where(x => x.IsDeleted == false && x.Id == request.UserId).FirstOrDefaultAsync();
            if (user == null)
            {
                return new ResponseDto(ResponseCode.Validate,"Tài khoản không tồn tại");
            }
            var removedRoles = request.Roles.Where(x => x.Selected == false).Select(x => x.Name).ToList();
            foreach (var roleName in removedRoles)
            {
                if (await _userManager.IsInRoleAsync(user, roleName) == true)
                {
                    await _userManager.RemoveFromRoleAsync(user, roleName);
                }
            }
            await _userManager.RemoveFromRolesAsync(user, removedRoles);
            var addedRoles = request.Roles.Where(x => x.Selected).Select(x => x.Name).ToList();
            foreach (var roleName in addedRoles)
            {
                if (await _userManager.IsInRoleAsync(user, roleName) == false)
                {
                    await _userManager.AddToRoleAsync(user, roleName);
                }
            }
            return new ResponseDto(ResponseCode.Validate, "Cập nhật thành công");
        }

        public async Task<ResponseDto> UpdateAccount(UpdateAccountRequestDto request)
        {
            var user = await _userManager.Users.Where(x => x.IsDeleted == false && x.Id == request.Id).FirstOrDefaultAsync();
            if (user == null)
            {
                return new ResponseDto(ResponseCode.Validate, "Tài khoản không tồn tại");
            }
            user.Email = request.Email;
            user.PhoneNumber = request.PhoneNumber;
            user.FullName = request.FullName;
            var result = await _userManager.UpdateAsync(user);
            if (result.Succeeded)
            {
                return new ResponseDto(ResponseCode.Success, "Cập nhật thành công");
            }
            return new ResponseDto(ResponseCode.LogicError, "Cập nhật thất bại");
        }
    }
}
