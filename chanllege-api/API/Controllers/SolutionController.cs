﻿using System.Collections.Generic;
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

        [HttpGet("{token}")]
        public async Task<List<object>> GetTSolutions(string token)
        {
            return await Mediator.Send(new GetSolutions.GetSolutionBytokenQuery{Token = token});
        }
    }
}