using AvaliacaoGibarco.BackEnd.Dominio.Commando.NivelAcessoCmd.Validacao;
using AvaliacaoGibarco.BackEnd.Dominio.Commando.NivelAcessoCmd;
using AvaliacaoGibarco.BackEnd.Dominio.Interfaces;
using AvaliacaoGibarco.BackEnd.Dominio.Interfaces.Repositorio;
using AvaliacaoGibarco.BackEnd.Dominio.Interfaces.Servico;
using AvaliacaoGibarco.BackEnd.Dominio.ObjetoDeValor;
using AvaliacaoGibarco.BackEnd.Dominio.Servicos.Comum;
using Cmd = AvaliacaoGibarco.BackEnd.Dominio.Commando.StatusCmd;
using System.Linq;

namespace AvaliacaoGibarco.BackEnd.Dominio.Servicos
{
    public class NivelAcessoServ : BaseService, INivelAcessoServ
    {
        public NivelAcessoServ(INotificador notificador,
                               INivelAcessoRep rep,
                               IStatusRep statusRep)
            :base(notificador)
        {
            _rep = rep;
            _statusRep = statusRep;
        }

        private readonly INivelAcessoRep _rep;
        private readonly IStatusRep _statusRep;

        public int Inserir(InserirCmd comando)
        {
            int resultado = -1;

            if (ExecutarValidacao(new InserirValidacao(), comando))
            {
                NivelAcesso nivelAcesso  = null;
                Status status = Obter("Ativo");

                if(!HaNotificacoes())
                {
                    comando.Aplicar(ref nivelAcesso, status);

                    nivelAcesso = _rep.Insert(nivelAcesso);
                    if (nivelAcesso.Codigo <= 0)
                    {
                        comando.Desfazer(ref nivelAcesso);
                        Notificar("Não foi possível inserir o Nível de Acesso.");
                    }
                }
            }

            return resultado;
        }

        public int Atualizar(AtualizarCmd comando)
        {
            int resultado = -1;

            if (ExecutarValidacao(new AtualizarValidacao(), comando))
            {
                NivelAcesso nivelAcesso = _rep.Get(comando.Codigo);
                Status status = Obter("Ativo");

                if(!HaNotificacoes() && !Equals(nivelAcesso, null))
                {
                    comando.Aplicar(ref nivelAcesso, status);
                    resultado = _rep.Update(nivelAcesso);

                    if (resultado < 0)
                    {
                        comando.Desfazer(ref nivelAcesso);
                        Notificar("Não foi possível atualizar o Nível de Acesso.");
                    }
                }
            }


            return resultado;
        }

        public NivelAcesso Obter(ObterCmd comando)
        {
            NivelAcesso nivelAcesso = null;

            if(ExecutarValidacao(new ObterValidacao(), comando))
            {
                nivelAcesso = _rep.Get(comando.Codigo.Value);

                if (object.Equals(nivelAcesso, null))
                    Notificar("Registro não encontrado!");
            }

            return nivelAcesso;
        }

        public NivelAcesso[] Filtrar(FiltrarCmd comando)
        {
            NivelAcesso[] nivelAcessos = null;

            if (ExecutarValidacao(new FiltrarValidacao(), comando))
                nivelAcessos = _rep.Filtrar(comando);

            if (Equals(nivelAcessos, null))
                Notificar("Registro não encontrado!");

            return nivelAcessos;
        }

        public int Delete(DeletarCmd comando)
        {
            int resultado = -1;

            if (ExecutarValidacao(new DeletarValidacao(), comando))
            {
                resultado = _rep.Delete(comando.Codigo.Value);

                if (resultado < 0)
                    Notificar("Não foi possível excluír o Nível de Acesso.");
            }

            return resultado;
        }

        public Status Obter(string nome)
        {
            Status status = _statusRep.Filtrar(new Cmd.FiltrarCmd() { Descricao = nome }).FirstOrDefault();

            if (Equals(status, null))
                Notificar("Não foi possível encontrar o status");

            return status;
        }
    }
}
