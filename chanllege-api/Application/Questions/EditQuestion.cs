using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using Application.Dtos;
using Application.Errors;
using Application.Interfaces;
using Domain;
using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Persistence;

namespace Application.Questions
{
    public class EditQuestion
    {
        public class EditQuestionCommand : IRequest<QuestionDto>
        {
            
            public int QuestionId { get; set; }
            public string Title { get; set; }
            public bool Required { get; set; }
            public bool MultipleOptions { get; set; }
            public int InputTypeId { get; set; }
            public IFormFile File { get; set; }
            public int DocumentId { get; set; }
            public List<Option> Options { get; set; }
        }
        
        public class EditQuestionCommaandValidator : AbstractValidator<EditQuestionCommand>
        {
            public EditQuestionCommaandValidator()
            {
                RuleFor(x => x.QuestionId).NotEmpty();
                RuleFor(x => x.Required);
                RuleFor(x => x.MultipleOptions);
                RuleFor(x => x.InputTypeId).NotEmpty();
                RuleFor(x => x.Title).NotEmpty();
            }
            
            public class EditQuestionCommandHandler : IRequestHandler<EditQuestionCommand, QuestionDto>
            {
                private readonly DataContext _context;
                private readonly ICustomMapper _customMapper;
                private readonly IUploadFiles _uploadFiles;
                private readonly IDocumentsUrl _documentsUrl;

                public EditQuestionCommandHandler(DataContext context, ICustomMapper customMapper, 
                    IUploadFiles uploadFiles, IDocumentsUrl documentsUrl)
                {
                    _context = context;
                    _customMapper = customMapper;
                    _uploadFiles = uploadFiles;
                    _documentsUrl = documentsUrl;
                }

                public async Task<QuestionDto> Handle(EditQuestionCommand request, CancellationToken cancellationToken)
                {
                    var question = await _context.Questions
                        .Where(x => x.Id == request.QuestionId)
                        .FirstOrDefaultAsync();

                    if (question == null)
                    {
                        throw new RestException(HttpStatusCode.NotFound, "Question Not Found");
                    }
                    
                    var inputType = await _context.InputTypes
                        .Where(x => x.Id == request.InputTypeId)
                        .FirstOrDefaultAsync();
                        
                    if (inputType == null)
                    {
                        throw new RestException(HttpStatusCode.NotFound, "Input Type Not Found");
                    }
                    
                    await using var transaction = await _context.Database.BeginTransactionAsync();

                    question.Title = request.Title;
                    question.Required = request.Required;
                    question.MultipleOptions = request.MultipleOptions;
                    question.InputType = inputType;
                    
                    var document = new Document();
                    string documentUrl = "";
                    
;                    if (request.File != null)
                    {
                        if (request.DocumentId != null && request.DocumentId > 0)
                        {
                            document = await _context.Documents
                                .Where(x => x.Id == request.DocumentId)
                                .FirstOrDefaultAsync();
                            
                            if (document == null)
                            {
                                throw new RestException(HttpStatusCode.NotFound, "document Not Found");
                            }

                            await _uploadFiles.RemoveDocuments(document);
                            var newDocument = await _uploadFiles.UploadDocuments(request.File);
                            document.Name = newDocument.Name;
                            document.Url = newDocument.Url;
                            
                        }
                        else
                        {
                            document = await _uploadFiles.UploadDocuments(request.File);
                            document.Question = question;

                            await _context.Documents.AddAsync(document); 
                        }
                        
                        if (await _context.SaveChangesAsync() < 1)
                        {
                            await transaction.RollbackAsync();
                            throw new RestException(HttpStatusCode.BadRequest, "Error savig document changes");
                        } 
                        
                        document = await _documentsUrl.GetDocumentUrl(document, "Questions");
                    }

                    var options = new List<OptionDto>();

                    if (request.MultipleOptions && request.Options != null)
                    {
                        foreach (var item in request.Options)
                        {
                            var option = await _context.Options
                                .Where(x => x.Id == item.Id)
                                .FirstOrDefaultAsync();
                        
                            if (option == null)
                            {
                                var op = new Option
                                {
                                    Description = item.Description,
                                    Question = question
                                };

                                await _context.Options.AddAsync(op);
                            }
                            else
                            {
                                option.Description = item.Description;
                                _context.Entry(option).State = EntityState.Modified;  
                            }
                        
                            options.Add(new OptionDto
                            {
                                Id = option.Id,
                                Description = option.Description,
                                QuestionId = question.Id
                            });
                        
                            if (await _context.SaveChangesAsync() < 1)
                            {
                                await transaction.RollbackAsync();
                                throw new RestException(HttpStatusCode.BadRequest, "Error savig options changes");
                            }
                        }
                    }

                    await transaction.CommitAsync();
                    
                    return new QuestionDto
                    {
                        Title = question.Title,
                        Required = question.Required,
                        Document = document,
                        InputType = inputType,
                        MultipleOptions = question.MultipleOptions,
                        Options = options,
                    };
                }
            }
        }
    }
}