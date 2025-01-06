
using API.DTOs;
using API.Entities;
using API.Helpers;

namespace API.Interfaces;

public interface IDislikesRepository
{
    Task<UserDislike?> GetUserDislike(int sourceUserId, int targetUserId);
    Task<PagedList<MemberDto>> GetUserDislikes(DislikesParams dislikesParams);
    Task<IEnumerable<int>> GetCurrentUserDislikeIds(int currentUserId);
    void DeleteDislike(UserDislike dislike);
    void AddDislike(UserDislike dislike);
    Task<bool> SaveChanges();
}
