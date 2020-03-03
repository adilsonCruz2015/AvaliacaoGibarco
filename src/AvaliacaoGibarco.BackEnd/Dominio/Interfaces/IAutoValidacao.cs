namespace AvaliacaoGibarco.BackEnd.Dominio.Interfaces
{
    public interface IAutoValidacao
    {
        INotificador Notificador { get; }

        bool EhValido();
    }
}
