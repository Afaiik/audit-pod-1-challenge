using AutoMapper;
using Core.Entities;
using Core.Product;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace eShop.Tests.Product
{
    public class ProductServiceShould
    {
        private readonly IProductReadRepository _productReadRepository;
        private readonly IMapper _mapper;


        [Fact]
        public void ValidateGetPageSize()
        {
            //Arrange
            int AllowPageSize = 10;

            //Act
            bool isValid = AllowPageSize % 5 == 0 && AllowPageSize <= 20 && AllowPageSize > 0;

            //Assert
            Assert.True(isValid, $"{AllowPageSize} is not a valid page index.");
        }


        [Theory]
        [InlineData("apple", 5, 1)]
        [InlineData("sdad", 5, 1)]
        public void GetWithPagination(string searchCriteria, int pageSize, int pageIndex)
        {
            var lstproduct = GetSampleProducts()
                .Where(e => e.Description.Trim().ToLower().StartsWith(searchCriteria.Trim().ToLower()))
                .OrderBy(x => x.Description)
                .Skip(CalculateSkipSize(pageSize, pageIndex))
                .Take(pageSize)
                .ToList();

            Assert.True(lstproduct.Count > 0,$"The parameters pageSize or pageIndex are invalid");
        }

        private int CalculateSkipSize(int pageSize, int pageIndex) => pageIndex > 1 ? (pageSize * pageIndex) - pageSize : 0;

        private List<ProductEntity> GetSampleProducts()
        {
            var products = new List<ProductEntity>{
               new ProductEntity {
                   Id = 1,
                   Description = "Apple 1",
                   AvailableStock = 10,
                   Price = 230
                },
                new ProductEntity {
                   Id = 2,
                   Description = "Apple 2",
                   AvailableStock = 32,
                   Price = 456
               },
                new ProductEntity {
                   Id = 3,
                   Description = "Apple 3",
                   AvailableStock = 686,
                   Price = 3454
               },
                new ProductEntity {
                   Id = 4,
                   Description = "Apple 4",
                   AvailableStock = 3242,
                   Price = 564
               },
                new ProductEntity {
                   Id = 5,
                   Description = "Apple 5",
                   AvailableStock = 24,
                   Price = 5324
               }
            };

            return products;
        }
    }
}
