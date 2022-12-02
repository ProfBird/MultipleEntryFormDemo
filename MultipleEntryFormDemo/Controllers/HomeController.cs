using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using MultipleEntryFormDemo.Data;
using MultipleEntryFormDemo.Models;

namespace MultipleEntryFormDemo.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;
    private BirdRepository repo;

    public HomeController(ILogger<HomeController> logger)
    {
        _logger = logger;
        repo = new BirdRepository();
    }

    public IActionResult Index()
    {
        return View(repo.GetAllBirds(HttpContext));
    }

    public IActionResult BirdSightings()
    {
        return View();
    }

    [HttpPost]
    public RedirectToActionResult BirdSightings(string[] name, string[] order, int[] number)
    {
        Bird model = null;
        // Create as many Bird model objects as there are items in the arrays.
        // Copy the field data from the form into the model object.
        // All three arrays should be the same length.
        for (int i = 0; i < name.Length; i++)
        {
            // Use the field array to set the model properties.
            model = new Bird
            {
                Name = name[i],
                Order = order[i],
                Number = number[i]
            };
            // Store the bird model object
            repo.Add(model, HttpContext);
        }

        return RedirectToAction("Index");
    }

    public IActionResult Privacy()
    {
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}

