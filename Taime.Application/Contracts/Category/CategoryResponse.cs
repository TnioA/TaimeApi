using Taime.Application.Data.MySql.Entities;

namespace Taime.Application.Contracts.Category
{
    public class CategoryResponse
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public string Url { get; set; }

        public CategoryResponse() { }

        public CategoryResponse(CategoryEntity categoryEntity) 
        { 
            Id = categoryEntity.Id;
            Title = categoryEntity.Title;
            Url = categoryEntity.Url;
        }
    }
}
