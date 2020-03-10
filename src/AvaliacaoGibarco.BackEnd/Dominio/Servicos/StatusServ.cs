using AvaliacaoGibarco.BackEnd.Dominio.Commando.StatusCmd.Validacao;
using AvaliacaoGibarco.BackEnd.Dominio.Commando.StatusCmd;
using AvaliacaoGibarco.BackEnd.Dominio.Interfaces;
using AvaliacaoGibarco.BackEnd.Dominio.Interfaces.Repositorio;
using AvaliacaoGibarco.BackEnd.Dominio.Interfaces.Servico;
using AvaliacaoGibarco.BackEnd.Dominio.ObjetoDeValor;
using AvaliacaoGibarco.BackEnd.Dominio.Servicos.Comum;

namespace AvaliacaoGibarco.BackEnd.Dominio.Servicos
{
    public class StatusServ : BaseService, IStatusServ
    {
        public StatusServ(INotificador notificador,
                          IStatusRep rep)
            :base(notificador)
        {
            _rep = rep;
        }

        private readonly IStatusRep _rep;

        public int Atualizar(AtualizarCmd comando)
        {
            int resultado = -1;

            if(ExecutarValidacao(new AtualizarValidacao(), comando))
            {
                Status status = _rep.Get(comando.Codigo);

                if(!Equals(status, null))
                {
                    comando.Aplicar(ref status);
                    resultado = _rep.Update(status);

                    if (resultado < 0)
                    {
                        comando.Desfazer(ref status);
                        Notificar("Não foi possível atualizar o Status");
                    }
                }
            }

            return resultado;
        }

        public int Delete(DeletarCmd comando)
        {
            int resultado = -1;

            if (ExecutarValidacao(new DeletarValidacao(), comando))
            {
                resultado = _rep.Delete(comando.Codigo.Value);

                if (resultado < 0)
                    Notificar("Não foi possível excluír o Status");
            }

            return resultado;
        }

        public Status[] Filtrar(FiltrarCmd comando)
        {
            Status[] status = null;

            if (ExecutarValidacao(new FiltrarValidacao(), comando))
            {
                status = _rep.Filtrar(comando);
                if (!object.Equals(status, null) && status.Length.Equals(0))
                    Notificar("Registro não encontrado");
            }

            return status;
        }

        public int Inserir(InserirCmd comando)
        {
            int resultado = -1;

            if(ExecutarValidacao(new InserirValidacao(), comando))
            {
                Status status = null;
                comando.Aplicar(ref status);

                status = _rep.Insert(status);

                if (status.Codigo <= 0)
                    Notificar("Não foi possível inserir o Status");
            }

            return resultado;
        }

        public Status Obter(ObterCmd comando)
        {
            Status status = null;

            if (ExecutarValidacao(new ObterValidacao(), comando))
            {
                status = _rep.Get(comando.Codigo.Value);

                if (object.Equals(status, null))
                    Notificar("Registro não encontrado!");
            }

            return status;
        }
    }
}
