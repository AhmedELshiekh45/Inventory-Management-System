using DataModels.Context;
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
    public class UnitOfWork : IUnitOfWork
    {
        private readonly MyContext context;

        public UserManager<User> UserManager{ get; }

        public RoleManager<IdentityRole> RoleManager{ get; }
        public IProductRepo ProductRepo{ get; }
        public ICategoryRepo CategoryRepo { get; }


        public UnitOfWork(UserManager<User> userManager,RoleManager<IdentityRole> roleManager,MyContext context)
        {
            this.UserManager = userManager;
            this.RoleManager = roleManager;
            this.context = context;
            this.ProductRepo=new ProductRepo(context);
            this.CategoryRepo=new CategoryRepo(context);
        }

        public async Task CompleteAsync()
        {
           await context.SaveChangesAsync();
        }

        public void Dispose()
        {
            context.Dispose();
        }
    }
}
