
using System.ComponentModel.DataAnnotations;

namespace Schedule.domain.Entities
{
    public class Contact : BaseEntity
    {
        [Required(ErrorMessage = "Este campo é obrigatório")]
        [MaxLength(124, ErrorMessage = "Este campo deve conter entre 3 e 60 caracteres")]
        public string Name { get; set; }

        [DataType(DataType.EmailAddress, ErrorMessage = "E-mail inválido")]
        public string EMail { get; set; }

        [Required(ErrorMessage = "Este campo é obrigatório")]
        [DataType(DataType.PhoneNumber)]
        public string PhoneNumber { get; set; }
        public int UserId { get; set; }
        public User User { get; set; }
    }
}
