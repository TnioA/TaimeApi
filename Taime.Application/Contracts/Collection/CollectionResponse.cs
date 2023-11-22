using Taime.Application.Data.MySql.Entities;

namespace Taime.Application.Contracts.Collection
{
    public class CollectionResponse
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public string Url { get; set; }

        public CollectionResponse() { }

        public CollectionResponse(CollectionEntity collectionEntity) 
        { 
            Id = collectionEntity.Id;
            Title = collectionEntity.Title;
            Url = collectionEntity.Url;
        }
    }
}
