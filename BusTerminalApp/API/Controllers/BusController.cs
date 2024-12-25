using Data.DTOs;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

[ApiController]
[Route("[controller]")]
public class BusController : ControllerBase
{
    [HttpGet(Name = "GetAvailableBusses")]
    public IActionResult GetAvailableBusses()
    {
        // Returns a list of BusDTOs
        return Ok("Everything works on my maine...");
    }
    
    [HttpPost(Name = "PostBooking")]
    public IActionResult PostBooking()
    {
        return Ok("You have now booked a bos.");
    }
}