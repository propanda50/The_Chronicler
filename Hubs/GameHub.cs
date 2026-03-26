using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;

namespace TheChronicler.Web.Hubs
{
    [Authorize]
    public class GameHub : Hub
    {
        // Join a campaign room
        public async Task JoinCampaign(int campaignId)
        {
            await Groups.AddToGroupAsync(Context.UserIdentifier!, $"campaign-{campaignId}");
            
            // Notify others
            await Clients.Group($"campaign-{campaignId}").SendAsync("PlayerJoined", new
            {
                UserId = Context.UserIdentifier,
                UserName = Context.User?.Identity?.Name,
                CampaignId = campaignId,
                Timestamp = DateTime.UtcNow
            });
        }

        // Leave a campaign room
        public async Task LeaveCampaign(int campaignId)
        {
            await Groups.RemoveFromGroupAsync(Context.UserIdentifier!, $"campaign-{campaignId}");
            
            await Clients.Group($"campaign-{campaignId}").SendAsync("PlayerLeft", new
            {
                UserId = Context.UserIdentifier,
                UserName = Context.User?.Identity?.Name,
                CampaignId = campaignId
            });
        }

        // Real-time chat
        public async Task SendMessage(int campaignId, string message, string messageType = "chat")
        {
            await Clients.Group($"campaign-{campaignId}").SendAsync("NewMessage", new
            {
                UserId = Context.UserIdentifier,
                UserName = Context.User?.Identity?.Name,
                Message = message,
                Type = messageType,
                Timestamp = DateTime.UtcNow
            });
        }

        // Dice roll broadcast
        public async Task BroadcastRoll(int campaignId, string notation, int result, string? modifierNote = null)
        {
            await Clients.Group($"campaign-{campaignId}").SendAsync("DiceRolled", new
            {
                UserId = Context.UserIdentifier,
                UserName = Context.User?.Identity?.Name,
                Notation = notation,
                Result = result,
                ModifierNote = modifierNote,
                Timestamp = DateTime.UtcNow
            });
        }

        // Map update (marker added, moved, etc.)
        public async Task UpdateMap(int campaignId, object mapUpdate)
        {
            await Clients.OthersInGroup($"campaign-{campaignId}").SendAsync("MapUpdated", new
            {
                UserId = Context.UserIdentifier,
                Update = mapUpdate,
                Timestamp = DateTime.UtcNow
            });
        }

        // Turn tracker update
        public async Task UpdateTurnTracker(int campaignId, object trackerState)
        {
            await Clients.Group($"campaign-{campaignId}").SendAsync("TurnTrackerUpdated", new
            {
                UserId = Context.UserIdentifier,
                State = trackerState,
                Timestamp = DateTime.UtcNow
            });
        }

        // Cursor position (for map collaboration)
        public async Task UpdateCursor(int campaignId, double lat, double lng)
        {
            await Clients.OthersInGroup($"campaign-{campaignId}").SendAsync("CursorMoved", new
            {
                UserId = Context.UserIdentifier,
                UserName = Context.User?.Identity?.Name,
                Lat = lat,
                Lng = lng
            });
        }

        // Reveal/Hide fog of war
        public async Task UpdateFogOfWar(int campaignId, object fogData)
        {
            await Clients.Group($"campaign-{campaignId}").SendAsync("FogUpdated", new
            {
                UserId = Context.UserIdentifier,
                FogData = fogData,
                Timestamp = DateTime.UtcNow
            });
        }

        // Character hp/status update
        public async Task UpdateCharacterStatus(int campaignId, int characterId, int currentHp, string? condition = null)
        {
            await Clients.Group($"campaign-{campaignId}").SendAsync("CharacterUpdated", new
            {
                UserId = Context.UserIdentifier,
                CharacterId = characterId,
                CurrentHp = currentHp,
                Condition = condition,
                Timestamp = DateTime.UtcNow
            });
        }

        // On connection
        public override async Task OnConnectedAsync()
        {
            await base.OnConnectedAsync();
        }

        // On disconnection
        public override async Task OnDisconnectedAsync(Exception? exception)
        {
            await base.OnDisconnectedAsync(exception);
        }
    }
}
