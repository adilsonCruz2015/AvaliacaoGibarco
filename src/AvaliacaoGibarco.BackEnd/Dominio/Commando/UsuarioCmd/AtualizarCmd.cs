using AvaliacaoGibarco.BackEnd.Dominio.Entidade;

namespace AvaliacaoGibarco.BackEnd.Dominio.Commando.UsuarioCmd
{
    public class AtualizarCmd : InserirCmd
    {
        public int Codigo { get; set; }

        public override void Aplicar(ref Usuario usuario)
        {
            if (!string.IsNullOrWhiteSpace(Email))
                usuario.Email = Email;

            if (!string.IsNullOrWhiteSpace(Senha))
                usuario.Senha = Senha;
        }
    }
}
