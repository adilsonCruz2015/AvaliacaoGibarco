using AvaliacaoGibarco.BackEnd.Dominio.Commando.Comum;

namespace AvaliacaoGibarco.BackEnd.Dominio.Commando.UsuarioCmd
{
    public class ObterCmd : CodigoCmd
    {
        public ObterCmd() : base() { }

        public ObterCmd(int? codigo)
            : base(codigo) { }
    }
}
