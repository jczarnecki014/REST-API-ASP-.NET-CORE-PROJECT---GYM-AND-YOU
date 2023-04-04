using Microsoft.AspNetCore.Authorization;
using System.Net;
using System.Security.Claims;

namespace GymAndYou.AutorizationRules
{
    public class MinimumDaysSinceCreateAccountHandler : AuthorizationHandler<MinimumDaysSinceCreateAccount>
    {
        private readonly ILogger<MinimumDaysSinceCreateAccountHandler> _logger;

        public MinimumDaysSinceCreateAccountHandler(ILogger<MinimumDaysSinceCreateAccountHandler> logger)
        {
            _logger = logger;
        }

        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, MinimumDaysSinceCreateAccount requirement)
        {
            var registerDay = DateTime.Parse(context.User.FindFirst("DayOfRegister").Value);
            var currentDay = DateTime.Now;
            var minimumDay = registerDay.AddDays(requirement.days);

            //Log information
                var userId = context.User.FindFirst( c => c.Type == ClaimTypes.NameIdentifier).Value;
                var userName = context.User.FindFirst( c => c.Type == ClaimTypes.Name).Value;

            if(currentDay >= minimumDay)
            {
                _logger.LogInformation($"User witch ID = [{userId}] and USERNAME = [{userName}] " +
                                        $"recive access to delete gym method ---- > REGISTER DATE = [{registerDay}] - REQUIRED DATE = [{minimumDay}] ");
                context.Succeed(requirement);
            }
            
            return Task.CompletedTask;
        }
    }
}
