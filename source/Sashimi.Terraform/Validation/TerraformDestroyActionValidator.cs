using System.Linq;
using FluentValidation;
using Sashimi.Server.Contracts.ActionHandlers.Validation;
using Sashimi.Server.Contracts.CloudTemplates;

namespace Sashimi.Terraform.Validation
{
    class TerraformDestroyActionValidator : TerraformDeploymentActionValidator
    {
        public TerraformDestroyActionValidator(ICloudTemplateHandlerFactory cloudTemplateHandlerFactory)
            : base(cloudTemplateHandlerFactory)
        {
            When(a => !IsTemplateFromPackage(a.Properties),
                 () =>
                 {
                     RuleFor(a => a.Properties)
                         .MustHaveProperty(TerraformSpecialVariables.Action.Terraform.Template,
                                           "Please provide the Terraform template.");
                     RuleFor(a => a.Properties)
                         .Must(a => !ValidationVariables(a).Any())
                         .WithMessage(a =>
                                          $"The variable(s) could not be parsed: {string.Join(", ", ValidationVariables(a.Properties))}.");
                 });

            RuleFor(a => a.Packages)
                .MustHaveExactlyOnePackage("Please provide the Terraform template package.")
                .When(a => IsTemplateFromPackage(a.Properties));
        }
    }
}