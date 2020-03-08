using AvaliacaoGibarco.BackEnd.Dominio.Entidade;
using System.ComponentModel.DataAnnotations;

namespace AvaliacaoGibarco.BackEnd.Dominio.Commando.UsuarioCmd
{
    public class InserirCmd
    {
        [Display(Name = "E-mail")]
        public string Email { get; set; }

        public string Senha { get; set; }

        public virtual void Aplicar(ref Usuario usuario) 
        {
            usuario = new Usuario(Email, Senha);
        }
    }
}
