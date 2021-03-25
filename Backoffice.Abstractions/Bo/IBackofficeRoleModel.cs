using System.Collections.Generic;

namespace Backoffice.Abstractions.Bo
{
    public interface IBackofficeRoleModel
    {
        string Id { get; }
        string Name { get; }
        IEnumerable<string> Rights { get; }
    }
    
    public class BackofficeRoleModel : IBackofficeRoleModel
    {
        public string Id { get; set;}
        public string Name { get; set;}
        public IEnumerable<string> Rights { get; set; }

        public static BackofficeRoleModel Create(IBackofficeRoleModel src)
        {
            return new BackofficeRoleModel
            {
                Id = src.Id,
                Name = src.Name,
                Rights = src.Rights
            };
        }
        
        public static BackofficeRoleModel Create(string id, string name, IEnumerable<string> rights)
        {
            return new BackofficeRoleModel
            {
                Id = id,
                Name = name,
                Rights = rights
            };
        }
    }
}