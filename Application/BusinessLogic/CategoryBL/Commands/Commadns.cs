using DataModels.Models;
using MediatR;

namespace Application.BusinessLogic.CategoryBL.Commands
{
    public record Add(Category Category):IRequest;
    public record Edit(Category Category):IRequest;
    public record Delete(string id):IRequest;

}
