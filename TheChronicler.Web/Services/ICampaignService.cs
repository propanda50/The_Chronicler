using TheChronicler.Web.Models;

namespace TheChronicler.Web.Services
{
    public interface ICampaignService
    {
        Task<bool> IsUserGameMaster(int campaignId, string userId);
        Task<bool> IsUserMember(int campaignId, string userId);
        Task<bool> IsUserOwner(int campaignId, string userId);
        Task<CampaignRole?> GetUserRole(int campaignId, string userId);
        Task<bool> CanUserEdit(int campaignId, string userId);
        Task<bool> CanUserAddNotes(int campaignId, string userId);
    }
}
