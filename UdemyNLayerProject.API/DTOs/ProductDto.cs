using System.ComponentModel.DataAnnotations;

namespace UdemyNLayerProject.API.DTOs
{
    public class ProductDto
    {
        public int Id { get; set; }
        [Required(ErrorMessage ="İsim alanı boş olamaz")]
        public string Name { get; set; }
        [Range(1, int.MaxValue, ErrorMessage = "Stok alanı 1 den büyük değer olmalıdır.")]
        public int Stock { get; set; }
        [Range(1, double.MaxValue, ErrorMessage = "Fiyat alanı 1 den büyük değer olmalıdır.")]
        public decimal Price { get; set; }
        public int CategoryId { get; set; }
    }
}
