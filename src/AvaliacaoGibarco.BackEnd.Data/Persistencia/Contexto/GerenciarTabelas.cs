using AvaliacaoGibarco.BackEnd.Data.Persistencia.Interfaces;
using AvaliacaoGibarco.BackEnd.Dominio.Entidade;
using AvaliacaoGibarco.BackEnd.Dominio.ObjetoDeValor;
using Dapper;
using System;
using System.IO;
using System.Text;

namespace AvaliacaoGibarco.BackEnd.Data.Persistencia.Contexto
{
    public class GerenciarTabelas
    {
        public GerenciarTabelas(IConexao conexao)
        {
            Execute(conexao);
        }

        private void Execute(IConexao conexao)
        {
            if(!File.Exists(conexao.DbFile))
            {
                try
                {
                    conexao.Sessao.Open();
                    conexao.Sessao.Execute(GerarTabelas(), new { });
                    conexao.Sessao.Execute(GerarDados(), new { });
                }
                catch(Exception ex)
                {

                }
            }
        }

        private string GerarTabelas()
        {
            StringBuilder sb = new StringBuilder();

            sb.Append($@"
                CREATE TABLE [dbo].[{nameof(Pais)}] (
                     [Codigo]    INT           IDENTITY (1, 1) NOT NULL,
                     [Descricao] VARCHAR (100) NULL,
                     PRIMARY KEY CLUSTERED ([Codigo] ASC)
                );  
            " );

            sb.Append($@"
                 CREATE TABLE [dbo].[{nameof(Usuario)}] (
                    [Codigo] INT           IDENTITY (1, 1) NOT NULL,
                    [Email]  VARCHAR (255) NULL,
                    [Senha]  VARCHAR (20)  NULL,
                    PRIMARY KEY CLUSTERED ([Codigo] ASC)
               );
            " );

            sb.Append($@"
                CREATE TABLE [dbo].[{nameof(Cliente)}] (
                    [Codigo]      INT           IDENTITY (1, 1) NOT NULL,
                    [Cnpj]        VARCHAR (18)  NULL,
                    [RazaoSocial] VARCHAR (255) NULL,
                    [CodigoPais]  INT           NULL,
                    PRIMARY KEY CLUSTERED ([Codigo] ASC),
                    CONSTRAINT [FK_Cliente_Pais] FOREIGN KEY ([Codigo]) REFERENCES [dbo].[Pais] ([Codigo])
                );
            " );

            return sb.ToString();
        }

        private string GerarDados()
        {
            StringBuilder sb = new StringBuilder();

            sb.Append($@"
                    INSERT INTO {nameof(Usuario)} (Email, Senha)
                           VALUES('teste@gmail.com', 'abcd@1234')
         
                ");

            return sb.ToString();
        }
    }
}
