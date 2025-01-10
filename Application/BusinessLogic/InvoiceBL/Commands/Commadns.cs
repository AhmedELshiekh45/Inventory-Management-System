using DataModels.Models;
using MediatR;

namespace Application.BusinessLogic.InvoiceBL.Commands
{
    public record Add(Invoice entity):IRequest;
    public record Edit(Invoice entity):IRequest;
    public record Delete(string id):IRequest;

}
