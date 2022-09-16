using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TalkToApiStudyTest.V1.Models.dto
{
    public abstract class BaseDTO
    {

        public List<LinkDTO> links { get; set; } = new List<LinkDTO>();
    }
}
