using System;
using FluentValidation;
using Sashimi.Server.Contracts.ActionHandlers.Validation;
using Sashimi.Server.Contracts.CloudTemplates;
using Sashimi.Terraform.Validation;

namespace Sashimi.Terraform.ActionHandler
{
    class TerraformPlanDestroyActionHandler : TerraformActionHandler
    {
        public TerraformPlanDestroyActionHandler(ICloudTemplateHandlerFactory cloudTemplateHandlerFactory)
            : base(cloudTemplateHandlerFactory)
        {
            Validator = new TerraformPlanDestroyActionValidator(cloudTemplateHandlerFactory);
        }

        public override string Id => TerraformActionTypes.PlanDestroy;
        public override string Name => "Plan a Terraform destroy";
        public override string Description => "Plans the destruction of Terraform resources";
        public override string ToolCommand => "destroyplan-terraform";
        public override IValidator<DeploymentActionValidationContext>? Validator { get; }
    }
}