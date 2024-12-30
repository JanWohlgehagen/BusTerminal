using Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnitTest
{
    public static class TestData
    {
        #region GetAvailableBusData
        public static List<Bus> EmptyBusList
        {
            get
            {
                return new List<Bus>();
            }

        }

        public static IEnumerable<object[]> OneUnoccupiedBus
        {
            get
            {
                yield return new object[]
                {
                    new List<Bus>
                    {
                        new Bus
                        {
                            Id = "1",
                            BookedTimes = new List<DateTime>(),
                            Capacity = 42,
                            Name = "Big Green"
                        }
                    },
                    10,
                    DateTime.Today.AddDays(1)
                };
            }
        }
        public static IEnumerable<object[]> OneBusOccupiedInThreeDays
        {
            get
            {
                yield return new object[]
                {
                    new List<Bus>
                    {
                        new Bus
                        {
                            Id = "1",
                            BookedTimes = new List<DateTime>(){ DateTime.Today.AddDays(3).AddHours(14)},
                            Capacity = 42,
                            Name = "Big Green"
                        }
                    },
                    10,
                    DateTime.Today.AddDays(1)
                };
            }
        }
        public static IEnumerable<object[]> OneBusOccupiedTomorrow
        {
            get
            {
                yield return new object[]
                {
                    new List<Bus>
                    {
                        new Bus
                        {
                            Id = "1",
                            BookedTimes = new List<DateTime>(){ DateTime.Today.AddDays(1).AddHours(14)},
                            Capacity = 42,
                            Name = "Big Green"
                        }
                    },
                    10,
                    DateTime.Today.AddDays(1)
                };
            }
        }
        public static IEnumerable<object[]> InvaidDateGetAvailableBus
        {
            get
            {
                yield return new object[]
                {
                    new List<Bus>
                    {
                        new Bus
                        {
                            Id = "1",
                            BookedTimes = new List<DateTime>(){ DateTime.Today.AddDays(1).AddHours(14)},
                            Capacity = 42,
                            Name = "Big Green"
                        }
                    },
                    10,
                    DateTime.Today.AddDays(-1),"Start time cannot be in the past."
                };
            }
        }

        public static IEnumerable<object[]> EmptyDbData
        {
            get
            {
                yield return new object[]
                {
                    new List<Bus>
                    {
                    },
                    10,
                    DateTime.Today.AddDays(1)
                };
            }
        }
        #endregion

        #region BookBusData
        public static IEnumerable<object[]> BookAvailableBusWithOccupiedDates
        {
            get
            {
                yield return new object[]
                {
                    new List<Bus>
                    {
                        new Bus
                        {
                            Id = "1",
                            BookedTimes = new List<DateTime>(){ DateTime.Today.AddDays(3).AddHours(14)},
                            Capacity = 42,
                            Name = "Big Green"
                        }
                    },
                    "1",
                    DateTime.Today.AddDays(1).AddHours(1),
                    DateTime.Today.AddDays(1).AddHours(2),
                };
            }
        }

        public static IEnumerable<object[]> BookAvailableBusWithNoOccupiedDates
        {
            get
            {
                yield return new object[]
                {
                    new List<Bus>
                    {
                        new Bus
                        {
                            Id = "1",
                            BookedTimes = new List<DateTime>(){},
                            Capacity = 42,
                            Name = "Big Green"
                        }
                    },
                    "1",
                    DateTime.Today.AddDays(1).AddHours(1),
                    DateTime.Today.AddDays(1).AddHours(2),
                };
            }
        }

        public static IEnumerable<object[]> BookBusWrongId
        {
            get
            {
                yield return new object[]
                {
                    new List<Bus>
                    {
                        new Bus
                        {
                            Id = "1",
                            BookedTimes = new List<DateTime>(){ DateTime.Today.AddDays(3).AddHours(14)},
                            Capacity = 42,
                            Name = "Big Green"
                        }
                    },
                    "-1",
                    DateTime.Today.AddDays(1).AddHours(1),
                    DateTime.Today.AddDays(1).AddHours(2),
                    "Bus not found."
                };
            }
        }

        public static IEnumerable<object[]> BookBusOnBookedDay
        {
            get
            {
                yield return new object[]
                {
                    new List<Bus>
                    {
                        new Bus
                        {
                            Id = "1",
                            BookedTimes = new List<DateTime>(){ DateTime.Today.AddDays(1).AddHours(14)},
                            Capacity = 42,
                            Name = "Big Green"
                        }
                    },
                    "1",
                    DateTime.Today.AddDays(1).AddHours(1),
                    DateTime.Today.AddDays(1).AddHours(2),
                    "Bus is already booked at that time."
                };
            }
        }

        #endregion
    }
}
