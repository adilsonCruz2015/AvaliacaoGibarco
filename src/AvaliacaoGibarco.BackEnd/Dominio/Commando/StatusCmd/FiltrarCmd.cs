using System.ComponentModel.DataAnnotations;

namespace AvaliacaoGibarco.BackEnd.Dominio.Commando.StatusCmd
{
    public class FiltrarCmd
    {
        [Display(Name = "Descrição")]
        public string Descricao { get; set; }
    }
}
