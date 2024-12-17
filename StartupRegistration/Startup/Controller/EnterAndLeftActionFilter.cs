using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StartupRegistration.Startup.Controller
{
    public class EnterAndLeftActionFilter : Microsoft.AspNetCore.Mvc.Filters.IActionFilter
    {
        private readonly ILogger<EnterAndLeftActionFilter> _logger;

        public EnterAndLeftActionFilter(ILogger<EnterAndLeftActionFilter> logger)
        {
            _logger = logger;
        }

        public void OnActionExecuted(ActionExecutedContext context)
        {
            var UserToKen = context.HttpContext.Request.Headers["Authorization"].ToList().FirstOrDefault() ?? "";
            var route = context.HttpContext.Request.Path.Value.Split("/").Last().ToLower();
            _logger.LogWarning("Enter to ActionName : {ActionName} Token : {Token}", route, UserToKen);
        }

        public void OnActionExecuting(ActionExecutingContext context)
        {
            var UserToKen = context.HttpContext.Request.Headers["Authorization"].ToList().FirstOrDefault() ?? "";
            var route = context.HttpContext.Request.Path.Value.Split("/").Last().ToLower();
            _logger.LogWarning("Left to ActionName : {ActionName} Token : {Token}", route, UserToKen);
        }
    }
}
