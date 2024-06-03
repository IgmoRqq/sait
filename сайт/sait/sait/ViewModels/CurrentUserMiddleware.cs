using Microsoft.EntityFrameworkCore;
using sait.DataBase;
using sait.Models;
using System.Security.Claims;

namespace sait.ViewModels
{
    public class CurrentUserMiddleware
    {
        private readonly RequestDelegate _next;

        public CurrentUserMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context, ApplicationDbContext dbContext)
        {
            if (context.User.Identity.IsAuthenticated)
            {
                var email = context.User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Name)?.Value;
                if (email != null)
                {
                    var user = await dbContext.Users.FirstOrDefaultAsync(u => u.email == email);
                    if (user != null)
                    {
                        CurrentUser.user = user;
                    }
                }
            }

            await _next(context);
        }
    }

}