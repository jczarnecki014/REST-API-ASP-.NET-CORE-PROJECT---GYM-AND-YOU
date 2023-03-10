using Microsoft.AspNetCore.Authorization;

namespace GymAndYou.AutorizationRules
{
    public class MinimumDaysSinceCreateAccount : IAuthorizationRequirement
    {
        public int days;
        public MinimumDaysSinceCreateAccount(int days)
        {
            this.days = days;
        }
    }
}
