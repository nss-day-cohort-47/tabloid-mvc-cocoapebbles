using System.Collections.Generic;
using System.ComponentModel;

namespace TabloidMVC.Models.ViewModels
{
    public class EditUserProfileViewModel
    {
        [DisplayName("User Name")]
        public UserProfile UserProfile { get; set; }
        [DisplayName("User Rank")]
        public List<UserType> UserTypes { get; set; }
        public int AdminCount { get; set; }
    }
}