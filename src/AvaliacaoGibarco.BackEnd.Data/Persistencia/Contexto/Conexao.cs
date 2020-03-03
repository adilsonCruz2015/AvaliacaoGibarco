using AvaliacaoGibarco.BackEnd.Data.Persistencia.Interfaces;
using AvaliacaoGibarco.BackEnd.Dominio.ObjetoDeValor;
using System;
using System.Data;
using System.Data.SqlClient;

namespace AvaliacaoGibarco.BackEnd.Data.Persistencia.Contexto
{
    public class Conexao : IConexao
    {
        public Conexao(IResolverConexao dadosDeConexao)
        {
            _dadosDeConexao = dadosDeConexao;
        }

        private readonly IResolverConexao _dadosDeConexao;
        private IDbConnection _sessao;
        private Banco _banco;

        public IDbTransaction Transicao { get; private set; }

        public IDbConnection Sessao
        {
            get { return _sessao ?? Abrir(); }
        }

        public IDbConnection Abrir()
        {
            if (object.Equals(_sessao, null) || _sessao.State.Equals(ConnectionState.Closed))
            {
                _sessao = new SqlConnection(_dadosDeConexao.ObterConexao(_banco));
                _sessao.Open();
            }

            return _sessao;
        }

        public void DesfazerTransicao()
        {
            Transicao.Rollback();
        }

        public void Dispose()
        {
            Fechar();
            GC.SuppressFinalize(this);
        }

        public void Fechar()
        {
            if (!object.Equals(_sessao, null))
            {
                if (_sessao.State.Equals(ConnectionState.Open))
                    _sessao.Close();

                _sessao.Dispose();
                _sessao = null;
            }
        }

        public void FecharTransicao()
        {
            Transicao.Commit();
        }

        public bool HaSessao()
        {
            return !Equals(_sessao, null);
        }

        public bool HaTransicao()
        {
            return !Equals(Transicao, null) && !Equals(Transicao.Connection, null);
        }

        public void IniciarTransicao()
        {
            if (HaSessao()) Fechar();

            Abrir();

            Transicao = Sessao.BeginTransaction();
        }

        public void InformarBanco(Banco banco)
        {
            _banco = banco;
        }
    }
}
