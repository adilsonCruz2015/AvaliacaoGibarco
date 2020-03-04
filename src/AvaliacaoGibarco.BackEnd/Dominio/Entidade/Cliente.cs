using AvaliacaoGibarco.BackEnd.Dominio.ObjetoDeValor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AvaliacaoGibarco.BackEnd.Dominio.Entidade
{
    public class Cliente
    {
        public Cliente(string cnpj, 
                       string razaoSocial,
                       Pais pais)
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
