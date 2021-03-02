using System.Threading.Tasks;
using Application.Dtos;
using Application.Quizzes;
using Domain;
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
        
        [HttpPut]
        public async Task<ActionResult<QuizDto>> EditQuiz(EditQuiz.EditQuizCommand command)
        {
            return await Mediator.Send(command);
        }

        [HttpGet("{token}")]
        public async Task<QuizDto> GetQuizByToken(string token)
        {
            return await Mediator.Send(new GetQuizByToken.GetQuizByTokenQuery {Token = token});
        }
    }
}