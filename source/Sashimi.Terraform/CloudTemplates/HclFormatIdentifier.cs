using System;
using Octopus.CoreParsers.Hcl;
using Sprache;

namespace Sashimi.Terraform.CloudTemplates
{
    static class HclFormatIdentifier
    {
        public static bool IsHcl(string template)
        {
            return !string.IsNullOrWhiteSpace(template) && HclParser.HclTemplate.TryParse(HclParser.NormalizeLineEndings(template)).WasSuccessful;
        }
    }
}