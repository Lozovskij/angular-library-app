using LibraryApp.Core.Entities;
using LibraryApp.Core.Interfaces;

namespace LibraryApp.Web.Services;
public class UserService : IUserService
{
    private readonly IHttpContextAccessor _httpContextAccessor;

    public UserService(IHttpContextAccessor httpContextAccessor) =>
        _httpContextAccessor = httpContextAccessor;

    public int GetUserId()
    {
        var userId = _httpContextAccessor?.HttpContext?.User.Claims.First(c => c.Type == "userId").Value
            ?? throw new Exception("Error. User Id is null");
        return int.Parse(userId);
    }
}
