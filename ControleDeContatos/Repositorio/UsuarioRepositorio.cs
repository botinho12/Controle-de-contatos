using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ControleDeContatos.Data;
using ControleDeContatos.Models;
using Microsoft.EntityFrameworkCore;

namespace ControleDeContatos.Repositorio
{
    public class UsuarioRepositorio : IUsuarioRepositorio
    {
        private readonly BancoContext _bancoContext;
        public UsuarioRepositorio(BancoContext bancoContext)
        {
            _bancoContext = bancoContext;
        }
        public UsuarioModel BuscarPorLogin(string login)
        {
#pragma warning disable CS8603 // Possível retorno de referência nula.
            return _bancoContext.Usuarios.FirstOrDefault(x => x.Login.ToUpper() == login.ToUpper());
#pragma warning restore CS8603 // Possível retorno de referência nula.
        }

        public UsuarioModel BuscarPorEmailELogin(string email, string Login)
        {
#pragma warning disable CS8603 // Possível retorno de referência nula.
            return _bancoContext.Usuarios.FirstOrDefault(x => x.Email.ToUpper() == email.ToUpper() && x.Login.ToUpper() == Login.ToUpper());
#pragma warning restore CS8603 // Possível retorno de referência nula.
        }

        public UsuarioModel ListarPorId(int id)
        {
#pragma warning disable CS8603 // Possível retorno de referência nula.
            return _bancoContext.Usuarios.FirstOrDefault(x => x.Id == id);
#pragma warning restore CS8603 // Possível retorno de referência nula.
        }

        public List<UsuarioModel> BuscarTodos()
        {
            return _bancoContext.Usuarios
                .Include(x => x.Contatos)
                .ToList();
        }
        
        public UsuarioModel Adicionar(UsuarioModel usuario)
        {
            usuario.DataCadastro = DateTime.Now;
            usuario.SetSenhaHash();
            //GRAVAR NO BANCO DE DADOS
            Microsoft.EntityFrameworkCore.ChangeTracking.EntityEntry<UsuarioModel> entityEntry = _bancoContext.Usuarios.Add(usuario);
            _bancoContext.SaveChanges();
            return usuario;
        }
        public UsuarioModel Atualizar(UsuarioModel usuario)
        {
            UsuarioModel usuarioDB = ListarPorId(usuario.Id);

            if (usuarioDB == null) throw new Exception("Houve um erro na atualizaçao do usuario");

            usuarioDB.Nome = usuario.Nome;
            usuarioDB.Email = usuario.Email;
            usuarioDB.Login= usuario.Login;
            usuarioDB.Perfil = usuario.Perfil;
            usuarioDB.DataCadastro = DateTime.Now;

            _bancoContext.Usuarios.Update(usuarioDB);
            _bancoContext.SaveChanges();
            return usuarioDB;
           
        }

        public UsuarioModel AlterarSenha(AlterarSenhaModelcs alteraSenhaModel)
        {
            UsuarioModel usuarioDB = ListarPorId(alteraSenhaModel.Id);

            if (usuarioDB == null) throw new Exception("Houve um erro na atualizaçao da senha, usuario nao encontrado!.");

            if (!usuarioDB.SenhaValida(alteraSenhaModel.SenhaAtual)) throw new Exception("Senha atual nao confere!.");

            if (usuarioDB.SenhaValida(alteraSenhaModel.NovaSenha)) throw new Exception("Nova senha deve ser diferente da senha atual!.");

            usuarioDB.SetNovaSenha(alteraSenhaModel.NovaSenha);
            usuarioDB.DataAtualizacao = DateTime.Now;

            _bancoContext.Usuarios.Update(usuarioDB);
            _bancoContext.SaveChanges();

            return usuarioDB;

        }

        public bool Apagar(int id)
        {
            UsuarioModel usuarioDB = ListarPorId(id);
            if (usuarioDB == null) throw new Exception("Houve um erro na deleçao do usuario");

            _bancoContext.Usuarios.Remove(usuarioDB);
            _bancoContext.SaveChanges();

            return true;
        }

    }

}
