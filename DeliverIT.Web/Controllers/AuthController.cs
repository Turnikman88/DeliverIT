using DeliverIT.Services.Contracts;
using DeliverIT.Services.Helpers;
using DeliverIT.Web.Models;
using DeliverIT.Web.Models.Mappers;
using DeliverIT.Services.DTOMappers;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Threading.Tasks;

namespace DeliverIT.Web.Controllers
{
    public class AuthController : Controller
    {
        private readonly IFindUserService _auth;
        private readonly IAddressService _ads;
        private readonly ICustomerService _cs;


        public AuthController(IFindUserService auth, IAddressService ads, ICustomerService cs)
        {
            this._ads = ads;
            this._auth = auth;
            this._cs = cs;
        }

        //GET: /auth/login
        public IActionResult Login()
        {
            var loginViewModel = new LoginViewModel();

            return this.View(loginViewModel);
        }

        //POST: /auth/login
        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel loginViewModel)
        {
            if (!await _auth.IsExistingAsync(loginViewModel.Email))
            {
                this.ModelState.AddModelError("Email", "User with this email address doesn't exists.");
                return this.View(loginViewModel);
            }

            if (!await _auth.IsPasswordValidAsync(loginViewModel.Email, loginViewModel.Password))
            {
                this.ModelState.AddModelError("Password", "Wrong Password!");
            }

            if (!this.ModelState.IsValid)
            {
                return this.View(loginViewModel);
            }
            var credentials = $"{loginViewModel.Email} {loginViewModel.Password}";

            var user = await this._auth.FindUserAsync(credentials);
            if (user is null)
            {
                this.ModelState.AddModelError("Username", "Invalid input data");
                return this.View(loginViewModel);
            }
            else
            {
                this.HttpContext.Session.SetString(Constants.SESSION_AUTH_KEY, credentials);
                this.HttpContext.Session.SetString(Constants.SESSION_ROLE_KEY, user.Role);
                return this.RedirectToAction("index", "home");
            }
        }

        //GET: /auth/logout
        public IActionResult Logout()
        {
            this.HttpContext.Session.Remove(Constants.SESSION_AUTH_KEY);

            return this.RedirectToAction("index", "home");
        }

        [HttpGet]
        public IActionResult Register()
        {
            var model = new UserViewModel();
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Register(UserViewModel model)
        {
            if (await _auth.IsExistingAsync(model.Email))
            {
                this.ModelState.AddModelError("Email", "User with this email address already exists.");
            }
                       
            if (!this.ModelState.IsValid)
            {
                return this.View(model);
            }

            model.AddressId = await GetAddressID(model);
            var toCustomer = model.GetDTO();
            await this._cs.PostAsync(toCustomer);
            return this.Redirect(nameof(Login));
        }
        //GET: /auth/settings
        public async Task<IActionResult> Settings()
        {
            var userCredentials = this.HttpContext.Session.GetString(Constants.SESSION_AUTH_KEY);
            var userRole = this.HttpContext.Session.GetString(Constants.SESSION_ROLE_KEY);
            if(userRole == Constants.ROLE_EMPLOYEE)
            {
                var employee = await _auth.FindEmployee(userCredentials);
                return this.View(employee.GetModel());
            }
            else
            {
                var customerDTO =  _cs.GetCustomersByEmailAsync(userCredentials.Split().First()).Result.First();
                return  this.View(customerDTO.GetModel());
            }
        }

        //POST: /auth/settings
        [HttpPost]
        public async Task<IActionResult> Settings(UserViewModel model)
        {
            if (!this.ModelState.IsValid)
            {
                return this.View();
            }

            var userRole = this.HttpContext.Session.GetString(Constants.SESSION_ROLE_KEY);
            if (userRole == Constants.ROLE_EMPLOYEE)
            {
                return default; //todo: logic
            }
            else
            {
                model.AddressId = await GetAddressID(model);
                var toCustomer = model.GetDTO();
                var email = await this._cs.GetCustomersByEmailAsync(model.Email);
                await this._cs.UpdateAsync(email.First().Id, toCustomer);
            }
               
            return this.RedirectToAction("Home", "Index");

        }

        private async Task<int> GetAddressID(UserViewModel model)
        {
            return await _ads.AddressToID(model.Address, model.City, model.Country);
        }
    }
}
