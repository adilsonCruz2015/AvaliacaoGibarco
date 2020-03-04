using AvaliacaoGibarco.BackEnd.Data.Persistencia.Interfaces;
using AvaliacaoGibarco.BackEnd.Dominio.Commando.UsuarioCmd;
using AvaliacaoGibarco.BackEnd.Dominio.Entidade;
using AvaliacaoGibarco.BackEnd.Dominio.Interfaces.Repositorio;
using AvaliacaoGibarco.BackEnd.Dominio.ObjetoDeValor;
using Dapper;
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

        public Usuario Logar(LogarCmd comando)
        {
            StringBuilder sql = new StringBuilder();

            sql.Append($@"SELECT 
                                Codigo,
                                Email,
                                Senha
                          FROM { nameof(Usuario)} WHERE Email = @Email AND Senha = @Senha");

            var parametros = new DynamicParameters(new { comando.Email, comando.Senha });

            return _conexao.Sessao.Query<Usuario>(sql.ToString(),
                                                 parametros,
                                                 _conexao.Transicao).FirstOrDefault();
        }

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
    }
}
