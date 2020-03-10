using AvaliacaoGibarco.BackEnd.Dominio.ObjetoDeValor;
using System.ComponentModel.DataAnnotations;

namespace AvaliacaoGibarco.BackEnd.Dominio.Commando.StatusCmd
{
    public class AtualizarCmd
    {
        public int Codigo { get; set; }

        [Display(Name ="Descrição")]
        public string Descricao { get; set; }

        public string Nome { get; set; }

        public void Aplicar(ref Status status)
        {
            if (!string.IsNullOrWhiteSpace(Descricao))
                status.Descricao = Descricao;

            if (!string.IsNullOrWhiteSpace(Nome))
                status.Nome = Nome;
        }

        public void Desfazer(ref Status status)
        {
            status = null;
        }
    }
}
