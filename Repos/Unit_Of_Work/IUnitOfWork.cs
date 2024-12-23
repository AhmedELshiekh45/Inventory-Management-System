using DataModels.Models;
using Microsoft.AspNetCore.Identity;
using Repos.Repos.Category_Repo;
using Repos.Repos.Product_Repo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repos.Unit_Of_Work
{
    public interface IUnitOfWork:IDisposable
    {
        public UserManager<User> UserManager { get; }
        public RoleManager<IdentityRole> RoleManager { get; }
        public IProductRepo ProductRepo { get; }
        public ICategoryRepo CategoryRepo { get; }
    }
}
