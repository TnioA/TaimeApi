using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Taime.Application.Contracts.User;
using Taime.Application.Utils.Data.MySql;

namespace Taime.Application.Data.MySql.Entities
{
    [Table("user")]
    public class UserEntity : MySqlEntityBase
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("id")]
        public int Id { get; set; }

        [Column("name")]
        public string Name { get; set; }

        [Column("email")]
        public string Email { get; set; }

        [Column("document")]
        public string Document { get; set; }

        [Column("phone")]
        public string Phone { get; set; }

        [Column("password")]
        public string Password { get; set; }

        [Column("is_admin")]
        public bool IsAdmin { get; set; }

        public UserEntity() { }

        public UserEntity(UserRequest request) 
        {
            Name = request.Name;
            Email = request.Email;
            Document = request.Document;
            Phone = request.Phone;
            Password = request.Password;
            IsAdmin = false;
        }
    }
}