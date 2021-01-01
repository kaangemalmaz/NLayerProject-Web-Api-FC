namespace UdemyNLayerProject.Entity.Models
{
    public class Product
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Stock { get; set; } = 0;
        public decimal Price { get; set; } = 0;
        public int CategoryId { get; set; }

        public bool IsDeleted { get; set; } = false;

        public string InnerBarcode { get; set; }

        public virtual Category Category { get; set; }
    }
}
