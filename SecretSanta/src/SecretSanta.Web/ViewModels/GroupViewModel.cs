using System.ComponentModel.DataAnnotations;

namespace SecretSanta.Web.ViewModels
{
    public class GroupViewModel
    {
        public int Id { get; set; }
        [Required]
        [Display(Name="Group Name")]
        public string Name { get; set; } = "";
    }
}