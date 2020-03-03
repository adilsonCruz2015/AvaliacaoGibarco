using AvaliacaoGibarco.BackEnd.Dominio.Commando.PaisesCmd;
using AvaliacaoGibarco.BackEnd.Dominio.ObjetoDeValor;

namespace AvaliacaoGibarco.BackEnd.Dominio.Interfaces.Servico
{
    public interface IPaisesServ
    {
        int Inserir(InserirCmd comando);

        int Atualizar(AtualizarCmd comando);

        Paises Obter(int codigo);

        Paises[] Filtrar(FiltrarCmd comando);

        int Delete(DeletarCmd comando);
    }
}
