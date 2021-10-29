using DeliverIT.Models;
using DeliverIT.Models.DatabaseModels;
using DeliverIT.Services.Contracts;
using DeliverIT.Services.DTOMappers;
using DeliverIT.Services.DTOs;
using DeliverIT.Services.Helpers;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;


namespace DeliverIT.Services.Services
{
    public class FindUserService : IFindUserService
    {
        private readonly DeliverITDBContext _db;
        public FindUserService(DeliverITDBContext db)
        {
            this._db = db;
        }

        public async Task<bool> IsExistingAsync(string email)
        {

            return await _db.Customers.AnyAsync(x => x.Email == email) || await _db.Employees.AnyAsync(x => x.Email == email);
        }
        public async Task<bool> IsPasswordValidAsync(string email, string password)
        {
            var userPassword = await _db.AppUserRoles
                .Include(x => x.AppUser)
                .Where(x => x.AppUser.Email == email)
                .Select(x => x.AppUser.Password)
                .FirstOrDefaultAsync();
            return userPassword == password;
        }
        public async Task<UserDTO> FindUserAsync(string authorization)
        {
            if (!authorization.Contains(" "))
            {
                throw new AppException(Constants.WRONG_CREDENTIALS);
            }
            var splitted = authorization.Split();
            var email = splitted[0];
            var password = splitted[1];

            return await _db.AppUserRoles
                            .Include(x => x.AppUser)
                            .Include(x => x.AppRole)
                            .Where(x => x.AppUser.Email == email && x.AppUser.Password == password)
                            .Select(x =>
                            new UserDTO
                            {
                                Id = x.AppUserId.ToString(),
                                Email = x.AppUser.Email,
                                Role = x.AppRole.Name
                            }).FirstOrDefaultAsync();
        }

        public async Task<EmployeeDTO> FindEmployee(string authorization)
        {
            if (!authorization.Contains(" "))
            {
                throw new AppException(Constants.WRONG_CREDENTIALS);
            }
            var splitted = authorization.Split();
            var email = splitted[0];
            var password = splitted[1];

            var employee = await _db.Employees.Where(x => x.Email == email && x.Password == password).FirstAsync();
            return employee.GetDTO();
        }
    }
}
