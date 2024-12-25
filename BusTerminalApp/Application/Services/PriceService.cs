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
        decimal distanceRate = 10;
        if (bookingDay == DayOfWeek.Saturday)
        {
            startfee = 750;
            distanceRate = 12.5m;
        }
        else if (bookingDay == DayOfWeek.Sunday)
        {
            startfee = 1000;
            distanceRate = 15;
        }

        return startfee + (distanceRate * distance);
    }
}