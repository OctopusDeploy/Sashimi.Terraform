using FluentValidation;
using Sashimi.Server.Contracts.ActionHandlers.Validation;
using Sashimi.Server.Contracts.CloudTemplates;
using Sashimi.Terraform.Validation;

namespace Sashimi.Terraform.ActionHandler
{
    class TerraformApplyActionHandler : TerraformActionHandler
    {
        public TerraformApplyActionHandler(ICloudTemplateHandlerFactory cloudTemplateHandlerFactory)
            : base(cloudTemplateHandlerFactory)
        {
            Validator = new TerraformApplyActionValidator(cloudTemplateHandlerFactory);
        }

        public override string Id => TerraformActionTypes.Apply;
        public override string Name => "Apply a Terraform template";
        public override string Description => "Applies a Terraform template";
        public override string ToolCommand => "apply-terraform";
        public override IValidator<DeploymentActionValidationContext>? Validator { get; }
    }
}