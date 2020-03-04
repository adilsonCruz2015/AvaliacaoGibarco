
namespace AvaliacaoGibarco.BackEnd.Dominio.ObjetoDeValor
{
    public class Pais
    {
        public Pais() { }

        public Pais(string descricao)
            : this()
        {
            Descricao = descricao;
        }

        public int Codigo { get; set; }

        public string Descricao { get; set; }
    }
}
