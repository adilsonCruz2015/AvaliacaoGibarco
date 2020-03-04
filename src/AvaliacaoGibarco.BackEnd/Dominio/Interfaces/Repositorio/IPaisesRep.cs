using AvaliacaoGibarco.BackEnd.Dominio.Commando.PaisesCmd;
using AvaliacaoGibarco.BackEnd.Dominio.Interfaces.Repositorio.Comum;
using AvaliacaoGibarco.BackEnd.Dominio.ObjetoDeValor;

namespace AvaliacaoGibarco.BackEnd.Dominio.Interfaces.Repositorio
{
    public interface IPaisesRep :  IRepositorioBaseEscrita<Pais>,
                                   IRepositorioBaseLeitura<Pais>
    {
        Pais[] Filtrar(FiltrarCmd comando);
    }
}
