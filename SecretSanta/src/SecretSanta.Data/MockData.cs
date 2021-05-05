using System.Collections.Generic;

namespace SecretSanta.Data
{
    public static class MockData
    {
        public static Dictionary<int, User> Users { get; } = new()
        {
            {
                1,
                new User
                {
                    Id = 1,
                    FirstName = "Inigo",
                    LastName = "Montoya"
                }
            },
            {
                2, 
                new User
                {
                    Id = 2,
                    FirstName = "Princess",
                    LastName = "Buttercup"
                }
            },
            {
                3,
                new User
                {
                    Id = 3,
                    FirstName = "Prince",
                    LastName = "Humperdink"
                }
            },
            {
                4,
                new User
                {
                    Id = 4,
                    FirstName = "Count",
                    LastName = "Rugen"
                }
            },
            {
                5,
                new User
                {
                    Id = 5,
                    FirstName = "Miracle",
                    LastName = "Max"
                }
            }
        };
    }
}
