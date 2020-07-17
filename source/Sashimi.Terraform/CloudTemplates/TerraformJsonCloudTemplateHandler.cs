﻿using System;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json.Linq;
using Octopus.Server.Extensibility.Metadata;
using Sashimi.Server.Contracts;
using Sashimi.Server.Contracts.CloudTemplates;

namespace Sashimi.Terraform.CloudTemplates
{
    class TerraformJsonCloudTemplateHandler : ICloudTemplateHandler
    {
        readonly IFormatIdentifier formatIdentifier;

        public TerraformJsonCloudTemplateHandler(IFormatIdentifier formatIdentifier)
        {
            this.formatIdentifier = formatIdentifier;
        }

        public bool CanHandleTemplate(string providerId, string template)
        {
            return TerraformConstants.CloudTemplateProviderId.Equals(providerId, StringComparison.OrdinalIgnoreCase) && formatIdentifier.IsJson(template);
        }

        public Metadata ParseTypes(string template)
        {
            if (template == null) return new Metadata();

            var jTokenDict = GetVariables(template);
            var properties = jTokenDict.Select(p => new PropertyMetadata
                                       {
                                           DisplayInfo = new DisplayInfo
                                           {
                                               Description = p.Value.SelectToken("description")?.ToString(),
                                               Label = p.Key,
                                               Required = true
                                           },
                                           Type = GetType(p.Value),
                                           Name = p.Key
                                       })
                                       .ToList();

            return new Metadata
            {
                Types = new List<TypeMetadata>
                {
                    new TypeMetadata
                    {
                        Name = TerraformDataTypes.TerraformTemplateTypeName,
                        Properties = properties
                    }
                }
            };
        }

        public object ParseModel(string template)
        {
            var parameters = GetVariables(template);
            return parameters
                   .Select(x => new KeyValuePair<string, object?>(x.Key, GetDefaultValue(x.Value)))
                   .ToDictionary(x => x.Key, x => x.Value);
        }

        static object? GetDefaultValue(JToken argValue)
        {
            var defaultValueToken = argValue.SelectToken("default");

            return defaultValueToken?.ToString();
        }

        /// <summary>
        /// https://www.terraform.io/docs/configuration/variables.html
        /// Valid values are string, list, and map. If this field is omitted, the variable type will be inferred based on default.
        /// If no default is provided, the type is assumed to be string.
        /// </summary>
        static string GetType(JToken token)
        {
            var type = token.SelectToken("type");
            if (type != null)
                return TerraformDataTypes.MapToType(type.ToString());

            // We can determine the type from the default value
            var defaultValue = token.SelectToken("default");
            if (defaultValue == null) return "string";
            switch (defaultValue.Type)
            {
                case JTokenType.Array:
                    return TerraformDataTypes.RawList;
                case JTokenType.Object:
                    return TerraformDataTypes.RawMap;
            }

            return "string";

            // Otherwise we default to a string
        }

        static IDictionary<string, JToken> GetVariables(string template)
        {
            var o = JObject.Parse(template);
            var variables = o["variable"];

            if (variables == null) return new Dictionary<string, JToken>();

            var tokenDictionary = new Dictionary<string, JToken>();

            foreach (var (key, value) in (JObject)variables)
            {
                if (value == null)
                    throw new Exception($"{nameof(JToken)} with key '{key}' has a null value.");

                tokenDictionary.Add(key, value);
            }

            return tokenDictionary;
        }
    }
}