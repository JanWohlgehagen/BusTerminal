using Application.Interfaces;
using Reqnroll.Assist;
using Reqnroll;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PriceService.Specs.StepDefinitions
{
    [Binding]
    public sealed class PriceServiceStepDefinitions
    {

        private IPriceService priceService = new Application.Services.PriceService();


        private decimal _calculatedPrice;
        private Exception _exception;
        private DayOfWeek _bookingDay;
        private int _distance;

        [Given("the booking day is {word}")]
        public void GivenTheBookingDayIs(string bookingDay)
        {
            _bookingDay = Enum.Parse<DayOfWeek>(bookingDay);
        }

        [Given("the distance is {int} kilometers")]
        public void GivenTheDistanceIs(int distance)
        {
            _distance = distance;
        }

        [When("the price is calculated")]
        public void WhenThePriceIsCalculated()
        {
            try
            {
                _calculatedPrice = priceService.GetPrice(_bookingDay, _distance);
            }
            catch (Exception ex)
            {
                _exception = ex;
            }
        }

        [Then("the total price should be {decimal}")]
        public void ThenTheTotalPriceShouldBe(decimal expectedPrice)
        {
            Assert.Equal(expectedPrice, _calculatedPrice);
        }

        [Then("an error with message {string} should be thrown")]
        public void ThenAnErrorWithMessageShouldBeThrown(string expectedMessage)
        {
            Assert.NotNull(_exception);
            Assert.IsType<ArgumentException>(_exception);
            Assert.Equal(expectedMessage, _exception.Message);
        }
    }
}

