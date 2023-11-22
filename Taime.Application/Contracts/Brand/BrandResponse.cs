using Taime.Application.Data.MySql.Entities;

namespace Taime.Application.Contracts.Brand
{
    public class BrandResponse
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public string Url { get; set; }

        public BrandResponse() { }

        public BrandResponse(BrandEntity brandEntity) 
        { 
            Id = brandEntity.Id;
            Title = brandEntity.Title;
            Url = brandEntity.Url;
        }
    }
}
