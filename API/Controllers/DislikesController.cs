using API.DTOs;
using API.Entities;
using API.Extensions;
using API.Helpers;
using API.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

public class DislikesController(IUnitOfWork unitOfWork) : BaseApiController
{
    [HttpPost("{targetUserId:int}")]
    public async Task<ActionResult> ToggleDislike(int targetUserId)
    {
        var sourceUserId = User.GetUserId();
        
        if (sourceUserId == targetUserId) return BadRequest("You cannot dislike yourself");

        var existingDislike = await unitOfWork.DislikesRepository.GetUserDislike(sourceUserId, targetUserId);

        if (existingDislike == null)
        {
            var dislike = new UserDislike
            {
                SourceUserId = sourceUserId,
                TargetUserId = targetUserId
            };

            unitOfWork.DislikesRepository.AddDislike(dislike);
        }
        else
        {
            unitOfWork.DislikesRepository.DeleteDislike(existingDislike);
        }

        if (await unitOfWork.Complete()) return Ok();

        return BadRequest("Failed to update the dislike");
    }
    [HttpGet("list")]
    public async Task<ActionResult<IEnumerable<int>>> GetCurrentUserDislikeIds()
    {
        return Ok(await unitOfWork.DislikesRepository.GetCurrentUserDislikeIds(User.GetUserId()));
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<MemberDto>>> GetUserDislikes([FromQuery]DislikesParams dislikesParams)
    {
        dislikesParams.UserId = User.GetUserId();
        var users = await unitOfWork.DislikesRepository.GetUserDislikes(dislikesParams);

        Response.AddPaginationHeader(users);

        return Ok(users);        
    }
}
