namespace Data.DTOs;

public class BusDTO
{
    private string Id { get; set; }
    private List<DateTime> BookedTimes { get; set; }
    private string Name { get; set; }
    private int Capacity { get; set; }
    private decimal Price { get; set; }
    
}