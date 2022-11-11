using Microsoft.AspNetCore.SignalR;
using System.Threading.Tasks;

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
