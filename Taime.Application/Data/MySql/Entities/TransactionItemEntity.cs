using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Taime.Application.Utils.Data.MySql;

namespace Taime.Application.Data.MySql.Entities
{
    [Table("transaction_item")]
    public class TransactionItemEntity : MySqlEntityBase
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("id")]
        public int Id { get; set; }

        [Column("count")]
        public int Count { get; set; }

        [Column("transaction_id")]
        public int TransactionId { get; set; }

        [Column("sku_id")]
        public int SkuId { get; set; }
    }
}