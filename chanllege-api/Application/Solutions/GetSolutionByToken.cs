using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using Application.Dtos;
using Application.Errors;
using Domain;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Persistence;

namespace Application.Solutions
{
    public class GetSolutions
    {
        public class GetSolutionBytokenQuery : IRequest<List<object>>
        {
            public string Token { get; set; }
        }
        
        public class GetSolutionBytokenQueryHandler : IRequestHandler<GetSolutionBytokenQuery, List<Object>>
        {
            private readonly DataContext _context;

            public GetSolutionBytokenQueryHandler(DataContext context)
            {
                _context = context;
            }
            
            public async Task<List<object>> Handle(GetSolutionBytokenQuery request, CancellationToken cancellationToken)
            {
                var quiz = await _context.Quizzes
                    .Where(x => x.QuizToken == request.Token || x.EditToken == request.Token)
                    .FirstOrDefaultAsync();
 
                if (quiz == null)
                {
                    throw new RestException(HttpStatusCode.NotFound, "Quiz Not Found");
                }

                var solutions = await _context.Solutions
                    .Where(x => x.Quiz == quiz)
                    .ToListAsync();
                    
                if (solutions == null)
                {
                    throw new RestException(HttpStatusCode.NotFound, "These quiz has no solutions");
                }

                var allSolutionsData = new List<Object>();
                
                foreach (var solution in solutions)
                {
                    var answers = await _context.Answers
                        .Where(x => x.Solution == solution)
                        .Include(x => x.Question)
                        .Select( x => new AnswerOptionsDto
                        {
                            AnswerId = x.Id,
                            CreatedAt = solution.CreatedAt,
                            Description = x.Description,
                            SolutionId = solution.Id,
                            QuestionId = x.Question.Id
                        })
                        .ToListAsync();

                    foreach (var answer in answers)
                    {
                        var optionsId = await _context.AnswerOptions
                            .Where(x => x.Answer.Id == answer.AnswerId)
                            .Select(x => x.Option.Id)
                            .ToListAsync();

                        answer.OptionsId = optionsId;
                    }
                    
                    allSolutionsData.Add(answers);
                }

                return allSolutionsData;
            }
        }
    }
}