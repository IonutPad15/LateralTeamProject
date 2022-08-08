using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Models
{
    public class Participant
    {
        public int Id { get; set; }
        public Guid UserId { get; set; }
        public int ProjectId { get; set; }
        public int IssueId { get; set; }
    }
}
