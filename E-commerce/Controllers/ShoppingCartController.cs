using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using E_commerce.Data;
using E_commerce.Models;
using System.Linq;
using Microsoft.AspNetCore.Mvc.Rendering;

public class ShoppingCartController : Controller
{
    private readonly ApplicationDbContext _dbContext;

    public ShoppingCartController(ApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public IActionResult Index()
    {
        var cartItems = _dbContext.ShoppingCarts.Include(sc => sc.Product).ToList();
        return View(cartItems);
    }

    public IActionResult AddToCart()
    {
        var products = _dbContext.Products.ToList();
        ViewBag.Products = new SelectList(products, "ProductID", "ProductName");

        return View();
    }

    [HttpPost]
    public IActionResult AddToCart(ShoppingCart cartItem)
    {
        if (ModelState.IsValid)
        {
            var product = _dbContext.Products.Find(cartItem.ProductID);

            if (product != null)
            {
                cartItem.Product = product;
                cartItem.UnitPrice = int.Parse(product.ProductPrice);

                _dbContext.ShoppingCarts.Add(cartItem);
                _dbContext.SaveChanges();
            }

            return RedirectToAction("Index", "ShoppingCart");
        }

        var products = _dbContext.Products.ToList();
        ViewBag.Products = new SelectList(products, "ProductID", "ProductName", cartItem.ProductID);
        return View(cartItem);
    }
    public IActionResult Edit(int id)
    {
        var cartItem = _dbContext.ShoppingCarts.Find(id);
        if (cartItem == null)
        {
            return NotFound();
        }

        var products = _dbContext.Products.ToList();
        ViewBag.Products = new SelectList(products, "ProductID", "ProductName", cartItem.ProductID);

        return View(cartItem);
    }

    [HttpPost]
    public IActionResult Edit(int id, ShoppingCart cartItem)
    {
        if (id != cartItem.CartID)
        {
            return NotFound();
        }

        if (ModelState.IsValid)
        {
            var product = _dbContext.Products.Find(cartItem.ProductID);

            if (product != null)
            {
                cartItem.Product = product;
                cartItem.UnitPrice = int.Parse(product.ProductPrice);
                _dbContext.Entry(cartItem).State = EntityState.Modified;
                _dbContext.SaveChanges();
            }

            return RedirectToAction("Index");
        }

        var products = _dbContext.Products.ToList();
        ViewBag.Products = new SelectList(products, "ProductID", "ProductName", cartItem.ProductID);
        return View(cartItem);
    }

    public IActionResult Delete(int id)
    {
        var cartItem = _dbContext.ShoppingCarts.Find(id);
        if (cartItem == null)
        {
            return NotFound();
        }

        return View(cartItem);
    }

    [HttpPost, ActionName("Delete")]
    public IActionResult DeleteConfirmed(int id)
    {
        var cartItem = _dbContext.ShoppingCarts.Find(id);
        if (cartItem != null)
        {
            _dbContext.ShoppingCarts.Remove(cartItem);
            _dbContext.SaveChanges();
            
        }
        return RedirectToAction("Index");
        
    }
}