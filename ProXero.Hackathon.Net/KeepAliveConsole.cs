using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using US.OpenServer.Protocols.KeepAlive;

namespace ProXero.Hackathon.Net
{
	public class KeepAliveConsole : KeepAliveProtocol
	{
		public override void OnPacketReceived(BinaryReader br)
		{
			base.OnPacketReceived(br);

			Console.WriteLine("received keepalive...");
		}
	}
}
