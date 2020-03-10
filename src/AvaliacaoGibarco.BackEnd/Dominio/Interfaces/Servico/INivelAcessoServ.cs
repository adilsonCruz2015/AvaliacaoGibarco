using AvaliacaoGibarco.BackEnd.Dominio.Commando.NivelAcessoCmd;
using AvaliacaoGibarco.BackEnd.Dominio.ObjetoDeValor;

namespace AvaliacaoGibarco.BackEnd.Dominio.Interfaces.Servico
{
    public interface INivelAcessoServ
    {
        int Inserir(InserirCmd comando);

        int Atualizar(AtualizarCmd comando);

        NivelAcesso Obter(ObterCmd comando);

        NivelAcesso[] Filtrar(FiltrarCmd comando);

        int Delete(DeletarCmd comando);

        Status Obter(string nome);
    }
}
