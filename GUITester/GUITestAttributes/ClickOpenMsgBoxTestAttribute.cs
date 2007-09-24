using System;
using System.Reflection;
using System.Diagnostics;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using System.Threading;

namespace GuiTester.TestAttributes
{
	/// Test the onclick event of any Control that allows such an event to 
	/// be called, once clicked this test looks for a new Window of the 
	/// provided make (the contents of the title bar)
	[AttributeUsage(AttributeTargets.Field, AllowMultiple=true)]
	public sealed class ClickOpenMsgBoxTestAttribute :ClickOpenFormTestBaseAttribute
	{
		/// <summary>
		/// Constructor
		/// </summary>
		/// <param name="testLabel">The name for the test</param>
		/// <param name="targetName">The name of the form to look for</param>
		public ClickOpenMsgBoxTestAttribute(string testLabel, string targetName): base(testLabel, targetName, true, true)
		{
		}

	} // end class
} // ns
