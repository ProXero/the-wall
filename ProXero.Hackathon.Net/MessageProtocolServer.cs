using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reactive.Subjects;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;
using US.OpenServer;

namespace ProXero.Hackathon.Net
{
	public abstract class MessageProtocolServer<TMessage> : MessageProtocol<TMessage>
	{
		protected abstract void ReceiveMessage(TMessage message, Action<TMessage> sendMessage);

		public override void OnPacketReceived(BinaryReader br)
		{
			var message = (TMessage)(new BinaryFormatter()).Deserialize(br.BaseStream);

			Log(Level.Info, string.Format("server received: {0}", message.ToString()));
			
			ReceiveMessage(message, Send);
		}
	}
}
