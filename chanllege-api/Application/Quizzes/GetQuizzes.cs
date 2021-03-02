using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Application.Dtos;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Persistence;

namespace Application.Quizzes
{
    public class GetQuizzes
    {
        public class GetQuizzesQuery : IRequest<List<QuizDto>>
        {
        }

        public class GetQuizzesQueryHandler : IRequestHandler<GetQuizzesQuery, List<QuizDto>>
        {
            public DataContext _context;

            public GetQuizzesQueryHandler(DataContext context)
            {
                _context = context;
            }

            public async Task<List<QuizDto>> Handle(GetQuizzesQuery request, CancellationToken cancellationToken)
            {
                var quiz = await _context.Quizzes
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
                    .ToListAsync();

                return quiz;
            }
        }
    }
}