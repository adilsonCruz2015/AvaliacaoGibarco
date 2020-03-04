using System.ComponentModel.DataAnnotations;

namespace AvaliacaoGibarco.BackEnd.Dominio.Commando.ClienteCmd
{
    public class FiltrarCmd
    {
        public string Cnpj { get; set; }

        [Display(Name = "Razão Social")]
        public string RazaoSocial { get; set; }

        [Display(Name = "País")]
        public int Pais { get; set; }
    }
}
