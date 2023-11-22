namespace Taime.Application.Contracts.Product
{
    public class ProductRequest
    {
        public string Title { get; set; }

        public string Description { get; set; }

        public string Details { get; set; }

        public string Url { get; set; }

        public decimal Price { get; set; }

        public decimal LastPrice { get; set; }

        public string UnityCount { get; set; }

        public int CategoryId { get; set; }

        public int BrandId { get; set; }

        public int CollectionId { get; set; }
    }
}
