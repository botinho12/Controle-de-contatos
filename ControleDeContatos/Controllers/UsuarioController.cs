using ControleDeContatos.Filters;
using ControleDeContatos.Models;
using ControleDeContatos.Repositorio;
using Microsoft.AspNetCore.Mvc;
using System;

namespace ControleDeContatos.Controllers
{
    [PaginaRestritaSomenteAdmin]
    public class UsuarioController(IUsuarioRepositorio usuarioRepositorio,IContatoRepositorio contatoRepositorio) : Controller
    {
        private readonly IUsuarioRepositorio _usuarioRepositorio = usuarioRepositorio;
        private readonly IContatoRepositorio _contatoRepositorio = contatoRepositorio;
        public IActionResult Index()
        {
            List<UsuarioModel> usuarios = _usuarioRepositorio.BuscarTodos();
            return View(usuarios);
        }

        public IActionResult Criar()
        {
            return View();
        }

        public IActionResult ListarContatosPorUsuarioId(int id)
        {
            List<ContatoModel> contatos = _contatoRepositorio.BuscarTodos(id);
            return PartialView("_ContatosUsuario", contatos);
        }

        [HttpPost]
        public IActionResult Criar(UsuarioModel usuario)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    _usuarioRepositorio.Adicionar(usuario);
                    TempData["MensagemSucesso"] = "Usuario cadastrado com sucesso";
                    return RedirectToAction("Index");
                }

                return View(usuario);
            }
            catch (Exception erro)
            {
                TempData["MensagemErro"] = $"Ops , nao conseguimos cadastrar seu usuario , tente novamente , detalhe do erro:{erro.Message}";
                return RedirectToAction("Index");
            }
        }

        public IActionResult ApagarConfirmacao(int id)
        {
            UsuarioModel usuario = _usuarioRepositorio.ListarPorId(id);
            return View(usuario);
        }
        
        public IActionResult Apagar(int id)
        {
            try
            {
                bool apagado = _usuarioRepositorio.Apagar(id);

                if (apagado)
                {
                    TempData["MensagemSucesso"] = "Usuario apagado com sucesso";
                    return RedirectToAction("Index");
                }
                else
                {
                    TempData["MensagemErro"] = "Nao conseguimos apagar seu usuario";
                    return RedirectToAction("Index");
                }
            }
            catch (Exception erro)
            {
                TempData["MensagemErro"] = $"Ops , nao conseguimos apagar seu usuario, tente novamente , detalhe do erro:{erro.Message}";
                return RedirectToAction("Index");
            }
        }

        public IActionResult Editar(int id)
        {
            UsuarioModel usuario = _usuarioRepositorio.ListarPorId(id);
            return View(usuario);
        }
        [HttpPost]
        public IActionResult Editar(UsuarioSemSenhaModel UsuarioSemSenhaModel)
        {
            try
            {
                UsuarioModel usuario = null;
                if (ModelState.IsValid)
                {
#pragma warning disable CS8601 // Possível atribuição de referência nula.
                    usuario = new UsuarioModel()
                    {
                        Id = UsuarioSemSenhaModel.Id,
                        Nome = UsuarioSemSenhaModel.Nome,
                        Login = UsuarioSemSenhaModel.Login,
                        Email = UsuarioSemSenhaModel.Email,
                        Perfil = UsuarioSemSenhaModel.Perfil
                    };
#pragma warning restore CS8601 // Possível atribuição de referência nula.

                    usuario = _usuarioRepositorio.Atualizar(usuario);
                    TempData["MensagemSucesso"] = "Usuario alterado com sucesso";
                    return RedirectToAction("Index");
                }
                return View(usuario);
            }

            catch (Exception erro)
            {
                TempData["MensagemErro"] = $"Ops , nao conseguimos atualizar seu usuario , tente novamente , detalhe do erro:{erro.Message}";
                return RedirectToAction("Index");
            }
        }
    }
}
