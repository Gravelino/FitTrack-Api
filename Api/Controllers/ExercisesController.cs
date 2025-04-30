using Application.Abstracts;
using Domain.Entities;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers;

public class ExercisesController : Controller<Exercise>
{
    public ExercisesController(IRepository<Exercise> repository) : base(repository)
    {
    }
}