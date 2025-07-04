namespace E_Commerce.Models
{
    public class Product
    {
        public string Name { get; set; }
        public decimal Price { get; set; }
        public int Stock { get; set; }
        public bool NeedsShipping { get; set; }
        public double Weight { get; set; }
        public DateTime? ExpiryDate { get; set; }
        public bool IsExpired
        {
            get { return ExpiryDate.HasValue && DateTime.Now > ExpiryDate.Value; }
        }
    }
}
