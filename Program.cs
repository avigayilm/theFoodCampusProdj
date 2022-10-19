using Microsoft.AspNetCore.Rewrite;
using Microsoft.EntityFrameworkCore;
using theFoodCampus.Data;
using theFoodCampus.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

// here we connect to the database
builder.Services.AddDbContext<ApplicationDbContext>(options => options.UseSqlServer(
    builder.Configuration.GetConnectionString("DefaultConnection")
    ));

var app = builder.Build();
// if recipes DB is not initialized, initialize now
using(var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    SeedData.Intitialize(services);
}


// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
}
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

var rules = new RewriteOptions()
    .AddRedirect(@"^.{0}$", "/home/index");
app.UseRewriter(rules);

app.MapControllerRoute(
    name: "default",
    pattern: "{controller}/{action}/{id?}");

app.Run();
