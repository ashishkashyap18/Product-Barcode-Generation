using Microsoft.AspNetCore.Mvc;
using PBMS.Interface;
using PBMS.Models;

namespace PBMS.Controllers
{
    public class ProductController : Controller
    {
        private readonly IProductServices _productServices;

        public ProductController(IProductServices productServices)
        {
            _productServices = productServices;
        }

        public async Task<IActionResult> Index()
        {
            var products = await _productServices.GetAllProduct();
            return View(products);
        }

        public async Task<IActionResult> GetQRCode(int id)
        {
            var product = await _productServices.GetProductById(id);
            if(product == null || product.QrCode == null || product.QrCode.Length==0)
            {
                return NotFound();
            }
            return File(product.QrCode, "image/png");
        }
 
        public IActionResult Create()
        {
            ViewBag.IsEdit = false;
            return View("CreateorEdit", new Product());
        }

        [HttpPost]
        public async Task<IActionResult> Create(Product product)
        {
            if (ModelState.IsValid)
            {
                await _productServices.Create(product);
                return RedirectToAction("Index");
            }

            ViewBag.IsEdit = false;
            return View("CreateorEdit", product);
        }

        public async Task<IActionResult> Edit(int id)
        {
            var product = await _productServices.GetProductById(id);
            if (product == null)
            {
                return NotFound();
            }
            ViewBag.IsEdit = true;
            return View("CreateorEdit", product);
        }


        [HttpPost]
        public async Task<IActionResult> Edit(int id, Product product)
        {
            if (id != product.Id)
            {
                return BadRequest();
            }
            if (ModelState.IsValid)
            {
                var result = await _productServices.Update(product);
                if (result)
                {
                    return RedirectToAction("Index");
                }
                else
                {
                    ModelState.AddModelError("", "Unable to update the product. Please try again.");
                }
            }
            ViewBag.IsEdit = true;
            return View("CreateorEdit", product);
        }

        public async Task<IActionResult> Delete(int id)
        {
            if (id <= 0)
            {
                return NotFound();
            }
            var result = await _productServices.Delete(id);
            if (result)
            {
                return RedirectToAction("Index");
            }
            else
            {
                return NotFound();
            }
        }
        
    }
}
