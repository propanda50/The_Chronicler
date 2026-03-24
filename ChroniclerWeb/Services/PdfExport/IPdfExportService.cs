using ChroniclerWeb.Models;

namespace ChroniclerWeb.Services.NewFolder
{
    public interface IPdfExportService
    {
        byte[] ExportCampaignSummary(Campaign campaign, List<Session> sessions);
        byte[] ExportCharacterSheet(Character character);
    }
}