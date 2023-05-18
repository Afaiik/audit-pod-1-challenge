using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;

namespace Core.Models.Product
{
    [DataContract]
    public class ProductDto
    {
        [DataMember(Name = "id")]
        [Required]
        public int Id { get; set; }

        [DataMember(Name = "description")]
        [MaxLength(255)]
        public string Description { get; set; } = default!;

        [DataMember(Name = "availableStock")]
        [Required]
        public int AvailableStock { get; set; }

        [DataMember(Name = "price")]
        [Required]
        public float Price { get; set; }
    }
}
