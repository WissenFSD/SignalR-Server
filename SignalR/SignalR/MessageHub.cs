using Microsoft.AspNetCore.SignalR;

namespace SignalR.SignalR
{
	public class MessageHub:Hub
	{
		public async Task SendName(string name)
		{
			await Clients.All.SendAsync("Name", name);
		}
	}
}
