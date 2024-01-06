using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace Relations.Models
{
    public class Product
    {
        [DisplayName("Id")]
        public int Id { get; set; }

        [DisplayName("Name"), Required(ErrorMessage = "Input require")]
        public string Name { get; set; }

        [DisplayName("Price")]
        [Range(1, 99999, ErrorMessage = "Input Between {1} To {2}")]
        [Required(ErrorMessage = "Input require")]
        public int Price { get; set; }

        [DisplayName("Amount")]
        [Range(1, 999, ErrorMessage = "Input Between {1} To {2}")]
        [Required(ErrorMessage = "Input require")]
        public int Amount { get; set; }

        [DisplayName("Picture")]
        public string? ImageUrl { get; set; }

        public ProductExtend ProductExtend { get; set; }
        public int CategoryId { get; set; }
        public Category Category { get; set; }

    }
}
