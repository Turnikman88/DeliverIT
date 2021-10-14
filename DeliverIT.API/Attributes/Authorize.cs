using DeliverIT.Services.Contracts;
using DeliverIT.Services.Helpers;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Threading.Tasks;

namespace DeliverIT.API.Attributes
{
    public class Authorize : ActionFilterAttribute, IAuthorizationFilter
    {
        public string Roles { get; set; }
        public string QueryId { get; set; }
        public void OnAuthorization(AuthorizationFilterContext context)
        {
            if (context.HttpContext.Request.Headers.ContainsKey(Constants.HEADER_AUTH_KEY))
            {
                var credentials = context.HttpContext.Request.Headers[Constants.HEADER_AUTH_KEY].ToString();
                var authService = context.HttpContext.RequestServices.GetService(typeof(IFindUserService)) as IFindUserService;
                var user = Task.Run(async () => await authService.FindUs(credentials)).GetAwaiter().GetResult();
                if (user == null || Roles != user.Role)
                {
                    throw new UnauthorizedAppException(Constants.NOT_AUTHORIZED);
                }
                if (QueryId != null && context.HttpContext.Request.Query.ContainsKey(QueryId))
                {
                    var queryUserId = context.HttpContext.Request.Query[QueryId];
                    if (queryUserId != user.Id)
                    {
                        throw new UnauthorizedAppException(Constants.NOT_AUTHORIZED);
                    }
                }
            }
            else
            {
                throw new UnauthorizedAppException(Constants.NOT_LOGGED);
            }
        }
    }
}
