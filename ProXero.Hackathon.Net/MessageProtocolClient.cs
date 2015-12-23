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
	public class MessageProtocolClient<TMessage> : MessageProtocol<TMessage>
	{
		private readonly Subject<TMessage> inbox = new Subject<TMessage>();

		public override void OnPacketReceived(BinaryReader br)
		{

			var message = (TMessage)(new BinaryFormatter()).Deserialize(br.BaseStream);

			Log(Level.Info, string.Format("client received: {0}", message.ToString()));

			inbox.OnNext(message);
		}

		public IObservable<TMessage> Inbox { get { return inbox; } }
	}
}
