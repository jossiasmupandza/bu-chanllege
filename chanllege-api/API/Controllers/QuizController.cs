using System.Threading.Tasks;
using Application.Dtos;
using Application.Quiz;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    public class QuizController : BaseController
    {
        [HttpPost]
        public async Task<ActionResult<QuizDto>> CreateQuiz(CreateQuiz.CreateQuizCommand command)
        {
            return await Mediator.Send(command);
        }
    }
}