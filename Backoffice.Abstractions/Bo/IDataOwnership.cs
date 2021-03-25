namespace Backoffice.Abstractions.Bo
{
    public interface IOffice
    {
        string Id { get; set;}
        string Name { get; set;}
        bool IsDisabled { get; set;}
        string PhonePoolId { get; set;}
    }
    
    public class Office : IOffice
    {
        public string Id { get; set;}
        public string Name { get; set;}
        public bool IsDisabled { get; set;}
        public string PhonePoolId { get; set;}
    }
}