using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Threading.Tasks;
using UdemyNLayerProject.Web.ApiService;
using UdemyNLayerProject.Web.DTOs;

namespace UdemyNLayerProject.Web.Controllers
{
    public class ProductsController : Controller
    {
        private readonly ProductApiService _productApiService;
        private readonly CategoryApiService _categoryApiService;

        public ProductsController(ProductApiService productApiService, CategoryApiService categoryApiService)
        {
            _productApiService = productApiService;
            _categoryApiService = categoryApiService;
        }

        // GET: Products
        public async Task<IActionResult> Index()
        {
            var products = await _productApiService.GetProducts();
            return View(products);
        }

        // GET: Products/Create
        public async Task<IActionResult> Create()
        {
            ViewData["CategoryId"] = new SelectList(await _categoryApiService.GetAllAsync(), "Id", "Name");
            return View();
        }

        // POST: Products/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(ProductDto productDto)
        {
            if (ModelState.IsValid)
            {
                await _productApiService.AddProducts(productDto);
                return RedirectToAction(nameof(Index));
            }
            return View(productDto);
        }

        // GET: Products/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var product = await _productApiService.GetProduct((int)id);

            ViewData["CategoryId"] = new SelectList(await _categoryApiService.GetAllAsync(), "Id", "Name", product.CategoryId);

            if (product == null)
            {
                return NotFound();
            }
            return View(product);
        }

        // POST: Products/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, ProductDto productDto)
        {
            if (id != productDto.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                var uploadedProduct = await _productApiService.GetProduct(id);
                uploadedProduct.Name = productDto.Name;
                uploadedProduct.Stock = productDto.Stock;
                if (productDto.Price != 0)
                {
                    uploadedProduct.Price = productDto.Price;
                }
                if (productDto.CategoryId != 0)
                {
                    uploadedProduct.CategoryId = productDto.CategoryId;
                }
                await _productApiService.UpdateProducts(id, uploadedProduct);
                return RedirectToAction(nameof(Index));
            }

            return View(productDto);
        }

        // GET: Products/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var product = await _productApiService.GetProduct((int)id);
            if (product == null)
            {
                return NotFound();
            }

            return View(product);
        }

        // POST: Products/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await _productApiService.DeleteProducts(id);
            return RedirectToAction(nameof(Index));
        }
    }
}
