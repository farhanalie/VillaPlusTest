using System;
using System.Collections.Generic;
using VillaPlusTest.Infrastructure.Entities;

namespace VillaPlusTest.Infrastructure
{
    public static class DbInitializer
    {
        public static void AddTestData(VpDbContext context)
        {
            var random = new Random();
            for (var i = 1; i <= 10; i++)
            {
                var product = new Product
                {
                    Id = i,
                    Name = $"Product{i}",
                    Price = 100,
                    Buy2Get1Free = i % 2 == 0,
                    Active = true,
                };
                context.Products.Add(product);
            }

            var discountCodes = new List<DiscountCode>
            {
                new DiscountCode
                {
                    Code = "NEWCUSTOMER",
                    DiscountPercentage = 20,
                    Expiry = DateTime.Today.AddYears(1)
                },
                new DiscountCode
                {
                    Code = "10OFF",
                    DiscountPercentage = 10,
                    Expiry = DateTime.Today.AddYears(1)
                },
                new DiscountCode
                {
                    Code = "20OFF",
                    DiscountPercentage = 20,
                    Expiry = DateTime.Today.AddYears(1)
                }
            };

            context.DiscountCodes.AddRange(discountCodes);
            context.SaveChanges();
        }
    }
}
