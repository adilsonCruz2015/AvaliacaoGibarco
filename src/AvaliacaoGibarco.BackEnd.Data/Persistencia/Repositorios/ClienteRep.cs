using AvaliacaoGibarco.BackEnd.Data.Persistencia.Interfaces;
using AvaliacaoGibarco.BackEnd.Dominio.Commando.ClienteCmd;
using AvaliacaoGibarco.BackEnd.Dominio.Entidade;
using AvaliacaoGibarco.BackEnd.Dominio.Interfaces.Repositorio;
using AvaliacaoGibarco.BackEnd.Dominio.ObjetoDeValor;
using Dapper;
using System.Data;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace AvaliacaoGibarco.BackEnd.Data.Persistencia.Repositorios
{
    public class ClienteRep : IClienteRep
    {
        public ClienteRep(IConexao conexao)
        {
            _conexao = conexao;
            _conexao.InformarBanco(Banco.AvaliacaoGibarco);
        }

        private readonly IConexao _conexao;

        public Cliente[] Filtrar(FiltrarCmd comando)
        {
            StringBuilder sql = new StringBuilder();
            StringBuilder sqlFiltro = new StringBuilder();
            var parametros = new DynamicParameters();

            sql.Append($@"SELECT 
                                C.Codigo,
                                C.Cnpj,
                                C.RazaoSocial,
                                P.Codigo,
                                P.Descricao
                          FROM { nameof(Cliente)} AS C ");
            sql.Append($"INNER JOIN { nameof(Pais)} AS P ON P.Codigo = C.CodigoPais ");

            if (!string.IsNullOrEmpty(comando.Cnpj))
            {
                sqlFiltro.Append(" AND C.Cnpj = @Cnpj ");
                parametros.Add("@Cnpj", comando.Cnpj, DbType.AnsiString, size: 18);
            }

            if (!string.IsNullOrEmpty(comando.RazaoSocial))
            {
                sqlFiltro.Append(" AND C.RazaoSocial = @RazaoSocial ");
                parametros.Add("@RazaoSocial", comando.RazaoSocial, DbType.AnsiString, size: 255);
            }

            if (comando.Pais > 0)
            {
                sqlFiltro.Append(" AND P.Codigo = @Codigo ");
                parametros.Add("@Codigo", comando.Pais, DbType.Int32);
            }

            sql.Append(Regex.Replace(sqlFiltro.ToString(), @"^ AND ", " WHERE "));
           
            return _conexao.Sessao.Query<Cliente, Pais, Cliente>(sql.ToString(),
            (cliente, pais) =>
            {
                cliente.Pais = pais;
                return cliente;
            },
            parametros,
            _conexao.Transicao,
            splitOn: "Codigo, Codigo"
            ).ToArray();
           
        }

        public int Insert(Cliente obj)
        {
            StringBuilder sql = new StringBuilder();
            sql.Append($@"
                         INSERT INTO { nameof(Cliente) } (Cnpj, RazaoSocial, CodigoPais)
                                VALUES(@Cnpj, @RazaoSocial, @CodigoPais)");

            var parametros = new DynamicParameters();

            parametros.Add("@Cnpj", obj.Cnpj, DbType.AnsiString, size: 18);
            parametros.Add("@RazaoSocial", obj.RazaoSocial, DbType.AnsiString, size: 255);
            parametros.Add("@CodigoPais", obj.Pais.Codigo, DbType.Int32);

            return _conexao.Sessao.Execute(sql.ToString(),
                                           parametros,
                                           _conexao.Transicao);
        }

        public int Update(Cliente obj)
        {
            StringBuilder sql = new StringBuilder();
            sql.Append($@"
                         Update { nameof(Cliente) } SET Cnpj = @Cnpj,
                                                        RazaoSocial = @RazaoSocial, 
                                                        CodigoPais = @CodigoPais,
                                                    WHERE Codigo = @Codigo");

            var parametros = new DynamicParameters(new { obj.Codigo });

            parametros.Add("@Cnpj", obj.Cnpj, DbType.AnsiString, size: 18);
            parametros.Add("@RazaoSocial", obj.RazaoSocial, DbType.AnsiString, size: 255);
            parametros.Add("@CodigoPais", obj.Pais.Codigo, DbType.Int32);

            return _conexao.Sessao.Execute(sql.ToString(),
                                           parametros,
                                           _conexao.Transicao);
        }

        public int Delete(int id)
        {
            StringBuilder sql = new StringBuilder();
            sql.Append($@"DELETE 
                          FROM { nameof(Cliente)} WHERE Codigo = @Codigo ");

            var parametros = new DynamicParameters(new { id });

            return _conexao.Sessao.Execute(sql.ToString(),
                                           parametros,
                                           _conexao.Transicao);
        }

        public Cliente[] Get()
        {
            StringBuilder sql = new StringBuilder();
            var parametros = new DynamicParameters();

            sql.Append($@"SELECT 
                                C.Codigo,
                                C.Cnpj,
                                C.RazaoSocial,
                                P.Codigo,
                                P.Descricao
                          FROM { nameof(Cliente)} AS C ");
            sql.Append($"INNER JOIN { nameof(Pais)} AS P ON P.Codigo = C.CodigoPais ");

            return _conexao.Sessao.Query<Cliente, Pais, Cliente>(sql.ToString(),
                (cliente, pais) =>
                {
                    cliente.Pais = pais;
                    return cliente;
                },
                parametros,
                _conexao.Transicao,
                splitOn: "Codigo, Codigo"
                ).ToArray();
        }

        public Cliente Get(int id)
        {
            StringBuilder sql = new StringBuilder();
            var parametros = new DynamicParameters( new { Codigo = id });

            sql.Append($@"SELECT 
                                C.Codigo,
                                C.Cnpj,
                                C.RazaoSocial,
                                P.Codigo,
                                P.Descricao
                          FROM { nameof(Cliente)} AS C ");
            sql.Append($"INNER JOIN { nameof(Pais)} AS P ON P.Codigo = C.CodigoPais ");
            sql.Append(" WHERE C.Codigo = @Codigo");

            return _conexao.Sessao.Query<Cliente, Pais, Cliente>(sql.ToString(),
                (cliente, pais) =>
                {
                    cliente.Pais = pais;
                    return cliente;
                },
                parametros,
                _conexao.Transicao,
                splitOn: "Codigo, Codigo"
                ).FirstOrDefault();
        }
    }
}
