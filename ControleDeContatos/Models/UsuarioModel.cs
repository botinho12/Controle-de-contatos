using System.ComponentModel.DataAnnotations;
using ControleDeContatos.Enums;
using ControleDeContatos.Helper;

namespace ControleDeContatos.Models
{
    public class UsuarioModel
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Digite o nome do usuario")]
        public required string Nome { get; set; }

        [Required(ErrorMessage = "Digite o Login do usuario")]
        public required string Login { get; set; }
   
        [Required(ErrorMessage = "Digite o E-mail do usuario")]
        [EmailAddress(ErrorMessage = "O e-mail informado nao e valido")]
        public required string Email { get; set; }

        [Required(ErrorMessage = "Informe o perfil do usuario")]
        public PerfilEnum? Perfil { get; set; }
        [Required(ErrorMessage = "Digite a senha do usuario")]
        public  string? Senha { get; set; }
        public DateTime DataCadastro { get; set; }
        public DateTime? DataAtualizacao { get; set; }

        public virtual List<ContatoModel>?Contatos { get; set; }

        public bool SenhaValida(string senha)
        {
            return Senha == senha.GerarHash();
        }

        public void SetSenhaHash()
        {
#pragma warning disable CS8604 // Possível argumento de referência nula.
            Senha = Senha.GerarHash();
#pragma warning restore CS8604 // Possível argumento de referência nula.
        }

        public void SetNovaSenha(string novaSenha)
        {
            Senha = novaSenha.GerarHash();
        }

        public string GerarNovaSenha()
        {
            string novaSenha = Guid.NewGuid().ToString().Substring(0, 8);
            Senha = novaSenha.GerarHash();
            return novaSenha;
        }
    }
}
