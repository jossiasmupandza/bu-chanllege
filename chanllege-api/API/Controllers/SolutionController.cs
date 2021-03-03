using System.Collections.Generic;
using System.Threading.Tasks;
using Application.Dtos;
using Application.Solutions;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    public class SolutionController : BaseController
    {
        [HttpPost]
        public async Task<ActionResult<List<AnswerOptionsDto>>> CreateSolution(CreateSolution.CreateSolutionCommand command)
        {
            return await Mediator.Send(command);
        }
    }
}