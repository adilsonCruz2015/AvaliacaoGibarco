using AvaliacaoGibarco.BackEnd.Dominio.Commando.PaisesCmd;
using AvaliacaoGibarco.BackEnd.Dominio.Interfaces.Repositorio.Comum;
using AvaliacaoGibarco.BackEnd.Dominio.ObjetoDeValor;

namespace AvaliacaoGibarco.BackEnd.Dominio.Interfaces.Repositorio
{
    public interface IPaisesRep :  IRepositorioBaseEscrita<Paises>,
                            IRepositorioBaseLeitura<Paises>
    {
        Paises[] Filtrar(FiltrarCmd comando);
    }
}
