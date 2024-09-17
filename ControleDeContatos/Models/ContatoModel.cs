using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;



namespace ControleDeContatos.Models
{
    [Table("CONTATOS")]
    public class ContatoModel
    {
        [Key]
        public  int? Id { get; set; }
        [Required(ErrorMessage = "Digite o nome do contato")]
        public required string?  Nome { get; set; }
        [Required(ErrorMessage = "Digite o E-mail do contato")]
        [EmailAddress(ErrorMessage = "O e-mail informado nao e valido")]
        public required string? Email { get; set; }
        [Required(ErrorMessage = "Digite o Celular do contato")]
        [Phone(ErrorMessage = "O celular informado nao e valido")]
        public required string?Celular { get; set; }

        public int? UsuarioId { get; set; }

        public required UsuarioModel? Usuario { get; set; }
    }
}
