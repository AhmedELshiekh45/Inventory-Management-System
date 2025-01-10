using DataModels.Models;
using Domain.InterFaces.Context;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.BusinessLogic.CategoryBL.Queries
{
    public class QueriesHandler : IRequestHandler<GetById, Category>, IRequestHandler<GetAll, IEnumerable<Category>>
    {
        public QueriesHandler(IContext context)
        {
            _Context = context;
        }

        public IContext _Context { get; }

        public async Task<IEnumerable<Category>> Handle(GetAll request, CancellationToken cancellationToken)
        {
           return await _Context.Categories.ToListAsync();
        }

        public async Task<Category> Handle(GetById request, CancellationToken cancellationToken)
        {
           return await _Context.Categories.FindAsync(request.id);
        }
    }
}
