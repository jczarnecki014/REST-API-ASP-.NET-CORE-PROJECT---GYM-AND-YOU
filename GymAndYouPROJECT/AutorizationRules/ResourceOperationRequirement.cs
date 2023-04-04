using Microsoft.AspNetCore.Authorization;

namespace GymAndYou.AutorizationRules
{
    public enum ResourceOperations
    {
        Create,
        Read,
        Update, 
        Delete
    }
    public class ResourceOperationRequirement : IAuthorizationRequirement
    {   
        public ResourceOperations operations{ get;set; }
        public ResourceOperationRequirement(ResourceOperations operations)
        {
            this.operations = operations;
        }
    }
}
