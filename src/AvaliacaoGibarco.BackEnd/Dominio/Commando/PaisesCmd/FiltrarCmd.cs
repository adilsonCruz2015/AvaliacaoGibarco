using System.ComponentModel.DataAnnotations;

namespace AvaliacaoGibarco.BackEnd.Dominio.Commando.PaisesCmd
{
    public class FiltrarCmd
    {
        [Display(Name = "Código")]
        public int Codigo { get; set; }

        [Display(Name = "Descrição")]
        public string Descricao { get; set; }
    }
}
