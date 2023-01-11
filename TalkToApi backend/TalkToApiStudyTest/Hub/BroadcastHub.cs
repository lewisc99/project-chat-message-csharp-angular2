using Microsoft.AspNetCore.SignalR;
using System.Threading.Tasks;

namespace TalkToApiStudyTest.Hub

#pragma warning disable
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
