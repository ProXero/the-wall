using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProXero.Hackathon.Net.IntegrationTests
{
	class MessageReceiver : MessageProtocolServer<int>
	{
		protected override void ReceiveMessage(int message, Action<int> sendMessage)
		{
			sendMessage(message + 100);
		}
	}
}
