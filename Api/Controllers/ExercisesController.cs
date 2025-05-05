using Application.Abstracts;
using Application.Abstracts.IServices;
using Application.DTOs;
using Application.DTOs.Exercise;
using Domain.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers;

[Route("api/[controller]")]
[ApiController]
[Authorize]
public class ExercisesController : Controller<ExerciseReadDto, ExerciseCreateDto, ExerciseUpdateDto, Exercise>
{
    public ExercisesController(IService<ExerciseReadDto, ExerciseCreateDto, ExerciseUpdateDto, Exercise> service)
        : base(service)
    {
    }
}