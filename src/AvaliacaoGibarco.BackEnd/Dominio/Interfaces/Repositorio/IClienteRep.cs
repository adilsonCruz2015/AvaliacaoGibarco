using AvaliacaoGibarco.BackEnd.Dominio.Commando.ClienteCmd;
using AvaliacaoGibarco.BackEnd.Dominio.Entidade;
using AvaliacaoGibarco.BackEnd.Dominio.Interfaces.Repositorio.Comum;

namespace AvaliacaoGibarco.BackEnd.Dominio.Interfaces.Repositorio
{
    public interface IClienteRep : IRepositorioBaseEscrita<Cliente>,
                                   IRepositorioBaseLeitura<Cliente>
    {
        Cliente[] Filtrar(FiltrarCmd comando);
    }
}
