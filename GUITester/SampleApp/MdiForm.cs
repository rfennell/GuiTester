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
	public class MdiForm : System.Windows.Forms.Form
	{
		private System.Windows.Forms.MainMenu mainMenu;
		[ClickOpenFormTest("menuItem Click Open Child Window Test 1 (should fail)","Form1",true)] // true
		private System.Windows.Forms.MenuItem menuItem;
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;

		public MdiForm()
		{
			//
			// Required for Windows Form Designer support
			//
			InitializeComponent();

		}

		/// <summary>
		/// The main entry point for the application.
		/// </summary>
		[STAThread]
		static void Main() 
		{
			Application.Run(new MdiForm());
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
			this.mainMenu = new System.Windows.Forms.MainMenu();
			this.menuItem = new System.Windows.Forms.MenuItem();
			// 
			// mainMenu1
			// 
			this.mainMenu.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
																					  this.menuItem});
			// 
			// menuItem
			// 
			this.menuItem.Index = 0;
			this.menuItem.Text = "Load Form 1";
			this.menuItem.Click += new System.EventHandler(this.menuItem_Click);
			// 
			// MdiForm
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			this.ClientSize = new System.Drawing.Size(292, 266);
			this.IsMdiContainer = true;
			this.Menu = this.mainMenu;
			this.Name = "MdiForm";
			this.Text = "MdiForm";

		}
		#endregion

		private void menuItem_Click(object sender, System.EventArgs e)
		{
			Form f1=  new Form1();
			f1.MdiParent =this;
			f1.Show();

		}
	}
}
