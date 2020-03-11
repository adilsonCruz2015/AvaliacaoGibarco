using AvaliacaoGibarco.BackEnd.Dominio.ObjetoDeValor;
using System;

namespace AvaliacaoGibarco.BackEnd.Dominio.Entidade
{
    public class Usuario
    {
        protected Usuario() 
        {
            CriadoEm = DateTime.Now;
            AlteradoEm = DateTime.Now;
        }

        public Usuario(string nome,
                       string email, 
                       string senha, 
                       Status status,
                       NivelAcesso nivelAcesso)
            : this()
        {
            Nome = nome;
            Email = email;
            Senha = senha;
            Status = status;
            NivelAcesso = nivelAcesso;
        }

        public int Codigo { get; set; }

        public string Nome { get; set; }

        public string Email { get; set; }

        public string Senha { get; set; }

        public Status Status { get; set; }

        public NivelAcesso NivelAcesso { get; set; }

        public DateTime CriadoEm { get; protected internal set; }

        public DateTime AlteradoEm { get; protected internal set; }

        public override string ToString()
        {
            return Nome;
        }
    }
}
