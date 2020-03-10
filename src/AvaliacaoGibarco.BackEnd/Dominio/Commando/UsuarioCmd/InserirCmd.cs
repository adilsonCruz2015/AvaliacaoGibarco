using AvaliacaoGibarco.BackEnd.Dominio.Entidade;
using AvaliacaoGibarco.BackEnd.Dominio.ObjetoDeValor;
using System.ComponentModel.DataAnnotations;

namespace AvaliacaoGibarco.BackEnd.Dominio.Commando.UsuarioCmd
{
    public class InserirCmd
    {
        public string Nome { get; set; }

        [Display(Name = "E-mail")]
        public string Email { get; set; }

        public string Senha { get; set; }

        [Display(Name = "Nível Acesso")]
        public int CodigoNivelAcesso { get; set; }

        public virtual void Aplicar(ref Usuario usuario, 
                                        Status status,
                                        NivelAcesso nivelAcesso) 
        {
            usuario = new Usuario(Email, Senha, status, nivelAcesso);
        }

        public void Desfazer(ref Usuario usuario)
        {
            usuario = null;
        }
    }
}
