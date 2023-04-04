using Microsoft.AspNetCore.Http.HttpResults;

namespace GymAndYou.Exceptions
{
    public class BadRequest:Exception
    {
        public BadRequest(string message):base(message)
        {
        }
    }
}
