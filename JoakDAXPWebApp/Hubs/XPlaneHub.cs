using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR;
using XPlaneUDPExchange.Model.Data;

namespace JoakDAXPWebApp.Hubs
{
    public class XPlaneHub : Hub
    {
        public async Task SendXPData(XPlaneData data)
        {
            await Clients.All.SendAsync("XPlaneData", data);
        }
    }
}
