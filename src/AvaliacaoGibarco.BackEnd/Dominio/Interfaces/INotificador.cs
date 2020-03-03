using AvaliacaoGibarco.BackEnd.Dominio.Notificacoes;
using System.Collections.Generic;

namespace AvaliacaoGibarco.BackEnd.Dominio.Interfaces
{
    public interface INotificador
    {
        bool TemNotificacao();

        List<Notificacao> ObterNotificacoes();

        void Handle(Notificacao notificacao);
    }
}
