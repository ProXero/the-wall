using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProXero.Hackathon.Net.Authentication
{
	public enum AuthProtocolCommands : byte
	{
		/// <summary>
		/// Sent by clients to authenticate a user.
		/// </summary>
		AUTHENTICATE = 0x01,

		/// <summary>
		/// Sent by the server in response to a successful authentication
		/// request.
		/// </summary>
		AUTHENTICATED = 0x02,

		/// <summary>
		/// Sent by the server in response to a failed authentication attempt.
		/// </summary>
		ACCESS_DENIED = 0x03,

		/// <summary>
		/// Sent by the server when an authentication attempt errors or an invalid or
		/// unsupported command is received.
		/// </summary>
		ERROR = 0xFF,
	}
}
