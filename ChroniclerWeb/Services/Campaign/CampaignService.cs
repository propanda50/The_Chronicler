using ChroniclerWeb.Data;
using ChroniclerWeb.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChroniclerWeb.Services
{
    public class CampaignService : ICampaignService
    {
        private readonly ApplicationDbContext _context;

        public CampaignService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<bool> IsUserOwner(int campaignId, string userId)
        {
            return await _context.Campaigns
                .AnyAsync(c => c.Id == campaignId && c.OwnerId == userId);
        }

        public async Task<bool> IsUserGameMaster(int campaignId, string userId)
        {
            // Owner is always GM
            if (await IsUserOwner(campaignId, userId))
                return true;

            return await _context.CampaignMembers
                .AnyAsync(cm => cm.CampaignId == campaignId
                    && cm.UserId == userId
                    && cm.Role == CampaignRole.GameMaster);
        }

        public async Task<bool> IsUserMember(int campaignId, string userId)
        {
            if (await IsUserOwner(campaignId, userId))
                return true;

            return await _context.CampaignMembers
                .AnyAsync(cm => cm.CampaignId == campaignId && cm.UserId == userId);
        }

        public async Task<CampaignRole?> GetUserRole(int campaignId, string userId)
        {
            if (await IsUserOwner(campaignId, userId))
                return CampaignRole.GameMaster;

            var member = await _context.CampaignMembers
                .FirstOrDefaultAsync(cm => cm.CampaignId == campaignId && cm.UserId == userId);

            return member?.Role;
        }

        public async Task<bool> CanUserEdit(int campaignId, string userId)
        {
            return await IsUserGameMaster(campaignId, userId);
        }

        public async Task<bool> CanUserAddNotes(int campaignId, string userId)
        {
            if (await IsUserGameMaster(campaignId, userId))
                return true;

            var member = await _context.CampaignMembers
                .FirstOrDefaultAsync(cm => cm.CampaignId == campaignId && cm.UserId == userId);

            return member?.CanAddNotes ?? false;
        }
    }
}
