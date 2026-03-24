namespace ChroniclerWeb.Services.Avatar
{
    public interface IAvatarService
    {
        string GetAvatarUrl(string avatarIdentifier);
        IEnumerable<string> GetDefaultAvatars();
        string GetRandomDefaultAvatar();
        bool IsDefaultAvatar(string avatarPath);
    }
}