using Application.Interfaces;
using Data.Models;
using Domain.DTOs;

namespace Application.Services;

public class BusService : IBusService
{
    private readonly IBusRepository _busRepository;
    private readonly IPriceService _priceService;

    public BusService(IBusRepository busRepository, IPriceService priceService)
    {
        _busRepository = busRepository;
        _priceService = priceService;
    }

    public BusDTO BookBus(string busId, DateTime startTime, DateTime endTime)
    {
        DateTime bookingDate = ValidateTime(startTime, endTime);

        Bus bus = _busRepository.GetMockData().Find(b => b.Id == busId) ?? throw new ArgumentException("Bus not found.");

        foreach (var time in bus.BookedTimes)
        {
            if (time.Date == bookingDate)
            {
                throw new ArgumentException("Bus is already booked at that time.");
            }
        }

            bus.BookedTimes.Add(bookingDate);

            return new BusDTO
            {
                Id = bus.Id,
                Name = bus.Name,
                Capacity = bus.Capacity,
                BookedTimes = bus.BookedTimes
            };
    }

    public List<BusDTO> GetAvailableBuses(DateTime startTime, DateTime endTime, int distance)
    {
        var allBuses = _busRepository.GetMockData();
        var availableBuses = new List<BusDTO>();

        var price = _priceService.GetPrice(startTime.DayOfWeek, distance);

        var bookingDate = ValidateTime(startTime, endTime);

        foreach (var bus in allBuses)
        {
            if (bus.BookedTimes.Count == 0)
            {
                availableBuses.Add(new BusDTO
                {
                    Id = bus.Id,
                    Name = bus.Name,
                    Capacity = bus.Capacity,
                    Price = price
                });
            }
            else
            {
                var isAvailable = true;
                foreach (var time in bus.BookedTimes)
                {
                    if (time.Date == bookingDate)
                    {
                        isAvailable = false;
                        break;
                    }
                }

                if (isAvailable)
                {
                    availableBuses.Add(new BusDTO
                    {
                        Id = bus.Id,
                        Name = bus.Name,
                        Capacity = bus.Capacity,
                        Price = price
                    });
                }
            }
        }

        return availableBuses;
    }

    public DateTime ValidateTime(DateTime startTime, DateTime endTime)
    {
        if (endTime < startTime)
        {
            throw new ArgumentException("End time cannot be before start time.");
        }

        if (endTime.Date != startTime.Date)
        {
            throw new ArgumentException("Start and end time must be on the same day.");
        }

        if (startTime < DateTime.Now)
        {
            throw new ArgumentException("Start time cannot be in the past.");
        }

        return startTime.Date;
    }

    public void ResetAndGenerateMockData()
    {
        _busRepository.SetMockData();
    }
}