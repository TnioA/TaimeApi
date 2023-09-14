using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Taime.Application.Utils.Data.MySql;

namespace Taime.Application.Data.MySql.Entities
{
    [Table("product")]
    public class ProductEntity : MySqlEntityBase
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("id")]
        public int Id { get; set; }

        [Column("description")]
        public string Description { get; set; }
    }
}