using System;
using FluentValidation;
using Sashimi.Server.Contracts.ActionHandlers.Validation;
using Sashimi.Server.Contracts.CloudTemplates;
using Sashimi.Terraform.Validation;

namespace Sashimi.Terraform.ActionHandler
{
    class TerraformPlanActionHandler : TerraformActionHandler
    {
        public TerraformPlanActionHandler(ICloudTemplateHandlerFactory cloudTemplateHandlerFactory)
            : base(cloudTemplateHandlerFactory)
        {
            Validator = new TerraformPlanActionValidator(cloudTemplateHandlerFactory);
        }

        public override string Id => TerraformActionTypes.Plan;
        public override string Name => "Plan to apply a Terraform template";
        public override string Description => "Plans the creation of a Terraform deployment";
        public override string ToolCommand => "plan-terraform";
        public override IValidator<DeploymentActionValidationContext>? Validator { get; }
    }
}