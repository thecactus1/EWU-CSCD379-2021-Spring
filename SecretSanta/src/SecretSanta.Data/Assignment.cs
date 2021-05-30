using System;

namespace SecretSanta.Data
{
    public class Assignment
    {
        public User Giver { get; }
        public User Receiver { get; }

        public Assignment(User giver, User recipient)
        {
            Giver = giver ?? throw new ArgumentNullException(nameof(giver));
            Receiver = recipient ?? throw new ArgumentNullException(nameof(recipient));
        }
    }
}