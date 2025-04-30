using Application.Abstracts;
using Domain.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers;

[Route("api/[controller]")]
[ApiController]
[Authorize]
public class ExercisesController : Controller<Exercise>
{
    public ExercisesController(IRepository<Exercise> repository) : base(repository)
    {
    }
}