using Domain.InterFaces.Context;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.BusinessLogic.CategoryBL.Commands
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
            var category = await _context.Categories.FindAsync(request.id);
            _context.Categories.Remove(category);
            await _context.SaveChangesAsync();
        }

        public async Task Handle(Edit request, CancellationToken cancellationToken)
        {
            _context.Categories.Update(request.Category);
            await _context.SaveChangesAsync();
        }

        public async Task Handle(Add request, CancellationToken cancellationToken)
        {
            await _context.Categories.AddAsync(request.Category);
            await _context.SaveChangesAsync();
        }
    }
}
