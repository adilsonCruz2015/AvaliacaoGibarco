using AvaliacaoGibarco.BackEnd.Data.Persistencia.Interfaces;
using AvaliacaoGibarco.BackEnd.Dominio.Commando.AutenticacaoCmd;
using AvaliacaoGibarco.BackEnd.Dominio.Entidade;
using AvaliacaoGibarco.BackEnd.Dominio.Interfaces.Repositorio;
using Dapper;
using System.Data;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace AvaliacaoGibarco.BackEnd.Data.Persistencia.Repositorios
{
    public class AutenticacaoRep : IAutenticacaoRep
    {
        public AutenticacaoRep(IConexao conexao)
        {
            _conexao = conexao;
        }

        private readonly IConexao _conexao;

        public int Delete(int id)
        {
            StringBuilder sql = new StringBuilder();
            sql.Append($@"DELETE 
                          FROM { nameof(Autenticacao)} WHERE Codigo = @Codigo ");

            return _conexao.Sessao.Execute(sql.ToString(),
                                           new { Codigo =  id },
                                           _conexao.Transicao);
        }

        public Autenticacao[] Get()
        {
            StringBuilder sql = new StringBuilder();
            sql.Append($@"SELECT 
                                A.Codigo,
                                A.Token,
                                A.ExpiraEm,
                                U.Codigo,
                                U.Email
                          FROM { nameof(Autenticacao)} AS A ");
            sql.Append($"INNER JOIN { nameof(Usuario)} AS U ON U.Codigo = A.CodigoUsuario ");

            return _conexao.Sessao.Query<Autenticacao, Usuario, Autenticacao>(sql.ToString(),
                (autenticacao, usuario) => 
                {
                    autenticacao.Usuario = usuario;
                    return autenticacao;
                }, 
                new { },
                _conexao.Transicao,
                splitOn: "Codigo, Codigo"
                ).ToArray();
        }

        public Autenticacao Get(int id)
        {
            StringBuilder sql = new StringBuilder();
            sql.Append($@"SELECT 
                                A.Codigo,
                                A.Token,
                                A.ExpiraEm,
                                U.Codigo,
                                U.Email
                          FROM { nameof(Autenticacao)} AS A ");
            sql.Append($"INNER JOIN { nameof(Usuario)} AS U ON U.Codigo = A.CodigoUsuario ");
            sql.Append(" WHERE C.Codigo = @Codigo");

            return _conexao.Sessao.Query<Autenticacao, Usuario, Autenticacao>(sql.ToString(),
                (autenticacao, usuario) =>
                {
                    autenticacao.Usuario = usuario;
                    return autenticacao;
                },
                new { Codigo = id},
                _conexao.Transicao,
                splitOn: "Codigo, Codigo"
                ).FirstOrDefault();
        }

        public int Insert(Autenticacao obj)
        {
            StringBuilder sql = new StringBuilder();
            sql.Append($@"
                         INSERT INTO { nameof(Autenticacao) } (Token, CodigoUsuario, ExpiraEm)
                                VALUES(@Token, @CodigoUsuario, @ExpiraEm)");
            sql.Append(" SELECT last_insert_rowid() ");

            var parametros = new DynamicParameters();
            parametros.Add("@Token", obj.Token, DbType.AnsiString, size: 1000);
            parametros.Add("@CodigoUsuario", obj.Usuario.Codigo, DbType.Int32);
            parametros.Add("@ExpiraEm", obj.ExpiraEm, DbType.Int32);

            return _conexao.Sessao.Execute(sql.ToString(),
                                           parametros,
                                           _conexao.Transicao);
        }

        public Usuario Logar(LogarCmd comando)
        {
            StringBuilder sql = new StringBuilder();

            sql.Append($@"SELECT 
                                Codigo,
                                Email,
                                Senha
                          FROM { nameof(Usuario)} WHERE Email = @Email AND Senha = @Senha");

            var parametros = new DynamicParameters();
            parametros.Add("@Email", comando.Login, DbType.String, size: 255);
            parametros.Add("@Senha", comando.Senha, DbType.String, size: 20);

            return _conexao.Sessao.Query<Usuario>(sql.ToString(),
                                                  parametros,
                                                  _conexao.Transicao).FirstOrDefault();
        }

        public int Update(Autenticacao obj)
        {
            StringBuilder sql = new StringBuilder();
            sql.Append($@"
                         Update { nameof(Autenticacao) } SET Token = @Token,
                                                             CodigoUsuario = @CodigoUsuario,
                                                             ExpiraEm = @ExpiraEm
                                                         WHERE Codigo = @Codigo");

            var parametros = new DynamicParameters(new { obj.Codigo });

            parametros.Add("@Token", obj.Token, DbType.AnsiString, size: 1000);
            parametros.Add("@CodigoPais", obj.Usuario.Codigo, DbType.Int32);
            parametros.Add("@ExpiraEm", obj.ExpiraEm, DbType.Int32);

            return _conexao.Sessao.Execute(sql.ToString(),
                                           parametros,
                                           _conexao.Transicao);
        }

        public Autenticacao[] Filtrar(FiltrarCmd comando)
        {
            StringBuilder sql = new StringBuilder();
            StringBuilder sqlFiltro = new StringBuilder();
            var parametros = new DynamicParameters();

            sql.Append($@"SELECT 
                                A.Codigo,
                                A.Token,
                                A.ExpiraEm,                                
                                U.Email
                          FROM { nameof(Autenticacao)} AS A ");
            sql.Append($"INNER JOIN { nameof(Usuario)} AS U ON U.Codigo = A.CodigoUsuario ");

            if (!string.IsNullOrEmpty(comando.Email))
            {
                sqlFiltro.Append(" AND U.Email = @Email ");
                parametros.Add("@Cnpj", comando.Email, DbType.AnsiString, size: 18);
            }

            sql.Append(Regex.Replace(sqlFiltro.ToString(), @"^ AND ", " WHERE "));

            return _conexao.Sessao.Query<Autenticacao, Usuario, Autenticacao>(sql.ToString(),
                (autenticacao, usuario) => 
                {
                    autenticacao.Usuario = usuario;
                    return autenticacao;
                },
                parametros,
                _conexao.Transicao,
                splitOn: "Codigo, Codigo"
                ).ToArray();
        }
    }
}
