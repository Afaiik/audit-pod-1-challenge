using FluentValidation;
using System.Runtime.Serialization;

namespace Core.Models.Product
{
    public class ProductFluent
    {
        [DataMember(Name = "id")]
        public int Id { get; set; }

        [DataMember(Name = "description")]
        public string Description { get; set; } = default!;

        [DataMember(Name = "availableStock")]
        public int AvailableStock { get; set; }

        [DataMember(Name = "price")]
        public float Price { get; set; }

        public class ProductValidator : AbstractValidator<ProductFluent>
        {
            public ProductValidator()
            {
                RuleFor(x => x.Id).NotNull();
                RuleFor(x => x.Description).NotNull();
                RuleFor(x => x.AvailableStock).NotNull().GreaterThanOrEqualTo(1);
                RuleFor(x => x.Price).NotNull().GreaterThanOrEqualTo(1);
            }
        }
    }

}
