using System;
using System.Reflection;
using System.Diagnostics;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using System.Threading;

namespace GuiTester.TestAttributes
{
	/// <summary>
	/// Test the onclick event of any Control that allows such an event to 
	/// be called, once clicked this test looks for a new Window of the 
	/// provided make (the contents of the title bar)
	/// 
	/// www.blackmarble.co.uk
	/// Copyright Black Marble 2005 
	/// Author: Richard Fennell
	/// </summary>
	[AttributeUsage(AttributeTargets.Field, AllowMultiple=true)]
	public sealed class ClickOpenFormTestAttribute :ClickOpenFormTestBaseAttribute
	{
		/// <summary>
		/// Constructor
		/// </summary>
		/// <param name="testLabel">The name for the test</param>
		/// <param name="targetName">The name of the form to look for</param>
		/// <param name="closeAfterTest">True of the new form should be closed on completion</param>
		public ClickOpenFormTestAttribute(string testLabel, string targetName, bool closeAfterTest): base(testLabel, targetName, closeAfterTest, false)
		{
		}

	} // end class
} // ns
