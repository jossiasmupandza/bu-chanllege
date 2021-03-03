using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using Application.Dtos;
using Application.Errors;
using Application.Helpers;
using Application.Interfaces;
using Domain;
using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Persistence;

namespace Application.Questions
{
    public class CreateQuestions
    {
        public class CreateQuestionsCommand : IRequest<QuestionDto>
        {
            public int QuizId { get; set; }
            public string Title { get; set; }
            public bool Required { get; set; }
            public bool MultipleOptions { get; set; }
            public int InputTypeId { get; set; }
            public IFormFile File { get; set; }
            public string[] Options { get; set; }
        }

        public class CreateQuestionsCommandValidator : AbstractValidator<CreateQuestionsCommand>
        {
            public CreateQuestionsCommandValidator()
            {
                RuleFor(x => x.QuizId).NotEmpty();
                RuleFor(x => x.Required);
                RuleFor(x => x.MultipleOptions);
                RuleFor(x => x.InputTypeId).NotEmpty();
                RuleFor(x => x.Title).NotEmpty();
            }

            public class CreateQuestionCommandHandler : IRequestHandler<CreateQuestionsCommand, QuestionDto>
            {
                private readonly DataContext _context;
                private readonly ICustomMapper _customMapper;
                private readonly IUploadFiles _uploadFiles;
                private readonly IDocumentsUrl _documentsUrl;

                public CreateQuestionCommandHandler(DataContext context, ICustomMapper customMapper, 
                    IUploadFiles uploadFiles, IDocumentsUrl documentsUrl)
                {
                    _context = context;
                    _customMapper = customMapper;
                    _uploadFiles = uploadFiles;
                    _documentsUrl = documentsUrl;
                }
                
                public async Task<QuestionDto> Handle(CreateQuestionsCommand request, CancellationToken cancellationToken)
                {
                    var quiz = await _context.Quizzes
                        .Where(x => x.Id == request.QuizId)
                        .FirstOrDefaultAsync();
                    
                    if (quiz == null)
                    {
                        throw new RestException(HttpStatusCode.NotFound, "Quiz Not Found");
                    }
                    
                    await using var transaction = await _context.Database.BeginTransactionAsync();
                    
                    var inputType = await _context.InputTypes
                        .Where(x => x.Id == request.InputTypeId)
                        .FirstOrDefaultAsync();
                        
                    if (inputType == null)
                    {
                        await transaction.RollbackAsync();
                        throw new RestException(HttpStatusCode.NotFound, "Input Type Not Found");
                    }
                        
                    var question = new Domain.Question  
                    {
                        Title = request.Title,
                        Required = request.Required,
                        MultipleOptions = request.MultipleOptions,
                        Quiz = quiz,
                        InputType = inputType
                    };

                    await _context.Questions.AddAsync(question);
                        
                    if (await _context.SaveChangesAsync() < 1)
                    {
                        await transaction.RollbackAsync();
                        throw new RestException(HttpStatusCode.BadRequest, "Error savig questions changes");
                    }

                    if (request.MultipleOptions && request.Options != null)
                    {
                        foreach (var option in request.Options)
                        {
                            var op = new Option
                            {
                                Description = option,
                                Question = question
                            };

                            await _context.Options.AddAsync(op);
                        }
                        
                        if (await _context.SaveChangesAsync() < 1)
                        {
                            await transaction.RollbackAsync();
                            throw new RestException(HttpStatusCode.BadRequest, "Error savig option changes");
                        }
                    }
                    
                    var document = new Document();
                    if (request.File != null)
                    {
                        document = await _uploadFiles.UploadDocuments(request.File);
                        document.Question = question;

                        await _context.Documents.AddAsync(document);
                        
                        if (await _context.SaveChangesAsync() < 1)
                        {
                            await transaction.RollbackAsync();
                            throw new RestException(HttpStatusCode.BadRequest, "Error savig document changes");
                        }  
                    }

                    await transaction.CommitAsync();

                    // var mapper = _customMapper.GetMapper();
                    // var questionDto = mapper.Map<QuestionDto>(question);
                    
        
                    var documentUrl = new Document();
                    if (request.File != null)
                    {
                        documentUrl = await _documentsUrl.GetDocumentUrl(document, "Questions");
                    }
                    
                    var options = await _context.Options
                        .Where(x => x.Question == question)
                        .Select(x => new OptionDto
                        {
                            Description = x.Description,
                            QuestionId = question.Id
                        })
                        .ToListAsync();

                    return new QuestionDto
                    {
                        Title = question.Title,
                        Required = question.Required,
                        Document = documentUrl,
                        InputType = inputType,
                        MultipleOptions = question.MultipleOptions,
                        Options = options,
                        QuizId = request.QuizId
                    };
                }
            }
        }
        
    }
}