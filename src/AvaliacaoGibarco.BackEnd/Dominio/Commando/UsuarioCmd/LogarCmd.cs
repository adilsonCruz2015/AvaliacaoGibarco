using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AvaliacaoGibarco.BackEnd.Dominio.Commando.UsuarioCmd
{
    public class LogarCmd
    {
        [Display(Name ="E-mail")]
        public string Email { get; set; }

        public string Senha { get; set; }
    }
}
