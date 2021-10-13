using DeliverIT.Models;
using DeliverIT.Services.Contracts;
using DeliverIT.Services.DTOs;
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

        public async Task<UserDTO> FindUs(string authorization)
        {
            var splitted = authorization.Split();
            var email = splitted[0];
            var password = splitted[1];

            return await _db.AppUserRoles
                            .IgnoreQueryFilters()
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
    }
}
