using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.Response
{
    public class CommentResponse
    {
        public int Id { get; set; }
        public Guid? UserId { get; set; }
        public int? IssueId { get; set; }
        public int? CommentId { get; set; }
        public string Author { get; set; } = string.Empty;
        public string Body { get; set; } = string.Empty;
        public DateTime Created { get; set; }
        public DateTime Updated { get; set; }

    }
}
