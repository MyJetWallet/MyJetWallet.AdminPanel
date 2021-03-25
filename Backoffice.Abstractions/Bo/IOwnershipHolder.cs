namespace Backoffice.Abstractions.Bo
{
    public interface IOwnershipHolder
    {
        string Owner { get; set; }
        string AssignedUserId { get; set; }
        string RetentionManager { get; set; }
    }
}