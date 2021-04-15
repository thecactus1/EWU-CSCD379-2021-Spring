using System;
using System.Collections.Generic;
using SecretSanta.Web.ViewModels;

namespace SecretSanta.Web.Data
{
    public static class MockData
    {
        public static List<GiftViewModel> Gifts = new List<GiftViewModel>{
            new GiftViewModel {Title = "The Princess Bride", 
            Description = "A classic children's novel, but really anyone can enjoy it.",
            URL = "https://www.amazon.com/Princess-Bride-Deluxe-Morgensterns-Adventure/dp/1328948854/ref=sr_1_2?dchild=1&keywords=the+princess+bride&qid=1618381069&sr=8-2",
            UserID = 1,
            Priority = 1,
            },
        };

        public static List<GroupViewModel> Groups = new List<GroupViewModel>{
            new GroupViewModel {Name = "Florinians"},
        };

        
        public static List<UserViewModel> Users = new List<UserViewModel>{
            new UserViewModel {FirstName = "Inigo", LastName = "Montoya"},
            new UserViewModel {FirstName = "Princess", LastName = "Buttercup"},
        };
    }
}