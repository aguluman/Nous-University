using Nous_University.DataLayer.Data;
using Microsoft.EntityFrameworkCore;



var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.AddDbContext<NousUniversityDbContext>(options => 
{ var defaultConnection = builder.Configuration.GetSection
    ("ConnectionStrings")["DefaultConnection"]; options.UseSqlServer(defaultConnection);
});

//The factory is not meant to be used like this, but it's demo code :-)
var context = new NousUniversityDbContextFactory().CreateDbContext(null);
//Make some changes
context.SaveChanges();



builder.Services.AddDatabaseDeveloperPageExceptionFilter();

// Create the database if it doesn't exist
static void CreateDbIfNotExists(IHost host)
{
    using var scope = host.Services.CreateScope();
    {
        var services = scope.ServiceProvider;
        try
        {
            var context = services.GetRequiredService<NousUniversityDbContext>();
            SeedData.Initialize(context);
        }
        catch (Exception ex)
        {
            var logger = services.GetRequiredService<ILogger<Program>>();
            logger.LogError(ex, "An error occurred creating the DB");
        }
    }
}

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

app.UseAuthorization();

app.UseEndpoints(endpoints =>
{
    endpoints.MapControllerRoute(
        name: "default",
        pattern: "{controller=Home}/{action=Index}/{id?}");
});


CreateDbIfNotExists(app);
app.Run();