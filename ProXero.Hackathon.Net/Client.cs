using ProXero.Hackathon.Net.Authentication;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive.Linq;
using System.Reactive.Subjects;
using System.Text;
using System.Threading.Tasks;
using US.OpenServer.Configuration;
using US.OpenServer.Protocols;
using US.OpenServer.Protocols.KeepAlive;

namespace ProXero.Hackathon.Net
{
	public class Client
	{
		private US.OpenServer.Client client = new US.OpenServer.Client();
		public readonly BehaviorSubject<bool> connectionLost = new BehaviorSubject<bool>(false);

		public Client()
		{
			ServerConfiguration serverConf = new ServerConfiguration();
			serverConf.Host = "localhost";

			Dictionary<ushort, ProtocolConfiguration> protocolConfigurations =
					new Dictionary<ushort, ProtocolConfiguration>();

			protocolConfigurations.Add(AuthProtocol.PROTOCOL_IDENTIFIER,
				new ProtocolConfiguration(AuthProtocol.PROTOCOL_IDENTIFIER, typeof(AuthProtocolClient)));
			protocolConfigurations.Add(KeepAliveProtocol.PROTOCOL_IDENTIFIER,
				new ProtocolConfiguration(KeepAliveProtocol.PROTOCOL_IDENTIFIER, typeof(KeepAliveProtocol)));

			client = new US.OpenServer.Client(serverConf, protocolConfigurations);
			client.Connect();

			var wap = client.Initialize(AuthProtocol.PROTOCOL_IDENTIFIER) as AuthProtocolClient;
			wap.Authenticate("test", "T3stus3r");

			client.Initialize(KeepAliveProtocol.PROTOCOL_IDENTIFIER);

			var obs = Observable.FromEventPattern<US.OpenServer.Client.OnConnectionLostHandler, object>(ev => client.OnConnectionLost += ev, ev => client.OnConnectionLost -= ev);
			obs.Subscribe(EventArgs => connectionLost.OnNext(true));
		}

		public void Close()
		{
			client.Close();
		}
	}
}
