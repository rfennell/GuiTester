using System;
using System.Reflection;
using System.Diagnostics;
using System.Windows.Forms;


namespace GuiTester.TestAttributes
{
	/// <summary>
	/// Test the onclick event of any Control that allows such an event to 
	/// be called, the result is looked for in an Control that has a .Text property
	/// A Control can have multiple events tested
	/// 
	/// www.blackmarble.co.uk
	/// Copyright Black Marble 2005 
	/// Author: Richard Fennell
	/// </summary>
	[AttributeUsage(AttributeTargets.Field, AllowMultiple=true)]
	public sealed class ClickTextTestAttribute: GuiTestAttribute
	{
        /// <summary>
        /// Treat the strings provided as regular expresssions
        /// </summary>
        private bool _matchAsRegularExpressions = false;

		/// <summary>
		/// The name of the label field we are interested looking at
		/// </summary>
		private string _targetLabel="";

		/// <summary>
		/// The result we looking for
		/// </summary>
		private string _textAfter=null;

		/// <summary>
		/// The result we looking for
		/// </summary>
		private string _textBefore=null;

		/// <summary>
		/// The name for the target
		/// </summary>
		public string TargetLabel
		{
			get 
			{
				return this._targetLabel;
			}
		}


		/// <summary>
		/// The text of the target before testing
		/// </summary>
		public string TextBefore
		{
			get 
			{
				return this._textBefore;
			}
		}

		/// <summary>
		/// The text of the target after testing
		/// </summary>
		public string TextAfter
		{
			get 
			{
				return this._textAfter;
			}
		}


		/// <summary>
		/// Sets the attribute properties via a constructor
		/// </summary>
		/// <param name="testLabel">The meaningful name for a test</param>
		/// <param name="targetLabel">The target label for the control to look for the result in</param>
		/// <param name="textAfter">The result to look in</param>
		public ClickTextTestAttribute(string testLabel, string targetLabel, string textAfter)
            : this(testLabel,targetLabel,null, textAfter, false)
		{
		}

        /// <summary>
        /// Sets the attribute properties via a constructor
        /// </summary>
        /// <param name="testLabel">The meaningful name for a test</param>
        /// <param name="targetLabel">The target label for the control to look for the result in</param>
        /// <param name="textAfter">The result to look in</param>
        /// <param name="useRegularExpressions">True if the test data provided is in the forms as regular expressions</param>
        public ClickTextTestAttribute(string testLabel, string targetLabel, string textAfter, bool useRegularExpressions)
            : this(testLabel, targetLabel ,null, textAfter, useRegularExpressions)
        {
        }


        /// <summary>
		/// Sets the attribute properties via a constructor
		/// </summary>
		/// <param name="testLabel">The meaningful name for a test</param>
		/// <param name="targetLabel">The target label for the control to look for the result in</param>
		/// <param name="textBefore">The state before the click</param>
		/// <param name="textAfter">The result to look in</param>
        public ClickTextTestAttribute(string testLabel, string targetLabel, string textBefore, string textAfter)
            : this(testLabel, targetLabel, textBefore, textAfter, false)
        {
        }

		/// <summary>
		/// Sets the attribute properties via a constructor
		/// </summary>
		/// <param name="testLabel">The meaningful name for a test</param>
		/// <param name="targetLabel">The target label for the control to look for the result in</param>
		/// <param name="textBefore">The state before the click</param>
		/// <param name="textAfter">The result to look in</param>
        /// <param name="useRegularExpressions">True if the test data provided is in the forms as regular expressions</param>
		public ClickTextTestAttribute(string testLabel, string targetLabel, string textBefore, string textAfter, bool useRegularExpressions):base(testLabel)
		{
			_targetLabel = targetLabel;
			_textAfter = textAfter;
			_textBefore = textBefore;
            _matchAsRegularExpressions = useRegularExpressions;
		}


		/// <summary>
		/// Called to run this test
		/// </summary>
		/// <param name="obj">The instance of the application</param>
		/// <param name="mInfo">The item to test</param>
		/// <returns>True if the test is passed</returns>
		public override bool DoTest(object obj,FieldInfo mInfo)
		{
			
			// find the control we are looking for a change in
			object testControl = FindControl((Form)obj,this._targetLabel);  // and look at a control, anything with a .Text property

			if (testControl==null) 
			{
				throw new TestFailedException("Cannot find target item of name [" + this._targetLabel + "]");
			}

			// do we have to check the state before?
			if (this._textBefore!=null) 
			{
                if (_matchAsRegularExpressions == false)
                {
                    // text match
                    if (this._textBefore != ((Control)testControl).Text)
                    {
                        System.Diagnostics.Trace.WriteLineIf(this.TraceSwitch.Level >= TraceLevel.Verbose, "Before pressing [" + ((Control)testControl).Name + "] showed [" + ((Control)testControl).Text + "]");
                        return false;
                    } // if failed
                }
                else
                {
                    // regular expression
                    if (System.Text.RegularExpressions.Regex.IsMatch(((Control)testControl).Text, this._textBefore) == false)
                    {
                        System.Diagnostics.Trace.WriteLineIf(this.TraceSwitch.Level >= TraceLevel.Verbose, "Before pressing [" + ((Control)testControl).Name + "] showed [" + ((Control)testControl).Text + "]");
                        return false;
                    }
                }
			} // end if before test

			//do the click
			InvokeEventOnObject(obj,mInfo,"OnClick");

			System.Diagnostics.Trace.WriteLineIf(this.TraceSwitch.Level >= TraceLevel.Verbose,"After pressing [" + ((Control)testControl).Name + "] showed [" + ((Control)testControl).Text+"]");
            if (_matchAsRegularExpressions == false)
            {
                // text match
                return (this._textAfter == ((Control)testControl).Text);
            }
            else
            {
                // regular expression
                return (System.Text.RegularExpressions.Regex.IsMatch(((Control)testControl).Text, this._textAfter));
            }
			
		}

	} // end class
} // end ns
