using System.Data.Common;
using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Nous_University.Models;
using Nous_University.DataLayer.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Nous_University.DataLayer.Data;


namespace Nous_University.MVC.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;
    private readonly NousUniversityDbContext _context;

    public HomeController(ILogger<HomeController> logger, NousUniversityDbContext context)
    {
        _logger = logger;
        _context = context;
    }

    public IActionResult Index()
    {
        return View();
    }

    public IActionResult Privacy()
    {
        return View();
    }

    public async Task<ActionResult> About()
    {
        var groups = new List<EnrollmentDateGroup>();
        var conn = _context.Database.GetDbConnection();
        try
        {
            await conn.OpenAsync();
            await using var command = conn.CreateCommand();
            const string query = "SELECT EnrollmentDate, COUNT(*) AS StudentCount "
                                 + "FROM Person "
                                 + "WHERE Discriminator = 'Student' "
                                 + "GROUP BY EnrollmentDate";
            command.CommandText = query;
            var reader = await command.ExecuteReaderAsync();

            if (reader.HasRows)
            {
                while (await reader.ReadAsync())
                {
                    var row = new EnrollmentDateGroup
                        { EnrollmentDate = reader.GetDateTime(0), StudentCount = reader.GetInt32(1) };
                    groups.Add(row);
                }
            }

            await reader.DisposeAsync();
        }
        finally
        {
            await conn.CloseAsync();
        }

        return View(groups);
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}