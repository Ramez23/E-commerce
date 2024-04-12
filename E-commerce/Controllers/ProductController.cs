using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using E_commerce.Models;
using E_commerce.Data;

public class ProductController : Controller
{
    private readonly ApplicationDbContext _dbContext;

    public ProductController(ApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public IActionResult Index()
    {
        var products = _dbContext.Products.ToList();
        return View(products);
    }

    public IActionResult Add()
    {
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Add(Product product)
    {
        if (ModelState.IsValid)
        {
            _dbContext.Products.Add(product);
            await _dbContext.SaveChangesAsync();
            return RedirectToAction("Index");
        }
        return View(product);
    }

    
}
