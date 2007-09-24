using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using GuiTester.TestAttributes;

namespace GuiTester.SampleApp
{
	/// <summary>
	/// A sample application to demonstrate the use of the GUITestHarness
	/// 
	/// www.blackmarble.co.uk
	/// Copyright Black Marble 2005 
	/// Author: Richard Fennell
	/// </summary>
	[GuiTestable]
	[UserDefinedTest("User Test 1 (should fail)","UserTest1")]
	[UserDefinedTest("User Test 2","UserTest2")]
	public class Form2 : System.Windows.Forms.Form
	{
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;

		public Form2()
		{
			//
			// Required for Windows Form Designer support
			//
			InitializeComponent();

		}

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		protected override void Dispose( bool disposing )
		{
			if( disposing )
			{
				if(components != null)
				{
					components.Dispose();
				}
			}
			base.Dispose( disposing );
		}

		#region Windows Form Designer generated code
		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			this.components = new System.ComponentModel.Container();
			this.Size = new System.Drawing.Size(300,300);
			this.Text = "Form2";
		}
		#endregion


		/// <summary>
		/// User defined test
		/// </summary>
		/// <returns></returns>
		public bool UserTest1() 
		{
			return false;
		}


		/// <summary>
		/// User defined test
		/// </summary>
		/// <returns></returns>
		public bool UserTest2() 
		{
			return true;
		}


	}
}
