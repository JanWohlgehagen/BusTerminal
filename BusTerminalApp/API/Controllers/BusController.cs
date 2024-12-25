using Application.Interfaces;
using Application.Services;
using Domain.DTOs;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

[ApiController]
[Route("[controller]")]
public class BusController : ControllerBase
{
    public List<BusDTO>? mockData;

    private readonly IBusService _busService;
    public BusController(IBusService BusService)
    {
        _busService = BusService;
    }

    [HttpPost(Name = "ResetAndGenerateMockData")]
    public IActionResult ResetAndGenerateMockData()
    {
         _busService.ResetAndGenerateMockData();
        return Ok("Mock data generated.");
    }

    [HttpGet(Name = "GetAvailableBuses")]
    public IActionResult GetAvailableBuses(DateTime startTime, DateTime endTime, int distance)
    {
        _busService.GetAvailableBuses(startTime, endTime, distance);
        return Ok("Available buses fetched.");
    }
    
    [HttpPost(Name = "PostBooking")]
    public IActionResult PostBooking(string busId, DateTime startTime, DateTime endTime)
    {
        BusDTO bus = _busService.BookBus(busId, startTime, endTime);
        return Ok(bus);
    }
}