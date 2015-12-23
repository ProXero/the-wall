using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;
using US.OpenServer;
using US.OpenServer.Protocols;

namespace ProXero.Hackathon.Net
{
	public abstract class MessageProtocol<TMessage> : ProtocolBase
	{
		public const ushort PROTOCOL_IDENTIFIER = 0x0010;

		protected override void Log(Level level, string message)
		{
			Session.Log(level, string.Format("[Message] {0}", message));
		}

		public void Send(TMessage message)
		{
			MemoryStream ms = new MemoryStream();
			BinaryWriter bw = new BinaryWriter(ms, Encoding.UTF8);
			bw.Write(PROTOCOL_IDENTIFIER);
			(new BinaryFormatter()).Serialize(bw.BaseStream, message);
			Session.Send(ms);
		}
	}
}
