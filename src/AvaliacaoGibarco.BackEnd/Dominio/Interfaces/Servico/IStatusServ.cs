using AvaliacaoGibarco.BackEnd.Dominio.Commando.StatusCmd;
using AvaliacaoGibarco.BackEnd.Dominio.ObjetoDeValor;

namespace AvaliacaoGibarco.BackEnd.Dominio.Interfaces.Servico
{
    public interface IStatusServ
    {
        int Inserir(InserirCmd comando);

        int Atualizar(AtualizarCmd comando);

        Status Obter(ObterCmd comando);

        Status[] Filtrar(FiltrarCmd comando);

        int Delete(DeletarCmd comando);
    }
}
