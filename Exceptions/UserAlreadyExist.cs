namespace GymAndYou.Exceptions
{
    public class UserAlreadyExist:Exception
    {
        public UserAlreadyExist(string message) : base(message) 
        { }
    }
}
