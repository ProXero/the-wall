using NUnit.Framework;
using ProXero.Hackathon.Net.IntegrationTests;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ProXero.Hackathon.Net.IntegrationTests
{
	[TestFixture]
	public class CommTests
	{
		[Test]
		public void test1()
		{
			Server<MessageReceiver> s = new Server<MessageReceiver>();
			AutoResetEvent receive = new AutoResetEvent(false);
			Client<int> c = new Client<int>();
			int result = 0;

			c.Inbox.Subscribe(m =>
			{
				result = m;
				receive.Set();
			});
			c.SendMessage(33);

			receive.WaitOne();
			s.Close();
			c.Close();

			Assert.That(result, Is.EqualTo(133));
		}
	}
}
