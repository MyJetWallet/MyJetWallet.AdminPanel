using System.Transactions;

namespace Backoffice.Abstractions.Models
{
    public interface IStatusModel
    {
        string Id { get; set; }
        string Name { get; set; }
    }

    public interface IAchievementsStatusModel
    {
        string Id { get; }
    }

    public class AchievementsStatusModel : IAchievementsStatusModel
    {
        public string Id { get; set; }

        public static AchievementsStatusModel Create(string Id)
        {
            return new AchievementsStatusModel
            {
                Id = Id
            };
        }
    }

    public class TradingStatusModel: IStatusModel
    {
        public string Id { get; set; }
        public string Name { get; set; }

        public static TradingStatusModel Create(string id, string name)
        {
            return new TradingStatusModel
            {
                Id = id,
                Name = name
            };
        }
    }
    
    public class CrmStatusModel: IStatusModel
    {
        public string Id { get; set; }
        public string Name { get; set; }

        public static CrmStatusModel Create(string id, string name)
        {
            return new CrmStatusModel
            {
                Id = id,
                Name = name
            };
        }
    }
}