using System.Collections.Generic;


namespace TalkToApiStudyTest.V1.Models.dto
{
    public abstract class BaseDTO
    {
        public List<LinkDTO> links { get; set; } = new List<LinkDTO>();
    }
}
