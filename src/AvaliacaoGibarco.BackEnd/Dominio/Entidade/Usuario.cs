using AvaliacaoGibarco.BackEnd.Dominio.ObjetoDeValor;
using System;

namespace AvaliacaoGibarco.BackEnd.Dominio.Entidade
{
    public class Usuario
    {
        public Usuario() {  }

        public Usuario(string email, string senha)
            : this()
        {
            Email = email;
            Senha = senha;
        }

        public int Codigo { get; set; }

        public string Nome { get; set; }

        public string Email { get; set; }

        public string Senha { get; set; }

        public Status Status { get; set; }

        public DateTime CriadoEm { get; protected internal set; }

        public DateTime AlteradoEm { get; protected internal set; }

        public override string ToString()
        {
            return Nome;
        }
    }
}
