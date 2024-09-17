using System.ComponentModel.DataAnnotations;

namespace ControleDeContatos.Models
{
    public class AlterarSenhaModelcs
    {
        public int Id { get; set; }

        [Required(ErrorMessage ="Digite a senha atual do usuario")]
        public required string SenhaAtual { get; set; }
        [Required(ErrorMessage = "Digite a nova senha do usuario")]
        public required string NovaSenha { get; set; }
        [Required(ErrorMessage = "Confirme a nova senha  do usuario")]
        [Compare("NovaSenha" , ErrorMessage ="Senha nao confere com a nova senha ")]
        public required string ConfirmarNovaSenha { get; set; }
    }
}
