using Core.Entities;
using Core.Utils;
using FluentValidation;
using Services.Base;

namespace Services.Product
{
    public class ProductValidator : AbstractValidator<ProductEntity>, IValidator<ProductEntity>
    {
        public ProductValidator()
        {
            RuleSet(BaseValidationType.Get.GetDescription(), () =>
            {

            });

            RuleSet(BaseValidationType.Post.GetDescription(), () =>
            {
                //    ------TBD------
                //RuleFor(x => x.Id).NotNull();
                //RuleFor(x => x.Description).NotNull();
                //RuleFor(x => x.AvailableStock).NotNull().GreaterThanOrEqualTo(1);
                //RuleFor(x => x.Price).NotNull().GreaterThanOrEqualTo(1);
            });

        }
    }
}
