using FlashCards.Entities;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;

namespace FlashCards_I.Authorization
{
    public class ResourceOperationRequirementHandler : AuthorizationHandler<ResourceOperationRequirement, FlashCardSet>
    {
        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, ResourceOperationRequirement requirement, FlashCardSet flashCardSet)
        {
            if(requirement.ResourceOperation == ResourceOperation.Create) { context.Succeed(requirement); }


            var userId = context.User.FindFirst(c => c.Type == ClaimTypes.NameIdentifier).Value;
            if(flashCardSet.CreatedById == int.Parse(userId)) 
            { 
                context.Succeed(requirement);
            }

            return Task.CompletedTask;

        }
    }
}
