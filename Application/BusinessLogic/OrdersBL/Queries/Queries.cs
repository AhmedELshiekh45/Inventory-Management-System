using DataModels.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.BusinessLogic.OrdersBL.Queries
{
    public record GetAll() : IRequest<IEnumerable<Order>>;
    public record GetById(string id) : IRequest<Order>;
}
