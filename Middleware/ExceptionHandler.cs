using GymAndYou.Exceptions;

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
            catch(EntityNotFound error)
            {
                context.Response.StatusCode = 404;
                await context.Response.WriteAsync(error.Message);
                _logger.LogError(error,error.Message);
            }
            catch(FileNotFound error)
            {
                context.Response.StatusCode = 404;
                await context.Response.WriteAsync(error.Message);
                _logger.LogError(error,error.Message);
            }
            catch(Exception error)
            {
                context.Response.StatusCode = 500;
                await context.Response.WriteAsync("Something went wrong..");
                _logger.LogError(error,error.Message);
            }
        }
    }
}
