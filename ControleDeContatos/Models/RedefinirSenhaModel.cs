using System.ComponentModel.DataAnnotations;

namespace ControleDeContatos.Models
{
    public class RedefinirSenhaModel
    {

        [Required(ErrorMessage = "Digite o Login")]
        public required string Login { get; set; }

        [Required(ErrorMessage = "Digite o e-mail")]
        public required string Email { get; set; }
    }
}
