using System.Net;
using System.Threading;
using System.Threading.Tasks;
using Application.Errors;
using Domain;
using FluentValidation;
using MediatR;
using Persistence;

namespace Application.Categories
{
    public class CreateCategory
    {
        public class CreateCategoryCommand: IRequest<Category>
        {
            public string Description { get; set; }
        }

        public class CreateCategoryCommandValidator : AbstractValidator<Category>
        {
            public CreateCategoryCommandValidator()
            {
                RuleFor(x => x.Description).NotEmpty();
            }
            
            public class CreateCategoryCommandHandler : IRequestHandler<CreateCategoryCommand, Category>
            {
                private readonly DataContext _context;

                public CreateCategoryCommandHandler(DataContext context)
                {
                    _context = context;
                }
                
                public async Task<Category> Handle(CreateCategoryCommand request, CancellationToken cancellationToken)
                {
                    var category = new Category {Description = request.Description};
                    
                    await _context.Categories.AddAsync(category);
                    
                    if (await _context.SaveChangesAsync() < 1)
                    {
                        throw new RestException(HttpStatusCode.BadRequest, "Error savig category changes");
                    }

                    return category;
                }
            }
        }
    }
}