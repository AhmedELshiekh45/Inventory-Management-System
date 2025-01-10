using DataModels.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.BusinessLogic.PaymentBL.Queries
{
    public record GetAll() : IRequest<IEnumerable<Payment>>;
    public record GetById(string id) : IRequest<Payment>;
}
