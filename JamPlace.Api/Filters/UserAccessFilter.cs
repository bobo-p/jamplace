using JamPlace.DomainLayer.Interfaces.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace JamPlace.Api.Filters
{
    public class UserAccessFilter : IActionFilter
    {
        private readonly IJamEventService _jamEventService;
        public UserAccessFilter(IJamEventService jamEventService)
        {
            _jamEventService = jamEventService;
        }
        public void OnActionExecuted(ActionExecutedContext context)
        {
            
        }

        public void OnActionExecuting(ActionExecutingContext context)
        {
            string userId = context.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value;
            if (context.ActionArguments.TryGetValue("id", out object id))
            {
                var eventId = int.Parse(id.ToString());
                var accessMode = _jamEventService.GetAccesTypeForUser(eventId, userId);
                if(accessMode == DomainLayer.Common.UserAccessModeEnum.None)
                {
                    context.Result = new ObjectResult("Nie masz dostępu do projektu")
                    {
                        StatusCode = 403
                    };
                    return;
                }
            }
        }
    }
}
