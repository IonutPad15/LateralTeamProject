using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Models
{
    public class Comment
    {
        public int Id { get; set; }
        public Guid? UserId { get; set; }
        public int IssueId { get; set; }
        public string Body { get; set; } = null!;
        public DateTime Created { get; set; }
        public DateTime Updated { get; set; }
        public bool IsDeleted { get; set; }
        public User? User { get; set; }
    }
}
