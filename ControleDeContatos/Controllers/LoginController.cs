﻿using ControleDeContatos.Helper;
using ControleDeContatos.Models;
using ControleDeContatos.Repositorio;
using Microsoft.AspNetCore.Mvc;
using System;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace ControleDeContatos.Controllers
{
    public class LoginController : Controller
    {
        private readonly IUsuarioRepositorio _usuarioRepositorio;
        private readonly ISessao _sessao;
        public LoginController(IUsuarioRepositorio usuarioRepositorio,
                                ISessao sessao) 
        {
            _usuarioRepositorio = usuarioRepositorio;
            _sessao = sessao;
        }
        public IActionResult Index()
        {
            // se o usuario estiver logado , redirecionar para a hoome

            if(_sessao.BuscarSessaoDoUsuario() != null)
                return RedirectToAction("Index","Home");

            return View();
        }

        public IActionResult RedefinirSenha()
        {
            return View();
        }
        public IActionResult Sair()
        {
            _sessao.RemoverSessaoDoUsuario();
            return RedirectToAction("Index", "Login");
        }
        [HttpPost]
        public IActionResult Entrar(LoginModel loginModel)
        {
            try
            {
                if (ModelState.IsValid)
                {
                   UsuarioModel usuario = _usuarioRepositorio.BuscarPorLogin(loginModel.Login);

                    if(usuario != null)
                    {
                        if(usuario.SenhaValida(loginModel.Senha))
                        {
                            _sessao.CriarSessaoDoUsuario(usuario);
                            return RedirectToAction("Index", "Home");
                        }

                        TempData["MensagemErro"] = $"A  senha do usuario é  invalida, tente novamente.";
                    }

                    TempData["MensagemErro"] = $"Usuario e/ou senha invalida(s).Por favor tente novamente.";
                }
                return View("Index");
            }
            catch (Exception erro)
            {
                TempData["MensagemErro"] = $"Ops nao conseguimos realizar seu login,tente novamente,detalhe do erro: {erro.Message}";
                return RedirectToAction("Index");
            }

        }

        [HttpPost] 
        public IActionResult EnviarLinkParaRedefinirSenha(RedefinirSenhaModel redefinirSenhaModel)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    UsuarioModel usuario = _usuarioRepositorio.BuscarPorEmailELogin(redefinirSenhaModel.Email,redefinirSenhaModel.Login);

                    if (usuario != null)
                    {
                        string novaSenha = usuario.GerarNovaSenha();

                        TempData["MensagemSucesso"] = $" Enviamos para seu e-mail cadastrado uma nova senha.";
                        return RedirectToAction("Index", "Login");
                    }

                    TempData["MensagemErro"] = $" Nao conseguimos redefinir sua senha. Por favor , verifique os dados informados.";
                }
                return View("Index");
            }
            catch (Exception erro)
            {
                TempData["MensagemErro"] = $"Ops nao conseguimos redefinir sua senha, tente novamente, detalhe do erro: {erro.Message}";
                return RedirectToAction("Index");
            }
        }
    }
}
