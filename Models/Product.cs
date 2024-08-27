using System.ComponentModel.DataAnnotations;

namespace PBMS.Models
{
    public class Product
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string? Name { get; set; }

        public decimal Price { get; set; }

        public int Qty { get; set; }

        public byte[]? QrCode { get; set; }

    }
}
