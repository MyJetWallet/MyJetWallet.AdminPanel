using System.Collections.Generic;

namespace Backoffice.Abstractions.Models
{
    public interface IPhoneNumberModel
    {
        string Id { get; set; }
        string Name { get; set; }
        string Number { get; set; }
    }

    public class PhoneNumberModel : IPhoneNumberModel
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Number { get; set; }
    }

    public interface IPhonePoolModel
    {
        string Id { get; set; }
        string Name { get; set; }
        IEnumerable<IPhoneNumberModel> Numbers { get; set; }
    }

    public class PhonePoolModel : IPhonePoolModel
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public IEnumerable<IPhoneNumberModel> Numbers { get; set; }
    }

    public interface ICallModel
    {
        string ManagerNumberId { get; }
        string CustomerNumber { get; }
        string ManagerPhoneNumber { get; }
        string TraderId { get; }
    }
}