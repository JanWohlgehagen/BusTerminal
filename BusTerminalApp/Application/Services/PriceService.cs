using Application.Interfaces;

namespace Application.Services;

public class PriceService : IPriceService
{
    public decimal GetPrice(DayOfWeek bookingDay, int distance)
    {
        if(distance <= 0)
        {
            throw new ArgumentException("We need a distance to calculate the price.");
        }
        var startfee = 500;
        decimal distanceRate = 10m;
        if (bookingDay == DayOfWeek.Saturday)
        {
            startfee = 750;
            distanceRate = 12.5m;
        }
        else if (bookingDay == DayOfWeek.Sunday)
        {
            startfee = 1000;
            distanceRate = 15m;
        }

        return startfee + (distanceRate * distance);
    }
}