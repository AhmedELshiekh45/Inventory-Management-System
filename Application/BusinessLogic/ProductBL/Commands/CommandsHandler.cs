using Domain.InterFaces.Context;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.BusinessLogic.ProductBL.Commands
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
            var product = await _context.Products.FindAsync(request.productid);
            _context.Products.Remove(product);
            await _context.SaveChangesAsync();
        }

        public async Task Handle(Edit request, CancellationToken cancellationToken)
        {
            _context.Products.Update(request.Product);
            await _context.SaveChangesAsync();
        }

        public async Task Handle(Add request, CancellationToken cancellationToken)
        {
            await _context.Products.AddAsync(request.Product);
            await _context.SaveChangesAsync();
        }
    }
}
