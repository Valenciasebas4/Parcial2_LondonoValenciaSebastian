using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;
using System.Reflection.Metadata.Ecma335;

namespace Parcial2_LondonoValenciaSebastian.DAL.Entities
{
    public class NaturalPerson : Entity
    {

        #region Properties


        [Display(Name = "Nombre Completo")]
        [MaxLength(50, ErrorMessage = "El campo {0} debe tener máximo {1} caracteres.")]
        [Required(ErrorMessage = "El campo {0} es obligatorio.")]
        public string FullName { get; set; }


        [Display(Name = "Correo")]
        [MaxLength(50)]
        [Required(ErrorMessage = "El campo {0} es obligatorio")]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        [Display(Name = "Año de Nacimiento")]
        [Required(ErrorMessage = "El campo {0} es obligatorio")]
        public int BirthYear { get; set; }

        [Display(Name = "Edad")]
        public int Age { get; set; }







        #endregion
    }
}
