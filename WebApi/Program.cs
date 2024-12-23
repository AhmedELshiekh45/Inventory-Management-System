using DataModels.Context;
using DataModels.Models;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Stripe;
using System.Security.Claims;
using System.Text;

namespace WebAPIDotNet
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers().ConfigureApiBehaviorOptions(
                options =>
                    options.SuppressModelStateInvalidFilter = true);

            builder.Services.AddDbContext<MyContext>(options =>
            {
                options.UseSqlServer(builder.Configuration.GetConnectionString("def"));
            });

            StripeConfiguration.ApiKey = builder.Configuration["Stripe:Secretkey"];



            //-------------------------------------------------
            builder.Services.AddIdentity<User, IdentityRole>()
                .AddEntityFrameworkStores<MyContext>().AddDefaultTokenProviders();

            #region Auto Mapping
            //builder.Services.AddAutoMapper(typeof(RoomProfile));

            #endregion


            //#region Register Services
            //builder.Services.AddScoped<RoomService>();
            //builder.Services.AddScoped<BookingService>();
            //builder.Services.AddScoped<Business_Layer.Services.InvoiceService>();
            //builder.Services.AddScoped<ServicesService>();
            //builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
            //#endregion



            #region AddAuthentication
            builder.Services.AddAuthentication(options =>
            {
                //Check JWT Token Header
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                //[authrize]
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;//unauth
                options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(options =>//verified key
            {
                options.SaveToken = true;
                options.RequireHttpsMetadata = false;
                options.TokenValidationParameters = new TokenValidationParameters()
                {
                    ValidateIssuer = true,
                    ValidIssuer = builder.Configuration["JWT:Issuer"],
                    ValidateAudience = true,
                    ValidAudience = builder.Configuration["JWT:Audience"],
                    IssuerSigningKey =
                        new SymmetricSecurityKey(
                            Encoding.UTF8.GetBytes(builder.Configuration["JWT:Key"]))

                };
            });
            #endregion


            #region AddPolicy
            //-------------------------------------------------
            builder.Services.AddCors(options =>
            {
                options.AddPolicy("MyPolicy", policy =>
                {
                    policy.AllowAnyOrigin()
                    .AllowAnyMethod()
                    .AllowAnyHeader();

                });
            });

            #endregion


            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            //builder.Services.AddSwaggerGen();

            #region Swagger Setting
            builder.Services.AddSwaggerGen(swagger =>
            {
                //This is to generate the Default UI of Swagger Documentation    
                swagger.SwaggerDoc("v1", new OpenApiInfo
                {
                    Version = "v1",
                    Title = "ASP.NET 8 Web API",
                    Description = " ITI Projrcy"
                });
                // To Enable authorization using Swagger (JWT)    
                swagger.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme()
                {
                    Name = "Authorization",
                    Type = SecuritySchemeType.ApiKey,
                    Scheme = "Bearer",
                    BearerFormat = "JWT",
                    In = ParameterLocation.Header,
                    Description = "Enter 'Bearer' [space] and then your valid token in the text input below.\r\n\r\nExample: \"Bearer eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9\"",
                });
                swagger.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {
                    new OpenApiSecurityScheme
                    {
                    Reference = new OpenApiReference
                    {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer"
                    }
                    },
                    new string[] {}
                    }
                    });
            });
            #endregion

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
            app.UseStaticFiles();

            app.UseCors("MyPolicy");

            app.UseAuthentication(); //by default "Cookie"
            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }

        static async Task SeedRoles(RoleManager<IdentityRole> roleManager)
        {
            var roles = new[] { "Admin", "Employee", "User", "Supplier" };

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
    }
}

