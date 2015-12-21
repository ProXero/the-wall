using NUnit.Framework;
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
			Server s = new Server();
			Client c = new Client();

			Thread.Sleep(3000);

			s.Close();
			c.Close();
		}
	}
}
