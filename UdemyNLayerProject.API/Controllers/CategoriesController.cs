using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using UdemyNLayerProject.API.DTOs;
using UdemyNLayerProject.DataAccess;
using UdemyNLayerProject.Entity.Models;
using UdemyNLayerProject.Entity.Services;

namespace UdemyNLayerProject.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriesController : ControllerBase
    {
        private readonly ICategoryService _categoryService;
        private readonly IMapper _mapper;

        public CategoriesController(ICategoryService categoryService, IMapper mapper)
        {
            _categoryService = categoryService;
            _mapper = mapper;
        }


        // GET: api/Categories
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Category>>> GetCategories()
        {
            var categories = await _categoryService.GetAllAsync();

            return Ok(_mapper.Map<IEnumerable<CategoryDto>>(categories));
        }

        // GET: api/Categories/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Category>> GetCategory(int id)
        {
            var category = await _categoryService.SingleOrDefaultAsync(i => i.Id == id);

            if (category == null)
            {
                return NotFound();
            }

            return Ok(_mapper.Map<CategoryDto>(category));
        }

        // PUT: api/Categories/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCategory(int id, CategoryDto categoryDto)
        {
            if (id != categoryDto.Id)
            {
                return BadRequest();
            }

            _categoryService.Update(_mapper.Map<Category>(categoryDto));

            return NoContent();
        }

        // POST: api/Categories
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<Category>> PostCategory(CategoryDto categoryDto)
        {
            var newcategory = await _categoryService.AddAsync(_mapper.Map<Category>(categoryDto));

            return CreatedAtAction("GetCategory", new { id = newcategory.Id }, _mapper.Map<CategoryDto>(newcategory));
        }

        // DELETE: api/Categories/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Category>> DeleteCategory(int id)
        {
            var category = await _categoryService.SingleOrDefaultAsync(i => i.Id == id);
            if (category == null)
            {
                return NotFound();
            }

            _categoryService.Remove(category);

            return NoContent();
        }

        [HttpGet("{categoryId}/products")]
        public async Task<IActionResult> GetWithProductByIdAsync(int categoryId)
        {
            var category = await _categoryService.GetWithProductByIdAsync(categoryId);

            return Ok(_mapper.Map<CategoryWithProductsDto>(category)); 
        }

        private async Task<bool> CategoryExistsAsync(int id)
        {
            var category = await _categoryService.GetByIdAsync(id);
            if (category == null) return true;
            else return false;
        }
    }
}
