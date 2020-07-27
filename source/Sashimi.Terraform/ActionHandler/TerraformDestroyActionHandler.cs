using System;
using FluentValidation;
using Sashimi.Server.Contracts.ActionHandlers.Validation;
using Sashimi.Server.Contracts.CloudTemplates;
using Sashimi.Terraform.Validation;

namespace Sashimi.Terraform.ActionHandler
{
    class TerraformDestroyActionHandler : TerraformActionHandler
    {
        public TerraformDestroyActionHandler(ICloudTemplateHandlerFactory cloudTemplateHandlerFactory)
            : base(cloudTemplateHandlerFactory)
        {
            Validator = new TerraformDestroyActionValidator(cloudTemplateHandlerFactory);
        }

        public override string Id => TerraformActionTypes.Destroy;
        public override string Name => "Destroy Terraform resources";
        public override string Description => "Destroys Terraform resources";
        public override string ToolCommand => "destroy-terraform";
        public override IValidator<DeploymentActionValidationContext>? Validator { get; }
    }
}