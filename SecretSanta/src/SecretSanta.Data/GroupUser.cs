using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace SecretSanta.Data
{
    public class GroupUser
    {
        public int GroupId { get; set; }
        public Group Group { get; set; } = new();
        public int UserId { get; set; }
        public User User { get; set; } = new();
    }
}