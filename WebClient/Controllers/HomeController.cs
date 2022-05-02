using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Diagnostics;
using WebClient.Contract;
using WebClient.Models;

namespace WebClient.Controllers;

public class HomeController : Controller
{
    private readonly IProductService _service;
    private readonly ILogger<HomeController> _logger;
    public HomeController(IProductService service,
                            ILogger<HomeController> logger)
    {
        _service = service;
        _logger = logger;
    }

    //TODO exception:
    //No connection could be made because the target machine actively refused it.

    public async Task<IActionResult> Products(
            int? pageNumber, int? pageSize, string nameProduct = "")
    {
        GroupViewModel model = await _service
         .GetGroupModelProductsAsync(pageNumber, pageSize, nameProduct);

        model.selectList = await _service.GetNamesProductAsync(nameProduct);

        if (model == null && model.products.Count == 0)
            TempData["Message"] = "List is empty";

        return View(model);
    }
    [HttpGet]
    public ActionResult Create()
    {
        return PartialView("_ProductCreatePartial");
    }

    [HttpPost]
    public async Task<IActionResult> Create(Product product)
    {
        if (ModelState.IsValid)
        {
            var response = await _service.CreateAsync(product);

            if (response.IsSuccessStatusCode)
            {
                TempData["Message"] = "Inserted successfully!";
            }
            else
            {

                TempData["Message"] = $"Error while calling Web-API! {response.StatusCode}";
            }
        }
        return RedirectToAction("Products");
    }


    public async Task<IActionResult> Update(Guid id)
    {
        Product product = await _service.GetProductAsync(id);

        return PartialView("_ProductUpdatePartial", product);
    }


    [HttpPost]
    public async Task<IActionResult> Update(Product product)
    {
        var response = await _service.UpdateAsync(product);

        if (response.IsSuccessStatusCode)
        {
            TempData["Message"] = "Update successfully!";
        }
        else
        {
            TempData["Message"] = "Error while calling Web-API!";
        }

        return RedirectToAction("Products");
    }


    [HttpGet]
    public ActionResult DeletePartail(Product product)
    {
        return PartialView("_ProductDeletePartial", product);
    }

    [HttpPost]
    public async Task<ActionResult> Delete(Guid id)
    {
        HttpResponseMessage response = await _service.DeleteAsync(id);

        if (response.IsSuccessStatusCode)
        {
            TempData["Message"] = "Deleted successfully!";
        }
        else
        {
            TempData["Message"] = "Error while calling Web-API!";
        }

        return RedirectToAction("Products");
    }


    public IActionResult Index()
    {
        return View();
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