using AvaliacaoGibarco.BackEnd.Dominio.Commando.PaisesCmd;
using AvaliacaoGibarco.BackEnd.Dominio.Commando.PaisesCmd.Validacao;
using AvaliacaoGibarco.BackEnd.Dominio.Interfaces;
using AvaliacaoGibarco.BackEnd.Dominio.Interfaces.Repositorio;
using AvaliacaoGibarco.BackEnd.Dominio.Interfaces.Servico;
using AvaliacaoGibarco.BackEnd.Dominio.ObjetoDeValor;
using AvaliacaoGibarco.BackEnd.Dominio.Servicos.Comum;

namespace AvaliacaoGibarco.BackEnd.Dominio.Servicos
{
    public class PaisServ : BaseService, IPaisServ
    {
        public PaisServ(INotificador notificador,
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
                Pais pais = _rep.Get(comando.Codigo);

                if(!object.Equals(pais, null))
                {
                    comando.Aplicar(ref pais);
                    resultado = _rep.Update(pais);

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
                resultado = _rep.Delete(comando.Codigo.Value);

                if (resultado < 0)
                    Notificar("Não foi possível excluír o País");
            }

            return resultado;
        }

        public Pais[] Filtrar(FiltrarCmd comando)
        {
            Pais[] paises = null;

            if (ExecutarValidacao(new FiltrarValidacao(), comando))
                paises = _rep.Filtrar(comando);

            if (Equals(paises, null))
                Notificar("Registro não encontrado!");

            return paises;
        }

        public int Inserir(InserirCmd comando)
        {
            int resultado = -1;

            if (ExecutarValidacao(new InserirValidacao(), comando))
            {
                Pais pais = null;
                comando.Aplicar(ref pais);

                pais = _rep.Insert(pais);
                if (pais.Codigo <= 0)
                    Notificar("Não foi possível inserir o País");
            }

            return resultado;
        }

        public Pais Obter(ObterCmd comando)
        {
            Pais pais = null;

            if (ExecutarValidacao(new ObterValidacao(), comando))
            {
                pais = _rep.Get(comando.Codigo.Value);

                if (object.Equals(pais, null))
                    Notificar("Registro não encontrado!");
            }

            return pais;
        }
    }
}
