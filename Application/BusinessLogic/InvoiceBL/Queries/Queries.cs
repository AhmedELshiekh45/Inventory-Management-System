using DataModels.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.BusinessLogic.InvoiceBL.Queries
{
    public record GetAll() : IRequest<IEnumerable<Invoice>>;
    public record GetById(string id) : IRequest<Invoice>;
}
