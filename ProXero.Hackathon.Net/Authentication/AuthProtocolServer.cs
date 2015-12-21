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
	public class AuthProtocolServer : AuthProtocol
	{
		#region Private Variables
		/// <summary>
		/// The connection session.
		/// </summary>
		private Session sessionEx;
		private AuthProtocolConfigurationServer pc;

		#endregion

		#region Constructor
		/// <summary>
		/// Creates a WinAuthProtocolServer object.
		/// </summary>
		public AuthProtocolServer()
		{
		}
		#endregion

		#region Public Functions
		/// <summary>
		/// Calls the base <see cref="US.OpenServer.Protocols.ProtocolBase.Initialize"/>
		/// function then saves a reference to the Session and WinAuthProtocolConfigurationServer.
		/// </summary>
		/// <param name="session">A SessionBase that encapsulates the connection
		/// session. This value is casted to Session.</param>
		///<param name="pc">A ProtocolConfiguration that contains configuration
		/// properties. This value is casted to WinAuthProtocolConfigurationServer.</param>
		/// <param name="userData">An object that may be used by client applications to
		/// pass objects or data to client side protocol implementations. This parameter is
		/// not used.</param>
		public override void Initialize(SessionBase session, ProtocolConfiguration pc, object userData = null)
		{
			base.Initialize(session, pc, userData);
			this.sessionEx = (Session)session;
			this.pc = pc as AuthProtocolConfigurationServer;
		}

		/// <summary>
		/// Handles the <see cref="US.OpenServer.Protocols.WinAuth.WinAuthProtocolCommands.AUTHENTICATE"/>
		/// command packet request. 
		/// </summary>
		/// <param name="br">A BinaryReader that contains the command packet.</param>
		public override void OnPacketReceived(BinaryReader br)
		{
			lock (this)
			{
				if (Session == null)
					return;

				AuthProtocolCommands command = (AuthProtocolCommands)br.ReadByte();
				try
				{
					switch (command)
					{
						case AuthProtocolCommands.AUTHENTICATE:

							string userName = br.ReadString();
							string password = br.ReadString();

							try
							{
								if (!Authenticate(userName))
									throw new Exception("Insufficient privileges.");

								UserId = userName;
								UserName = userName;
								IsAuthenticated = true;

								Session.UserName = userName;
								Session.IsAuthenticated = true;
								Session.AuthenticationProtocol = this;

								Log(Level.Info, string.Format(@"Authenticated {0}.", userName));

								MemoryStream ms = new MemoryStream();
								GetBinaryWriter(ms, AuthProtocolCommands.AUTHENTICATED);
								Session.Send(ms);
							}
							catch (Exception ex)
							{
								Log(Level.Notice, string.Format(@"Access denied.  {0}.  User: {1}", ex.Message, userName));

								MemoryStream ms = new MemoryStream();
								GetBinaryWriter(ms, AuthProtocolCommands.ACCESS_DENIED);
								Session.Send(ms);
							}
							break;
						default:
							throw new Exception("Invalid or unsupported command.");
					}
				}
				catch (Exception ex)
				{
					Log(Level.Error, string.Format("{0}  Command: {1}", ex.Message, command));

					MemoryStream ms = new MemoryStream();
					BinaryWriter bw = GetBinaryWriter(ms, AuthProtocolCommands.ERROR);
					bw.Write(ex.Message);
					Session.Send(ms);
				}
			}
		}

		/// <summary>
		/// Checks if the authenticated user is a member of the passed role.
		/// </summary>
		/// <remarks>
		/// This function is made available so application layer protocols can include
		/// fine grained security.
		/// </remarks>
		/// <param name="role">A String that contains the name of the role.</param>
		/// <returns>True if user is a member of the role, otherwise False.</returns>
		public override bool IsInRole(string role)
		{
			return true;
			//if (cachedAssignedRoles.Contains(role))
			//	return true;

			//if (cachedUnAssignedRoles.Contains(role))
			//	return false;

			//if (wp.IsInRole(role))
			//{
			//	cachedAssignedRoles.Add(role);
			//	return true;
			//}
			//else
			//{
			//	cachedUnAssignedRoles.Add(role);
			//	return false;
			//}
		}
		#endregion

		#region Private Functions
		/// <summary>
		/// Looks up the user in the list of cached users. If not found, enumerates each
		/// cached group then calls into the WindowsPrinciple to lookup the user in the
		/// group.
		/// </summary>
		/// <param name="userName">A String that contains the user's name.</param>
		/// <returns>True if authenticated, otherwise false.</returns>
		private bool Authenticate(string userName)
		{
			if (pc == null)
				return true;

			return true;
			//if (pc.Users.Count == 0 && pc.Roles.Count == 0)
			//	return true;

			//if (string.IsNullOrEmpty(userName))
			//	return false;

			//if (pc.Users.Contains(userName.ToLower()))
			//	return true;

			//if (wp == null)
			//	return false;

			//foreach (string group in pc.Roles)
			//{
			//	if (wp.IsInRole(group))
			//		return true;
			//}
			//return false;
		}

		#endregion
	}
}
