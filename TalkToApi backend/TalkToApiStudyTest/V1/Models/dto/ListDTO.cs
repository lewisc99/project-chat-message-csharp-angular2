using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

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
