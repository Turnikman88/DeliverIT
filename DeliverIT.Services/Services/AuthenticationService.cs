﻿using DeliverIT.Models;
using DeliverIT.Services.Contracts;
using DeliverIT.Services.DTOs;
using DeliverIT.Services.Helpers;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace DeliverIT.Services.Services
{
    public class AuthenticationService : IAppAuthenticationService
    {
        private readonly DeliverITDBContext _db;
        public AuthenticationService(DeliverITDBContext db)
        {
            this._db = db;
        }

        public bool FindUser(string authorization)
        {
            if (authorization == null || !authorization.Contains(" "))
            {
                throw new KeyNotFoundException(Constants.ACCOUNT_NOT_FOUND);
            }

            var splitted = authorization.Split();
            var email = splitted[0];
            var password = splitted[1];
            return this._db.Customers.Any(x => x.Email == email && x.Password == password);
        }

        public async Task<string> GetUserID(string authorization)
        {
            var splitted = authorization.Split();
            var email = splitted[0];
            var password = splitted[1];
            var user = await this._db.Customers.FirstAsync(x => x.Email == email && x.Password == password);
            return user.Id.ToString();
        }

        public async Task<string> GetEmployeeID(string authorization)
        {
            var splitted = authorization.Split();
            var email = splitted[0];
            var password = splitted[1];
            var employee = await this._db.Employees.FirstAsync(x => x.Email == email && x.Password == password);
            return employee.Id.ToString();
        }

        public bool FindEmployee(string authorization) //TODO: await?
        {
            if (authorization == null || !authorization.Contains(" "))
            {
                throw new KeyNotFoundException(Constants.ACCOUNT_NOT_FOUND);
            }

            var splitted = authorization.Split();
            var email = splitted[0];
            var password = splitted[1];
            return this._db.Employees.Any(x => x.Email == email && x.Password == password);
        }
        public async Task<UserDTO> FindUs(string authorization)
        {
            var splitted = authorization.Split();
            var email = splitted[0];
            var password = splitted[1];
            
            return await _db.AppUserRoles.Include(x => x.AppUser).Include(x => x.AppRole)
                .Where(x => x.AppUser.Email == email && x.AppUser.Password == password)
                .Select(x => new UserDTO {Email = x.AppUser.Email, Role = x.AppRole.Name })
                .FirstOrDefaultAsync();
        }
    }
}
