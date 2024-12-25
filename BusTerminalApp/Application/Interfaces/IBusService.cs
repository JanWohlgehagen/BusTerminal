namespace Application.Interfaces;

public interface IBusService
{
    Task<IEnumerable<BusDTO>> GetBusesAsync();
    Task<BusDTO> GetBusByIdAsync(string id);
    Task<BusDTO> CreateBusAsync(BusDTO bus);
    Task<BusDTO> UpdateBusAsync(string id, BusDTO bus);
    Task<BusDTO> DeleteBusAsync(string id);
}