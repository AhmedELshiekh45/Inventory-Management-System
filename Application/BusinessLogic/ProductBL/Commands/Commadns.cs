using DataModels.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.BusinessLogic.ProductBL.Commands
{
    public record Add(Product Product):IRequest;
    public record Edit(Product Product):IRequest;
    public record Delete(string productid):IRequest;

}
