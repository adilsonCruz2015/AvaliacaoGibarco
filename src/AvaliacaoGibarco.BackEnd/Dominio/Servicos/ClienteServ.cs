using AvaliacaoGibarco.BackEnd.Dominio.Commando.ClienteCmd;
using AvaliacaoGibarco.BackEnd.Dominio.Commando.ClienteCmd.Validacao;
using AvaliacaoGibarco.BackEnd.Dominio.Entidade;
using AvaliacaoGibarco.BackEnd.Dominio.Interfaces;
using AvaliacaoGibarco.BackEnd.Dominio.Interfaces.Repositorio;
using AvaliacaoGibarco.BackEnd.Dominio.Interfaces.Servico;
using AvaliacaoGibarco.BackEnd.Dominio.ObjetoDeValor;
using AvaliacaoGibarco.BackEnd.Dominio.Servicos.Comum;

namespace AvaliacaoGibarco.BackEnd.Dominio.Servicos
{
    public class ClienteServ : BaseService, IClienteServ
    {
        public ClienteServ(INotificador notificador,
                           IClienteRep rep,
                           IPaisesRep paisesRep)
            : base(notificador)
        {
            _rep = rep;
            _paisesRep = paisesRep;
        }

        private readonly IClienteRep _rep;
        private readonly IPaisesRep _paisesRep;

        public int Atualizar(AtualizarCmd comando)
        {
            int resultado = -1;

            if (ExecutarValidacao(new AtualizarValidacao(), comando))
            {
                Pais pais = _paisesRep.Get(comando.Pais);
                Cliente cliente = _rep.Get(comando.Codigo);

                if (!object.Equals(cliente, null))
                {
                    if(!object.Equals(pais, null))
                    {
                        comando.Aplicar(ref cliente, pais);
                        resultado = _rep.Update(cliente);

                        if (resultado < 0)
                            Notificar("Não foi possível atualizar o País");
                    }
                    else
                    {
                        Notificar("País não encontrado!");
                    }
                }
                else
                {
                    Notificar("Cliente não encontrado!");
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
                    Notificar("Não foi possível excluír o País");
            }

            return resultado;
        }

        public Cliente[] Filtrar(FiltrarCmd comando)
        {
            Cliente[] clientes = null;

            if (ExecutarValidacao(new FiltrarValidacao(), comando))
            {
                clientes = _rep.Filtrar(comando);
                if (!object.Equals(clientes, null) && clientes.Length.Equals(0))
                    Notificar("Registro não encontrado");
            }

            return clientes;
        }

        public int Inserir(InserirCmd comando)
        {
            int resultado = -1;

            if (ExecutarValidacao(new InserirValidacao(), comando))
            {
                Pais pais = _paisesRep.Get(comando.Pais);

                if (!object.Equals(pais, null))
                {
                    Cliente cliente = null;
                    comando.Aplicar(ref cliente, pais);

                    cliente = _rep.Insert(cliente);
                    if (cliente.Codigo <= 0)
                        Notificar("Não foi possível inserir o Cliente");
                }
                else
                {
                    Notificar("País não encontrado!");
                }
            }

            return resultado;
        }

        public Cliente Obter(ObterCmd comando)
        {
            Cliente cliente = null;

            if (ExecutarValidacao(new ObterValidacao(), comando))
            {
                cliente = _rep.Get(comando.Codigo.Value);

                if (object.Equals(cliente, null))
                    Notificar("Registro não encontrado!");
            }

            return cliente;
        }
    }
}
