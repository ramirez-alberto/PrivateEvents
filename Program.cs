using PrivateEvents.Entities;
using PrivateEvents.Entities.Models;
using PrivateEvents.Extensions;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddAutoMapper(typeof(Program));
builder.Services.ConfigureSQLServer(builder.Configuration);
builder.Services.ConfigureRepository();
builder.Services.AddControllersWithViews();
builder.Services.AddIdentity<User, IdentityRole>(
    opt => {
        opt.Password.RequireNonAlphanumeric = false;
        opt.Password.RequireUppercase = false;

        opt.User.RequireUniqueEmail = true;
    }
)
    .AddEntityFrameworkStores<RepositoryContext>();
builder.Services.ConfigureCORS();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseCors("CorsPolicy");

app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
