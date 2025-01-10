using DataModels.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.BusinessLogic.CategoryBL.Queries
{
    public record GetAll() : IRequest<IEnumerable<Category>>;
    public record GetById(string id) : IRequest<Category>;
}
