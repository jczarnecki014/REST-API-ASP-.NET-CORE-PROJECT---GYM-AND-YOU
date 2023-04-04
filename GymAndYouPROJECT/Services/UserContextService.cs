using System.Security.Claims;

namespace GymAndYou.Services
{
         /// <summary>
        /// Ensure claims about currently logged user from httpContext
        /// </summary>
    public interface IUserContextService
    {
        int? GetUserId { get; }
        ClaimsPrincipal user { get; }
    }

    public class UserContextService : IUserContextService
    {
        private readonly IHttpContextAccessor _httpContextAccessor;


        public UserContextService(IHttpContextAccessor httpContextAccessor)
        {
        _httpContextAccessor = httpContextAccessor;
        }

        public ClaimsPrincipal user => _httpContextAccessor.HttpContext.User;

        /// <summary>
        /// Return currently logged user Id from HttpContext
        /// </summary>
        public int? GetUserId => Int32.Parse(user.FindFirst(c => c.Type == ClaimTypes.NameIdentifier).Value);
    }
}
