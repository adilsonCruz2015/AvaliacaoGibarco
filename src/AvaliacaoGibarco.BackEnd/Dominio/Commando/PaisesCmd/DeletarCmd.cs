using AvaliacaoGibarco.BackEnd.Dominio.Commando.Comum;

namespace AvaliacaoGibarco.BackEnd.Dominio.Commando.PaisesCmd
{
    public class DeletarCmd : CodigoCmd
    {
        public DeletarCmd() : base() {  }

        public DeletarCmd(int codigo) 
            : base(codigo) { }
        
    }
}
