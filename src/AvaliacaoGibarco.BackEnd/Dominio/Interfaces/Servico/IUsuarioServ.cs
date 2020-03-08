﻿using AvaliacaoGibarco.BackEnd.Dominio.Commando.UsuarioCmd;
using AvaliacaoGibarco.BackEnd.Dominio.Entidade;

namespace AvaliacaoGibarco.BackEnd.Dominio.Interfaces.Servico
{
    public interface IUsuarioServ
    {
        Usuario ObterUserName(string username);

        int Inserir(InserirCmd comando);

        int Atualizar(AtualizarCmd comando);

        Usuario Obter(ObterCmd comando);

        Usuario[] Filtrar(FiltrarCmd comando);

        int Delete(DeletarCmd comando);
    }
}
