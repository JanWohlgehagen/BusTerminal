namespace Application.Interfaces;
using Data.Models;

public interface IBusRepository
{
    public void SetMockData();
    public List<Bus> GetMockData();
}