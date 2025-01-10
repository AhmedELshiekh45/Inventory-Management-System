using DataModels.Models;
using Domain.InterFaces.Context;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.BusinessLogic.OrdersBL.Queries
{
    public class QueriesHandler : IRequestHandler<GetById, Order>, IRequestHandler<GetAll, IEnumerable<Order>>
    {
        public QueriesHandler(IContext context)
        {
            _Context = context;
        }

        public IContext _Context { get; }

        public async Task<IEnumerable<Order>> Handle(GetAll request, CancellationToken cancellationToken)
        {
           return await _Context.Orders.ToListAsync();
        }

        public async Task<Order> Handle(GetById request, CancellationToken cancellationToken)
        {
           return await _Context.Orders.FindAsync(request.id);
        }
    }
}
