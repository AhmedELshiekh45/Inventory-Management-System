using DataModels.Models;
using Domain.InterFaces.Context;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.BusinessLogic.ProductBL.Queries
{
    public class QueriesHandler : IRequestHandler<GetById, Product>, IRequestHandler<GetAll, IEnumerable<Product>>
    {
        public QueriesHandler(IContext context)
        {
            _Context = context;
        }

        public IContext _Context { get; }

        public async Task<IEnumerable<Product>> Handle(GetAll request, CancellationToken cancellationToken)
        {
           return await _Context.Products.ToListAsync();
        }

        public async Task<Product> Handle(GetById request, CancellationToken cancellationToken)
        {
           return await _Context.Products.FindAsync(request.id);
        }
    }
}
