using Application.Interfaces;
using Application.Repositories;
using Application.Services;
using BusService.Specs.Mock;
using Data.Models;
using Domain.DTOs;
using Reqnroll;
using System;
using System.Collections.Generic;
using Xunit;

namespace BusService.Specs.StepDefinitions
{
    [Binding]
    public sealed class GetAvailableBusesStepDefinitions
    {
        private readonly IBusService _busService;
        private MockBusRepository mockRepository = new MockBusRepository();
        private Exception _exception;
        private DateTime _startTime;
        private DateTime _endTime;
        private int _distance;
        private List<BusDTO> _availableBuses;

        public GetAvailableBusesStepDefinitions()
        {
            var mockPriceService = new PriceService();
            _busService = new Application.Services.BusService(mockRepository, mockPriceService);
        }

        [Given("the distance is {int} kilometers")]
        public void GivenTheDistanceIs(int distance)
        {
            _distance = distance;
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
                    BookedTimes =  new List<DateTime> { DateTime.Today.AddDays(1) }
                }
            };
            mockRepository.SetMockData(_mockedBuses);
        }

        [When("I search for available buses")]
        public void WhenISearchForAvailableBuses()
        {
            try
            {
                _availableBuses = _busService.GetAvailableBuses(_startTime, _endTime, _distance);
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

        [Then("I should see a list of available buses")]
        public void ThenIShouldSeeAListOfAvailableBuses()
        {
            Assert.NotNull(_availableBuses);
            Assert.NotEmpty(_availableBuses);
        }
        [Then("I should see an empty list")]
        public void ThenIShouldSeeAnEmptyList()
        {
            Assert.NotNull(_availableBuses);
            Assert.Empty(_availableBuses);
        }

        [Then("each bus should have a price")]
        public void ThenEachBusShouldHaveAPrice()
        {
            Assert.All(_availableBuses, bus => Assert.NotNull(bus.Price));
        }

    }
}
