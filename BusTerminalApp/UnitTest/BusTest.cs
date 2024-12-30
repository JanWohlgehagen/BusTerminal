using Application.Interfaces;
using Application.Services;
using Data.Models;
using Domain.DTOs;
using Moq;

namespace UnitTest
{
    public class BusTest
    {

        #region ValidateTime
        [Theory]
        [InlineData(24, 25)]
        public void ValidTimeTestSucces(int start, int end)
        {
            //Arrange
            var mockpriceService = new Mock<IPriceService>();
            var mockRepo = new Mock<IBusRepository>();
            IBusService busService = new BusService(mockRepo.Object, mockpriceService.Object);
            var starttime = DateTime.Today.AddHours(start);
            var endtime = DateTime.Today.AddHours(end);

            // Act
            var resultDate = busService.ValidateTime(starttime, endtime);

            //Assert
            Assert.Equal(starttime.Date, resultDate);
        }

        [Theory]
        [InlineData(1, 0, "End time cannot be before start time.")]
        [InlineData(0, 24, "Start and end time must be on the same day.")]
        [InlineData(-1, 1, "Start time cannot be in the past.")]
        public void ValidTimeTestFail(int start, int end, string failMessage)
        {
            //Arrange
            var mockpriceService = new Mock<IPriceService>();
            var mockRepo = new Mock<IBusRepository>();
            IBusService busService = new BusService(mockRepo.Object, mockpriceService.Object);
            var starttime = DateTime.Now.AddHours(start);
            var endtime = DateTime.Now.AddHours(end);

            //Act & Assert
            var ex = Assert.Throws<ArgumentException>(() => busService.ValidateTime(starttime, endtime));
            Assert.Equal(ex.Message, failMessage);
        }
        #endregion

        #region GetAvailableBus
        [Theory]
        [MemberData(nameof(TestData.OneUnoccupiedBus), MemberType = typeof(TestData))]
        [MemberData(nameof(TestData.OneBusOccupiedInThreeDays), MemberType = typeof(TestData))]
        public void GetAvailableBusesSucces(List<Bus> busListData, int distance, DateTime bookingDate)
        {
            var mockpriceService = new Mock<IPriceService>();
            var mockRepo = new Mock<IBusRepository>();
            mockRepo.Setup(r => r.GetMockData()).Returns(busListData);
            IBusService busService = new BusService(mockRepo.Object, mockpriceService.Object);


            // Act
            var busList = busService.GetAvailableBuses(bookingDate.AddHours(5), bookingDate.AddHours(10), distance);

            //Assert
            Assert.NotEmpty(busList);
            Assert.Equal(busListData.Count, busList.Count);
        }

        [Theory]
        [MemberData(nameof(TestData.OneBusOccupiedTomorrow), MemberType = typeof(TestData))]
        [MemberData(nameof(TestData.EmptyDbData), MemberType = typeof(TestData))]
        public void GetAvailableBusesEmpty(List<Bus> busListData, int distance, DateTime bookingDate)
        {
            //Arrange
            var mockpriceService = new Mock<IPriceService>();
            var mockRepo = new Mock<IBusRepository>();
            mockRepo.Setup(r => r.GetMockData()).Returns(busListData);
            IBusService busService = new BusService(mockRepo.Object, mockpriceService.Object);


            // Act
            var busList = busService.GetAvailableBuses(bookingDate.AddHours(5), bookingDate.AddHours(10), distance);

            //Assert
            Assert.Empty(busList);
        }
        #endregion


        #region BookBus

        [Theory]
        [MemberData(nameof(TestData.BookAvailableBusWithOccupiedDates), MemberType = typeof(TestData))]
        [MemberData(nameof(TestData.BookAvailableBusWithNoOccupiedDates), MemberType = typeof(TestData))]
        public void BookBusSuccesful(List<Bus> busListData, string id, DateTime starttime, DateTime endtime)
        {
            //Arrange
            var mockpriceService = new Mock<IPriceService>();
            var mockRepo = new Mock<IBusRepository>();
            mockRepo.Setup(r => r.GetMockData()).Returns(busListData);
            IBusService busService = new BusService(mockRepo.Object, mockpriceService.Object);

            //Act 
            var bus = busService.BookBus(id, starttime, endtime);

            //Assert
            Assert.NotNull(bus);
            Assert.Equal(bus.Id, id);
            Assert.IsType<BusDTO>(bus);
        }

        [Theory]
        [MemberData(nameof(TestData.BookBusWrongId), MemberType = typeof(TestData))]
        [MemberData(nameof(TestData.BookBusOnBookedDay), MemberType = typeof(TestData))]
        public void BookBusFail(List<Bus> busListData, string id, DateTime starttime, DateTime endtime, string errorMessage)
        {
            //Arrange
            var mockpriceService = new Mock<IPriceService>();
            var mockRepo = new Mock<IBusRepository>();
            mockRepo.Setup(r => r.GetMockData()).Returns(busListData);
            IBusService busService = new BusService(mockRepo.Object, mockpriceService.Object);

            //Act & Assert
            var ex = Assert.Throws<ArgumentException>( () => busService.BookBus(id, starttime, endtime));
            Assert.Equal(ex.Message, errorMessage);
            #endregion
        }
        }
    }