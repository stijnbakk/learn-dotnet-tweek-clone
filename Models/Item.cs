using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace tweekClone.Models
{
    public class Item
    {
        public int Id { get; set; }

        [Required]
        [DisplayName("Item title")]
        public string Title { get; set; }
        public Boolean Completed { get; set; }
    }
}