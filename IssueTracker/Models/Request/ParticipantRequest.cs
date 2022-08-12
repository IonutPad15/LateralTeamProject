using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.Request
{
    public class ParticipantRequest
    {
        public int Id { get; set; }
        public Guid UserId { get; set; }
        public int ProjectId { get; set; }
        public int RoleId { get; set; }
    }
}
