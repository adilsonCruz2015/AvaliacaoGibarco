
namespace AvaliacaoGibarco.BackEnd.Dominio.ObjetoDeValor
{
    public class Paises
    {
        public Paises(string descricao)
        {
            Descricao = descricao;
        }

        public int Codigo { get; set; }

        public string Descricao { get; set; }
    }
}
