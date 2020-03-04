using AvaliacaoGibarco.BackEnd.Dominio.ObjetoDeValor;

namespace AvaliacaoGibarco.BackEnd.Dominio.Entidade
{
    public class Cliente
    {
        public Cliente() { }

        public Cliente(string cnpj, 
                       string razaoSocial,
                       Pais pais)
            : this()
        {
            Cnpj = cnpj;
            RazaoSocial = razaoSocial;
            Pais = pais;
        }

        public int Codigo { get; set; }

        public string Cnpj { get; set; }

        public string RazaoSocial { get; set; }

        public Pais Pais { get; set; }
    }
}
