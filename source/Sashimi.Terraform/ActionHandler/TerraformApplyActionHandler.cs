﻿using System;
using Sashimi.Server.Contracts.CloudTemplates;

namespace Sashimi.Terraform.ActionHandler
{
    class TerraformApplyActionHandler : TerraformActionHandler
    {
        public TerraformApplyActionHandler(ICloudTemplateHandlerFactory cloudTemplateHandlerFactory)
            : base(cloudTemplateHandlerFactory)
        {
        }

        public override string Id => TerraformActionTypes.Apply;
        public override string Name => "Apply a Terraform template";
        public override string Description => "Applies a Terraform template";
        public override string ToolCommand => "apply-terraform";
    }
}