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
            System.Console.WriteLine(currentTime.ToString());
            Console.WriteLine("open: "+TimeSpan.FromHours(systemAvailabilityTime.OpenTime).ToString());
            Console.WriteLine("close: "+ TimeSpan.FromHours(systemAvailabilityTime.CloseTime).Hours.ToString());
            Console.WriteLine("today hour: " + DateTime.Now.TimeOfDay.Hours.ToString());

            var requestPath = httpContext.Request.Path;

            if (requestPath.StartsWithSegments("/api/Auth/login"))
            {
                await _next(httpContext);
                return;
            }

            if ((systemAvailabilityTime.CloseTime -24) < systemAvailabilityTime.OpenTime)
            {

                //if(systemAvailabilityTime.OpenTime >= 0 && systemAvailabilityTime.OpenTime < 12 &&  currentTime.Hours >=0 && currentTime.Hours < (systemAvailabilityTime.CloseTime -24)) {
                //    await _next(httpContext);
                //    return;
                //}
                //if (systemAvailabilityTime.OpenTime >= 12 && systemAvailabilityTime.OpenTime < 24 && currentTime.Hours >= 12 && currentTime.Hours < (systemAvailabilityTime.CloseTime - 24))
                //{
                //    await _next(httpContext);
                //    return;
                //}

                if (systemAvailabilityTime.OpenTime >= 0 && systemAvailabilityTime.OpenTime < 12 && 23 >= 0 && 23 < (systemAvailabilityTime.CloseTime - 24))
                {
                    await _next(httpContext);
                    return;
                }
                if (systemAvailabilityTime.OpenTime >= 12 && systemAvailabilityTime.OpenTime < 24 && 13 >= 12 && 13< (systemAvailabilityTime.CloseTime))
                {
                    await _next(httpContext);
                    return;
                }

            }
            else
            {
                if (currentTime > TimeSpan.FromHours(systemAvailabilityTime.OpenTime) && currentTime < TimeSpan.FromHours(systemAvailabilityTime.CloseTime))
                {
                    await _next(httpContext);
                    return;
                }
            }


          

            var isAdmin = httpContext.User.IsInRole("Admin");
            if (isAdmin)
            {
                await _next(httpContext);
                return;
            }


            httpContext.Response.StatusCode = StatusCodes.Status503ServiceUnavailable;
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
