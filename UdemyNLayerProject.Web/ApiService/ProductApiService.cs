using Newtonsoft.Json;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using UdemyNLayerProject.Web.DTOs;

namespace UdemyNLayerProject.Web.ApiService
{
    public class ProductApiService
    {
        private readonly HttpClient _httpClient;

        public ProductApiService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<IEnumerable<ProductDto>> GetProducts()
        {
            IEnumerable<ProductDto> productDtos;
            var response = await _httpClient.GetAsync("products");
            if (response.IsSuccessStatusCode)
            {
                productDtos = JsonConvert.DeserializeObject<IEnumerable<ProductWithCategoryDto>>(await response.Content.ReadAsStringAsync());
            }
            else
            {
                productDtos = null;
            }
            return productDtos;
        }

        public async Task<ProductDto> GetProduct(int id)
        {
            ProductDto productDto;
            var response = await _httpClient.GetAsync($"products/{id}");
            if (response.IsSuccessStatusCode)
            {
                productDto = JsonConvert.DeserializeObject<ProductDto>(await response.Content.ReadAsStringAsync());
            }
            else
            {
                productDto = null;
            }
            return productDto;
        }

        public async Task<ProductDto> AddProducts(ProductDto productDto)
        {
            StringContent stringContent = new StringContent(JsonConvert.SerializeObject(productDto),
                                                            Encoding.UTF8,
                                                            "application/json");

            var response = await _httpClient.PostAsync("products", stringContent);
            if (response.IsSuccessStatusCode)
            {
                productDto = JsonConvert.DeserializeObject<ProductDto>(await response.Content.ReadAsStringAsync());
            }
            else
            {
                productDto = null;
            }
            return productDto;
        }

        public async Task<ProductDto> UpdateProducts(int id, ProductDto productDto)
        {
            StringContent stringContent = new StringContent(JsonConvert.SerializeObject(productDto), Encoding.UTF8, "application/json");
            var response = await _httpClient.PutAsync($"products/{id}", stringContent);
            if (response.IsSuccessStatusCode)
            {
                productDto = JsonConvert.DeserializeObject<ProductDto>(await response.Content.ReadAsStringAsync());
            }
            else
            {
                productDto = null;
            }
            return productDto;
        }

        public async Task<bool> DeleteProducts(int id)
        {

            var response = await _httpClient.DeleteAsync($"products/{id}");
            if (response.IsSuccessStatusCode)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
