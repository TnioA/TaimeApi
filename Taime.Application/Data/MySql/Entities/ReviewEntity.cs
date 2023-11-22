using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Taime.Application.Utils.Data.MySql;

namespace Taime.Application.Data.MySql.Entities
{
    [Table("review")]
    public class ReviewEntity : MySqlEntityBase
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("id")]
        public int Id { get; set; }

        [Column("name")]
        public string Name { get; set; }

        [Column("email")]
        public string Email { get; set; }

        [Column("comment")]
        public string Comment { get; set; }

        [Column("evaluation")]
        public int Evaluation { get; set; }

        [Column("product_id")]
        public int ProductId { get; set; }
    }
}