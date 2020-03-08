using AvaliacaoGibarco.BackEnd.Dominio.Entidade;
using System.ComponentModel.DataAnnotations;

namespace AvaliacaoGibarco.BackEnd.Dominio.Commando.AutenticacaoCmd
{
    public class InserirCmd
    {
        public string Token { get; set; }

        [Display(Name ="Usuário")]
        public int CodigoUsuario { get; set; }

        [Display(Name = "Expira em")]
        public int ExpiraEm { get; set; }

        public virtual void Aplicar(ref Autenticacao autenticacao, Usuario usuario)
        {
            autenticacao = new Autenticacao(usuario, ExpiraEm);
        }
    }
}
