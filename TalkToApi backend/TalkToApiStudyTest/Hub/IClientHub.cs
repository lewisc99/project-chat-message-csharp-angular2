using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TalkToApiStudyTest.V1.Models.dto;
namespace TalkToApiStudyTest.Hub
{
   public interface IClientHub
    {
       public Task brodcastNotification(MessageConnectionId messageConnection);
       public Task brodcastConnectionId( string userName);
    }
}
