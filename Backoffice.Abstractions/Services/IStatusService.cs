using System.Collections.Generic;
using System.Threading.Tasks;

namespace Backoffice.Abstractions.Services
{
    public interface IBoTraderStatus
    {
        string Id { get; }
        string Name { get; }
    }

    public interface IBoAchievementsStatus
    {
        string Id { get; }
    }
    
    public class BoAchievementsStatus :  IBoAchievementsStatus
    {
        public string Id { get; set; }

        public static BoAchievementsStatus Create(string id)
        {
            return new BoAchievementsStatus {Id = id};
        }
    }

    public class BoTraderStatus : IBoTraderStatus
    {
        public string Id { get; set;}
        public string Name { get; set;}

        public static BoTraderStatus Create(string id, string name)
        {
            return new BoTraderStatus
            {
                Id = id,
                Name = name
            };
        }
    }

    public interface IStatusService
    {
        Task<IEnumerable<IBoTraderStatus>> GetCrmStatusesAsync();
        Task<IEnumerable<IBoTraderStatus>> GetTradingStatusesAsync();
        Task<IEnumerable<IBoAchievementsStatus>> GetAchievementsStatusesAsync();
        ValueTask UpdateCrmStatusAsync(string traderId, string statusId);
        ValueTask UpdateTradingStatusAsync(string traderId, string statusId);
        ValueTask UpdateAchievementsStatusAsync(string traderId, string statusId);
    }
}