using AvaliacaoGibarco.BackEnd.Dominio.Entidade;
using AvaliacaoGibarco.BackEnd.Dominio.ObjetoDeValor;
using System.ComponentModel.DataAnnotations;

namespace AvaliacaoGibarco.BackEnd.Dominio.Commando.UsuarioCmd
{
    public class AtualizarCmd : InserirCmd
    {
        public int Codigo { get; set; }

        [Display(Name = "Status")]
        public int CodigoStatus { get; set; }        

        public override void Aplicar(ref Usuario usuario, 
                                         Status status,
                                         NivelAcesso nivelAcesso )
        {
            if (!string.IsNullOrWhiteSpace(Nome))
                usuario.Nome = Nome;

            if (!string.IsNullOrWhiteSpace(Email))
                usuario.Email = Email;

            if (CodigoStatus > 0)
                usuario.Status = status;

            if (CodigoNivelAcesso > 0)
                usuario.NivelAcesso = nivelAcesso;
        }
    }
}
