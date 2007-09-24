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
	public abstract class ClickOpenFormTestBaseAttribute: GuiTestAttribute
	{
		/// <summary>
		/// The name of the label field we are interested looking at
		/// </summary>
		private string _targetName="";

		/// <summary>
		/// If true the opened forms should be closed at the end of the test
		/// </summary>
		private bool _closeAfterTest=false;

		/// <summary>
		/// Should we thread the click event
		/// </summary>
		private bool _useThreading = false;


		/// <summary>
		/// The name for the target
		/// </summary>
		public string TargetName
		{
			get 
			{
				return this._targetName;
			}
		}

		/// <summary>
		/// Close the form after the test
		/// </summary>
		public bool CloseAfterTest
		{
			get 
			{
				return this._closeAfterTest;
			}
		}


		/// <summary>
		/// Use threading for the test
		/// </summary>
		public bool UseThreading
		{
			get 
			{
				return this._useThreading;
			}
		}


		/// <summary>
		/// Native call to find a window, usually used one or the other
		/// paramter not both, generally the WindowName
		/// </summary>
		/// <param name="strclassName">The class to look for</param>
		/// <param name="strWindowName">The title, name of the window</param>
		/// <returns>Handle to the window</returns>
		[DllImport("user32.dll")]
		public static extern int FindWindow(string strclassName, string strWindowName);

		/// <summary>
		/// Sends a Windows message to a given window
		/// </summary>
		/// <param name="hWnd">The handle of the target window</param>
		/// <param name="msg">The message to send</param>
		/// <param name="wParam">Parameters</param>
		/// <param name="lParam">Parameters</param>
		/// <returns>Success or failure</returns>
		[DllImport("user32.dll", CharSet=CharSet.Auto)]
		public static extern int SendMessage(IntPtr hWnd, int msg, int wParam, int lParam);

		/// <summary>
		/// Value for a command msg
		/// </summary>
		public const int WM_SYSCOMMAND = 0x0112;

		/// <summary>
		/// Parameter for a command msg
		/// </summary>
		public const int SC_CLOSE = 0xF060;


		/// <summary>
		/// Constructor
		/// </summary>
		/// <param name="testLabel">The name for the test</param>
		/// <param name="targetName">The name of the form to look for</param>
		/// <param name="closeAfterTest">True if the new form should be closed on completion</param>
		/// <param name="useThreading">True if the click event should be in its own thread, need for modal box test</param>
		protected ClickOpenFormTestBaseAttribute(string testLabel, string targetName, bool closeAfterTest, bool useThreading):base(testLabel)
		{

			_targetName = targetName;
			_closeAfterTest = closeAfterTest;
			_useThreading = useThreading;
		}

		/// <summary>
		/// Used for thread data passing
		/// </summary>
		private object _obj;
		/// <summary>
		/// Used for thread data passing
		/// </summary>
		private FieldInfo _mInfo;

		/// <summary>
		/// Thread worker process
		/// </summary>
		private void ClickIt()
		{
			InvokeEventOnObject(_obj,_mInfo,"OnClick");
		}

		/// <summary>
		/// Called to run this test
		/// </summary>
		/// <param name="obj">The instance of the application</param>
		/// <param name="mInfo">The item to test</param>
		/// <returns>True if the test is passed</returns>
		public override bool DoTest(object obj,FieldInfo mInfo)
		{

			if (_useThreading==true) 
			{
				_obj =obj;
				_mInfo = mInfo;

                // when GUITester was updated from VS.Net 1.1 to 2.0 the following
                // line had to be added as 2.0 stops CrossThreadCalls secuirty checks
                // this is not the best solution, 
                System.Windows.Forms.Form.CheckForIllegalCrossThreadCalls = false; 

				Thread worker = new Thread(new ThreadStart(ClickIt));
				worker.Start();
				Application.DoEvents();
			} 
			else 
			{
				InvokeEventOnObject(obj,mInfo,"OnClick");
			}

		
			//look to see if the window we want open 
			int hwnd = FindWindow(null,this._targetName);
			System.Diagnostics.Trace.WriteLineIf(this.TraceSwitch.Level >= TraceLevel.Verbose,"After pressing FindWindow return [" + hwnd +"]");

			if (hwnd>0) 
			{
				if (_closeAfterTest==true) 
				{
					// close the window that was opened for the test
					SendMessage(
						(System.IntPtr)hwnd, 
						WM_SYSCOMMAND, 
						SC_CLOSE, 
						0); // try soft kill
				}
				return true;
			} 
			else 
			{
				return false;
			}
		}


	} // end class
} // ns
