using GymAndYou.Exceptions;
using Microsoft.Extensions.Logging;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace GymAndYou.Middleware
{
    public class ExceptionHandler : IMiddleware
    {
        private readonly ILogger<ExceptionHandler> _logger;

        public ExceptionHandler(ILogger<ExceptionHandler> logger)
        {
            _logger = logger;
        }

        public async Task InvokeAsync(HttpContext context, RequestDelegate next)
        {
            try
            {
                await next.Invoke(context);
            }
            catch(ForbidException error)
            {
                context.Response.StatusCode = 404;
                GetError(context,error);
            }
            catch(BadRequest error)
            {
                context.Response.StatusCode = 404;
                GetError(context,error);
            }
            catch(UserAlreadyExist error)
            {
                context.Response.StatusCode = 409;
                GetError(context,error);
            }
            catch(EntityNotFound error)
            {
                context.Response.StatusCode = 404;
                GetError(context,error);
            }
            catch(FileNotFound error)
            {
                context.Response.StatusCode = 404;
                GetError(context,error);
            }
            catch(Exception error)
            {
                context.Response.StatusCode = 500;
                GetError(context,"something went wrong");
            }
        }
        private async void GetError(HttpContext context,Exception error)
        {
               await context.Response.WriteAsync(error.Message);
              _logger.LogError(error,error.Message);
        }
        private async void GetError(HttpContext context,string errorMessage)
        {
               await context.Response.WriteAsync(errorMessage);
              _logger.LogError(errorMessage);
        }
    }
}
