using AvaliacaoGibarco.BackEnd.Dominio.ObjetoDeValor;
using System.ComponentModel.DataAnnotations;

namespace AvaliacaoGibarco.BackEnd.Dominio.Commando.PaisesCmd
{
    public class InserirCmd
    {
        [Display(Name = "Descrição")]
        public string Descricao { get; set; }

        public void Aplicar(ref Pais paises)
        {
            paises = new Pais(Descricao);
        }
    }
}
