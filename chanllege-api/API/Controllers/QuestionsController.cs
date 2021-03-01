using System.Collections.Generic;
using System.Threading.Tasks;
using Application.Dtos;
using Application.Questions;
using BrunoZell.ModelBinding;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    public class QuestionsController : BaseController
    {
        [HttpPost]
        public async Task<ActionResult<QuestionDto>> CreateQuestion(
            [ModelBinder(BinderType = typeof(JsonModelBinder))] 
            CreateQuestions.CreateQuestionsCommand command,
            [FromForm] IList<IFormFile> files)
        {
            command.File = files[0];
            return await Mediator.Send(command);
        }
    }
}