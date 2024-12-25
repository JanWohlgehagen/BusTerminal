namespace Data.Models;

public class Bus
{
    private string Id { get; set; }
    private List<DateTime> BookedTimes { get; set; }
    private string Name { get; set; }
    private int Capacity { get; set; }
}