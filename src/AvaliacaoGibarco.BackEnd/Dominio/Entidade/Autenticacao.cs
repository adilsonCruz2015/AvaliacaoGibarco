
namespace AvaliacaoGibarco.BackEnd.Dominio.Entidade
{
    public class Autenticacao
    {
        protected internal Autenticacao() 
        {
            ExpiraEm = 60;
        }

        public Autenticacao(Usuario usuario)
            :this()
        {
            Usuario = usuario;
        }

        public Autenticacao(Usuario usuario, int expiraEm)
            :this(usuario)
        {
            ExpiraEm = expiraEm;
        }

        public int Codigo { get; set; }

        public string Token { get; set; }

        public Usuario Usuario { get; set; }

        public int ExpiraEm { get; set; }
    }
}
