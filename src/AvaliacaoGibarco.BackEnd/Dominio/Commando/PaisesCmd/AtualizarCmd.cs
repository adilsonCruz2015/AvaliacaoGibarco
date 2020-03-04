using AvaliacaoGibarco.BackEnd.Dominio.ObjetoDeValor;
using System.ComponentModel.DataAnnotations;

namespace AvaliacaoGibarco.BackEnd.Dominio.Commando.PaisesCmd
{
    public class AtualizarCmd
    {
        [Display(Name = "Código")]
        public int Codigo { get; set; }

        [Display(Name = "Descrição")]
        public string Descricao { get; set; }

        public void Aplicar(ref Pais paises)
        {
            if (!string.IsNullOrEmpty(Descricao))
                paises.Descricao = Descricao;
        }
    }
}
