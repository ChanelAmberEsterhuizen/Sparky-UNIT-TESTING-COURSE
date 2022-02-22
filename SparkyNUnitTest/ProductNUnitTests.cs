using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sparky
{
    [TestFixture]
    public class ProductNUnitTests
    {
        [Test]
        public void GetPropductPrice_PlatinumCustomer_ReturnPriceWith20Discount()
        {
               Product product = new Product() { Price = 50 };
            var res = product.Getprice(new Customer() { IsPlatinum = true });
            Assert.That(res, Is.EqualTo(40)); 
        }
        [Test]
        public void GetPropductPriceMOQAbuse_PlatinumCustomer_ReturnPriceWith20Discount()
        {
            var customer = new Mock<ICustomer>(); 
            customer.Setup(u => u.IsPlatinum).Returns(true);
            Product product = new Product() { Price = 50 };
            var res = product.Getprice(customer.Object);
            Assert.That(res, Is.EqualTo(40));
        }
    }
}
