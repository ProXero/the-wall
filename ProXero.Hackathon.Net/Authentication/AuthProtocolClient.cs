using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using US.OpenServer;

namespace ProXero.Hackathon.Net.Authentication
{
	public class AuthProtocolClient : AuthProtocol
	{ 
			/// <summary>
			/// Defines the maximum number of milliseconds to wait for an authentication request. 
			/// </summary>
			private const int TIMEOUT = 120000;

		
			public bool Authenticate(string userName, string password)
			{
				lock (this)
				{
					if (Session == null)
						return false;

					UserName = userName;
					Session.UserName = userName;

					MemoryStream ms = new MemoryStream();
					BinaryWriter bw = GetBinaryWriter(ms, AuthProtocolCommands.AUTHENTICATE);
					bw.WriteString(userName);
					bw.WriteString(password);
					Session.Send(ms);

					if (!Monitor.Wait(this, TIMEOUT))
						throw new TimeoutException();

					return IsAuthenticated;
				}
			}


			public override void OnPacketReceived(BinaryReader br)
			{
				lock (this)
				{
					if (Session == null)
						return;

					AuthProtocolCommands command = (AuthProtocolCommands)br.ReadByte();
					switch (command)
					{
						case AuthProtocolCommands.AUTHENTICATED:
							IsAuthenticated = true;
							Session.IsAuthenticated = true;
							Log(Level.Info, "Authenticated.");
							Monitor.PulseAll(this);
							break;

						case AuthProtocolCommands.ACCESS_DENIED:
							Log(Level.Notice, "Access denied.");
							Monitor.PulseAll(this);
							break;

						case AuthProtocolCommands.ERROR:
							{
								string errorMessage = br.ReadString();
								Log(Level.Notice, errorMessage);
								Monitor.PulseAll(this);
								break;
							}

						default:
							Log(Level.Error, string.Format("Invalid or unsupported command.  Command: {0}", command));
							break;
					}
				}
			}

		}
}
