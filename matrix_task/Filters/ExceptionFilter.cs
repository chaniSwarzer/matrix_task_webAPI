using matrix_task.Helpers;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace matrix_task.Filters
{
    public class ExceptionFilter : IExceptionFilter
    {
        private ILog _logger;

        public ExceptionFilter(ILog logger)
        {
            _logger = logger;
        }

        public void OnException(ExceptionContext context)
        {

            /// write to log
            _logger.Error(context.Exception.Message);
        }
    }
}
