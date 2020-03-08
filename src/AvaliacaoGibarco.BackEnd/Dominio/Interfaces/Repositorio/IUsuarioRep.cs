using AvaliacaoGibarco.BackEnd.Dominio.Commando.UsuarioCmd;
using AvaliacaoGibarco.BackEnd.Dominio.Entidade;
using AvaliacaoGibarco.BackEnd.Dominio.Interfaces.Repositorio.Comum;

namespace AvaliacaoGibarco.BackEnd.Dominio.Interfaces.Repositorio
{
    public interface IUsuarioRep : IRepositorioBaseEscrita<Usuario>,
                                   IRepositorioBaseLeitura<Usuario>
    {
        Usuario ObterUserName(string username);

        Usuario[] Filtrar(FiltrarCmd comando);
    }
}
