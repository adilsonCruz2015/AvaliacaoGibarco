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

        public Usuario ObterUserName(string username)
        {
            return _rep.ObterUserName(username);
        }

        public int Inserir(InserirCmd comando)
        {
            int resultado = -1;

            if (ExecutarValidacao(new InserirValidacao(), comando))
            {
                Usuario usuario = null;
                comando.Aplicar(ref usuario);

                resultado = _rep.Insert(usuario);
                if (resultado < 0)
                    Notificar("Não foi possível inserir o Usuário");
            }

            return resultado;
        }

        public int Atualizar(AtualizarCmd comando)
        {
            int resultado = -1;

            if (ExecutarValidacao(new AtualizarValidacao(), comando))
            {
                Usuario usuario = _rep.Get(comando.Codigo);

                if (!object.Equals(usuario, null))
                {
                    comando.Aplicar(ref usuario);
                    resultado = _rep.Update(usuario);

                    if (resultado < 0)
                        Notificar("Não foi possível atualizar o Usuário");
                }
                else
                {
                    Notificar("Registro não encontrado!");
                }
            }

            return resultado;
        }

        public Usuario Obter(ObterCmd comando)
        {
            Usuario usuario = null;

            if (ExecutarValidacao(new ObterValidacao(), comando))
            {
                usuario = _rep.Get(comando.Codigo.Value);

                if (object.Equals(usuario, null))
                    Notificar("Registro não encontrado!");
            }

            return usuario;
        }

        public Usuario[] Filtrar(FiltrarCmd comando)
        {
            Usuario[] usuarios = null;

            if (ExecutarValidacao(new FiltrarValidacao(), comando))
                usuarios = _rep.Filtrar(comando);

            if (Equals(usuarios, null))
                Notificar("Registro não encontrado!");

            return usuarios;
        }

        public int Delete(DeletarCmd comando)
        {
            int resultado = -1;

            if (ExecutarValidacao(new DeletarValidacao(), comando))
            {
                resultado = _rep.Delete(comando.Codigo.Value);

                if (resultado < 0)
                    Notificar("Não foi possível excluír o Usuário.");
            }

            return resultado;
        }
    }
}
