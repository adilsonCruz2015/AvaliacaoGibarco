using AvaliacaoGibarco.BackEnd.Data.Persistencia.Interfaces;
using AvaliacaoGibarco.BackEnd.Dominio.Commando.UsuarioCmd;
using AvaliacaoGibarco.BackEnd.Dominio.Entidade;
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
    public class UsuarioRep : IUsuarioRep
    {
        public UsuarioRep(IConexao conexao)
        {
            _conexao = conexao;
            _conexao.InformarBanco(Banco.AvaliacaoGibarco);
        }

        private readonly IConexao _conexao;        

        public Usuario[] Get()
        {
            StringBuilder sql = new StringBuilder();
            sql.Append($@"SELECT 
                                U.Codigo,
                                U.Nome,
                                U.Email,
                                U.CriadoEm,
                                U.AlteradoEm,
                                S.Codigo,
                                S.Nome,
                                S.Descricao,
                                S.CriadoEm,
                                S.AlteradoEm,
                                N.Codigo,
                                N.Nome,
                                N.Descricao,
                                N.CriadoEm,
                                N.AlteradoEm
                          FROM { nameof(Usuario) } As U ");
            sql.Append($"INNER JOIN { nameof(Status) } AS S ON S.Codigo = U.CodigoStatus ");
            sql.Append($"INNER JOIN { nameof(NivelAcesso) } AS N ON N.Codigo = U.CodigoNivelAcesso ");

            return _conexao.Sessao.Query<Usuario, Status, NivelAcesso, Usuario>(sql.ToString(),
                (usuario, status, nivelAcesso) => 
                {
                    usuario.Status = status;
                    usuario.NivelAcesso = nivelAcesso;

                    return usuario;
                },
                new { },
                _conexao.Transicao,
                splitOn: "Codigo, Codigo, Codigo").ToArray();
        }

        public Usuario Get(int id)
        {
            StringBuilder sql = new StringBuilder();
            sql.Append($@"SELECT 
                                U.Codigo,
                                U.Nome,
                                U.Email,
                                U.CriadoEm,
                                U.AlteradoEm,
                                S.Codigo,
                                S.Nome,
                                S.Descricao,
                                S.CriadoEm,
                                S.AlteradoEm,
                                N.Codigo,
                                N.Nome,
                                N.Descricao,
                                N.CriadoEm,
                                N.AlteradoEm
                          FROM { nameof(Usuario) } As U ");
            sql.Append($"INNER JOIN { nameof(Status) } AS S ON S.Codigo = U.CodigoStatus ");
            sql.Append($"INNER JOIN { nameof(NivelAcesso) } AS N ON N.Codigo = U.CodigoNivelAcesso ");
            sql.Append(" WHERE U.Codigo = @Codigo ");

            return _conexao.Sessao.Query<Usuario, Status, NivelAcesso, Usuario>(sql.ToString(),
                (usuario, status, nivelAcesso) =>
                {
                    usuario.Status = status;
                    usuario.NivelAcesso = nivelAcesso;

                    return usuario;
                },
                new { Codigo = id },
                _conexao.Transicao,
                splitOn: "Codigo, Codigo, Codigo").FirstOrDefault();
        }

        public Usuario ObterUserName(string username)
        {
            StringBuilder sql = new StringBuilder();
            sql.Append($@"SELECT 
                                Codigo,
                                Nome,
                                Email,                                
                                CodigoStatus,                                
                                CodigoNivelAcesso,                                
                                CriadoEm
                                AlteradoEm
                          FROM { nameof(Usuario)} WHERE Email = @Email ");

            return _conexao.Sessao.Query<Usuario>(sql.ToString(),
                                               new { Email = username },
                                               _conexao.Transicao).FirstOrDefault();
        }

        public Usuario Insert(Usuario obj)
        {
            int resultado = -1;

            StringBuilder sql = new StringBuilder();
            StringBuilder sqlLastRow = new StringBuilder();
            sql.Append($@"
                         INSERT INTO { nameof(Usuario) } (Nome, Email, Senha, CodigoStatus, CriadoEm, AlteradoEm, CodigoNivelAcesso)
                                VALUES(@Nome, @Email, @Senha, @CodigoStatus, @CriadoEm, @AlteradoEm, @CodigoNivelAcesso)");

            var parametros = new DynamicParameters();

            parametros.Add("@Nome", obj.Nome, DbType.AnsiString, size: 255);
            parametros.Add("@Email", obj.Email, DbType.AnsiString, size: 255);
            parametros.Add("@Senha", obj.Senha, DbType.AnsiString, size: 20);
            parametros.Add("@CodigoStatus", obj.Status.Codigo, DbType.Int32);
            parametros.Add("@CodigoNivelAcesso", obj.NivelAcesso.Codigo, DbType.Int32);
            parametros.Add("@AlteradoEm", obj.AlteradoEm, DbType.DateTime);
            parametros.Add("@CriadoEm", obj.CriadoEm, DbType.DateTime);

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

        public int Update(Usuario obj)
        {
            StringBuilder sql = new StringBuilder();
            sql.Append($@"
                         Update { nameof(Usuario) } SET Nome = @Nome,
                                                        Email = @Email,
                                                        CodigoStatus = @CodigoStatus,
                                                        CodigoNivelAcesso = @CodigoNivelAcesso,
                                                        AlteradoEm = @AlteradoEm
                                                    WHERE Codigo = @Codigo");
            
            var parametros = new DynamicParameters(new { obj.Codigo });
            parametros.Add("@Nome", obj.Nome, DbType.AnsiString, size: 255);
            parametros.Add("@Email", obj.Email, DbType.AnsiString, size: 255);
            parametros.Add("@CodigoStatus", obj.Status.Codigo, DbType.Int32);
            parametros.Add("@CodigoNivelAcesso", obj.NivelAcesso.Codigo, DbType.Int32);
            parametros.Add("@AlteradoEm", DateTime.Now, DbType.DateTime);

            return _conexao.Sessao.Execute(sql.ToString(),
                                           parametros,
                                           _conexao.Transicao);
        }

        public int Delete(int id)
        {
            StringBuilder sql = new StringBuilder();
            sql.Append($@"DELETE 
                          FROM { nameof(Usuario)} WHERE Codigo = @Codigo ");

            var parametros = new DynamicParameters(new { Codigo = id });

            return _conexao.Sessao.Execute(sql.ToString(),
                                           parametros,
                                           _conexao.Transicao);
        }

        public Usuario[] Filtrar(FiltrarCmd comando)
        {
            StringBuilder sql = new StringBuilder();
            StringBuilder sqlFiltro = new StringBuilder();
            var parametros = new DynamicParameters();

            sql.Append($@"SELECT 
                                U.Codigo,
                                U.Email,
                                U.Nome,
                                U.Senha,
                                U.CriadoEm,
                                U.AlteradoEm,
                                S.Codigo,
                                S.Nome,
                                S.Descricao,
                                S.CriadoEm,
                                S.AlteradoEm,
                                N.Codigo,
                                N.Nome,
                                N.Descricao,
                                N.CriadoEm,
                                N.AlteradoEm
                          FROM { nameof(Usuario) } As U ");
            sql.Append($"INNER JOIN { nameof(Status) } AS S ON S.Codigo = U.CodigoStatus ");
            sql.Append($"INNER JOIN { nameof(NivelAcesso) } AS N ON N.Codigo = U.CodigoNivelAcesso ");

            if (!string.IsNullOrEmpty(comando.Email))
            {
                sqlFiltro.Append(" AND U.Email = @Email ");
                parametros.Add("@Email", comando.Email, DbType.AnsiString, size: 255);
            }

            if (!string.IsNullOrEmpty(comando.Nome))
            {
                sqlFiltro.Append(" AND U.Nome LIKE @Nome ");
                parametros.Add("@Nome", string.Format("%{0}%", comando.Nome), DbType.AnsiString, size: 255);
            }

            if (comando.Status > 0)
            {
                sqlFiltro.Append(" AND S.Codigo = @CodigoStatus ");
                parametros.Add("@CodigoStatus", comando.Status, DbType.Int32);
            }

            if (comando.NivelAcesso > 0)
            {
                sqlFiltro.Append(" AND N.Codigo = @CodigoNivelAcesso ");
                parametros.Add("@CodigoNivelAcesso", comando.NivelAcesso, DbType.Int32);
            }

            sql.Append(Regex.Replace(sqlFiltro.ToString(), @"^ AND ", " WHERE "));

            return _conexao.Sessao.Query<Usuario, Status, NivelAcesso, Usuario>(sql.ToString(),
                (usuario, status, nivelAcesso) => 
                {
                    usuario.Status = status;
                    usuario.NivelAcesso = nivelAcesso;
                    return usuario;
                },
                parametros,
                _conexao.Transicao,
                splitOn: "Codigo, Codigo, Codigo").ToArray();
        }
    }
}
