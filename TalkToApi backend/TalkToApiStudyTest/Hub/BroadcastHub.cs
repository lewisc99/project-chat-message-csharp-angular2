using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.SignalR;
using System.Security.Claims;
using System.Threading.Tasks;
using TalkToApiStudyTest.V1.Models;


namespace TalkToApiStudyTest.Hub
{
    public class BroadcastHub :Hub<IClientHub>
    {

        public string getConnectionId() => Context.ConnectionId;

        public override Task OnConnectedAsync()
        {

            return base.OnConnectedAsync();
        }
    }
   

}
