using System;
using System.Linq;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.EntityFrameworkCore;
using SharedKernel.Exceptions;

namespace EShoppingTutorialWebAPI.Filters
{
    public class AopExceptionHandlerFilter : IActionFilter, IOrderedFilter
    {
        public int Order { get; } = int.MaxValue - 10;

        public void OnActionExecuting(ActionExecutingContext context) { }

        public void OnActionExecuted(ActionExecutedContext context)
        {
            if (context.Exception is BusinessRuleBrokenException exception)
            {
                var dictionary = new ModelStateDictionary();

                dictionary.AddModelError("Message", exception.Message);

                dictionary.Keys.Append("errors");

                context.Result = new BadRequestObjectResult(dictionary);

                context.ExceptionHandled = true;

                return;
            }

            if (context.Exception is DbUpdateConcurrencyException)
            {
                var dictionary = new ModelStateDictionary();

                dictionary.AddModelError("Message", "Concurrency violation error when updating information, please retrieve the data and try again ");

                dictionary.Keys.Append("errors");

                context.Result = new BadRequestObjectResult(dictionary);

                context.ExceptionHandled = true;

                return;
            }

            if (context.Exception is Exception)
            {
                context.Result = new StatusCodeResult(StatusCodes.Status500InternalServerError);

                context.ExceptionHandled = true;

                return;
            }
        }
    }
}
