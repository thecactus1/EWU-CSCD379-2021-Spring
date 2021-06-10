using System.Collections.Generic;
using System.Linq;


//Example data to load into the database. Loads every start. The database deletes itself every run so these values are new every time
namespace SecretSanta.Data{
    public class DbData{
        public static List<User> Users(){
            return new List<User>{
                new User()
                {
                    Id = 1,
                    FirstName = "Inigo",
                    LastName = "Montoya"
                },
                new User
                {
                    Id = 2,
                    FirstName = "Princess",
                    LastName = "Buttercup"
                },
                new User
                {
                    Id = 3,
                    FirstName = "Prince",
                    LastName = "Humperdink"
                },
                new User
                {
                    Id = 4,
                    FirstName = "Count",
                    LastName = "Rugen"
                },
                new User
                {
                    Id = 5,
                    FirstName = "Miracle",
                    LastName = "Max"
                }
            };
        }

        public static List<Group> Groups(){
            return new List<Group>{
                new Group
                    {
                        Id = 1,
                        Name = "IntelliTect Christmas Party"
                    },
                new Group
                {
                    Id = 2,
                    Name = "The Band, Ghost"
                }
                
            };
        }
        public static List<Gift> Gifts(){
            return new List<Gift>{
            new Gift { Id = 1, Title = "Drone", Description = "A fun little way to get a birds eye view", Url="https://www.google.com", Priority = 2, UserId = 1 },
            new Gift { Id = 2, Title = "Rat Poison", Description = "Hopefully a way to get rid of those unusual size rodents", Url="https://www.google.com", Priority = 1, UserId = 2 },
            //new Gift { Id = 3, Title = "Drone", Description = "A fun little way to get a birds eye view", Url="https://www.google.com", Priority = 2, UserId = 3 },
            //new Gift { Id = 4, Title = "Rat Poison", Description = "Hopefully a way to get rid of those unusual size rodents", Url="https://www.google.com", Priority = 1, UserId = 4 },
            };
        }
    }   
}