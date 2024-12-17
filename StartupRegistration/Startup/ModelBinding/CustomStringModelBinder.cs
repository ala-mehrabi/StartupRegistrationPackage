using Microsoft.AspNetCore.Mvc.ModelBinding;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StartupRegistration.Startup.ModelBinding
{
    public class CustomStringModelBinder : IModelBinder
    {
        private readonly IModelBinder _fallbackBinder;
        public CustomStringModelBinder(IModelBinder fallbackBinder)
        {
            if (fallbackBinder == null)
            {
                throw new ArgumentNullException(nameof(fallbackBinder));
            }
            _fallbackBinder = fallbackBinder;
        }

        public Task BindModelAsync(ModelBindingContext bindingContext)
        {
            if (bindingContext == null)
            {
                throw new ArgumentNullException(nameof(bindingContext));
            }

            var valueProviderResult = bindingContext.ValueProvider.GetValue(bindingContext.ModelName);
            if (valueProviderResult != ValueProviderResult.None)
            {
                bindingContext.ModelState.SetModelValue(bindingContext.ModelName, valueProviderResult);

                var valueAsString = valueProviderResult.FirstValue;
                if (string.IsNullOrWhiteSpace(valueAsString))
                {
                    return _fallbackBinder.BindModelAsync(bindingContext);
                }

                var model = valueAsString
                    .Replace((char)1610, (char)1740)
                    .Replace((char)1603, (char)1705)
                    .Replace("٠", "0")
                    .Replace("۰", "0")
                    .Replace("١", "1")
                    .Replace("۱", "1")
                    .Replace("٢", "2")
                    .Replace("۲", "2")
                    .Replace("٣", "3")
                    .Replace("۳", "3")
                    .Replace("٤", "4")
                    .Replace("۴", "4")
                    .Replace("۵", "5")
                    .Replace("٥", "5")
                    .Replace("٦", "6")
                    .Replace("۶", "6")
                    .Replace("۷", "7")
                    .Replace("٧", "7")
                    .Replace("٨", "8")
                    .Replace("۸", "8")
                    .Replace("٩", "9")
                    .Replace("۹", "9")
                    ;
                bindingContext.Result = ModelBindingResult.Success(model);
                return Task.CompletedTask;
            }

            return _fallbackBinder.BindModelAsync(bindingContext);
        }
    }
}
