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
			List<int> result = new List<int>();

			c.Inbox.Subscribe(m =>
			{
				result.Add(m);
				receive.Set();
			});
			c.SendMessage(33);

			receive.WaitOne();
			receive.Reset();
			s.Close();


			s = new Server<MessageReceiver>();
			c.SendMessage(20);

			receive.WaitOne();

			s.Close();
			c.Close();

			Assert.That(result, Is.EquivalentTo(new[] { 133, 120 }));
		}
	}
}
