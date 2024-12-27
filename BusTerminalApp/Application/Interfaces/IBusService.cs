using Domain.DTOs;

namespace Application.Interfaces;

public interface IBusService
{
    void ResetAndGenerateMockData();
    List<BusDTO> GetAvailableBuses(DateTime startTime, DateTime endTime, int distance);
    BusDTO BookBus(string busId, DateTime startTime, DateTime endTime);
    DateTime ValidateTime(DateTime startTime, DateTime endTime);
}