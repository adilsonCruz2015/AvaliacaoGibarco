using AvaliacaoGibarco.BackEnd.Dominio.ObjetoDeValor;
using System.ComponentModel.DataAnnotations;

namespace AvaliacaoGibarco.BackEnd.Dominio.Commando.NivelAcessoCmd
{
    public class AtualizarCmd
    {
        [Display(Name ="Nível Acesso")]
        public int Codigo { get; set; }

        public string Nome { get; set; }

        [Display(Name = "Descrição")]
        public string Descricao { get; set; }

        [Display(Name = "Status")]
        public int CodigoStatus { get; set; }

        public void Aplicar(ref NivelAcesso nivelAcesso, Status status) 
        {
            if (!string.IsNullOrWhiteSpace(Nome))
                nivelAcesso.Nome = Nome;

            if (!string.IsNullOrWhiteSpace(Descricao))
                nivelAcesso.Descricao = Descricao;

            if (CodigoStatus > 0)
                nivelAcesso.Status = status;
        }

        public void Desfazer(ref NivelAcesso nivelAcesso)
        {
            nivelAcesso = null;
        }
    }
}
