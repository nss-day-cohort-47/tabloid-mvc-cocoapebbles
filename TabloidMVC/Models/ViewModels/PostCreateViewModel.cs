using System.Collections.Generic;
using System.ComponentModel;


namespace TabloidMVC.Models.ViewModels
{
    public class PostCreateViewModel
    {
        public Post Post { get; set; }
        [DisplayName("Category Options")]
        public List<Category> CategoryOptions { get; set; }
    }
}
