namespace Application.Interfaces;

public interface IPriceService
{
    /// <summary>
    /// Calculates the price of a busride. use Datetime.DayOfWeek and distance must be greater than 0 and in whole kilometers.
    /// </summary>
    /// <param name="bookingDay"></param>
    /// <param name="distance"></param>
    /// <returns></returns>
    public decimal GetPrice(DayOfWeek bookingDay, int distance);
    
}