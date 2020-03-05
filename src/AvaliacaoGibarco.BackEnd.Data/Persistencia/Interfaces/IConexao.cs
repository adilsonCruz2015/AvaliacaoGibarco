using AvaliacaoGibarco.BackEnd.Dominio.ObjetoDeValor;
using System;
using System.Data;

namespace AvaliacaoGibarco.BackEnd.Data.Persistencia.Interfaces
{
    public interface IConexao : IDisposable
    {
        void Fechar();

        IDbConnection Abrir();

        bool HaSessao();

        bool HaTransicao();

        void IniciarTransicao();

        void FecharTransicao();

        void DesfazerTransicao();

        IDbConnection Sessao { get; }

        IDbTransaction Transicao { get; }

        void InformarBanco(Banco banco);
               
    }
}
