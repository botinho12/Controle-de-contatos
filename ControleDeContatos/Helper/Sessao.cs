using System.Text.Json.Serialization;
using ControleDeContatos.Models;
using Newtonsoft.Json;

namespace ControleDeContatos.Helper
{
    public class Sessao : ISessao
    {

        private readonly IHttpContextAccessor _httpcontext;

        public Sessao(IHttpContextAccessor httpcontext)
        {
            _httpcontext = httpcontext;
        }

#pragma warning disable CS8766 // A nulidade de tipos de referência no tipo de retorno não corresponde ao membro implementado implicitamente (possivelmente devido a atributos de nulidade).
        public UsuarioModel? BuscarSessaoDoUsuario()
#pragma warning restore CS8766 // A nulidade de tipos de referência no tipo de retorno não corresponde ao membro implementado implicitamente (possivelmente devido a atributos de nulidade).
        {
#pragma warning disable CS8602 // Desreferência de uma referência possivelmente nula.
            string sessaoUsuario = _httpcontext.HttpContext.Session.GetString("sessaoUsuarioLogado");
#pragma warning restore CS8602 // Desreferência de uma referência possivelmente nula.

            if (string.IsNullOrEmpty(sessaoUsuario)) return null;
            return JsonConvert.DeserializeObject<UsuarioModel>(sessaoUsuario);
        }

        public void CriarSessaoDoUsuario(UsuarioModel usuario)
        {
            string valor = JsonConvert.SerializeObject(usuario);

#pragma warning disable CS8602 // Desreferência de uma referência possivelmente nula.
            _httpcontext.HttpContext.Session.SetString("sessaoUsuarioLogado", valor);
#pragma warning restore CS8602 // Desreferência de uma referência possivelmente nula.
        }

#pragma warning disable CS8602 // Desreferência de uma referência possivelmente nula.
        public void RemoverSessaoDoUsuario() => _httpcontext.HttpContext.Session.Remove("sessaoUsuarioLogado");
#pragma warning restore CS8602 // Desreferência de uma referência possivelmente nula.
    }
}
