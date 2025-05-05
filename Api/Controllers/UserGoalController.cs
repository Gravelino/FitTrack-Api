using Application.Abstracts.IServices;
using Application.DTOs.UserGoal;
using Domain.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize]
public class UserGoalController : Controller<UserGoalCreateDto, UserGoalReadDto, UserGoalUpdateDto, UserGoal>
{
    public UserGoalController(IService<UserGoalCreateDto, UserGoalReadDto, UserGoalUpdateDto, UserGoal> service) : base(service)
    {
    }
}