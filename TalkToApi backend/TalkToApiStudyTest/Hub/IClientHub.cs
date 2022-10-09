using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TalkToApiStudyTest.Hub
{
   public interface IClientHub
    {
        Task brodcastNotification();
    }
}
