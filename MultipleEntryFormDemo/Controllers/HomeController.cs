using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using MultipleEntryFormDemo.Data;
using MultipleEntryFormDemo.Models;

namespace MultipleEntryFormDemo.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;
    private SightingRepository repo;

    public HomeController(ILogger<HomeController> logger)
    {
        _logger = logger;
        repo = new SightingRepository();
    }

    public IActionResult Index()
    {
        return View(repo.GetAllSightings(HttpContext));
    }

    public IActionResult BirdSightings()
    {
        var orders = repo.GetAllOrders(HttpContext);
        List<SelectListItem> orderList = new();
        foreach (String order in orders)
        {
            orderList.Add(new SelectListItem { Text = order, Value = order });
        }   
        return View(orderList);
    }

    [HttpPost]
    public RedirectToActionResult BirdSightings(string[] name, string[] order, int[] number, string location, string birder)
    {
        Sighting model = new Sighting();
        model.Location = location;
        model.Date = DateOnly.FromDateTime(DateTime.Now);
        model.Birder = birder;
        // Create as many Bird model objects as there are items in the arrays.
        // Copy the field data from the form into the model object.
        // All three arrays should be the same length.
        for (int i = 0; i < name.Length; i++)
        {
            // Use the arrays from the input form fields to set the model properties.
            var bird = new Bird
            {
                Name = name[i],
                Order = order[i],
                Number = number[i]
            };
            model.Birds.Add(bird);
        }
        repo.AddSighting(model, HttpContext);
        return RedirectToAction("Index");
    }

    public JsonResult BirdFamiliesAjax(string order)
    {
        var families = repo.GetFamiliesByOrder(order, HttpContext);
        return Json(families);
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

