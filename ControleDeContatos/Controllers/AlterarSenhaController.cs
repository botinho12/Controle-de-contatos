using Microsoft.AspNetCore.Mvc;
using ControleDeContatos.Models;
using ControleDeContatos.Repositorio;
using ControleDeContatos.Helper;
namespace ControleDeContatos.Controllers
{
    public class AlterarSenhaController : Controller
    {
        private readonly IUsuarioRepositorio _usuarioRepositorio;
        private readonly ISessao _sessao;
        public AlterarSenhaController(IUsuarioRepositorio usuarioRepositorio,
                                      ISessao sessao)
        {
            _usuarioRepositorio = usuarioRepositorio;
            _sessao = sessao;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]

        public IActionResult Alterar(AlterarSenhaModelcs alterarSenhaModel)
        {
            try
            {
               UsuarioModel usuarioLogado =  _sessao.BuscarSessaoDoUsuario();
                alterarSenhaModel.Id = usuarioLogado.Id;

                if (ModelState.IsValid)
                {

                    _usuarioRepositorio.AlterarSenha(alterarSenhaModel);
                    TempData["MensagemSucesso"] = "Senha alterada  com sucesso";
                    return View("Index", alterarSenhaModel);
                }
                return View("Index", alterarSenhaModel);
            }
            catch (Exception erro)
            {
                {
                    TempData["MensagemErro"] = $"Ops , nao conseguimos alterar sua senha , tente novamente , detalhe do erro:{erro.Message}";
                    return View("Index", alterarSenhaModel);
                }
            }
        }
    }
}
