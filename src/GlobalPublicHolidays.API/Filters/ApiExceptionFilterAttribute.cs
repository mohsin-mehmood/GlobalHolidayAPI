using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;

namespace GlobalPublicHolidays.API.Filters
{
    public class ApiExceptionFilterAttribute : ExceptionFilterAttribute
    {
        private readonly IDictionary<Type, Action<ExceptionContext>> _exceptionHandlers;

        public ApiExceptionFilterAttribute()
        {
            // Register known exception types and handlers.
            //_exceptionHandlers = new Dictionary<Type, Action<ExceptionContext>>
            //{
            //    { typeof(ValidationException), HandleValidationException },
            //    { typeof(NotFoundException), HandleNotFoundException },
            //};
        }

        public override void OnException(ExceptionContext context)
        {
            //HandleException(context);

            base.OnException(context);
        }
    }
}
