using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using UdemyNLayerProject.DataAccess;
using UdemyNLayerProject.Entity.Models;
using UdemyNLayerProject.Entity.Services;
using UdemyNLayerProject.Web.DTOs;

namespace UdemyNLayerProject.Web.Controllers
{
    public class ProductsController : Controller
    {
        private readonly IProductService _productService;
        private readonly ICategoryService _categoryService;
        private readonly IMapper _mapper;

        public ProductsController(IProductService productService, IMapper mapper, ICategoryService categoryService)
        {
            _productService = productService;
            _mapper = mapper;
            _categoryService = categoryService;
        }

        // GET: Products
        public async Task<IActionResult> Index()
        {
            var products = await _productService.GetWithCategory();
            return View(_mapper.Map<IEnumerable<ProductDto>>(products));
        }

        // GET: Products/Create
        public async Task<IActionResult> Create()
        {
            ViewData["CategoryId"] = new SelectList(_mapper.Map<IEnumerable<CategoryDto>>(await _categoryService.GetAllAsync()), "Id", "Name");
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
                await _productService.AddAsync(_mapper.Map<Product>(productDto));
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

            var product = await _productService.GetWithCategoryByIdAsync((int)id);

            ViewData["CategoryId"] = new SelectList(_mapper.Map<IEnumerable<CategoryDto>>(await _categoryService.GetAllAsync()), "Id", "Name", product.CategoryId);

            if (product == null)
            {
                return NotFound();
            }
            return View(_mapper.Map<ProductDto>(product));
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

                _productService.Update(_mapper.Map<Product>(productDto));
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

            var product = await _productService.GetWithCategoryByIdAsync((int)id);
            if (product == null)
            {
                return NotFound();
            }

            return View(_mapper.Map<ProductDto>(product));
        }

        // POST: Products/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var product = await _productService.SingleOrDefaultAsync(i => i.Id == id);
            _productService.Remove(product);
            return RedirectToAction(nameof(Index));
        }
    }
}
