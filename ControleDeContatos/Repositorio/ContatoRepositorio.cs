﻿using ControleDeContatos.Data;
using ControleDeContatos.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ControleDeContatos.Repositorio
{
    public class ContatoRepositorio : IContatoRepositorio
    {
        private readonly BancoContext _context;

        public ContatoRepositorio(BancoContext bancoContext)
        {
            this._context = bancoContext;
        }

#pragma warning disable CS8603 // Possível retorno de referência nula.
        public ContatoModel ListarPorId(int id) => _context.Contatos.FirstOrDefault(x => x.Id == id);
#pragma warning restore CS8603 // Possível retorno de referência nula.

        public List<ContatoModel> BuscarTodos(int usuarioId)
        {
            return _context.Contatos.Where(x => x.UsuarioId == usuarioId).ToList();
        }

        public ContatoModel Adicionar(ContatoModel contato)
        {
            _context.Contatos.Add(contato);
            _context.SaveChanges();
            return contato;
        }

        public ContatoModel Atualizar(ContatoModel contato)
        {
            ContatoModel contatoDB = ListarPorId((int)contato.Id);

            if (contatoDB == null) throw new Exception("Houve um erro na atualização do contato!");

            contatoDB.Nome = contato.Nome;
            contatoDB.Email = contato.Email;
            contatoDB.Celular = contato.Celular;

            _context.Contatos.Update(contatoDB);
            _context.SaveChanges();

            return contatoDB;
        }

        public bool Apagar(int id)
        {
            ContatoModel contatoDB = ListarPorId(id);

            if (contatoDB == null) throw new Exception("Houve um erro na deleção do contato!");

            _context.Contatos.Remove(contatoDB);
            _context.SaveChanges();

            return true;
        }

     
    }
}