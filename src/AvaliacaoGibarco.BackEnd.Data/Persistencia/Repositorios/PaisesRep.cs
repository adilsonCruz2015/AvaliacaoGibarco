using AvaliacaoGibarco.BackEnd.Data.Persistencia.Interfaces;
using AvaliacaoGibarco.BackEnd.Dominio.Commando.PaisesCmd;
using AvaliacaoGibarco.BackEnd.Dominio.Interfaces.Repositorio;
using AvaliacaoGibarco.BackEnd.Dominio.ObjetoDeValor;
using Dapper;
using System;
using System.Data;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace AvaliacaoGibarco.BackEnd.Data.Persistencia.Repositorios
{
    public class PaisesRep : IPaisesRep
    {
        public PaisesRep(IConexao conexao)
        {
            _conexao = conexao;
            _conexao.InformarBanco(Banco.AvaliacaoGibarco);
        }

        private readonly IConexao _conexao;

        public int Delete(int id)
        {
            throw new NotImplementedException();
        }

        public Paises[] Filtrar(FiltrarCmd comando)
        {
            StringBuilder sql = new StringBuilder();
            StringBuilder sqlFiltro = new StringBuilder();
            var parametros = new DynamicParameters();

            sql.Append($@"SELECT 
                                Codigo,
                                Descricao
                          FROM { nameof(Paises)} ");

            if (!string.IsNullOrEmpty(comando.Descricao))
            {
                sqlFiltro.Append(" AND Descricao LIKE CONCAT('%',@Descricao,'%') ");
                parametros.Add("@Descricao", comando.Descricao, DbType.AnsiString, size: 100);
            }

            if (comando.Codigo > 0)
            {
                sqlFiltro.Append(" AND Codigo = @Codigo ");
                parametros.Add("@Codigo", comando.Codigo, DbType.Int32);
            }

            sql.Append(Regex.Replace(sqlFiltro.ToString(), @"^ AND ", " WHERE "));

            return _conexao.Sessao.Query<Paises>(sql.ToString(), 
                                                 parametros, 
                                                 _conexao.Transicao).ToArray();
        }

        public Paises[] Get()
        {
            StringBuilder sql = new StringBuilder();

            sql.Append($@"SELECT 
                                Codigo,
                                Descricao
                          FROM { nameof(Paises)} ");

            return _conexao.Sessao.Query<Paises>(sql.ToString(), 
                                                 new { }, 
                                                 _conexao.Transicao).ToArray();
        }

        public Paises Get(int id)
        {
            StringBuilder sql = new StringBuilder();
            sql.Append($@"SELECT 
                                Codigo,
                                Descricao
                          FROM { nameof(Paises)} WHERE Codigo = @Codigo");

            return _conexao.Sessao.Query<Paises>(sql.ToString(),
                                                 new { Codigo = id },
                                                 _conexao.Transicao).FirstOrDefault();
        }

        public int Insert(Paises obj)
        {
            StringBuilder sql = new StringBuilder();
            sql.Append($@"
                         INSERT INTO { nameof(Paises) } (Descricao)
                                VALUES(@Descricao)");

            var parametros = new DynamicParameters();
            parametros.Add("@Descricao", obj.Descricao, DbType.AnsiString, size: 100);

            return _conexao.Sessao.Execute(sql.ToString(), parametros, _conexao.Transicao);

        }

        public int Update(Paises obj)
        {
            StringBuilder sql = new StringBuilder();
            sql.Append($@"
                         UPDATE { nameof(Paises) } SET   Descricao = @Descricao, 
                                                   WHERE Codigo = @Codigo");

            var parametros = new DynamicParameters(new
            {
                obj.Codigo
            });

            parametros.Add("@Descricao", obj.Descricao, DbType.AnsiString, size: 100);

            return _conexao.Sessao.Execute(sql.ToString(), parametros, _conexao.Transicao);
        }
    }
}
