using AvaliacaoGibarco.BackEnd.Dominio.Entidade;
using AvaliacaoGibarco.BackEnd.Dominio.ObjetoDeValor;
using System.ComponentModel.DataAnnotations;

namespace AvaliacaoGibarco.BackEnd.Dominio.Commando.ClienteCmd
{
    public class InserirCmd
    {
        public string Cnpj { get; set; }

        [Display(Name = "Razão Social")]
        public string RazaoSocial { get; set; }

        [Display(Name = "País")]
        public int Pais { get; set; }

        public void Aplicar(ref Cliente cliente, Pais pais)
        {
            cliente = new Cliente(Cnpj, RazaoSocial, pais);
        }
    }
}
