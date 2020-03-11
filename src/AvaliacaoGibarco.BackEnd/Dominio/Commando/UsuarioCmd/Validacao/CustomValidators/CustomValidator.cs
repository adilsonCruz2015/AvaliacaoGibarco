using FluentValidation;

namespace AvaliacaoGibarco.BackEnd.Dominio.Commando.UsuarioCmd.Validacao.CustomValidators
{
    public static class CustomValidator
    {
        public static IRuleBuilderOptions<T, string> Teste<T>(this IRuleBuilder<T, string> ruleBuilder)
        {
            return ruleBuilder.Must(m => m != null && !m.StartsWith(" ")).WithMessage("'{PropertyName}' should not start with whitespace");
        }
    }
}
