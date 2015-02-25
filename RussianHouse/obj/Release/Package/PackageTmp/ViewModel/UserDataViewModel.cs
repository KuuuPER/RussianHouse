using System.ComponentModel.DataAnnotations;
using RussianHouse.Models;
using RussianHouse.SectionsContent;

namespace RussianHouse.ViewModel
{
    public class UserDataViewModel
    {
        [Required(ErrorMessage = "Необходимо ввести email!")]
        [EmailAddress(ErrorMessage = "Invalid Email Address")]
        [Display(Name = "Email")]
        public string Email { get; set; }
        public ISectionContent Content { get; set; }
        public EnSection SelectedSection { get; set; }
        public string Balance { get; set; }
    }
}