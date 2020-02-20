using System.ComponentModel.DataAnnotations;

namespace ServiceApp.DAL.Models
{
    public class Products
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public bool Status { get; set; }
    }
}
