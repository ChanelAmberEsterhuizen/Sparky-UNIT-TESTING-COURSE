using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Sparky
{
  
    public class ProductXUnitTests
    {
        [Fact]
        public void GetPropductPrice_PlatinumCustomer_ReturnPriceWith20Discount()
        {
               Product product = new Product() { Price = 50 };
            var res = product.Getprice(new Customer() { IsPlatinum = true });
            Assert.Equal(40, res); 
        }
        [Fact]
        public void GetPropductPriceMOQAbuse_PlatinumCustomer_ReturnPriceWith20Discount()
        {
            var customer = new Mock<ICustomer>(); 
            customer.Setup(u => u.IsPlatinum).Returns(true);
            Product product = new Product() { Price = 50 };
            var res = product.Getprice(customer.Object);
            Assert.Equal(40, res);
        }
    }
}
