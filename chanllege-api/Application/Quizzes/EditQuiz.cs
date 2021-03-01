using System;
using System.Linq;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using Application.Dtos;
using Application.Errors;
using Application.Interfaces;
using FluentValidation;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Persistence;

namespace Application.Quizzes
{
    public class EditQuiz
    {
        public class EditQuizCommand : IRequest<QuizDto>
        {
            public int QuizId { get; set; }
            public string Title { get; set; }
            public string Description { get; set; }
            public bool PublicQuiz { get; set; }
            public bool PublicAnswer { get; set; }
            public string NotificationEmail { get; set; }
            public int CategoryId { get; set; }
        }
        
        public class EditQuizCommandValidator : AbstractValidator<EditQuizCommand>
        {
            public EditQuizCommandValidator()
            {
                RuleFor(x => x.QuizId).NotEmpty();
                RuleFor(x => x.Title).NotEmpty();
                RuleFor(x => x.PublicQuiz).NotEmpty();
                RuleFor(x => x.PublicAnswer).NotEmpty();
                RuleFor(x => x.CategoryId).NotEmpty();
            }

            public class EditQuizCommandHandler : IRequestHandler<EditQuizCommand, QuizDto>
            {
                private readonly DataContext _context;
                private readonly ICustomMapper _customMapper;

                public EditQuizCommandHandler(DataContext context, ICustomMapper customMapper)
                {
                    _context = context;
                    _customMapper = customMapper;
                }

                public async Task<QuizDto> Handle(EditQuizCommand request, CancellationToken cancellationToken)
                {
                    var quiz = await _context.Quizzes
                        .Where(x => x.Id == request.QuizId)
                        .FirstOrDefaultAsync();
                    
                    if (quiz == null)
                    {
                        throw new RestException(HttpStatusCode.NotFound, "Quiz Not Found");
                    }
                    
                    var category = await _context.Categories
                        .Where(x => x.Id == request.CategoryId)
                        .FirstOrDefaultAsync();

                    if (category == null)
                    {
                        throw new RestException(HttpStatusCode.NotFound, "Category Not Found");
                    }

                    quiz.Title = request.Title;
                    quiz.Description = request.Description;
                    quiz.UpdatedAt = DateTime.Now;
                    quiz.Category = category;
                    quiz.NotificationEmail = request.NotificationEmail;
                    quiz.PublicAnswer = request.PublicAnswer;
                    quiz.PublicQuiz = request.PublicQuiz;
                    
                    _context.Entry(quiz).State = EntityState.Modified;
                    
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