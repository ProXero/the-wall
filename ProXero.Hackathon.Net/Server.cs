using ProXero.Hackathon.Net.Authentication;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using US.OpenServer.Configuration;
using US.OpenServer.Protocols;
using US.OpenServer.Protocols.KeepAlive;

namespace ProXero.Hackathon.Net
{
    public class Server
    {
		private US.OpenServer.Server server;
		public Server()
		{
			ServerConfiguration serverConf = new ServerConfiguration();
			serverConf.Host = "127.0.0.1";
			Dictionary<ushort, ProtocolConfiguration> protocolConfigurations =
					new Dictionary<ushort, ProtocolConfiguration>();

			protocolConfigurations.Add(AuthProtocol.PROTOCOL_IDENTIFIER,
				new ProtocolConfiguration(AuthProtocol.PROTOCOL_IDENTIFIER, typeof(AuthProtocolServer)));
			protocolConfigurations.Add(KeepAliveProtocol.PROTOCOL_IDENTIFIER,
				new ProtocolConfiguration(KeepAliveProtocol.PROTOCOL_IDENTIFIER, typeof(KeepAliveProtocol)));

			server = new US.OpenServer.Server(serverConf, protocolConfigurations);
		}

		public void Close()
		{
			server.Close();
		}
	}
}
