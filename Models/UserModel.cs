using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace TaimeApi.Models
{
    public class UserModel
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "Este campo é obrigatório.")]
        [MinLength(3, ErrorMessage = "Este campo deve conter entre 3 e 60 caracteres.")]
        [MaxLength(60, ErrorMessage = "Este campo deve conter entre 3 e 60 caracteres.")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Este campo é obrigatório.")]
        [MinLength(3, ErrorMessage = "Este campo deve conter entre 3 e 60 caracteres.")]
        [MaxLength(60, ErrorMessage = "Este campo deve conter entre 3 e 60 caracteres.")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Este campo é obrigatório.")]
        [MinLength(6, ErrorMessage = "Este campo deve conter entre 6 e 10 caracteres.")]
        [MaxLength(10, ErrorMessage = "Este campo deve conter entre 6 e 10 caracteres.")]
        public string Password { get; set; }
        
        [Required(ErrorMessage = "Este campo é obrigatório.")]
        [MinLength(10, ErrorMessage = "Este campo deve conter entre 10 e 11 caracteres.")]
        [MaxLength(11, ErrorMessage = "Este campo deve conter entre 10 e 11 caracteres.")]
        public string Phone { get; set; }

        [Required(ErrorMessage = "Este campo é obrigatório.")]
        public bool IsAdmin { get; set; }

        [Required(ErrorMessage = "Este campo é obrigatório.")]
        public List<AddressModel> AddressList { get; set; }
    }
}