

namespace AvaliacaoGibarco.BackEnd.Dominio.Entidade
{
    public class Contato
    {
        public Contato()
        {

        }

        public Contato(string nome, string canal, string valor)
            :this()
        {
            Nome = nome;
            Canal = canal;
            Valor = valor;
        }
        public int Id { get; set; }

        public string Nome { get; set; }

        public string Canal { get; set; }

        public string Valor { get; set; }

        public string Obs { get; set; }        
    }
}
