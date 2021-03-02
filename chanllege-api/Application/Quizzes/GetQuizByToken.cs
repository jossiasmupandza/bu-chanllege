using System.Linq;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using Application.Dtos;
using Application.Errors;
using Application.Interfaces;
using Domain;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Persistence;

namespace Application.Quizzes
{
    public class GetQuizByToken
    {
        public class GetQuizByTokenQuery : IRequest<QuizDto>
        {
            public string Token { get; set; }
        }

        public class GetQuizByTokenQueryHandler : IRequestHandler<GetQuizByTokenQuery, QuizDto>
        {
            public DataContext _context;
            public IDocumentsUrl _documentsUrl;
            private readonly ICustomMapper _customMapper;

            public GetQuizByTokenQueryHandler(DataContext context, IDocumentsUrl documentsUrl, ICustomMapper mapper)
            {
                _context = context;
                _documentsUrl = documentsUrl;
                _customMapper = mapper;
            }

            public async Task<QuizDto> Handle(GetQuizByTokenQuery request, CancellationToken cancellationToken)
            {
                var quiz = await _context.Quizzes
                    .Where(x => x.QuizToken == request.Token || x.EditToken == request.Token)
                    .Include(x => x.Category)
                    .Select(x => new QuizDto
                    {
                        Id = x.Id,
                        Category = x.Category.Description,
                        CreatedAt = x.CreatedAt,
                        UpdatedAt = x.UpdatedAt,
                        Description = x.Description,
                        QuizToken = x.QuizToken,
                        NotificationEmail = x.NotificationEmail,
                        Title = x.Title,
                        PublicAnswer = x.PublicAnswer,
                        PublicQuiz = x.PublicQuiz
                    })
                    .FirstOrDefaultAsync();
 
                if (quiz == null)
                {
                    throw new RestException(HttpStatusCode.NotFound, "Quiz Not Found");
                }

                var questions = await _context.Questions
                    .Where(x => x.Quiz.Id == quiz.Id)
                    .Include(x => x.Documents)
                    .Include(x => x.InputType)
                    .Select(x => new QuestionDto
                    {
                        Id = x.Id,
                        QuizId = quiz.Id,
                        InputType = x.InputType,
                        MultipleOptions = x.MultipleOptions,
                        Required = x.Required,
                        Title = x.Title
                    })
                    .ToListAsync();

                foreach (var question in questions)
                {
                    var document = new Document();
                    document = await _context.Documents
                        .Where(x => x.Question.Id == question.Id)
                        .FirstOrDefaultAsync();

                    if (document != null)
                    {
                        document = await _documentsUrl.GetDocumentUrl(document, "Questions");  
                    }
                    
                    question.Document = document;

                    var options = await _context.Options
                        .Where(x => x.Question.Id == question.Id)
                        .Select(x => new OptionDto
                            {
                                Id = x.Id,
                                Description = x.Description,
                            })
                        .ToListAsync();

                    question.Options = options;
                }
                
                // var mapper = _customMapper.GetMapper();
                // var quizDto = mapper.Map<QuizDto>(quiz);

                quiz.Questions = questions;
                
                return quiz;
            }
        }
    }
}