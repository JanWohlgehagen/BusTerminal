using Application.Interfaces;
using Application.Services;
using Data.Models;
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

    [HttpPost("ResetAndGenerateMockData")]
    public IActionResult ResetAndGenerateMockData()
    {
         _busService.ResetAndGenerateMockData();
        return Ok("Mock data generated. here is a DateTime to use for testing:   2025-07-21T17:32:28Z   ");
    }

    [HttpGet("GetAvailableBuses")]
    public IActionResult GetAvailableBuses(DateTime startTime, DateTime endTime, int distance)
    {
        List<BusDTO> buses = _busService.GetAvailableBuses(startTime, endTime, distance);
        return Ok(buses);
    }
    
    [HttpPost("PostBooking")]
    public IActionResult PostBooking(string busId, DateTime startTime, DateTime endTime)
    {
        BusDTO bus = _busService.BookBus(busId, startTime, endTime);
        return Ok(bus);
    }
}