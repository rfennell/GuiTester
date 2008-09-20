using System;
using System.Windows.Forms;
using System.Reflection;
using System.Drawing;

namespace GuiTester.TestAttributes
{
	/// <summary>
	/// Uses a regular expression to make sure the targeted field has the correct format
	/// 
	/// www.blackmarble.co.uk
	/// Copyright Black Marble 2007 
	/// Author: Richard Fennell
	/// </summary>
	[AttributeUsage(AttributeTargets.Field, AllowMultiple=true)]
	public sealed class RegularExpressionTestAttribute: GuiTestAttribute
	{
        /// <summary>
        /// The regular expression to match
        /// </summary>
        private string regularExpression = string.Empty;

		/// <summary>
		/// Checks to make sure the text on a control is rendered 
		/// such that it can be fully displayed
		/// </summary>
		/// <param name="testLabel">The label for the test</param>
        /// <param name="expression">The regular expression to match</param>
        public RegularExpressionTestAttribute(string testLabel, string expression) : base(testLabel)
		{
            regularExpression = expression;
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
            
			System.Diagnostics.Trace.WriteLine("Comparing text " + testControl.Text + " against expression " + this.regularExpression);
            return System.Text.RegularExpressions.Regex.IsMatch(testControl.Text, this.regularExpression);
		}
			

	} // class
} // ns
