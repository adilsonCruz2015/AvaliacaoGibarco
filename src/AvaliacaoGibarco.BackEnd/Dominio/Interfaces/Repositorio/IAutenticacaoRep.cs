using AvaliacaoGibarco.BackEnd.Dominio.Commando.AutenticacaoCmd;
using AvaliacaoGibarco.BackEnd.Dominio.Entidade;
using AvaliacaoGibarco.BackEnd.Dominio.Interfaces.Repositorio.Comum;

namespace AvaliacaoGibarco.BackEnd.Dominio.Interfaces.Repositorio
{
    public interface  IAutenticacaoRep : IRepositorioBaseEscrita<Autenticacao>,
                                         IRepositorioBaseLeitura<Autenticacao>
    {
        Usuario Logar(LogarCmd comando);
        
    }
}
