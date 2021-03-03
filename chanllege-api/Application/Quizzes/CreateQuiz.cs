using System;
using System.Linq;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using Application.Dtos;
using Application.Interfaces;
using Application.Errors;
using FluentValidation;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Persistence;

namespace Application.Quizzes
{
    public class CreateQuiz 
    {
        public class CreateQuizCommand : IRequest<QuizDto>
        {
            public string Title { get; set; }
            public string Description { get; set; }
            public bool PublicQuiz { get; set; }
            public bool PublicAnswer { get; set; }
            public string NotificationEmail { get; set; }
            public int CategoryId { get; set; }
        }
        
        public class CreateQuizCommandValidator : AbstractValidator<CreateQuizCommand>
        {
            public CreateQuizCommandValidator()
            {
                RuleFor(x => x.Title).NotEmpty();
                RuleFor(x => x.PublicQuiz);
                RuleFor(x => x.PublicAnswer);
                RuleFor(x => x.CategoryId).NotEmpty();
            }
            
            public class CreateQuizCommandHandler : IRequestHandler<CreateQuizCommand, QuizDto>
            {
                private readonly DataContext _context;
                private readonly IRandomStringGenerator _stringGenerator;
                private readonly ICustomMapper _customMapper;

                public CreateQuizCommandHandler(DataContext context, IRandomStringGenerator stringGenerator, 
                    ICustomMapper customMapper)
                {
                    _context = context;
                    _stringGenerator = stringGenerator;
                    _customMapper = customMapper;
                }
                
                public async Task<QuizDto> Handle(CreateQuizCommand request, CancellationToken cancellationToken)
                {
                    var category = await _context.Categories
                        .Where(x => x.Id == request.CategoryId)
                        .FirstOrDefaultAsync();

                    if (category == null)
                    {
                        throw new RestException(HttpStatusCode.NotFound, "Category Not Found");
                    }
                    
                    var quizToken = "quiz-" + _stringGenerator.RamdomString(25);

                    var quizWithToken = await _context.Quizzes
                        .Where(x => x.QuizToken == quizToken)
                        .FirstOrDefaultAsync();

                    while (quizWithToken != null)
                    {
                        quizToken = "quiz-" + _stringGenerator.RamdomString(25);

                        quizWithToken = await _context.Quizzes
                            .Where(x => x.QuizToken == quizToken)
                            .FirstOrDefaultAsync();
                    }

                    var editToken = "edit-" + _stringGenerator.RamdomString(25);

                    var quizWithEditToken = await _context.Quizzes
                        .Where(x => x.EditToken == editToken)
                        .FirstOrDefaultAsync();

                    while (quizWithEditToken != null)
                    {
                        editToken = "edit-" + _stringGenerator.RamdomString(25);

                        quizWithEditToken = await _context.Quizzes
                            .Where(x => x.EditToken == editToken)
                            .FirstOrDefaultAsync();
                    }

                    var quiz = new Domain.Quiz
                    {
                        Title   = request.Title,
                        Description = request.Description,
                        CreatedAt = DateTime.Now,
                        Category = category,
                        EditToken = editToken,
                        QuizToken = quizToken,
                        NotificationEmail = request.NotificationEmail,
                        PublicAnswer = request.PublicAnswer,
                        PublicQuiz = request.PublicQuiz
                    };

                    await _context.Quizzes.AddAsync(quiz);

                    if (await _context.SaveChangesAsync() < 1)
                    {
                        throw new RestException(HttpStatusCode.BadRequest, "Error savig changes");
                    }
                    
                    var mapper = _customMapper.GetMapper();
                    var quizDto = mapper.Map<QuizDto>(quiz);
                    quizDto.Category = category.Description;

                    return quizDto;
                }
            }
        }
    }
}