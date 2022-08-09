using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Models
{
    public class Issue
    {
        public int Id { get; set; }
        public int ProjectId { get; set; }
        public string Title { get; set; } = null!;
        public string Description { get; set; } = null!;
        public DateTime Created { get; set; }
        public int IssueTypeId { get; set; }
        public int UserAssignedId { get; set; }
        public int PriorityId { get; set; }
        public int StatusId { get; set; }
        public int RoleId { get; set; }
        public bool IsDeleted { get; set; }
        public IssueType? IssueType { get; set; }
    }
}
