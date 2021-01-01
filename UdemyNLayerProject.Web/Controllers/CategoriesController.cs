using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using UdemyNLayerProject.Web.ApiService;
using UdemyNLayerProject.Web.DTOs;
using UdemyNLayerProject.Web.Filters;

namespace UdemyNLayerProject.Web.Controllers
{
    public class CategoriesController : Controller
    {
        private readonly CategoryApiService _categoryApiService;


        public CategoriesController(CategoryApiService categoryApiService)
        {
            _categoryApiService = categoryApiService;
        }

        // GET: Categories
        public async Task<IActionResult> Index()
        
        {
            var categories = await _categoryApiService.GetAllAsync();
            return View(categories);
        }

        // GET: Categories/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Categories/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CategoryDto categoryDto)
        {
            if (ModelState.IsValid)
            {
                await _categoryApiService.AddAsync(categoryDto);
                return RedirectToAction(nameof(Index));
            }
            return View(categoryDto);
        }

        // GET: Categories/Edit/5
        [ServiceFilter(typeof(NotFoundFilter))]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var category = await _categoryApiService.GetById((int)id);
            if (category == null)
            {
                return NotFound();
            }

            return View(category);
        }

        // POST: Categories/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [ServiceFilter(typeof(NotFoundFilter))]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, CategoryDto categoryDto)
        {
            if (id != categoryDto.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {

                await _categoryApiService.Update(id,categoryDto);
                return RedirectToAction(nameof(Index));
            }
            return View(categoryDto);
        }

        [ServiceFilter(typeof(NotFoundFilter))]
        // GET: Categories/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var category = await _categoryApiService.GetById((int)id);
            if (category == null)
            {
                return NotFound();
            }

            return View(category);
        }

        // POST: Categories/Delete/5
        [ServiceFilter(typeof(NotFoundFilter))]
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await _categoryApiService.Remove(id);
            return RedirectToAction(nameof(Index));
        }
    }
}
