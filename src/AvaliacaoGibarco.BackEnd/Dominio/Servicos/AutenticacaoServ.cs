using AvaliacaoGibarco.BackEnd.Dominio.Commando.AutenticacaoCmd;
using AvaliacaoGibarco.BackEnd.Dominio.Commando.AutenticacaoCmd.Validacao;
using AvaliacaoGibarco.BackEnd.Dominio.Entidade;
using AvaliacaoGibarco.BackEnd.Dominio.Interfaces;
using AvaliacaoGibarco.BackEnd.Dominio.Interfaces.Repositorio;
using AvaliacaoGibarco.BackEnd.Dominio.Interfaces.Seguranca;
using AvaliacaoGibarco.BackEnd.Dominio.Interfaces.Servico;
using AvaliacaoGibarco.BackEnd.Dominio.Seguranca;
using AvaliacaoGibarco.BackEnd.Dominio.Servicos.Comum;

namespace AvaliacaoGibarco.BackEnd.Dominio.Servicos
{
    public class AutenticacaoServ : BaseService, IAutenticacaoServ
    {
        public AutenticacaoServ(INotificador notificador,
                                IAutenticacaoRep rep,
                                IUsuarioRep usuarioRep,
                                ITokenManager token)
            :base(notificador)
        {
            _rep = rep;
            _usuarioRep = usuarioRep;
            _token = token;
        }

        private readonly IAutenticacaoRep _rep;
        private readonly IUsuarioRep _usuarioRep;
        private readonly ITokenManager _token;

        public Autenticacao Autenticacao { get; protected internal set; }

        public int Atualizar(AtualizarCmd comando)
        {
            int resultado = -1;

            if(ExecutarValidacao(new AtualizarValidacao(), comando))
            {
                Usuario usuario = _usuarioRep.Get(comando.CodigoUsuario);

                if (!Equals(usuario, null))
                {
                    Autenticacao autenticacao = _rep.Get(comando.Codigo);

                    if (!Equals(autenticacao, null))
                    {
                        comando.Aplicar(ref autenticacao, usuario);

                        resultado = _rep.Update(autenticacao);
                        if (resultado < 0)
                            Notificar("Não foi possível atualizar a Autenticacao.");
                    }
                    else
                    {
                        Notificar("Notificação não encontrada.");
                    }
                }
                else
                {
                    Notificar("Usuário não encontrado.");
                }
            }

            return resultado;
        }

        public Autenticacao Logar(LogarCmd comando)
        {
            Autenticacao resultado = null;
            Usuario usuario = null;

            if(ExecutarValidacao(new LogarValidacao(), comando))
            {
                usuario = _rep.Logar(comando);

                if (Equals(usuario, null))
                    Notificar("Login ou Senha inválido!");
                else
                {
                    resultado = new Autenticacao(usuario);
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
    }
}
