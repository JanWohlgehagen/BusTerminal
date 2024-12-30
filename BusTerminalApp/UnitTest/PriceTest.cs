using Application.Interfaces;
using Application.Services;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;

namespace UnitTest
{
    public class PriceTest
    {

        [Theory]
        [InlineData(DayOfWeek.Saturday, 10, 875)]
        [InlineData(DayOfWeek.Sunday, 10, 1150)]
        [InlineData(DayOfWeek.Monday, 10, 600)]
        public void TestPriceSuccesful(DayOfWeek day, int distance, decimal expectedResult)
        {

            //Arrange
            IPriceService priceService = new PriceService();

            //Act
            var price = priceService.GetPrice(day, distance);

            //Assert
            Assert.Equal(expectedResult, price);
        }

        [Fact]
        public void TestPriceInvalidDistance()
        {
            //Arrange
            IPriceService priceService = new PriceService();

            //Act + Assert

            var ex = Assert.Throws<ArgumentException>(() =>priceService.GetPrice(DayOfWeek.Monday, -1));
            Assert.Equal(ex.Message, "We need a distance to calculate the price.");
        }
    }
}
