using AvaliacaoGibarco.BackEnd.Dominio.ObjetoDeValor;
using System;

namespace AvaliacaoGibarco.BackEnd.Dominio.Entidade
{
    public class Autenticacao
    {
        protected internal Autenticacao() 
        {
            IniciaEm = DateTime.Now;
            TerminaEm = DateTime.Now.AddDays(1);
            CriadoEm = DateTime.Now;
            AlteradoEm = DateTime.Now;
        }

        public Autenticacao(Usuario usuario, Status status)
            :this()
        {
            Usuario = usuario;
            Status = status;
        }

        public Autenticacao(Usuario usuario, DateTime terminaEm, Status status)
            :this(usuario, status)
        {
            TerminaEm = terminaEm;
        }

        public int Codigo { get; set; }

        public string Token { get; set; }

        public Usuario Usuario { get; set; }

        public Status Status { get; set; } 

        public DateTime IniciaEm { get; protected internal set; }

        public DateTime TerminaEm { get; protected internal set; }

        public DateTime CriadoEm { get; protected internal set; }

        public DateTime AlteradoEm { get; protected internal set; }
    }
}
