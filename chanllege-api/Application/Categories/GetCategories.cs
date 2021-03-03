using System.Collections.Generic;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using Application.Errors;
using Domain;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Persistence;

namespace Application.Categories
{
    public class GetCategories
    {
        public class GetCategoriesQuery : IRequest<List<Category>>
        {
        }
        
        public class GetCategoriesQueryHandler : IRequestHandler<GetCategoriesQuery, List<Category>>
        {
            private readonly DataContext _context;

            public GetCategoriesQueryHandler(DataContext context)
            {
                _context = context;
            }
                
            public async Task<List<Category>> Handle(GetCategoriesQuery request, CancellationToken cancellationToken)
            {
                var categories = await _context.Categories.ToListAsync();
                    
                if (categories == null)
                {
                    throw new RestException(HttpStatusCode.NoContent, "No categories");
                }

                return categories;
            }
        }
    }
}