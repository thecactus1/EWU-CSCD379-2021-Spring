using System.ComponentModel.DataAnnotations;

namespace SecretSanta.Web.ViewModels
{
    public class GiftViewModel
    {
        public int Id { get; set; }
        [Required]
        [Display(Name="Title")]
        public string Title { get; set; } = "";

        [Required]
        [Display(Name="Description")]
        public string Description { get; set; } = "";

        [Required]
        [Display(Name="URL")]
        public string URL { get; set; } = "";
        [Required]
        [Display(Name="Priority")]
        public int Priority { get; set; }
        [Required]
        [Display(Name="UserID")]
        public int UserID { get; set; }
    }
}