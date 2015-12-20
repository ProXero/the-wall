using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using US.OpenServer;
using US.OpenServer.Protocols;

namespace ProXero.Hackathon.Net
{
	class AuthProtocol : AuthenticationProtocolBase
	{
		public const ushort PROTOCOL_IDENTIFIER = 10;

		public override void OnPacketReceived(BinaryReader br)
		{

			base.OnPacketReceived(br);

			IsAuthenticated = true;
			UserName = "Csabi";
		}

		internal bool Authenticate(object userName, string v, object p)
		{
			Log(Level.Debug, "auth");
			return true;
		}
	}
}
