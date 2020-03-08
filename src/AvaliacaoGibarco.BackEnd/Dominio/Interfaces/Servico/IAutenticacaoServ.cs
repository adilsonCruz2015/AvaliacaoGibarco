using AvaliacaoGibarco.BackEnd.Dominio.Commando.AutenticacaoCmd;
using AvaliacaoGibarco.BackEnd.Dominio.Entidade;

namespace AvaliacaoGibarco.BackEnd.Dominio.Interfaces.Servico
{
    public interface IAutenticacaoServ
    {
        int Inserir(InserirCmd comando);

        int Atualizar(AtualizarCmd comando);

        Autenticacao Obter(ObterCmd comando);

        Autenticacao[] Filtrar(FiltrarCmd comando);

        int Delete(DeletarCmd comando);

        Autenticacao Logar(LogarCmd comando);
    }
}
