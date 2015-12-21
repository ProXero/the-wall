using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using US.OpenServer.Protocols;

namespace ProXero.Hackathon.Net.Authentication
{
	public class AuthProtocolConfigurationServer : ProtocolConfigurationEx
	{
		protected AuthProtocolConfigurationServer(ushort id, Type protocolType) : base(id, protocolType)
		{
		}
	}
}
