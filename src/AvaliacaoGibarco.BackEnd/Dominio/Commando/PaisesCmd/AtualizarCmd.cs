using AvaliacaoGibarco.BackEnd.Dominio.ObjetoDeValor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AvaliacaoGibarco.BackEnd.Dominio.Commando.PaisesCmd
{
    public class AtualizarCmd
    {
        public int Codigo { get; set; }

        public string Descricao { get; set; }

        public void Aplicar(ref Paises paises)
        {
            if (!string.IsNullOrEmpty(Descricao))
                paises.Descricao = Descricao;
        }
    }
}
