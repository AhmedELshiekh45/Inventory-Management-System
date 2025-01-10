using DataModels.Models;
using Domain.InterFaces.Context;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.BusinessLogic.StockBL.Queries
{
    public class QueriesHandler : IRequestHandler<GetById, Stock>, IRequestHandler<GetAll, IEnumerable<Stock>>
    {
        public QueriesHandler(IContext context)
        {
            _Context = context;
        }

        public IContext _Context { get; }

        public async Task<IEnumerable<Stock>> Handle(GetAll request, CancellationToken cancellationToken)
        {
           return await _Context.Stocks.ToListAsync();
        }

        public async Task<Stock> Handle(GetById request, CancellationToken cancellationToken)
        {
           return await _Context.Stocks.FindAsync(request.id);
        }
    }
}
