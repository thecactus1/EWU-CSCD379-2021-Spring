using System.Collections.Generic;

namespace SecretSanta.Data
{
    public static class EventContext
    {
        static EventContext()
        {
            
        }

        public static List<Gift> Gifts { get; } = new()
        {
            new Gift() { Id = 1, Title = "Stoke's Birthday", Priority = 1, UserId = 1 },
            new Gift() { Id = 2, Title = "Other Fun Activity", Priority = 2, UserId = 2 },
            new Gift() { Id = 3, Title = "Someone Else's Birthday", Priority = 3, UserId = 3 }
        };

        public static List<User> Users { get; } = new()
        {
            new User() { Id = 1, FirstName = "Mike", LastName = "Jones" },
            new User() { Id = 2, FirstName = "Mickey", LastName = "Mouse" },
            new User() { Id = 3, FirstName = "Mountain", LastName = "Dew" }
        };

        public static List<Group> Groups { get; } = new()
        {
            new Group() { Id = 1, Name = "RatSquad" },
            new Group() { Id = 2, Name = "GoonSquad" },
        };
    }
}