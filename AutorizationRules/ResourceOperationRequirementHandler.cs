using GymAndYou.Entities;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;

namespace GymAndYou.AutorizationRules
{
    public class ResourceOperationRequirementHandler : AuthorizationHandler<ResourceOperationRequirement, Gym>
    {
        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, ResourceOperationRequirement requirement, Gym gym)
        {
           
                if(requirement.operations == ResourceOperations.Create || requirement.operations == ResourceOperations.Read)
                {
                    context.Succeed(requirement);
                }

                var userId = Int32.Parse(context.User.FindFirst(c => c.Type == ClaimTypes.NameIdentifier).Value);
                
                if(gym.CreatedById == userId) 
                {   
                    context.Succeed(requirement);
                }

                return Task.CompletedTask;
        }
    }
}
