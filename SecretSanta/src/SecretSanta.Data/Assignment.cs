using System.Collections.Generic;
using System;

namespace SecretSanta.Data
{
    public class Assignment
    {
        public int Id {get; set;}
        public User Giver { get; set; }
        public User Receiver { get; set; }

        //Giver.FirstName +" "+ Giver.LastName + " " + Receiver.FirstName+ " " + Receiver.LastName;
        public String GiverReciever  {get; set;}

    }
}