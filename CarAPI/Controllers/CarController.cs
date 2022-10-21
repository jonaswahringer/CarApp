using Microsoft.AspNetCore.Mvc;
using CarAPI.Models;
using System.Linq;
using CarAPI.Database;
using Microsoft.EntityFrameworkCore;
using System.Drawing;
using System;

namespace CarAPI.Controllers;

[ApiController]
[Route("api/[controller]")]
public class CarController : ControllerBase
{

    private readonly ILogger<CarController> _logger;
    private readonly CarContext _carContext;
    
    public CarController(ILogger<CarController> logger, CarContext carContext)
    {
        _logger = logger;
        _carContext = carContext;
    }

    [HttpGet]
    public async Task<List<Car>> GetAllCars()
    {
        return (from data in _carContext.Cars.Include(t => t.Telemetries) select data).ToList();
    }

    [HttpGet("{id}/telemetry")]
    public async Task<List<Telemetry>> GetTelemetryByCarId(int id)
    {
        return await _carContext.Telemetries.Where(t => t.CarId == id).ToListAsync();
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<Car>> GetCarById(int id)
    {
        var car = await _carContext.Cars.Include(c => c.Telemetries).FirstOrDefaultAsync(c => c.IdCar == id);
        if (car == null)
        {
            return NotFound();
        }
        return car;
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> PutCar(int id, Car car)
    {
        if (id != car.IdCar)
        {
            return BadRequest();
        }

        var entity = _carContext.Cars.FirstOrDefault(item => item.IdCar == id);
        if (entity != null)
        {
            entity.Name = car.Name;
            entity.Type = car.Type;
            entity.ModifiedAt = DateTime.Now;
        }

        //_carContext.Entry(car).State = EntityState.Modified

        try
        {
            await _carContext.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!CarExists(id).Result)
            {
                return NotFound();
            }
            else
            {
                throw;
            }
        }

        return NoContent();
    }

    [HttpPost]
    public async Task<ActionResult<Car>> PostCar(Car car)
    {
        _carContext.Cars.Add(car);
        await _carContext.SaveChangesAsync();
        return CreatedAtAction("GetCarById", new { id = car.IdCar }, car);
    }

    private async Task<bool> CarExists(int id)
    {
        var car = await _carContext.Cars.FindAsync(id);
        if(car == null)
        {
            return false;
        }
        return true;
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult<Car>> DeleteCarById(int id)
    {
        var carToDelete = await _carContext.Cars.FindAsync(id);
        if (carToDelete == null)
        {
            return NotFound();
        }
        _carContext.Cars.Remove(carToDelete);
        await _carContext.SaveChangesAsync();
        return NoContent();
    }
}

