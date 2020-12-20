using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using UdemyNLayerProject.API.DTOs;
using UdemyNLayerProject.API.Filters;
using UdemyNLayerProject.DataAccess;
using UdemyNLayerProject.Entity.Models;
using UdemyNLayerProject.Entity.Services;

namespace UdemyNLayerProject.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IProductService _productService;

        public ProductsController(IMapper mapper, IProductService productService)
        {
            _mapper = mapper;
            _productService = productService;
        }

        // GET: api/Products
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Product>>> GetProducts()
        {
            var products =  await _productService.GetAllAsync();
            return Ok(_mapper.Map<IEnumerable<ProductDto>>(products));
        }

        // GET: api/Products/5
        [ServiceFilter(typeof(NotFoundFilter))]
        [HttpGet("{id}")]
        public async Task<ActionResult<Product>> GetProduct(int id)
        {
            var product = await _productService.SingleOrDefaultAsync(i=> i.Id == id);

            if (product == null)
            {
                return NotFound();
            }

            return Ok(_mapper.Map<ProductDto>(product));
        }

        // PUT: api/Products/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [ServiceFilter(typeof(NotFoundFilter))]
        [HttpPut("{id}")]
        public async Task<IActionResult> PutProduct(int id, ProductDto productDto)
        {
            if (id != productDto.Id)
            {
                return BadRequest();
            }

            _productService.Update(_mapper.Map<Product>(productDto));

            return NoContent();
        }

        // POST: api/Products
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<Product>> PostProduct(ProductDto productDto)
        {
            var newproduct = await _productService.AddAsync(_mapper.Map<Product>(productDto));

            return CreatedAtAction("GetProduct", new { id = newproduct.Id }, _mapper.Map<ProductDto>(newproduct));
        }

        // DELETE: api/Products/5
        [ServiceFilter(typeof(NotFoundFilter))]
        [HttpDelete("{id}")]
        public async Task<ActionResult<Product>> DeleteProduct(int id)
        {
            var product = await _productService.SingleOrDefaultAsync(i=>i.Id == id);
            if (product == null)
            {
                return NotFound();
            }

            _productService.Remove(product);

            return NoContent();
        }


        [HttpGet("{productId}/category")]
        public async Task<ActionResult> GetWithCategoryByIdAsync(int productId)
        {
            var product = await _productService.GetWithCategoryByIdAsync(productId);
            return Ok(_mapper.Map<ProductWithCategoryDto>(product));
        }

        private async Task<bool> ProductExists(int id)
        {
            var product = await _productService.SingleOrDefaultAsync(e => e.Id == id);
            if (product != null) return true;
            else return false;
        }
    }
}
