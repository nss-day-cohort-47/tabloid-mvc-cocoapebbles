using Microsoft.AspNetCore.Authorization;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace TabloidMVC.Models.ViewModels
{
    public class CommentViewModel
    {
        [DisplayName("Comments")]
        [Required]
        public List<Comment> comments { get; set; }
        [Required]
        public Comment comment { get; set; }
        public Post post { get; set; }
        [DisplayName("User Name")]
        public UserProfile UserProfile { get; set; }
    }
}