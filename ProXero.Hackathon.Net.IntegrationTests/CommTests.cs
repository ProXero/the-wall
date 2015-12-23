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
			Client<int> c = new Client<int>();
			int result = 0;

			c.Inbox.Subscribe(m => result = m);
			c.SendMessage(33);
			

			s.Close();
			c.Close();

			Assert.That(result, Is.EqualTo(133));
		}
	}
}
