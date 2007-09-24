using System;
using System.Runtime.Serialization;
using System.Security;
using System.Security.Permissions;

namespace GuiTester.TestAttributes
{
	/// <summary>
	/// The generic exception thrown by the test framework
	/// 
	/// www.blackmarble.co.uk
	/// Copyright Black Marble 2005 
	/// Author: Richard Fennell
	/// </summary>
	[Serializable()]
	public sealed class TestFailedException: System.Exception, ISerializable
	{
		/// <summary>
		/// Generic exception used when a test fails
		/// </summary>
		public TestFailedException():base()
		{
		}

		/// <summary>
		/// Generic exception used when a test fails
		/// </summary>
		/// <param name="message">A text message</param>
		public TestFailedException(string message):base(message)
		{
		}

		/// <summary>
		/// Generic exception used when a test fails
		/// </summary>
		/// <param name="message">A text message</param>
		/// <param name="ex">Inner exception</param>
		public TestFailedException(string message, Exception ex):base(message, ex)
		{
		}


		/// <summary>
		/// Generic exception used when a test fails
		/// </summary>
		/// <param name="info"></param>
		/// <param name="context"></param>
		private TestFailedException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			// Implement type-specific serialization constructor logic.
		}


	} // end class
} // end namespace
