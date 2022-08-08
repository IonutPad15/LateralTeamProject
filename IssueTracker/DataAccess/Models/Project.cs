using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Models
{
    public class Project
    {
        public Project()
        {
            Participants = new List<Participant>();
            Issues = new List<Issue>();
        }
        public int Id { get; set; }
        public string Title { get; set; } = null!;
        public string Description { get; set; } = null!;
        public DateTime Created { get; set; }
        public bool IsDeleted { get; set; }
        public List<Participant> Participants { get; set; } 
        public List<Issue> Issues { get; set; }
    }
}
