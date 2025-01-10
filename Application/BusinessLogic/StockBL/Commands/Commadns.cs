using DataModels.Models;
using MediatR;

namespace Application.BusinessLogic.StockBL.Commands
{
    public record Add(Stock entity):IRequest;
    public record Edit(Stock entity):IRequest;
    public record Delete(string id):IRequest;

}
