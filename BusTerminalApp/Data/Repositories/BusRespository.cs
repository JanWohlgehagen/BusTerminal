using Application.Interfaces;
using Data.Models;

namespace Application.Repositories;

public class BusRespository: IBusRepository
{
    private List<Bus> _mockData = new List<Bus>();

    public void SetMockData()
    {
         _mockData = new List<Bus>
        {
            new Bus { Id = "1", Name = "Bus Arnold", Capacity = 10, BookedTimes = new List<DateTime>() },
            new Bus { Id = "2", Name = "Bus Bob", Capacity = 40, BookedTimes = new List<DateTime>() },
            new Bus { Id = "3", Name = "Bus Connery", Capacity = 30, BookedTimes = new List<DateTime>() },
            new Bus { Id = "4", Name = "Bus Drew", Capacity = 70, BookedTimes = new List<DateTime>() },
        };
    }

    public List<Bus> GetMockData()
    {
        SetMockData();
        return _mockData;
    }

}