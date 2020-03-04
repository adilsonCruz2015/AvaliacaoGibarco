using System.ComponentModel.DataAnnotations;

namespace AvaliacaoGibarco.BackEnd.Dominio.Commando.Comum
{
    public class CodigoCmd
    {
        public CodigoCmd(int codigo)
        {
            Codigo = codigo;
        }

        [Display(Name = "Código")]
        public int Codigo { get; set; }
    }
}
