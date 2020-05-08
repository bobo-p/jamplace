using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace JamPlace.Api.Filters
{
    public class ApiExceptionFilter : ExceptionFilterAttribute
    {
        public override void OnException(ExceptionContext context)
        {
            Exception exception = null;
            exception = context.Exception;
            context.HttpContext.Response.StatusCode = (int)HttpStatusCode.UnprocessableEntity;
            context.Result = new JsonResult(exception);
            base.OnException(context);
        }
    }
}
