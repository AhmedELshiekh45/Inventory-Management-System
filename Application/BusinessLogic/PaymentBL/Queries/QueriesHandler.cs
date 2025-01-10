using DataModels.Models;
using Domain.InterFaces.Context;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.BusinessLogic.PaymentBL.Queries
{
    public class QueriesHandler : IRequestHandler<GetById, Payment>, IRequestHandler<GetAll, IEnumerable<Payment>>
    {
        public QueriesHandler(IContext context)
        {
            _Context = context;
        }

        public IContext _Context { get; }

        public async Task<IEnumerable<Payment>> Handle(GetAll request, CancellationToken cancellationToken)
        {
           return await _Context.Payments.ToListAsync();
        }

        public async Task<Payment> Handle(GetById request, CancellationToken cancellationToken)
        {
           return await _Context.Payments.FindAsync(request.id);
        }
    }
}
