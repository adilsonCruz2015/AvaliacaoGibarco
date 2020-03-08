using AvaliacaoGibarco.BackEnd.Dominio.Commando.AutenticacaoCmd;
using AvaliacaoGibarco.BackEnd.Dominio.Commando.AutenticacaoCmd.Validacao;
using AvaliacaoGibarco.BackEnd.Dominio.Entidade;
using AvaliacaoGibarco.BackEnd.Dominio.Interfaces;
using AvaliacaoGibarco.BackEnd.Dominio.Interfaces.Repositorio;
using AvaliacaoGibarco.BackEnd.Dominio.Interfaces.Servico;
using AvaliacaoGibarco.BackEnd.Dominio.Seguranca;
using AvaliacaoGibarco.BackEnd.Dominio.Servicos.Comum;

namespace AvaliacaoGibarco.BackEnd.Dominio.Servicos
{
    public class AutenticacaoServ : BaseService, IAutenticacaoServ
    {
        public AutenticacaoServ(INotificador notificador,
                                IAutenticacaoRep rep,
                                IUsuarioRep usuarioRep)
            :base(notificador)
        {
            _rep = rep;
            _usuarioRep = usuarioRep;
        }

        private readonly IAutenticacaoRep _rep;
        private readonly IUsuarioRep _usuarioRep;

        public int Inserir(InserirCmd comando)
        {
            int resultado = -1;

            if (ExecutarValidacao(new InserirValidacao(), comando))
            {
                Autenticacao autenticacao = null;
                Usuario usuario = _usuarioRep.Get(comando.CodigoUsuario);

                if(!Equals(usuario, null))
                {
                    comando.Aplicar(ref autenticacao, usuario);

                    resultado = _rep.Insert(autenticacao);
                    if (resultado < 0)
                        Notificar("Não foi possível inserir a Autenticacao.");
                }
                else
                {
                    Notificar("Usuário não encontrado.");
                }
            }

            return resultado;
        }

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

        public Autenticacao Obter(ObterCmd comando)
        {
            Autenticacao autenticacao = null;

            if (ExecutarValidacao(new ObterValidacao(), comando))
            {
                autenticacao = _rep.Get(comando.Codigo.Value);

                if (object.Equals(autenticacao, null))
                    Notificar("Registro não encontrado!");
            }

            return autenticacao;
        }

        public Autenticacao[] Filtrar(FiltrarCmd comando)
        {
            Autenticacao[] autenticacoes = null;

            if (ExecutarValidacao(new FiltrarValidacao(), comando))
                autenticacoes = _rep.Filtrar(comando);

            if (Equals(autenticacoes, null))
                Notificar("Registro não encontrado!");

            return autenticacoes;
        }

        public int Delete(DeletarCmd comando)
        {
            int resultado = -1;

            if (ExecutarValidacao(new DeletarValidacao(), comando))
            {
                resultado = _rep.Delete(comando.Codigo.Value);

                if (resultado < 0)
                    Notificar("Não foi possível excluír a Autenticação");
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
                    resultado.Token = TokenManager.GenerateToken(comando.Login);
                    _rep.Insert(resultado);
                }
            }

            return resultado;
        }
    }
}
