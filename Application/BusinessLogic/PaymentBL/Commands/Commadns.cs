using DataModels.Models;
using MediatR;

namespace Application.BusinessLogic.PaymentBL.Commands
{
    public record Add(Payment entity):IRequest;
    public record Edit(Payment entity):IRequest;
    public record Delete(string id):IRequest;

}
