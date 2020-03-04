using AvaliacaoGibarco.BackEnd.Dominio.Commando.Comum;

namespace AvaliacaoGibarco.BackEnd.Dominio.Commando.PaisesCmd
{
    public class ObterCmd : CodigoCmd
    {
        public ObterCmd(int codigo)
            :base(codigo) { }
    }
}
