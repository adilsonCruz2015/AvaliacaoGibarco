using AvaliacaoGibarco.BackEnd.Dominio.Commando.PaisesCmd;
using AvaliacaoGibarco.BackEnd.Dominio.ObjetoDeValor;

namespace AvaliacaoGibarco.BackEnd.Dominio.Interfaces.Servico
{
    public interface IPaisServ
    {
        int Inserir(InserirCmd comando);

        int Atualizar(AtualizarCmd comando);

        Pais Obter(ObterCmd comando);

        Pais[] Filtrar(FiltrarCmd comando);

        int Delete(DeletarCmd comando);
    }
}
