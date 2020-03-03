using AvaliacaoGibarco.BackEnd.Dominio.ObjetoDeValor;

namespace AvaliacaoGibarco.BackEnd.Dominio.Commando.PaisesCmd
{
    public class InserirCmd
    {
        public string Descricao { get; set; }

        public void Aplicar(ref Paises paises)
        {
            paises = new Paises(Descricao);
        }
    }
}
