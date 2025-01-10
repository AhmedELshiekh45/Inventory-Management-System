using DataModels.Models;
using MediatR;

namespace Application.BusinessLogic.OrdersBL.Commands
{
    public record Add(Order entity):IRequest;
    public record Edit(Order entity):IRequest;
    public record Delete(string id):IRequest;

}
