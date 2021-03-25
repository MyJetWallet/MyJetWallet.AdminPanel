using System;

namespace Backoffice.Abstractions.Models
{
    public interface ICommentModel
    {
        string Id { get; }
        DateTime DateTime { get; }
        string Category { get; }
        string Author { get; }
        string Comment { get; }
    }

    public class CommentModel : ICommentModel
    {
        public string Id { get; set; }
        public DateTime DateTime { get; set; }
        public string Category { get; set; }
        public string Author { get; set; }
        public string Comment { get; set; }
    }
}