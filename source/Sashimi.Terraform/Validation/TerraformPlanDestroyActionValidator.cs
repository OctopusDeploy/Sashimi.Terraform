using Sashimi.Server.Contracts.CloudTemplates;

namespace Sashimi.Terraform.Validation
{
    class TerraformPlanDestroyActionValidator : TerraformDeploymentActionValidator
    {
        public TerraformPlanDestroyActionValidator(ICloudTemplateHandlerFactory cloudTemplateHandlerFactory)
            : base(cloudTemplateHandlerFactory)
        {
        }
    }
}