using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Taime.Application.Contracts.Product;
using Taime.Application.Utils.Data.MySql;

namespace Taime.Application.Data.MySql.Entities
{
    [Table("product")]
    public class ProductEntity : MySqlEntityBase
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("id")]
        public int Id { get; set; }

        [Column("title")]
        public string Title { get; set; }

        [Column("description")]
        public string Description { get; set; }

        [Column("details")]
        public string Details { get; set; }

        [Column("url")]
        public string Url { get; set; }

        [Column("price")]
        public decimal Price { get; set; }

        [Column("lastPrice")]
        public decimal LastPrice { get; set; }

        [Column("unity_count")]
        public string UnityCount { get; set; }

        [Column("category_id")]
        public int CategoryId { get; set; }

        [Column("brand_id")]
        public int BrandId { get; set; }

        [Column("collection_id")]
        public int CollectionId { get; set; }

        public ProductEntity() { }

        public ProductEntity(ProductRequest request) 
        {
            Title = request.Title;
            Description = request.Description;
            Details = request.Details;
            Url = request.Url;
            Price = request.Price;
            LastPrice = request.LastPrice;
            UnityCount = request.UnityCount;
            CategoryId = request.CategoryId;
            BrandId = request.BrandId;
            CollectionId = request.CollectionId;
        }
    }
}