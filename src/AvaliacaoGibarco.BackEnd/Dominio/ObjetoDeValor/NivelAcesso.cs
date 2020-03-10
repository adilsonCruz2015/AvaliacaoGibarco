using System;

namespace AvaliacaoGibarco.BackEnd.Dominio.ObjetoDeValor
{
    public class NivelAcesso
    {
        protected NivelAcesso()
        {
            CriadoEm = DateTime.Now;
            AlteradoEm = DateTime.Now;
        }

        public NivelAcesso(string nome, Status status)
            :this()
        {
            Nome = nome;
            Status = status;
        }

        public int Codigo { get; set; }

        public string Nome { get; set; }

        public string Descricao { get; set; }

        public Status Status { get; set; }

        public DateTime CriadoEm { get; protected internal set; }

        public DateTime AlteradoEm { get; protected internal set; }

        public override string ToString()
        {
            return Nome;
        }
    }
}
