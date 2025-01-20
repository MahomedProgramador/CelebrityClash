
using API.DTOs;
using API.Entities;
using API.Helpers;
using API.Interfaces;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;

namespace API.Data;

public class DislikesRepository(DataContext context, IMapper mapper) : IDislikesRepository
{
    public void AddDislike(UserDislike dislike)
    {
        context.Dislikes.Add(dislike);
    }

    public void DeleteDislike(UserDislike dislike)
    {
        context.Dislikes.Remove(dislike);
    }

    public async Task<IEnumerable<int>> GetCurrentUserDislikeIds(int currentUserId)
    {
        return await context.Dislikes
            .Where(x => x.SourceUserId == currentUserId)
            .Select(x => x.TargetUserId)
            .ToListAsync();
    }

    public async Task<UserDislike?> GetUserDislike(int sourceUserId, int targetUserId)
    {
        return await context.Dislikes.FindAsync(sourceUserId, targetUserId);
    }

    public async Task<PagedList<MemberDto>> GetUserDislikes(DislikesParams dislikesParams)
    {
        var dislikes = context.Dislikes.AsQueryable();
        IQueryable<MemberDto> query;

        switch (dislikesParams.Predicate)
        {
             case "disliked":
                query = dislikes
                    .Where(x => x.SourceUserId == dislikesParams.UserId)
                    .Select(x => x.TargetUser)
                    .ProjectTo<MemberDto>(mapper.ConfigurationProvider);
                break;
                
            case "dislikedBy":
                query = dislikes
                    .Where(x => x.TargetUserId == dislikesParams.UserId)
                    .Select(x => x.SourceUser)
                    .ProjectTo<MemberDto>(mapper.ConfigurationProvider);
                break;

            default:
                var dislikesIds = await GetCurrentUserDislikeIds(dislikesParams.UserId);

                query = dislikes
                    .Where(x => x.TargetUserId == dislikesParams.UserId && dislikesIds.Contains(x.SourceUserId))
                    .Select(x => x.SourceUser)
                    .ProjectTo<MemberDto>(mapper.ConfigurationProvider);
                break;
        }   
        
        return await PagedList<MemberDto>.CreateAsync(query, dislikesParams.PageNumber, dislikesParams.PageSize);
    }
}
