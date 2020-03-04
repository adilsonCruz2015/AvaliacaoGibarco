using AvaliacaoGibarco.BackEnd.Dominio.Entidade;
using AvaliacaoGibarco.BackEnd.Dominio.ObjetoDeValor;
using System.ComponentModel.DataAnnotations;

namespace AvaliacaoGibarco.BackEnd.Dominio.Commando.ClienteCmd
{
    public class AtualizarCmd
    {
        [Display(Name = "Código")]
        public int Codigo { get; set; }

        public string Cnpj { get; set; }

        [Display(Name = "Razão Social")]
        public string RazaoSocial { get; set; }

        [Display(Name = "País")]
        public int Pais { get; set; }

        public void Aplicar(ref Cliente cliente, Pais pais)
        {
            if (!string.IsNullOrEmpty(Cnpj))
                cliente.Cnpj = Cnpj;

            if (!string.IsNullOrEmpty(RazaoSocial))
                cliente.RazaoSocial = RazaoSocial;

            if (Pais > 0)
                cliente.Pais = pais;
        }
    }
}
