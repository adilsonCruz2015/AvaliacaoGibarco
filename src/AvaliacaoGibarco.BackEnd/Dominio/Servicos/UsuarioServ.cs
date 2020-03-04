using AvaliacaoGibarco.BackEnd.Dominio.Commando.UsuarioCmd;
using AvaliacaoGibarco.BackEnd.Dominio.Commando.UsuarioCmd.Validacao;
using AvaliacaoGibarco.BackEnd.Dominio.Entidade;
using AvaliacaoGibarco.BackEnd.Dominio.Interfaces;
using AvaliacaoGibarco.BackEnd.Dominio.Interfaces.Repositorio;
using AvaliacaoGibarco.BackEnd.Dominio.Interfaces.Servico;
using AvaliacaoGibarco.BackEnd.Dominio.Servicos.Comum;

namespace AvaliacaoGibarco.BackEnd.Dominio.Servicos
{
    public class UsuarioServ : BaseService, IUsuarioServ
    {
        public UsuarioServ(INotificador notificador,
                          IUsuarioRep rep)
            : base(notificador)
        {
            _rep = rep;
        }

        private readonly IUsuarioRep _rep;

        public Usuario Logar(LogarCmd comando)
        {
            Usuario usuario = null;

            if(ExecutarValidacao(new LogarValidacao(), comando))
            {
                usuario = _rep.Logar(comando);

                if (object.Equals(usuario, null))
                    Notificar("Login ou Senha inválido!");
            }

            return usuario;
        }
    }
}
