using DeliverIT.Services.Contracts;
using DeliverIT.Services.Helpers;
using DeliverIT.Web.Models;
using DeliverIT.Web.Models.Mappers;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
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
            if (!this.ModelState.IsValid)
            {
                return this.View(loginViewModel);
            }
            var credentials = $"{loginViewModel.Email} {loginViewModel.Password}";

            var user = await this._auth.FindUs(credentials);
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
            var test = HttpContext.Session.Keys;
            var model = new UserViewModel();
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Register(UserViewModel model)
        {
            if (!this.ModelState.IsValid)
            {
                return this.View();
            }
            if (await _auth.IsExisting(model.Email))
            {
                this.ModelState.AddModelError("Email", "User with this email address already exists.");
                return this.View(model);
            }

            model.AddressId = await GetAddressID(model);
            var toCustomer = model.GetDTO();
            await this._cs.PostAsync(toCustomer);
            return this.Redirect(nameof(Login));
        }

        private async Task<int> GetAddressID(UserViewModel model)
        {
            return await _ads.AddressToID(model.Address, model.City, model.Country);
        }
    }
}
