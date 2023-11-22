using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Taime.Application.Utils.Data.MySql;

namespace Taime.Application.Data.MySql.Entities
{
    [Table("transaction_address")]
    public class TransactionAddressEntity : MySqlEntityBase
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("id")]
        public int Id { get; set; }

        [Column("zipcode")]
        public string ZipCode { get; set; }

        [Column("address")]
        public string Address { get; set; }

        [Column("number")]
        public int Number { get; set; }

        [Column("complement")]
        public string Complement { get; set; }

        [Column("district")]
        public string District { get; set; }

        [Column("city")]
        public string City { get; set; }

        [Column("state")]
        public string State { get; set; }

        [Column("transaction_id")]
        public int TransactionId { get; set; }
    }
}