using AvaliacaoGibarco.BackEnd.Data.Persistencia.Interfaces;
using AvaliacaoGibarco.BackEnd.Dominio.Commando.StatusCmd;
using AvaliacaoGibarco.BackEnd.Dominio.Interfaces.Repositorio;
using AvaliacaoGibarco.BackEnd.Dominio.ObjetoDeValor;
using Dapper;
using System.Data;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace AvaliacaoGibarco.BackEnd.Data.Persistencia.Repositorios
{
    public class StatusRep : IStatusRep
    {
        public StatusRep(IConexao conexao)
        {
            _conexao = conexao;
            _conexao.InformarBanco(Banco.AvaliacaoGibarco);
        }

        private readonly IConexao _conexao;

        public Status[] Filtrar(FiltrarCmd comando)
        {
            StringBuilder sql = new StringBuilder();
            StringBuilder sqlFiltro = new StringBuilder();
            var parametros = new DynamicParameters();

            sql.Append($@"SELECT 
                                Codigo,
                                Descricao,
                                Nome
                          FROM { nameof(Status)} ");

            if (!string.IsNullOrEmpty(comando.Descricao))
            {
                sqlFiltro.Append(" AND Descricao = @Descricao ");
                parametros.Add("@Descricao", comando.Descricao, DbType.AnsiString, size: 150);
            }

            sql.Append(Regex.Replace(sqlFiltro.ToString(), @"^ AND ", " WHERE "));

            return _conexao.Sessao.Query<Status>(sql.ToString(),
                                               parametros,
                                               _conexao.Transicao).ToArray();
        }

        public Status Insert(Status obj)
        {
            int resultado = -1;

            StringBuilder sql = new StringBuilder();
            StringBuilder sqlLastRow = new StringBuilder();
            sql.Append($@"
                         INSERT INTO { nameof(Status) } (Descricao, Nome)
                                VALUES(@Descricao, @Nome)");

            var parametros = new DynamicParameters();
            parametros.Add("@Descricao", obj.Descricao, DbType.AnsiString, size: 150);
            parametros.Add("@Nome", obj.Nome, DbType.AnsiString, size: 150);

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

        public int Update(Status obj)
        {
            StringBuilder sql = new StringBuilder();
            sql.Append($@"
                         UPDATE { nameof(Status) } SET   Descricao = @Descricao, 
                                                         Nome = @Nome  
                                                   WHERE Codigo = @Codigo");

            var parametros = new DynamicParameters(new { obj.Codigo });
            parametros.Add("@Descricao", obj.Descricao, DbType.AnsiString, size: 150);
            parametros.Add("@Nome", obj.Nome, DbType.AnsiString, size: 150);

            return _conexao.Sessao.Execute(sql.ToString(),
                                           parametros,
                                           _conexao.Transicao);
        }

        public int Delete(int id)
        {
            StringBuilder sql = new StringBuilder();
            sql.Append($@"DELETE 
                          FROM { nameof(Status)} WHERE Codigo = @Codigo ");

            var parametros = new DynamicParameters(new { Codigo = id });

            return _conexao.Sessao.Execute(sql.ToString(),
                                           parametros,
                                           _conexao.Transicao);
        }

        public Status[] Get()
        {
            StringBuilder sql = new StringBuilder();

            sql.Append($@"SELECT 
                                Codigo,
                                Descricao,
                                Nome
                          FROM { nameof(Status)} ");

            return _conexao.Sessao.Query<Status>(sql.ToString(),
                                                 new { },
                                                 _conexao.Transicao).ToArray();
        }

        public Status Get(int id)
        {
            StringBuilder sql = new StringBuilder();
            sql.Append($@"SELECT 
                                Codigo,
                                Descricao,
                                Nome
                          FROM { nameof(Status)} WHERE Codigo = @Codigo");

            return _conexao.Sessao.Query<Status>(sql.ToString(),
                                               new { Codigo = id },
                                               _conexao.Transicao).FirstOrDefault();
        }
    }
}
