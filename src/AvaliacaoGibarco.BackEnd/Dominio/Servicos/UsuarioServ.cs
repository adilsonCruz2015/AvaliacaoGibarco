using AvaliacaoGibarco.BackEnd.Dominio.Commando.UsuarioCmd;
using AvaliacaoGibarco.BackEnd.Dominio.Commando.UsuarioCmd.Validacao;
using AvaliacaoGibarco.BackEnd.Dominio.Entidade;
using AvaliacaoGibarco.BackEnd.Dominio.Interfaces;
using AvaliacaoGibarco.BackEnd.Dominio.Interfaces.Repositorio;
using AvaliacaoGibarco.BackEnd.Dominio.Interfaces.Servico;
using AvaliacaoGibarco.BackEnd.Dominio.ObjetoDeValor;
using AvaliacaoGibarco.BackEnd.Dominio.Servicos.Comum;
using System.Linq;
using Cmd = AvaliacaoGibarco.BackEnd.Dominio.Commando;

namespace AvaliacaoGibarco.BackEnd.Dominio.Servicos
{
    public class UsuarioServ : BaseService, IUsuarioServ
    {
        public UsuarioServ(INotificador notificador,
                          IUsuarioRep rep,
                          IStatusRep statusRep,
                          INivelAcessoRep nivelAcessoRep)
            : base(notificador)
        {
            _rep = rep;
            _statusRep = statusRep;
            _nivelAcessoRep = nivelAcessoRep;
        }

        private readonly IUsuarioRep _rep;
        private readonly IStatusRep _statusRep;
        private readonly INivelAcessoRep _nivelAcessoRep;

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
                Status status = ObterStatus("Ativo");
                NivelAcesso nivelAcesso = null;

                if (!HaNotificacoes())
                {
                    nivelAcesso = ObterNivelAcesso(comando.CodigoNivelAcesso);

                    if(!Equals(nivelAcesso))
                    {
                        comando.Aplicar(ref usuario, status, nivelAcesso);

                        usuario = _rep.Insert(usuario);
                        if (usuario.Codigo <= 0)
                        {
                            comando.Desfazer(ref usuario);
                            Notificar("Não foi possível inserir o Usuário");
                        }
                    }
                    else
                    {
                        Notificar("Não foi possível encontrar o nível de acesso.");
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
                Status status = _statusRep.Get(comando.CodigoStatus);

                if(!Equals(status, null))
                {
                    NivelAcesso nivelAcesso = _nivelAcessoRep.Get(comando.CodigoNivelAcesso);

                    if(!Equals(nivelAcesso, null))
                    {
                        Usuario usuario = _rep.Get(comando.Codigo);
                        if (!object.Equals(usuario, null))
                        {
                            comando.Aplicar(ref usuario, status, nivelAcesso);
                            resultado = _rep.Update(usuario);

                            if (resultado < 0)
                            {
                                comando.Desfazer(ref usuario);
                                Notificar("Não foi possível atualizar o usuário");
                            }
                        }
                        else
                        {
                            Notificar("Registro não encontrado!");
                        }
                    }
                    else
                    {
                        Notificar("Não foi possível encontrar o nível de acesso.");
                    }
                }
                else
                {
                    Notificar("Não foi possível encontrar o status.");
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

        public Status ObterStatus(string nome)
        {
            Status status = _statusRep.Filtrar(new Cmd.StatusCmd.FiltrarCmd() { Descricao = nome }).FirstOrDefault();

            if (Equals(status, null))
                Notificar("Não foi possível encontrar o status");

            return status;
        }

        public NivelAcesso ObterNivelAcesso(int codigo)
        {
            NivelAcesso nivelAcesso = _nivelAcessoRep.Get(codigo);

            if (Equals(nivelAcesso, null))
                Notificar("Não foi possível encontrar o nível de acesso.");

            return nivelAcesso;
        }
    }
}
