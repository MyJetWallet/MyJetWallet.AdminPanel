namespace Backoffice.Users
{
    public interface IBoUserAccessor
    {
        string UserId { get; }
    }

    public class BoUserAccessor : IBoUserAccessor
    {
        public string UserId { get; set; }
    }
}