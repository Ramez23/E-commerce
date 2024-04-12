using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace E_commerce.Models
{
    public class ShoppingCart
    {
        [Key]
        public int CartID { get; set; }
        public int ProductID { get; set; }

        [ForeignKey("ProductID")]
        public Product? Product { get; set; }


        public int UnitPrice { get; set; }

        public int Quantity { get; set; }

    }
}
