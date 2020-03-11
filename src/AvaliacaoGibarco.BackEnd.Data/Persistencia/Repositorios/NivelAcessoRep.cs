using AvaliacaoGibarco.BackEnd.Data.Persistencia.Interfaces;
using AvaliacaoGibarco.BackEnd.Dominio.Commando.NivelAcessoCmd;
using AvaliacaoGibarco.BackEnd.Dominio.Interfaces.Repositorio;
using AvaliacaoGibarco.BackEnd.Dominio.ObjetoDeValor;
using Dapper;
using System.Data;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace AvaliacaoGibarco.BackEnd.Data.Persistencia.Repositorios
{
    public class NivelAcessoRep : INivelAcessoRep
    {
        public NivelAcessoRep(IConexao conexao)
        {
            _conexao = conexao;
            _conexao.InformarBanco(Banco.AvaliacaoGibarco);
        }

        private readonly IConexao _conexao;

        public NivelAcesso[] Filtrar(FiltrarCmd comando)
        {
            StringBuilder sql = new StringBuilder();
            StringBuilder sqlFiltro = new StringBuilder();
            var parametros = new DynamicParameters();

            sql.Append($@"SELECT 
                                N.Codigo,
                                N.Nome,
                                N.Descricao,
                                N.CriadoEm,
                                N.AlteradoEm,
                                S.Codigo,
                                S.Nome,
                                S.Descricao,
                                S.CriadoEm,
                                S.AlteradoEm
                          FROM { nameof(NivelAcesso)} AS N ");
            sql.Append($"INNER JOIN { nameof(Status)} AS S ON S.Codigo = N.CodigoStatus ");

            if (!string.IsNullOrEmpty(comando.Nome))
            {
                sqlFiltro.Append(" AND N.Nome = @Nome ");
                parametros.Add("@Nome", comando.Nome, DbType.AnsiString, size: 200);
            }

            sql.Append(Regex.Replace(sqlFiltro.ToString(), @"^ AND ", " WHERE "));

            return _conexao.Sessao.Query<NivelAcesso, Status, NivelAcesso>(sql.ToString(),
                (nivelAcesso, status) =>
            {
                nivelAcesso.Status = status;
                return nivelAcesso;
            },
            parametros,
            _conexao.Transicao,
            splitOn: "Codigo, Codigo"
            ).ToArray();
        }

        public NivelAcesso Insert(NivelAcesso obj)
        {
            int resultado = -1;

            StringBuilder sql = new StringBuilder();
            StringBuilder sqlLastRow = new StringBuilder();
            sql.Append($@"
                         INSERT INTO { nameof(NivelAcesso) } (Nome, Descricao, CodigoStatus, CriadoEm, AlteradoEm)
                                VALUES(@Nome, @Descricao, @CodigoStatus, @CriadoEm, @AlteradoEm)");

            var parametros = new DynamicParameters();
            parametros.Add("@Nome", obj.Nome, DbType.AnsiString, size: 200);
            parametros.Add("@Descricao", obj.Descricao, DbType.AnsiString, size: 300);
            parametros.Add("@CodigoStatus", obj.Status.Codigo, DbType.Int32);
            parametros.Add("@CriadoEm", obj.CriadoEm, DbType.DateTime);
            parametros.Add("@AlteradoEm", obj.AlteradoEm, DbType.DateTime);

            resultado = _conexao.Sessao.Execute(sql.ToString(),
                                                parametros,
                                               _conexao.Transicao);

            if (resultado > 0)
            {
                sqlLastRow.Append("SELECT last_insert_rowid()");
                object id = _conexao.Sessao.ExecuteScalar(sqlLastRow.ToString(), new { }, _conexao.Transicao);

                if (!Equals(id, null))
                    obj.Codigo = int.Parse(id.ToString());
            }

            return obj;
        }

        public int Update(NivelAcesso obj)
        {
            StringBuilder sql = new StringBuilder();
            sql.Append($@"
                         Update { nameof(NivelAcesso) } SET Nome = @Nome,
                                                            Descricao = @Descricao,
                                                            CodigoStatus = @CodigoStatus, 
                                                            CriadoEm = @CriadoEm,
                                                            AlteradoEm = @AlteradoEm
                                                         WHERE Codigo = @Codigo");

            var parametros = new DynamicParameters(new { obj.Codigo });
            parametros.Add("@Nome", obj.Nome, DbType.AnsiString, size: 200);
            parametros.Add("@Descricao", obj.Descricao, DbType.AnsiString, size: 300);
            parametros.Add("@CodigoStatus", obj.Status.Codigo, DbType.Int32);
            parametros.Add("@CriadoEm", obj.CriadoEm, DbType.DateTime);
            parametros.Add("@AlteradoEm", obj.AlteradoEm, DbType.DateTime);

            return _conexao.Sessao.Execute(sql.ToString(),
                                           parametros,
                                           _conexao.Transicao);
        }

        public int Delete(int id)
        {
            StringBuilder sql = new StringBuilder();
            sql.Append($@"DELETE 
                          FROM { nameof(NivelAcesso)} WHERE Codigo = @Codigo ");

            return _conexao.Sessao.Execute(sql.ToString(),
                                           new { Codigo = id },
                                           _conexao.Transicao);
        }

        public NivelAcesso[] Get()
        {
            StringBuilder sql = new StringBuilder();
            sql.Append($@"SELECT 
                                N.Codigo,
                                N.Nome,
                                N.Descricao,
                                N.CriadoEm,
                                N.AlteradoEm,
                                S.Codigo,
                                S.Nome,
                                S.Descricao,
                                S.CriadoEm,
                                S.AlteradoEm
                          FROM { nameof(NivelAcesso)} As N");
            sql.Append($"INNER JOIN { nameof(Status)} AS S ON S.Codigo = N.CodigoStatus ");

            return _conexao.Sessao.Query<NivelAcesso, Status, NivelAcesso>(sql.ToString(),
                (nivelAcesso, status) =>
                {
                    nivelAcesso.Status = status;
                    return nivelAcesso;
                },
                new { },
                _conexao.Transicao,
                splitOn: "Codigo, Codigo"
                ).ToArray();
        }

        public NivelAcesso Get(int id)
        {
            StringBuilder sql = new StringBuilder();
            sql.Append($@"SELECT 
                                N.Codigo,
                                N.Nome,
                                N.Descricao,
                                N.CriadoEm,
                                N.AlteradoEm,
                                S.Codigo,
                                S.Nome,
                                S.Descricao,
                                S.CriadoEm,
                                S.AlteradoEm
                          FROM { nameof(NivelAcesso)} As N ");
            sql.Append($" INNER JOIN { nameof(Status)} AS S ON S.Codigo = N.CodigoStatus ");
            sql.Append(" WHERE N.Codigo = @Codigo");

            return _conexao.Sessao.Query<NivelAcesso, Status, NivelAcesso>(sql.ToString(),
                (nivelAcesso, status) =>
                {
                    nivelAcesso.Status = status;
                    return nivelAcesso;
                },
                new { Codigo = id },
                _conexao.Transicao,
                splitOn: "Codigo, Codigo"
                ).FirstOrDefault();
        }
    }
}
