using System.Collections;
using Application.Abstracts.IServices;
using Application.DTOs.Meal;
using Domain.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize]
public class MealController: Controller<MealReadDto, MealCreateDto, MealUpdateDto, Meal>
{
    private readonly IMealService _service;

    public MealController(IMealService service) : base(service)
    {
        _service = service;
    }

    [HttpGet("get-by-userId-and-day/{userId:guid}")]
    public async Task<ActionResult<IEnumerable<MealReadDto>>> GetMealsByUserIdAndDay(Guid userId,[FromQuery] DateTime date)
    {
        var meals = await _service.GetMealsByUserIdAndDayAsync(userId, date);
        if(!meals.Any())
            return NotFound();
        
        return Ok(meals);
    }
}