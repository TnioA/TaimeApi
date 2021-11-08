using System.ComponentModel.DataAnnotations;

namespace TaimeApi.Models
{
    public class AddressModel
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "Este campo é obrigatório.")]
        [StringLength(8, ErrorMessage = "Este campo deve conter 8 caracteres.")]
        public string ZipCode { get; set; }

        [Required(ErrorMessage = "Este campo é obrigatório.")]
        [MinLength(3, ErrorMessage = "Este campo deve conter entre 3 e 60 caracteres.")]
        [MaxLength(60, ErrorMessage = "Este campo deve conter entre 3 e 60 caracteres.")]
        public string Address { get; set; }

        [Required(ErrorMessage = "Este campo é obrigatório.")]
        [Range(1, int.MaxValue, ErrorMessage = "Este campo deve ser maior que zero.")]
        public int Number { get; set; }

        [Required(ErrorMessage = "Este campo é obrigatório.")]
        [MinLength(10, ErrorMessage = "Este campo deve conter entre 10 e 11 caracteres.")]
        [MaxLength(11, ErrorMessage = "Este campo deve conter entre 10 e 11 caracteres.")]
        public string District { get; set; }
        
        [MaxLength(60, ErrorMessage = "Este campo deve conter no máximo 60 caracteres.")]
        public string Complement { get; set; }

        [Required(ErrorMessage = "Este campo é obrigatório.")]
        [MinLength(3, ErrorMessage = "Este campo deve conter entre 3 e 60 caracteres.")]
        [MaxLength(60, ErrorMessage = "Este campo deve conter entre 3 e 60 caracteres.")]
        public string City { get; set; }

        [Required(ErrorMessage = "Este campo é obrigatório.")]
        [StringLength(2, ErrorMessage = "Este campo deve conter 2 caracteres.")]
        public string State { get; set; }
    }
}