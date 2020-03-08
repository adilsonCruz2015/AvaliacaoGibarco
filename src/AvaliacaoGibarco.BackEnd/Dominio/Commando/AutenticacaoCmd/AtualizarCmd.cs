using AvaliacaoGibarco.BackEnd.Dominio.Entidade;

namespace AvaliacaoGibarco.BackEnd.Dominio.Commando.AutenticacaoCmd
{
    public class AtualizarCmd : InserirCmd
    {
        public int Codigo { get; set; }

        public override void Aplicar(ref Autenticacao autenticacao, Usuario usuario)
        {
            if (string.IsNullOrWhiteSpace(Token))
                autenticacao.Token = Token;

            if (ExpiraEm > 0)
                autenticacao.ExpiraEm = ExpiraEm;

            if (CodigoUsuario > 0)
                autenticacao.Usuario = usuario;
        }
    }
}
