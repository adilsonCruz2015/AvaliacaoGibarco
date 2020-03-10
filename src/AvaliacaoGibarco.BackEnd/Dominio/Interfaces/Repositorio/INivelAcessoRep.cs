using AvaliacaoGibarco.BackEnd.Dominio.Commando.NivelAcessoCmd;
using AvaliacaoGibarco.BackEnd.Dominio.Interfaces.Repositorio.Comum;
using AvaliacaoGibarco.BackEnd.Dominio.ObjetoDeValor;

namespace AvaliacaoGibarco.BackEnd.Dominio.Interfaces.Repositorio
{
    public interface INivelAcessoRep : IRepositorioBaseEscrita<NivelAcesso>,
                                       IRepositorioBaseLeitura<NivelAcesso>
    {
        NivelAcesso[] Filtrar(FiltrarCmd comando);
    }
}
