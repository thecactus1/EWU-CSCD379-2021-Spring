using System.ComponentModel.DataAnnotations;
using System.Collections;
using System;

namespace SecretSanta.Web.ViewModels
{
    public class GiftViewModel : IComparable
    {
        public int Id { get; set; }

        [Display(Name="Description")]
        public string? Description { get; set; } = "";

        [Display(Name="Url")]
        public string? Url { get; set; } = "";
        [Required]
        [Display(Name="Title")]
        public string Title { get; set; } = "";
        [Display(Name="Priority")]
        public int Priority { get; set; }
        [Required]
        [Display(Name="UserID")]
        public int UserId { get; set; }

        public int CompareTo(Object other){
            if(!(other.GetType().Equals(this.GetType())))
                return 0;
            GiftViewModel prio = other as GiftViewModel;
            if(Priority > prio.Priority)
                return 1;
            if(Priority < prio.Priority)
                return -1;
            return 0;
        }
    }
}