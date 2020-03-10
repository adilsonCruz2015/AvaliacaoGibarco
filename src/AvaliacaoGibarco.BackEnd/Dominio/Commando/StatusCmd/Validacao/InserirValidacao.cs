﻿using FluentValidation;

namespace AvaliacaoGibarco.BackEnd.Dominio.Commando.StatusCmd.Validacao
{
    public class InserirValidacao : AbstractValidator<InserirCmd>
    {
        public InserirValidacao()
        {
            RuleFor(d => d.Descricao)
                .NotEmpty().WithMessage("O campo {PropertyName} precisa ser fornecido")
                .Length(3, 150)
                .WithMessage("O campo {PropertyName} precisa ter entre {MinLength} e {MaxLength} caracteres");

            RuleFor(n => n.Nome)
                .NotEmpty().WithMessage("O campo {PropertyName} precisa ser fornecido")
                .Length(3, 150)
                .WithMessage("O campo {PropertyName} precisa ter entre {MinLength} e {MaxLength} caracteres");
        }
    }
}