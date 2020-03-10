﻿using FluentValidation;

namespace AvaliacaoGibarco.BackEnd.Dominio.Commando.NivelAcessoCmd.Validacao
{
    public class ObterValidacao : AbstractValidator<ObterCmd>
    {
        public ObterValidacao()
        {
            RuleFor(f => f.Codigo)
                .NotEmpty().WithMessage("O campo {PropertyName} precisa ser fornecido");
        }
    }
}
