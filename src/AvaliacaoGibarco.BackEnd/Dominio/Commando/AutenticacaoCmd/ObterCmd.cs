using AvaliacaoGibarco.BackEnd.Dominio.Commando.Comum;

namespace AvaliacaoGibarco.BackEnd.Dominio.Commando.AutenticacaoCmd
{
    public class ObterCmd : CodigoCmd
    {
        public ObterCmd() : base() { }

        public ObterCmd(int? codigo)
            : base(codigo) { }
    }
}
