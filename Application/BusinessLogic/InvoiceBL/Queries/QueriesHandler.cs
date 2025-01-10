using DataModels.Models;
using Domain.InterFaces.Context;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.BusinessLogic.InvoiceBL.Queries
{
    public class QueriesHandler : IRequestHandler<GetById, Invoice>, IRequestHandler<GetAll, IEnumerable<Invoice>>
    {
        public QueriesHandler(IContext context)
        {
            _Context = context;
        }

        public IContext _Context { get; }

        public async Task<IEnumerable<Invoice>> Handle(GetAll request, CancellationToken cancellationToken)
        {
           return await _Context.Invoices.ToListAsync();
        }

        public async Task<Invoice> Handle(GetById request, CancellationToken cancellationToken)
        {
           return await _Context.Invoices.FindAsync(request.id);
        }
    }
}
