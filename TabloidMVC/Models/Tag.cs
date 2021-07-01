using System.ComponentModel;

namespace TabloidMVC.Models
{
    public class Tag
    {
        public int Id { get; set; }
        [DisplayName("Tag Name: ")]
        public string Name { get; set; }
    }
}
