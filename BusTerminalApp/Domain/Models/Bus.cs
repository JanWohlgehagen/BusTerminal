namespace Data.Models;

public class Bus
{
    public string Id { get; set; }
    public List<DateTime> BookedTimes { get; set; }
    public string Name { get; set; }
    public int Capacity { get; set; }
}