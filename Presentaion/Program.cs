using Application.BusinessLogic.CategoryBL.Commands;
using DataModels.Context;
using DataModels.Models;
using Domain.InterFaces.BaseRepository;
using Domain.InterFaces.Context;
using Infrastructure.Repos;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddControllers().ConfigureApiBehaviorOptions(
              options =>
                  options.SuppressModelStateInvalidFilter = true);
builder.Services.AddDbContext<MyContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("def"));
});
//adding the mediator 
builder.Services.AddMediatR(p =>
{
    p.RegisterServicesFromAssembly(typeof(CommandsHandler).Assembly);
});

builder.Services.AddScoped(typeof(IRepo<>), typeof(Repo<>));
builder.Services.AddScoped(typeof(IContext), typeof(MyContext));
//-------------------------------------------------
builder.Services.AddIdentity<User, IdentityRole>()
    .AddEntityFrameworkStores<MyContext>().AddDefaultTokenProviders();
var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var serviceProvider = scope.ServiceProvider;
    var roleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();
    var userManager = serviceProvider.GetRequiredService<UserManager<User>>();
    await SeedRoles(roleManager);
    await SeedAdmin(userManager);
}
// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();


static async Task SeedRoles(RoleManager<IdentityRole> roleManager)
{
    var roles = new[] { "Admin", "Branch_Head", "Employee", "Customer" };

    foreach (var role in roles)
    {
        if (!await roleManager.RoleExistsAsync(role))
        {
            await roleManager.CreateAsync(new IdentityRole(role));
        }
    }
}
static async Task SeedAdmin(UserManager<User> userManager)
{
    var admin =
       new User
       {
           Id = Guid.NewGuid().ToString(),
           Email = "ahm75295@gmail.com",
           PasswordHash = "Ahmed123!@#",
           UserName = "Ahmedelshiekh45",
           PhoneNumber = "01063038833"
       };
    var claim = new Claim("Role", "Admin");
    if (await userManager.FindByEmailAsync(admin.Email) == null)
    {
        var result = await userManager.CreateAsync(admin, "Ahmed123!@#");
        if (result.Succeeded)
        {
            await userManager.AddToRoleAsync(admin, "Admin");
            await userManager.AddClaimAsync(admin, claim);
        }


    }
}