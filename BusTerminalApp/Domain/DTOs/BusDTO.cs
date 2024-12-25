namespace Domain.DTOs;

public class BusDTO
{
    public string Id { get; set; }
    public List<DateTime> BookedTimes { get; set; }
    public string Name { get; set; }
    public int Capacity { get; set; }
    public decimal? Price { get; set; }
    
}