using System.Reflection.Metadata.Ecma335;
using API.DTOs;
using API.Entities;
using API.Extensions;
using API.Helpers;
using API.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

public class DislikesController(IDislikesRepository dislikesRepository) : BaseApiController
{
    [HttpPost("{targetUserId:int}")]
    public async Task<ActionResult> ToggleDislike(int targetUserId)
    {
        var sourceUserId = User.GetUserId();
        
        if (sourceUserId == targetUserId) return BadRequest("You cannot dislike yourself");

        var existingDislike = await dislikesRepository.GetUserDislike(sourceUserId, targetUserId);

        if (existingDislike == null)
        {
            var dislike = new UserDislike
            {
                SourceUserId = sourceUserId,
                TargetUserId = targetUserId
            };

            dislikesRepository.AddDislike(dislike);
        }
        else
        {
            dislikesRepository.DeleteDislike(existingDislike);
        }

        if (await dislikesRepository.SaveChanges()) return Ok();

        return BadRequest("Failed to update the dislike");
    }
    [HttpGet("list")]
    public async Task<ActionResult<IEnumerable<int>>> GetCurrentUserDislikeIds()
    {
        return Ok(await dislikesRepository.GetCurrentUserDislikeIds(User.GetUserId()));
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<MemberDto>>> GetUserDislikes([FromQuery]DislikesParams dislikesParams)
    {
        dislikesParams.UserId = User.GetUserId();
        var users = await dislikesRepository.GetUserDislikes(dislikesParams);

        Response.AddPaginationHeader(users);

        return Ok(users);        
    }
}
