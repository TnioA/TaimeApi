using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Taime.Application.Utils.Data.MySql;

namespace Taime.Application.Data.MySql.Entities
{
    [Table("sku")]
    public class SkuEntity : MySqlEntityBase
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("id")]
        public int Id { get; set; }

        [Column("color")]
        public string Color { get; set; }

        [Column("size")]
        public string Size { get; set; }

        [Column("weight")]
        public string Weight { get; set; }

        [Column("stock")]
        public int Stock { get; set; }

        [Column("url_img")]
        public string UrlImg { get; set; }
    }
}