using Application.Interfaces;
using Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusService.Specs.Mock
{
    public class MockBusRepository : IBusRepository
    {
        private List<Bus> _buses = new List<Bus>();

        public void SeedBus(string busId, bool isBooked, DateTime? bookingDate = null)
        {
            var bus = new Bus
            {
                Id = busId,
                Name = "Mock Bus",
                Capacity = 50,
                BookedTimes = isBooked && bookingDate.HasValue ? new List<DateTime> { bookingDate.Value } : new List<DateTime>()
            };

            _buses.Add(bus);
        }

        public List<Bus> GetMockData()
        {
            return _buses;
        }
        public void SetMockData(List<Bus> data)
        {
            _buses = data;
        }
        public void SetMockData()
        {
            throw new NotImplementedException();
        }
    }
}
