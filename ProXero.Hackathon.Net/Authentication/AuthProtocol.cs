using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using US.OpenServer;
using US.OpenServer.Protocols;

namespace ProXero.Hackathon.Net.Authentication
{
	public abstract class AuthProtocol : AuthenticationProtocolBase
	{
		public const ushort PROTOCOL_IDENTIFIER = 10;

		protected BinaryWriter GetBinaryWriter(MemoryStream ms, AuthProtocolCommands command)
		{
			BinaryWriter bw = new BinaryWriter(ms, Encoding.UTF8);
			bw.Write(PROTOCOL_IDENTIFIER);
			bw.Write((byte)command);
			return bw;
		}

		protected override void Log(Level level, string message)
		{
			Session.Log(level, string.Format("[Auth] {0}", message));
		}

	}
}
