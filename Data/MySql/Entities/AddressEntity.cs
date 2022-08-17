using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TaimeApi.Data.MySql.Entities
{
    [Table("address")]
    public class AddressEntity
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("id")]
        public int Id { get; set; }

        [Column("zipCode")]
        public string ZipCode { get; set; }

        [Column("address")]
        public string Address { get; set; }

        [Column("number")]
        public int Number { get; set; }

        [Column("district")]
        public string District { get; set; }

        [Column("complement")]
        public string Complement { get; set; }

        [Column("city")]
        public string City { get; set; }

        [Column("state")]
        public string State { get; set; }

        [Column("userId")]
        public int UserId { get; set; }
    }
}