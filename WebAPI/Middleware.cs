using Business.Abstract;
using Entities.Concrete;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using System.Security.Claims;
using System.Threading.Tasks;

namespace WebAPI
{
    public class Middleware
    {
        private readonly RequestDelegate _next;
        private ISystemAvailabilityTimeService _systemAvailabilityTimeService;
        public Middleware(RequestDelegate next, ISystemAvailabilityTimeService systemAvailabilityTimeService)
        {
            _next = next;
            _systemAvailabilityTimeService = systemAvailabilityTimeService;
        }

        public async Task Invoke(HttpContext httpContext)
        {
            SystemAvailabilityTime systemAvailabilityTime = _systemAvailabilityTimeService.GetSystemAvailabilityTime();
            var currentTime = DateTime.Now.TimeOfDay;
          
            var isAdmin = httpContext.User.IsInRole("Admin");
            if (isAdmin)
            {
                await _next(httpContext);
                return;
            } 
            if (currentTime > TimeSpan.FromHours(systemAvailabilityTime.OpenTime)&& currentTime < TimeSpan.FromHours(systemAvailabilityTime.CloseTime))
            {
                await _next(httpContext);
                return;
            }

            var requestPath = httpContext.Request.Path;

            if (requestPath.StartsWithSegments("/api/Auth/login"))
            {
                await _next(httpContext);
                return;
            }


            httpContext.Response.StatusCode = StatusCodes.Status403Forbidden;
            await httpContext.Response.WriteAsync("Backend is not available now!");
        }
    }
    public static class MiddlewareExtensions
    {
        public static IApplicationBuilder UseMiddleware(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<Middleware>();
        }
    }
}
