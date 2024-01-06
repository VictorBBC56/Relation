using Microsoft.EntityFrameworkCore;

namespace Relations.Models
{
    [Owned]
    public class ProductExtend
    {
        public string Color { get; set; }
        public double Weight { get; set; }

    }
}
