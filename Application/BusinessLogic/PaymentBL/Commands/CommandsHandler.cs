﻿using Domain.InterFaces.Context;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.BusinessLogic.PaymentBL.Commands
{
    public class CommandsHandler : IRequestHandler<Add>, IRequestHandler<Edit>, IRequestHandler<Delete>
    {
        private readonly IContext _context;

        public CommandsHandler(IContext context)
        {
            this._context = context;
        }
        public async Task Handle(Delete request, CancellationToken cancellationToken)
        {
            var item = await _context.Payments.FindAsync(request.id);
            _context.Payments.Remove(item);
            await _context.SaveChangesAsync();
        }

        public async Task Handle(Edit request, CancellationToken cancellationToken)
        {
            _context.Payments.Update(request.entity);
            await _context.SaveChangesAsync();
        }

        public async Task Handle(Add request, CancellationToken cancellationToken)
        {
            await _context.Payments.AddAsync(request.entity);
            await _context.SaveChangesAsync();
        }
    }
}