using GymAndYou.Entities;

namespace GymAndYou.StaticData
{
    public static class Static
    {
        public static List<string> BodyParts = new List<string>{"Chest","Arm","Legs","Stomach","Back"};

        //Gym query filters
        public static List<string> SortByAllowedColumns = new List<string>{nameof(Gym.Name),nameof(Gym.Description)};
        public static List<int> AviablePageSizes = new List<int>{5,10,15};
        public static int MimimumPageNumber = 0;

        //Password requirements
        public static string StrongPasswordREGEX = "(?=.*[a-z])(?=.*[A-Z])(?=.*[0-9])(?=.*[^A-Za-z0-9])(?=.{8,})";
    }
}
