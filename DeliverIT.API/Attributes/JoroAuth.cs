using DeliverIT.Services.Contracts;
using DeliverIT.Services.Helpers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;

namespace DeliverIT.API.Attributes
{
    public class JoroAuth : ActionFilterAttribute, IAuthorizationFilter
    {
        public void OnAuthorization(AuthorizationFilterContext context)
        {
            var a = context.HttpContext.Request.Headers["auth"];
            var ss = context.HttpContext.RequestServices.GetService(typeof(IFindUserService)) as IFindUserService;
            var text = context.HttpContext.Request.Headers["auth"].ToString();
            var user = Task.Run(async () => await ss.FindUs(text)).GetAwaiter().GetResult();
            if (user == null)
            {
                throw new AppException("works"); // ТРЯБВА ДРУГ ЕКСЕПШЪН КОЙТО ДА ХВЪРЛЯ 401
            }
        }
    }
}
