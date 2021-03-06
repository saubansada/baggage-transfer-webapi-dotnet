﻿using BaggageTransfer.Helpers;
using BaggageTransfer.SettingsHelpers;
using BaggageTransfer.Util;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web.Mvc;

namespace BaggageTransfer.Providers
{
    public class LanguageModelMetadataProvider : DataAnnotationsModelMetadataProvider
    {
        protected override ModelMetadata CreateMetadata(IEnumerable<Attribute> attributes, Type containerType, Func<object> modelAccessor, Type modelType, string propertyName)
        {
            foreach (var attr in attributes.Where(c => c is ValidationAttribute && c != null).ToList())
            {
                var langKey = ((ValidationAttribute)attr).ErrorMessage;
                var key = $"{containerType.FullName}_{propertyName}_{attr.GetType().Name}";

                if (!string.IsNullOrWhiteSpace(langKey))
                {
                    ((ValidationAttribute)attr).ErrorMessage = LanguageHelper.GetKey(PerformanceHelper.SetOrGet(key, langKey));
                }
            }

            return base.CreateMetadata(attributes, containerType, modelAccessor, modelType, propertyName);
        }
    }
}
