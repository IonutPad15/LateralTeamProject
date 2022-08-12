using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.Request
{
    public class CommentRequest
    {
        public int? IssueId { get; set; }
        public int? CommentId { get; set; }
        public string Body { get; set; } = string.Empty;

    }
}
