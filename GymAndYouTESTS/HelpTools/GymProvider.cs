using GymAndYou.Entities;

namespace GymAndYouTESTS.HelpTools
{
    public static class GymProvider
    {
        public static Gym GetGym(int? UserId = null)
        {
            var gym = new Gym()
            {
                Name = "test",
                Description= "test",
                OpeningHours = "test",
                Address = new Address {
                    City = "test",
                    StreetName= "test",
                    PostalCode= "test",
                }
            };

            if(UserId != null) {
                gym.CreatedById = UserId;
            }

            return gym;
        }
    }
}
