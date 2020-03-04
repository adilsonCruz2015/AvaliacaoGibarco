using AvaliacaoGibarco.BackEnd.Dominio.Commando.ClienteCmd;
using AvaliacaoGibarco.BackEnd.Dominio.Entidade;

namespace AvaliacaoGibarco.BackEnd.Dominio.Interfaces.Servico
{
    public interface IClienteServ
    {
        int Inserir(InserirCmd comando);

        int Atualizar(AtualizarCmd comando);

        Cliente Obter(ObterCmd comando);

        Cliente[] Filtrar(FiltrarCmd comando);

        int Delete(DeletarCmd comando);
    }
}
