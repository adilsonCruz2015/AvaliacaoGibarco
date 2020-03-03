using AvaliacaoGibarco.BackEnd.Dominio.Commando.PaisesCmd;
using AvaliacaoGibarco.BackEnd.Dominio.Commando.PaisesCmd.Validacao;
using AvaliacaoGibarco.BackEnd.Dominio.Interfaces;
using AvaliacaoGibarco.BackEnd.Dominio.Interfaces.Repositorio;
using AvaliacaoGibarco.BackEnd.Dominio.Interfaces.Servico;
using AvaliacaoGibarco.BackEnd.Dominio.ObjetoDeValor;
using AvaliacaoGibarco.BackEnd.Dominio.Servicos.Comum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AvaliacaoGibarco.BackEnd.Dominio.Servicos
{
    public class PaisesServ : BaseService, IPaisesServ
    {
        public PaisesServ(INotificador notificador,
                          IPaisesRep rep)
            :base(notificador)
        {
            _rep = rep;
        }

        private readonly IPaisesRep _rep;

        public int Atualizar(AtualizarCmd comando)
        {
            int resultado = -1;

            if(ExecutarValidacao(new AtualizarValidacao(), comando))
            {
                Paises paises = _rep.Get(comando.Codigo);

                if(!object.Equals(paises, null))
                {
                    comando.Aplicar(ref paises);
                    resultado = _rep.Update(paises);

                    if (resultado < 0)
                        Notificar("Não foi possível atualizar o País");
                }
                else
                {
                    Notificar("Registro não encontrado!");
                }
            }

            return resultado;
        }

        public int Delete(DeletarCmd comando)
        {
            int resultado = -1;

            if (ExecutarValidacao(new DeletarValidacao(), comando))
            {
                resultado = _rep.Delete(comando.Codigo);

                if (resultado < 0)
                    Notificar("Não foi possível excluír o País");
            }

            return resultado;
        }

        public Paises[] Filtrar(FiltrarCmd comando)
        {
            Paises[] paises = null;

            if (ExecutarValidacao(new FiltrarValidacao(), comando))
                paises = _rep.Filtrar(comando);

            return paises;
        }

        public int Inserir(InserirCmd comando)
        {
            int resultado = -1;

            if (ExecutarValidacao(new InserirValidacao(), comando))
            {
                Paises paises = null;
                comando.Aplicar(ref paises);

                resultado = _rep.Insert(paises);
                if (resultado < 0)
                    Notificar("Não foi possível inserir o País");
            }

            return resultado;
        }

        public Paises Obter(int codigo)
        {
            Paises pais = _rep.Get(codigo);

            if (object.Equals(pais, null))
                Notificar("Registro não encontrado!");

            return pais;
        }
    }
}
