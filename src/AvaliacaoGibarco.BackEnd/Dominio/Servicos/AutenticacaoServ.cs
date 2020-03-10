using AvaliacaoGibarco.BackEnd.Dominio.Commando.AutenticacaoCmd;
using AvaliacaoGibarco.BackEnd.Dominio.Commando.AutenticacaoCmd.Validacao;
using AvaliacaoGibarco.BackEnd.Dominio.Entidade;
using AvaliacaoGibarco.BackEnd.Dominio.Interfaces;
using AvaliacaoGibarco.BackEnd.Dominio.Interfaces.Repositorio;
using AvaliacaoGibarco.BackEnd.Dominio.Interfaces.Seguranca;
using AvaliacaoGibarco.BackEnd.Dominio.Interfaces.Servico;
using AvaliacaoGibarco.BackEnd.Dominio.ObjetoDeValor;
using AvaliacaoGibarco.BackEnd.Dominio.Servicos.Comum;
using Cmd = AvaliacaoGibarco.BackEnd.Dominio.Commando.StatusCmd;
using System.Linq;

namespace AvaliacaoGibarco.BackEnd.Dominio.Servicos
{
    public class AutenticacaoServ : BaseService, IAutenticacaoServ
    {
        public AutenticacaoServ(INotificador notificador,
                                IAutenticacaoRep rep,
                                IUsuarioRep usuarioRep,
                                ITokenManager token,
                                IStatusRep statusRep)
            :base(notificador)
        {
            _rep = rep;
            _usuarioRep = usuarioRep;
            _token = token;
            _statusRep = statusRep;
        }

        private readonly IAutenticacaoRep _rep;
        private readonly IUsuarioRep _usuarioRep;
        private readonly ITokenManager _token;
        private readonly IStatusRep _statusRep;

        public Autenticacao Autenticacao { get; protected internal set; }

        public int Atualizar(Autenticacao entidade)
        {
            int resultado = -1;
            resultado = _rep.Update(entidade);

            return resultado;
        }

        public Autenticacao Logar(LogarCmd comando)
        {
            Autenticacao resultado = null;
            Usuario usuario = null;
            Status status = null;

            if(ExecutarValidacao(new LogarValidacao(), comando))
            {
                usuario = _rep.Logar(comando);
                status = _statusRep.Filtrar(new Cmd.FiltrarCmd() { Descricao = "Ativo" }).FirstOrDefault();

                if (Equals(usuario, null))
                    Notificar("Login ou Senha inválido!");
                else
                {
                    resultado = new Autenticacao(usuario, status);
                    resultado = _rep.Insert(resultado);

                    if (resultado.Codigo < 0)
                    {
                        Notificar("Erro ao inserir a Autenticação!");
                    }
                    else
                    {
                        AplicarToken(resultado);
                        _rep.Update(resultado);
                    }
                }
            }

            return (Autenticacao = resultado); ;
        }

        public Autenticacao Inicializar(InicializarCmd comando)
        {
            Autenticacao resultado = null;
            int codigo = 0;

            if(ExecutarValidacao(new InicializarValidacao(), comando))
            {
                codigo = _token.Decode(comando.Token);
                resultado = _rep.Get(codigo);
            }

            return (Autenticacao = resultado);
        }

        private void AplicarToken(Autenticacao autenticacao)
        {
            autenticacao.Token = _token.GenerateToken(autenticacao);
        }

        public void Sair()
        {
            if (!Equals(Autenticacao, null))
                _rep.Delete(Autenticacao.Codigo);
        }
    }
}
