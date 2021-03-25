using Backoffice.Abstractions.Models;

namespace Backoffice.Services.PhonePool.Dto
{
    public class CallModelDto : ICallModel
    {
        public string ManagerNumberId { get; set; }
        public string CustomerNumber { get; set; }
        public string ManagerPhoneNumber { get; set; }
        public string TraderId { get; set; }
    }
}