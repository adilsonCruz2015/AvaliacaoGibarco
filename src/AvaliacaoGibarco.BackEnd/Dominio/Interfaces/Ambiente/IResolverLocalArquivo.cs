namespace AvaliacaoGibarco.BackEnd.Dominio.Interfaces.Ambiente
{
    public interface IResolverLocalArquivo
    {
        string ParaRelativo(string caminho);

        string ParaAbsoluto(string caminho);
    }
}
