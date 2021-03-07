using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Domain;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Persistence;

namespace Application.InputTypes
{
    public class GetInputTypes
    {
        public class GetInputTypesQuery : IRequest<List<InputType>>
        {
            
        }

        public class GetInputTypesQueryHandler : IRequestHandler<GetInputTypesQuery, List<InputType>>
        {
            private readonly DataContext _context;

            public GetInputTypesQueryHandler(DataContext context)
            {
                _context = context;
            }

            public async Task<List<InputType>> Handle(GetInputTypesQuery request, CancellationToken cancellationToken)
            {
                var inputTypes = await _context.InputTypes.ToListAsync();
                return inputTypes;
            }
        }
    }
}