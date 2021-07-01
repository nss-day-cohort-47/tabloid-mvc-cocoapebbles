using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace TabloidMVC.Models.ViewModels
{
    public class PostManageTagsViewModel
    {
        [DisplayName("Post ID")]
        [Required]
        public int PostId { get; set; }
        [DisplayName("Tag ID")]
        [Required]
        public int TagId { get; set; }
        [Required]
        public string Title { get; set; }
        [Required]
        public string Content { get; set; }

        public bool IsApproved { get; set; }
        [DisplayName("Post Tags")]
        public List<Tag> PostTags { get; set; }
    }
}