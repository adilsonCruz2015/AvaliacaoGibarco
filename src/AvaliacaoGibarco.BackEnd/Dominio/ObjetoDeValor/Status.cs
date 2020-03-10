using System;

namespace AvaliacaoGibarco.BackEnd.Dominio.ObjetoDeValor
{
    public class Status
    {
        public Status()
        {
            CriadoEm = DateTime.Now;
            AlteradoEm = DateTime.Now;
        }

        public Status(string descricao, string nome)
            :this()
        {
            Descricao = descricao;
            Nome = nome;
        }

        public int Codigo { get; set; }

        public string Nome { get; set; }

        public string Descricao { get; set; }

        public DateTime CriadoEm { get; protected internal set; }

        public DateTime AlteradoEm { get; protected internal set; }
    }
}
