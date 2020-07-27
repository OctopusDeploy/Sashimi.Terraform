using Sashimi.Server.Contracts.CloudTemplates;

namespace Sashimi.Terraform.Validation
{
    class TerraformPlanActionValidator : TerraformDeploymentActionValidator
    {
        public TerraformPlanActionValidator(ICloudTemplateHandlerFactory cloudTemplateHandlerFactory)
            : base(cloudTemplateHandlerFactory)
        {
        }
    }
}