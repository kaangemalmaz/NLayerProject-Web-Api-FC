using System.Collections.Generic;

namespace UdemyNLayerProject.Web.DTOs
{
    public class CategoryWithProductsDto : CategoryDto
    {
        public IEnumerable<ProductDto> Products { get; set; }
    }
}
