using System.ComponentModel.DataAnnotations;

namespace AvaliacaoGibarco.BackEnd.Dominio.Commando.UsuarioCmd
{
    public class FiltrarCmd
    {
        [Display(Name = "E-mail")]
        public string Email { get; set; }

        public string Nome { get; set; }

        [Display(Name = "Status")]
        public int Status { get; set; }

        [Display(Name = "Nível Acesso")]
        public int NivelAcesso { get; set; }
    }
}
