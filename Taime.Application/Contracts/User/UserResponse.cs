using Taime.Application.Data.MySql.Entities;

namespace Taime.Application.Contracts.User
{
    public class UserResponse
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Email { get; set; }

        public string Document { get; set; }

        public string Phone { get; set; }

        public UserResponse() { }

        public UserResponse(UserEntity userEntity) 
        { 
            Id = userEntity.Id;
            Name = userEntity.Name;
            Email = userEntity.Email;
            Document = userEntity.Document;
            Phone = userEntity.Phone;
        }
    }
}
