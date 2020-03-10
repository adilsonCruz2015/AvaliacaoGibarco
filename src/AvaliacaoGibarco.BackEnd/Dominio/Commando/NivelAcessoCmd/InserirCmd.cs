using AvaliacaoGibarco.BackEnd.Dominio.ObjetoDeValor;
using System.ComponentModel.DataAnnotations;

namespace AvaliacaoGibarco.BackEnd.Dominio.Commando.NivelAcessoCmd
{
    public class InserirCmd
    {
        public string Nome { get; set; }

        [Display(Name = "Descrição")]
        public string Descricao { get; set; }

        public void Aplicar(ref NivelAcesso nivelAcesso, Status status)
        {
            nivelAcesso = new NivelAcesso(Nome, status)
            {
                Descricao = Descricao
            };
        }

        public void Desfazer(ref NivelAcesso nivelAcesso)
        {
            nivelAcesso = null;
        }
    }
}
