using System.Collections.Generic;

namespace TalkToApiStudyTest.V1.Models.dto
{
    public class ListDTO<T>:BaseDTO
    {
        public List<T> Result { get; set; }
        public ListDTO()
        {

        }

    }
}
