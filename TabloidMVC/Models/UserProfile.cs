using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace TabloidMVC.Models
{
    public class UserProfile
    {
        public int Id { get; set; }
        [DisplayName("First Name")]
        [Required]
        public string FirstName { get; set; }
        [DisplayName("Last Name")]
        [Required]
        public string LastName { get; set; }
        [DisplayName("Display Name")]
        [Required]
        public string DisplayName { get; set; }
        [Required]
        public string Email { get; set; }
        [DisplayName("Date created")]
        public DateTime CreateDateTime { get; set; }
        [DisplayName("Profile Photo")]
        public string ImageLocation { get; set; }
        [DisplayName("User Rank")]
        public int UserTypeId { get; set; }
        [DisplayName("User Rank")]
        public UserType UserType { get; set; }
        public int AdminCount { get; set; }
        [DisplayName("Full Name")]
        public string FullName
        {
            get
            {
                return $"{FirstName} {LastName}";
            }
        }
    }
}