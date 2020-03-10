using AvaliacaoGibarco.BackEnd.Dominio.Commando.AutenticacaoCmd;
using AvaliacaoGibarco.BackEnd.Dominio.Entidade;

namespace AvaliacaoGibarco.BackEnd.Dominio.Interfaces.Servico
{
    public interface IAutenticacaoServ
    {
        int Atualizar(Autenticacao entidade);      
        
        Autenticacao Logar(LogarCmd comando);

        Autenticacao Autenticacao { get; }

        Autenticacao Inicializar(InicializarCmd comando);

        void Sair();
    }
}
