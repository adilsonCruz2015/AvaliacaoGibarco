using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AvaliacaoGibarco.BackEnd.Dominio.Entidade
{
    public class Usuario
    {
        public Usuario() {  }

        public Usuario(string email, string senha)
            : this()
        {
            Email = email;
            Senha = senha;
        }

        public int Codigo { get; set; }

        public string Email { get; set; }

        public string Senha { get; set; }
    }
}
