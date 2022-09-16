using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TalkToApiStudyTest.V1.Models.dto
{
    public class MessageDTO:BaseDTO
    {
        public int Id { get; set; }

        public ApplicationUser From { get; set; }
        public string FromId { get; set; }

        public ApplicationUser To { get; set; }

        public string ToId { get; set; }

        public string Text { get; set; }

        public bool Deleted { get; set; }


        public DateTime Created { get; set; }

        public DateTime? Updated { get; set; }
        
    }
}
