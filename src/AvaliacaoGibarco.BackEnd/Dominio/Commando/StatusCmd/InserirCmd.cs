using AvaliacaoGibarco.BackEnd.Dominio.ObjetoDeValor;
using System.ComponentModel.DataAnnotations;

namespace AvaliacaoGibarco.BackEnd.Dominio.Commando.StatusCmd
{
    public class InserirCmd
    {
        [Display(Name = "Descrição")]
        public string Descricao { get; set; }

        public string Nome { get; set; }

        public void Aplicar(ref Status status)
        {
            status = new Status(Descricao, Nome);
        }
    }
}
