using AvaliacaoGibarco.BackEnd.Data.Persistencia.Interfaces;
using AvaliacaoGibarco.BackEnd.Dominio.Commando.UsuarioCmd;
using AvaliacaoGibarco.BackEnd.Dominio.Entidade;
using AvaliacaoGibarco.BackEnd.Dominio.Interfaces.Repositorio;
using AvaliacaoGibarco.BackEnd.Dominio.ObjetoDeValor;
using Dapper;
using System.Data;
using System.Linq;
using System.Text;

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
                                Codigo,
                                Email,
                                Senha
                          FROM { nameof(Usuario)} ");

            return _conexao.Sessao.Query<Usuario>(sql.ToString(),
                                               new { },
                                               _conexao.Transicao).ToArray();
        }

        public Usuario Get(int id)
        {
            StringBuilder sql = new StringBuilder();
            sql.Append($@"SELECT 
                                Codigo,
                                Email,
                                Senha
                          FROM { nameof(Usuario)} WHERE Codigo = @Codigo ");

            return _conexao.Sessao.Query<Usuario>(sql.ToString(),
                                               new { Codigo = id },
                                               _conexao.Transicao).FirstOrDefault();
        }

        public Usuario ObterUserName(string username)
        {
            StringBuilder sql = new StringBuilder();
            sql.Append($@"SELECT 
                                Codigo,
                                Email,
                                Senha
                          FROM { nameof(Usuario)} WHERE Email = @Email ");

            return _conexao.Sessao.Query<Usuario>(sql.ToString(),
                                               new { Email = username },
                                               _conexao.Transicao).FirstOrDefault();
        }

        public int Insert(Usuario obj)
        {
            StringBuilder sql = new StringBuilder();
            sql.Append($@"
                         INSERT INTO { nameof(Usuario) } (Email, Senha)
                                VALUES(@Email, @Senha)");

            var parametros = new DynamicParameters();

            parametros.Add("@Email", obj.Email, DbType.AnsiString, size: 255);
            parametros.Add("@Senha", obj.Senha, DbType.AnsiString, size: 20);

            return _conexao.Sessao.Execute(sql.ToString(),
                                           parametros,
                                           _conexao.Transicao);
        }

        public int Update(Usuario obj)
        {
            StringBuilder sql = new StringBuilder();
            sql.Append($@"
                         Update { nameof(Usuario) } SET Email = @Email,
                                                        Senha = @Senha
                                                    WHERE Codigo = @Codigo");
            
            var parametros = new DynamicParameters(new { obj.Codigo });
            parametros.Add("@Email", obj.Email, DbType.AnsiString, size: 255);
            parametros.Add("@Senha", obj.Senha, DbType.AnsiString, size: 20);

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
                                Codigo,
                                Email,
                                Senha
                          FROM { nameof(Usuario)} WHERE Codigo = @Codigo ");

            parametros.Add("@Email", comando.Email, DbType.AnsiString, size: 255);

            return _conexao.Sessao.Query<Usuario>(sql.ToString(),
                                               parametros,
                                               _conexao.Transicao).ToArray();

        }
    }
}
