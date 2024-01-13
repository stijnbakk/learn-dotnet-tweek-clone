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
        public DateOnly LinkedDate { get; set; }
    }

    public class DayOverview
    {
        public DateOnly Date { get; set; }
        public string DayOfWeek { get; set; }
        public List<Item> DayItems { get; set; }

    }
}
