using DataModels.Context;
using DataModels.Models;
using Repos.Base_Repos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repos.Repos.Product_Repo
{
    public class ProductRepo : BaseRepo<Product>, IProductRepo
    {
        private readonly MyContext _context;

        public ProductRepo(MyContext context) : base(context)
        {
            this._context = context;
        }


      
    }
}
