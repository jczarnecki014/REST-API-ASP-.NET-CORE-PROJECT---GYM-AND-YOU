using Azure.Core.Pipeline;
using GymAndYou.Entities;

namespace GymAndYou.StaticData
{
    public static class Static
    {
        public static List<string> BodyParts = new List<string>{"Chest","Arm","Legs","Stomach","Back"};

        //Gym query filters
            public static List<string> SortByAllowedColumns = new List<string>{nameof(Gym.Name),nameof(Gym.Description),nameof(Gym.OpeningHours)};
            public static List<int> AviablePageSizes = new List<int>{5,10,15,20};
            public static int MimimumPageNumber = 0;

        //Password requirements
            public static string StrongPasswordREGEX = "(?=.*[a-z])(?=.*[A-Z])(?=.*[0-9])(?=.*[^A-Za-z0-9])(?=.{8,})";

        //Roles
            public static string System_Roles_Administrator = "Admin";
            public static string System_Roles_Manager = "Manager";
            public static string System_Roles_User = "User";

        //Authorization roles
            public static int Minimum_Days_Since_Account_Create = 14; // <---- After 14 days user will able to delete gym wihch he estabilished
            public static string[] Required_Nationality_To_Gym_Delete = new string[]{"Poland","German","United Kingdom", "United States"};
        //
    }
}
