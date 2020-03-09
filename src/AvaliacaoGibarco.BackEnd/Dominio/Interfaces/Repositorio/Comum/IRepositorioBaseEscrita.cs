namespace AvaliacaoGibarco.BackEnd.Dominio.Interfaces.Repositorio.Comum
{
    public interface IRepositorioBaseEscrita<TEntidade>
        where TEntidade : class
    {
        TEntidade Insert(TEntidade obj);

        int Update(TEntidade obj);

        int Delete(int id);
    }
}
