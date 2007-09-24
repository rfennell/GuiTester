using System;
using System.Windows.Forms;
using System.Reflection;
using System.Drawing;

namespace GuiTester.TestAttributes
{
	/// <summary>
	/// Test the size of the rendered text in a control to make sure that all of the
	/// text is visible
	/// 
	/// www.blackmarble.co.uk
	/// Copyright Black Marble 2005 
	/// Author: Richard Fennell
	/// </summary>
	[AttributeUsage(AttributeTargets.Field, AllowMultiple=true)]
	public sealed class TextSizeTestAttribute: GuiTestAttribute
	{
		/// <summary>
		/// Checks to make sure the text on a control is rendered 
		/// such that it can be fully displayed
		/// </summary>
		/// <param name="testLabel">The label for the test</param>
		public TextSizeTestAttribute(string testLabel): base(testLabel)
		{
		}

			
		/// <summary>
		/// Called to run this test on a control
		/// </summary>
		/// <param name="obj">The instance of the application</param>
		/// <param name="mInfo">The item to test</param>
		/// <returns>True if the test is passed</returns>
		public override bool DoTest(object obj,FieldInfo mInfo)
		{

			Control testControl = (Control)mInfo.GetValue(obj);
			// find the graphic context of our window						
			Graphics g = Graphics.FromHwnd(((Form)obj).Handle);

			Size controlSize = testControl.Size;
			SizeF stringSize = g.MeasureString(testControl.Text,testControl.Font);

			System.Diagnostics.Trace.WriteLine("Comparing text size " + stringSize + " against space of " + controlSize);
			if ((stringSize.Height <= controlSize.Height) && (stringSize.Width <= controlSize.Width))
			{
				return true;
			} 
			else 
			{
				return false;
			}
		}
			

	} // class
} // ns
