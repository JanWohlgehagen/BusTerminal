using Application.Interfaces;
using Reqnroll;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusService.Specs.StepDefinitions
{
    [Binding]
    public sealed class ValidateTimeStepDefinitions
    {
        private readonly IBusService _busService;
        private DateTime _startTime;
        private DateTime _endTime;
        private DateTime _result;
        private Exception _exception;

        public ValidateTimeStepDefinitions()
        {
            _busService = new Application.Services.BusService(null, null); // Mocked dependencies
        }
        [Given("the start time is (\\d+) days and (\\d+) hours in the (future|past)")]
        public void GivenTheStartTimeIsRelative(int days, int hours, string direction)
        {
            var modifier = direction == "future" ? 1 : -1;
            _startTime = DateTime.Today
                .AddDays(modifier * days)
                .AddHours(modifier * hours);
        }

        [Given("the end time is (\\d+) days and (\\d+) hours in the (future|past)")]
        public void GivenTheEndTimeIsRelative(int days, int hours, string direction)
        {
            var modifier = direction == "future" ? 1 : -1;
            _endTime = DateTime.Today
                .AddDays(modifier * days)
                .AddHours(modifier * hours);
        }

        [When("the time is validated")]
        public void WhenTheTimeIsValidated()
        {
            try
            {
                _result = _busService.ValidateTime(_startTime, _endTime);
            }
            catch (Exception ex)
            {
                _exception = ex;
            }
        }

        [Then("the result should be the date (\\d+) days in the (future|past)")]
        public void ThenTheResultShouldBeRelative(int days, string direction)
        {
            var modifier = direction == "future" ? 1 : -1;
            Assert.Equal(DateTime.Today.AddDays(modifier * days).Date, _result);
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
