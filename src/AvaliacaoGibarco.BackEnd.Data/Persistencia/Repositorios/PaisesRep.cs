using AvaliacaoGibarco.BackEnd.Data.Persistencia.Interfaces;
using AvaliacaoGibarco.BackEnd.Dominio.Commando.PaisesCmd;
using AvaliacaoGibarco.BackEnd.Dominio.Interfaces.Repositorio;
using AvaliacaoGibarco.BackEnd.Dominio.ObjetoDeValor;
using Dapper;
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
            StringBuilder sql = new StringBuilder();
            sql.Append($@"DELETE 
                          FROM { nameof(Pais)} WHERE Codigo = @Codigo ");

            var parametros = new DynamicParameters(new { id });

            return _conexao.Sessao.Execute(sql.ToString(), 
                                           parametros, 
                                           _conexao.Transicao);
        }

        public Pais[] Filtrar(FiltrarCmd comando)
        {
            StringBuilder sql = new StringBuilder();
            StringBuilder sqlFiltro = new StringBuilder();
            var parametros = new DynamicParameters();

            sql.Append($@"SELECT 
                                Codigo,
                                Descricao
                          FROM { nameof(Pais)} ");

            if (!string.IsNullOrEmpty(comando.Descricao))
            {
                sqlFiltro.Append(" AND Descricao = @Descricao ");
                parametros.Add("@Descricao", comando.Descricao, DbType.AnsiString, size: 100);
            }

            if (comando.Codigo > 0)
            {
                sqlFiltro.Append(" AND Codigo = @Codigo ");
                parametros.Add("@Codigo", comando.Codigo, DbType.Int32);
            }

            sql.Append(Regex.Replace(sqlFiltro.ToString(), @"^ AND ", " WHERE "));

            return _conexao.Sessao.Query<Pais>(sql.ToString(), 
                                                 parametros, 
                                                 _conexao.Transicao).ToArray();
        }

        public Pais[] Get()
        {
            StringBuilder sql = new StringBuilder();

            sql.Append($@"SELECT 
                                Codigo,
                                Descricao
                          FROM { nameof(Pais)} ");

            return _conexao.Sessao.Query<Pais>(sql.ToString(), 
                                                 new { }, 
                                                 _conexao.Transicao).ToArray();
        }

        public Pais Get(int id)
        {
            StringBuilder sql = new StringBuilder();
            sql.Append($@"SELECT 
                                Codigo,
                                Descricao
                          FROM { nameof(Pais)} WHERE Codigo = @Codigo");

            return _conexao.Sessao.Query<Pais>(sql.ToString(),
                                               new { Codigo = id },
                                               _conexao.Transicao).FirstOrDefault();
        }

        public Pais Insert(Pais obj)
        {
            int resultado = -1;

            StringBuilder sql = new StringBuilder();
            StringBuilder sqlLastRow = new StringBuilder();
            sql.Append($@"
                         INSERT INTO { nameof(Pais) } (Descricao)
                                VALUES(@Descricao)");

            var parametros = new DynamicParameters();
            parametros.Add("@Descricao", obj.Descricao, DbType.AnsiString, size: 100);

            resultado = _conexao.Sessao.Execute(sql.ToString(), parametros, _conexao.Transicao);

            if (resultado > 0)
            {
                sqlLastRow.Append("SELECT last_insert_rowid()");
                object id = _conexao.Sessao.ExecuteScalar(sqlLastRow.ToString(), new { }, _conexao.Transicao);

                if (!Equals(id, null))
                    obj.Codigo = int.Parse(id.ToString());
            }

            return obj;

        }

        public int Update(Pais obj)
        {
            StringBuilder sql = new StringBuilder();
            sql.Append($@"
                         UPDATE { nameof(Pais) } SET   Descricao = @Descricao, 
                                                   WHERE Codigo = @Codigo");

            var parametros = new DynamicParameters(new { obj.Codigo });
            parametros.Add("@Descricao", obj.Descricao, DbType.AnsiString, size: 100);

            return _conexao.Sessao.Execute(sql.ToString(), 
                                           parametros, 
                                           _conexao.Transicao);
        }
    }
}
