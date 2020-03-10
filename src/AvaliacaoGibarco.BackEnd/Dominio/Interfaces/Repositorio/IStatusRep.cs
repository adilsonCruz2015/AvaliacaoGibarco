using AvaliacaoGibarco.BackEnd.Dominio.Commando.StatusCmd;
using AvaliacaoGibarco.BackEnd.Dominio.Interfaces.Repositorio.Comum;
using AvaliacaoGibarco.BackEnd.Dominio.ObjetoDeValor;

namespace AvaliacaoGibarco.BackEnd.Dominio.Interfaces.Repositorio
{
    public interface IStatusRep : IRepositorioBaseEscrita<Status>,
                           IRepositorioBaseLeitura<Status>
    {
        Status[] Filtrar(FiltrarCmd comando);
    }
}
