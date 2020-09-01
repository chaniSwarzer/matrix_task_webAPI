using matrix_task.Helpers;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Threading.Tasks.Dataflow;

namespace matrix_task.Filters
{
    public class ActionFilter : IActionFilter
    {
        private ILog _logger;
        public ActionFilter(ILog logger)
        {
            _logger = logger;

        }
        public void OnActionExecuted(ActionExecutedContext context)
        {
           
        }

        public void OnActionExecuting(ActionExecutingContext context)
        {
            // write to log 
            _logger.Information(context.HttpContext.Request.Path);

        }
    }
}
