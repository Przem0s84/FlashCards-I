using FlashCards.Entities;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;

namespace FlashCards_I.Authorization
{
    public class FlashCardSetResourceOperationHandler : AuthorizationHandler<ResourceOperationRequirement, FlashCardSet>
    {
        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, ResourceOperationRequirement requirement, FlashCardSet flashCardSet)
        {
            if(requirement.ResourceOperation == ResourceOperation.Create) { context.Succeed(requirement); }
            if(requirement.ResourceOperation == ResourceOperation.Update || requirement.ResourceOperation == ResourceOperation.Read || requirement.ResourceOperation == ResourceOperation.Delete)
            {
                var role = context.User.FindFirst(c => c.Type == ClaimTypes.Role).Value;
                var userId = context.User.FindFirst(c => c.Type == ClaimTypes.NameIdentifier).Value;
                if (flashCardSet.CreatedById == int.Parse(userId) || (requirement.ResourceOperation != ResourceOperation.Update && role=="Admin"))
                {
                    context.Succeed(requirement);
                }

            }

            

            return Task.CompletedTask;

        }
    }
}
