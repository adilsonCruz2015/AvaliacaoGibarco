﻿using System.ComponentModel.DataAnnotations;

namespace AvaliacaoGibarco.BackEnd.Dominio.Commando.UsuarioCmd
{
    public class FiltrarCmd
    {
        [Display(Name = "E-mail")]
        public string Email { get; set; }
    }
}
