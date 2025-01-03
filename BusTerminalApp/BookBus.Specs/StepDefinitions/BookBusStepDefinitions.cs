using Application.Interfaces;
using Application.Repositories;
using Application.Services;
using BusService.Specs.Mock;
using Data.Models;
using Domain.DTOs;
using Reqnroll;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusService.Specs.StepDefinitions
{

    [Binding]
    public sealed class BookBusStepDefinitions
    {
        private readonly IBusService _busService;
        private MockBusRepository mockRepository = new MockBusRepository();

        private BusDTO _bookedBus { get; set; }
        private Exception _exception;
        private string _busId;
        private DateTime _startTime;
        private DateTime _endTime;

        public BookBusStepDefinitions()
        {
            var mockPriceService = new PriceService(); 
            _busService = new Application.Services.BusService(mockRepository, mockPriceService);
        }

        [Given("the bus ID is {string}")]
        public void GivenTheBusIDIs(string busId)
        {
            _busId = busId;
        }

        [Given("a bus with no booked times exists")]
        public void GivenABusWithNoBookedTimesExists()
        {
            var _mockedBuses = new List<Bus>
            {
                new Bus
                {
                    Id = "123",
                    Name = "Test Bus",
                    Capacity = 50,
                    BookedTimes = new List<DateTime>()
                }
            };
            mockRepository.SetMockData(_mockedBuses);
        }

        [Given("a bus with booked times")]
        public void GivenABusWithBookedTimesExists()
        {
            var _mockedBuses = new List<Bus>
            {
                    new Bus
                    {
                        Id = "123",
                        Name = "Test Bus",
                        Capacity = 50,
                        BookedTimes = new List<DateTime> { DateTime.Now.AddDays(1) }
                    }
            };
            mockRepository.SetMockData(_mockedBuses);
        }

        [Given("the start time is (\\d+) days and (\\d+) hours in the (future|past)")]
        public void GivenTheStartTimeIs(int days, int hours, string direction)
        {
            var modifier = direction == "future" ? 1 : -1;
            _startTime = DateTime.Today
                .AddDays(modifier * days)
                .AddHours(modifier * hours);
        }

        [Given("the end time is (\\d+) days and (\\d+) hours in the (future|past)")]
        public void GivenTheEndTimeIs(int days, int hours, string direction)
        {
            var modifier = direction == "future" ? 1 : -1;
            _endTime = DateTime.Today
                .AddDays(modifier * days)
                .AddHours(modifier * hours);
        }

        [When("I book the bus")]
        public void WhenIBookTheBus()
        {
            try
            {
                _bookedBus = _busService.BookBus(_busId, _startTime, _endTime);
            }
            catch (Exception ex)
            {
                _exception = ex;
            }
        }

        [Then("I should see an error \"(.*)\"")]
        public void ThenIShouldSeeAnError(string errorMessage)
        {
            Assert.NotNull(_exception);
            Assert.Equal(errorMessage, _exception.Message);
        }

        [Then("booking should be successful")]
        public void ThenBookingShouldBeSuccessful()
        {
            Assert.NotNull(_bookedBus);
            Assert.Contains(_startTime.Date, _bookedBus.BookedTimes);
        }

        [Then("the booking should be added to the bus' booked times")]
        public void ThenTheBookingShouldBeAddedToTheBusBookedTimes()
        {
            Assert.Contains(_startTime.Date, _bookedBus.BookedTimes);
        }

    }
}
