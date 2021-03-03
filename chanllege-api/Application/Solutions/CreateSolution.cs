using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using Application.Dtos;
using Application.Errors;
using Application.Questions;
using Domain;
using FluentValidation;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Persistence;

namespace Application.Solutions
{
    public class CreateSolution
    {
        public class CreateSolutionCommand : IRequest<List<AnswerOptionsDto>>
        {
            public int QuizId { get; set; }
            public List<AnswerOptionsDto> AnswerOptions { get; set; }
        }
        
        public class CreateSolutionCommandValidator : AbstractValidator<CreateSolutionCommand>
        {
            public CreateSolutionCommandValidator()
            {
                RuleFor(x => x.AnswerOptions).NotEmpty();
                RuleFor(x => x.QuizId).NotEmpty();
            }

            public class CreateSolutionCommandHandler : IRequestHandler<CreateSolutionCommand, List<AnswerOptionsDto>>
            {
                public DataContext _context;

                public CreateSolutionCommandHandler(DataContext context)
                {
                    _context = context;
                }

                public async Task<List<AnswerOptionsDto>> Handle(CreateSolutionCommand request, CancellationToken cancellationToken)
                {
                    var quiz = await _context.Quizzes
                        .Where(x => x.Id == request.QuizId)
                        .FirstOrDefaultAsync();

                    if (quiz == null)
                    {
                        throw new RestException(HttpStatusCode.NotFound, "Quiz Not Found");
                    }

                    var solution = new Solution
                    {
                        Quiz = quiz,
                        CreatedAt = DateTime.Now
                    };
                
                    await using var transaction = await _context.Database.BeginTransactionAsync();

                    try
                    {
                        await _context.Solutions.AddAsync(solution);
                
                        if (await _context.SaveChangesAsync() < 1)
                        {
                            await transaction.RollbackAsync();
                            throw new RestException(HttpStatusCode.BadRequest, "Error savig solution changes");
                        }

                        foreach (var answerOptions in request.AnswerOptions)
                        {
                            var question = await _context.Questions
                                .Where(x => x.Id == answerOptions.QuestionId)
                                .FirstOrDefaultAsync();

                            if (question == null)
                            {
                                await transaction.RollbackAsync();
                                throw new RestException(HttpStatusCode.NotFound, "Error question not found");
                            }
                        
                            var answer = new Answer();

                            if (answerOptions.Description != null)
                                answer.Description = answerOptions.Description;
                        
                            answer.Solution = solution;
                            answer.Question = question;

                            _context.Answers.AddAsync(answer);
                        
                            if (await _context.SaveChangesAsync() < 1)
                            {
                                await transaction.RollbackAsync();
                                throw new RestException(HttpStatusCode.BadRequest, "Error savig solution changes");
                            }

                            answerOptions.AnswerId = answer.Id;
                            if (answerOptions.OptionsId != null)
                            {
                                foreach (var optionId in answerOptions.OptionsId)
                                {
                                    var option = await _context.Options
                                        .Where(x => x.Id == optionId)
                                        .FirstOrDefaultAsync();
                                
                                    if (option ==null)
                                    {
                                        await transaction.RollbackAsync();
                                        throw new RestException(HttpStatusCode.NotFound, "Error invalid option changes");
                                    }

                                    await _context.AnswerOptions.AddAsync(new AnswerOption
                                    {
                                        Answer = answer,
                                        Option = option
                                    });
                                }
                            
                                if (await _context.SaveChangesAsync() < 1)
                                {
                                    await transaction.RollbackAsync();
                                    throw new RestException(HttpStatusCode.BadRequest, "Error savig answer option changes changes");
                                }
                            }

                            answerOptions.SolutionId = solution.Id;
                            answerOptions.CreatedAt = solution.CreatedAt;
                            
                        }

                        await transaction.CommitAsync();
                        
                        return request.AnswerOptions;
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine(e);
                        throw;
                    }
                    
                }
            }
        }
    }
}