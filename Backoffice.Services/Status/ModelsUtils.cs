using Backoffice.Abstractions.Services;
using MyCrm.TraderCrmStatuses.Grpc.Models;
using MyCrm.TraderMarketingSalesData.Grpc.Backoffice.Contracts.Responses.Models;

namespace Backoffice.Services.Status
{
    public static class ModelsUtils
    {
        public static IBoTraderStatus ToDomain(this CrmStatusGrpcModel src)
        {
            return new BoTraderStatus
            {
                Id = src.Id,
                Name = src.Name
            };
        }  
        
        public static IBoAchievementsStatus ToDomain(this AchievementStatusGrpsModel src)
        {
            return new BoAchievementsStatus
            {
                Id = src.AchievementStatus
            };
        }
    }
}