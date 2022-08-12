using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.Response
{
    public class CommentInfo
    {
        public int Id { get; set; }
        public Guid? UserId { get; set; }
        public int IssueId { get; set; }
        public string Body { get; set; } = null!;
        public DateTime Created { get; set; }
        public DateTime Updated { get; set; }
        public UserResponse? User { get; set; }
    }
}
