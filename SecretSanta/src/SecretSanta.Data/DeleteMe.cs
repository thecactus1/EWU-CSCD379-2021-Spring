using System.Collections.Generic;

namespace SecretSanta.Data
{
    public static class DeleteMe
    {
        public static List<User> Users { get; } = new()
        {
            new User() { Id = 1, FirstName = "Anigo", LastName= "Montoya",},
            new User() { Id = 2, FirstName = "Princess", LastName= "Buttercup"},
            new User() { Id = 3, FirstName = "Wesley", LastName= "Maskedman"}
        };
    }
}