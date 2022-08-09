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
#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
        public string Title { get; set; }
        public string Description { get; set; }
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
        public DateTime Created { get; set; }
        public int IssueTypeId { get; set; }
        public Guid? UserAssignedId { get; set; }
        public int PriorityId { get; set; }
        public int StatusId { get; set; }
        public int RoleId { get; set; }
        public bool IsDeleted { get; set; }
        public IssueType? IssueType { get; set; }
    }
}

//TODO: Object FK Priority & Status & Role & Project
