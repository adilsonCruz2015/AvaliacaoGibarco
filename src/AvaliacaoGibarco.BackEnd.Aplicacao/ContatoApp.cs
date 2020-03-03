using AvaliacaoGibarco.BackEnd.Dominio.Commando.ContatoCmd;
using AvaliacaoGibarco.BackEnd.Dominio.Entidade;
using AvaliacaoGibarco.BackEnd.Dominio.Interfaces.App;
using AvaliacaoGibarco.BackEnd.Dominio.Interfaces.Servico;

namespace AvaliacaoGibarco.BackEnd.Aplicacao
{
    public class ContatoApp : IContatoApp
    {
        public ContatoApp(IContatoServ serv)
        {
            _serv = serv;
        }

        private readonly IContatoServ _serv;

        public int Atualizar(AtualizarCmd comando)
        {
            return _serv.Atualizar(comando);
        }

        public Contato[] Filtrar(FiltrarCmd comando)
        {
            return _serv.Filtrar(comando);
        }

        public int Inserir(InserirCmd comando)
        {
            return _serv.Inserir(comando);
        }

        public Contato Obter(string id)
        {
            return _serv.Obter(id);
        }

        public int Delete(string id)
        {
            return _serv.Delete(id);
        }
    }
}
