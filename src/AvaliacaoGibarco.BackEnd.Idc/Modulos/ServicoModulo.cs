﻿using AvaliacaoGibarco.BackEnd.Dominio.Interfaces.Servico;
using AvaliacaoGibarco.BackEnd.Dominio.Servicos;
using SimpleInjector;

namespace AvaliacaoGibarco.BackEnd.Idc.Modulos
{
    public static class ServicoModulo
    {
        public static void Carregar(Container recipiente)
        {
            recipiente.Register<IPaisesServ, PaisesServ>();
        }
    }
}
