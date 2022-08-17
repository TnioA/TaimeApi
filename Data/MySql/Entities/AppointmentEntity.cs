using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using TaimeApi.Utils.Data.MySql;

namespace TaimeApi.Data.MySql.Entities
{
    [Table("appointment")]
    public class AppointmentEntity : MySqlEntityBase
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("id")]
        public int Id { get; set; }

        [Column("description")]
        public string Description { get; set; }

        [Column("userId")]
        public int UserId { get; set; }
    }
}