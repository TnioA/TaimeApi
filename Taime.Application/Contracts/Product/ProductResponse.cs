using Taime.Application.Data.MySql.Entities;

namespace Taime.Application.Contracts.Product
{
    public class ProductResponse
    {
        public int Id { get; set; }

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

        public ProductResponse() { }

        public ProductResponse(ProductEntity productEntity)
        {
            Id = productEntity.Id;
            Title = productEntity.Title;
            Description = productEntity.Description;
            Details = productEntity.Details;
            Url = productEntity.Url;
            Price = productEntity.Price;
            LastPrice = productEntity.LastPrice;
            UnityCount = productEntity.UnityCount;
            CategoryId = productEntity.CategoryId;
            BrandId = productEntity.BrandId;
            CollectionId = productEntity.CollectionId;
        }
    }
}
