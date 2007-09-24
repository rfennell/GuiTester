using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using System.Data;
using System.Xml.Serialization;
using System.IO;
using System.Data.Odbc;
using GuiTester.TestAttributes;
using System.Reflection	;

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
	public class Form1 : System.Windows.Forms.Form
	{
		[TextSizeTest("labelMessage Size test")] // true
		private System.Windows.Forms.Label labelMessage;

		[ClickTextTest("buttonChangeText Click Test 1 (should fail)","labelMessage","Hello World")] // false
		[ClickTextTest("buttonChangeText Click Test 2","textBoxDataEntry","Hello World")] // true
		private System.Windows.Forms.Button buttonChangeText;
		
		
		private System.Windows.Forms.MainMenu mainMenu;
		private System.Windows.Forms.MenuItem menuFile;
		private System.Windows.Forms.MenuItem menuTest;
		private System.Windows.Forms.MenuItem menuGenerateTestData;
		private System.Windows.Forms.SaveFileDialog saveFileDialog;
		private DataSet ds ;


		/// <summary>
		///  The XML data file to load
		/// </summary>
		private string xmlFilePath = "";
		private System.Windows.Forms.DataGrid dg;
		
		[TextSizeTest("textBoxDataEntry 1 Size test (should fail)")] // false
		private System.Windows.Forms.TextBox textBoxDataEntry;
		private System.Windows.Forms.MenuItem menuItem1;

		[ClickTextTest("menuClickHere Click Test 1","textBoxDataEntry","Hello World")] // true
		private System.Windows.Forms.MenuItem menuClickHere;

		[ClickOpenFormTest("buttonOpenWindow Click Open window Test 1","Form2",true)] // true
		private System.Windows.Forms.Button buttonOpenWindow;

		[ClickOpenMsgBoxTestAttribute("menuAlertTest Click Open window Test 1","Message")] // true
		private System.Windows.Forms.MenuItem menuAlertTest;

		[ClickDataGridCountTest("buttonLoadData Click DG Test 1","dg",0,5)] // true
        private System.Windows.Forms.Button buttonLoadData;

        [ClickTextTest("btnRegularExpression email test","textBoxRegularExpression",@"^\w+([-+.]\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*$",true)]
        private Button btnRegularExpression;

        private TextBox textBoxRegularExpression;
        private IContainer components;

		public Form1()
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
				if (components != null) 
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
            this.labelMessage = new System.Windows.Forms.Label();
            this.buttonChangeText = new System.Windows.Forms.Button();
            this.mainMenu = new System.Windows.Forms.MainMenu(this.components);
            this.menuFile = new System.Windows.Forms.MenuItem();
            this.menuTest = new System.Windows.Forms.MenuItem();
            this.menuGenerateTestData = new System.Windows.Forms.MenuItem();
            this.menuItem1 = new System.Windows.Forms.MenuItem();
            this.menuClickHere = new System.Windows.Forms.MenuItem();
            this.menuAlertTest = new System.Windows.Forms.MenuItem();
            this.saveFileDialog = new System.Windows.Forms.SaveFileDialog();
            this.dg = new System.Windows.Forms.DataGrid();
            this.textBoxDataEntry = new System.Windows.Forms.TextBox();
            this.buttonOpenWindow = new System.Windows.Forms.Button();
            this.buttonLoadData = new System.Windows.Forms.Button();
            this.btnRegularExpression = new System.Windows.Forms.Button();
            this.textBoxRegularExpression = new System.Windows.Forms.TextBox();
            ((System.ComponentModel.ISupportInitialize)(this.dg)).BeginInit();
            this.SuspendLayout();
            // 
            // labelMessage
            // 
            this.labelMessage.Location = new System.Drawing.Point(24, 8);
            this.labelMessage.Name = "labelMessage";
            this.labelMessage.Size = new System.Drawing.Size(248, 23);
            this.labelMessage.TabIndex = 0;
            this.labelMessage.Text = "Default Message";
            // 
            // buttonChangeText
            // 
            this.buttonChangeText.Location = new System.Drawing.Point(16, 40);
            this.buttonChangeText.Name = "buttonChangeText";
            this.buttonChangeText.Size = new System.Drawing.Size(75, 23);
            this.buttonChangeText.TabIndex = 1;
            this.buttonChangeText.Text = "Click Here";
            this.buttonChangeText.Click += new System.EventHandler(this.buttonChangeText_Click);
            // 
            // mainMenu
            // 
            this.mainMenu.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
            this.menuFile,
            this.menuTest});
            // 
            // menuFile
            // 
            this.menuFile.Index = 0;
            this.menuFile.Text = "File";
            // 
            // menuTest
            // 
            this.menuTest.Index = 1;
            this.menuTest.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
            this.menuGenerateTestData,
            this.menuItem1,
            this.menuClickHere,
            this.menuAlertTest});
            this.menuTest.Text = "Test";
            // 
            // menuGenerateTestData
            // 
            this.menuGenerateTestData.Index = 0;
            this.menuGenerateTestData.Text = "Generate Test Data";
            this.menuGenerateTestData.Click += new System.EventHandler(this.menuGenerateTestData_Click);
            // 
            // menuItem1
            // 
            this.menuItem1.Index = 1;
            this.menuItem1.Text = "-";
            // 
            // menuClickHere
            // 
            this.menuClickHere.Index = 2;
            this.menuClickHere.Text = "Click Here";
            this.menuClickHere.Click += new System.EventHandler(this.menuClickHere_Click);
            // 
            // menuAlertTest
            // 
            this.menuAlertTest.Index = 3;
            this.menuAlertTest.Text = "Alert Test";
            this.menuAlertTest.Click += new System.EventHandler(this.menuAlertTest_Click);
            // 
            // dg
            // 
            this.dg.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.dg.DataMember = "";
            this.dg.HeaderForeColor = System.Drawing.SystemColors.ControlText;
            this.dg.Location = new System.Drawing.Point(8, 135);
            this.dg.Name = "dg";
            this.dg.Size = new System.Drawing.Size(280, 233);
            this.dg.TabIndex = 2;
            // 
            // textBoxDataEntry
            // 
            this.textBoxDataEntry.Location = new System.Drawing.Point(16, 72);
            this.textBoxDataEntry.Name = "textBoxDataEntry";
            this.textBoxDataEntry.Size = new System.Drawing.Size(100, 20);
            this.textBoxDataEntry.TabIndex = 5;
            this.textBoxDataEntry.Text = "This text is too long and will cause an error";
            // 
            // buttonOpenWindow
            // 
            this.buttonOpenWindow.Location = new System.Drawing.Point(200, 40);
            this.buttonOpenWindow.Name = "buttonOpenWindow";
            this.buttonOpenWindow.Size = new System.Drawing.Size(75, 23);
            this.buttonOpenWindow.TabIndex = 6;
            this.buttonOpenWindow.Text = "Open Form";
            this.buttonOpenWindow.Click += new System.EventHandler(this.button1_Click);
            // 
            // buttonLoadData
            // 
            this.buttonLoadData.Location = new System.Drawing.Point(112, 40);
            this.buttonLoadData.Name = "buttonLoadData";
            this.buttonLoadData.Size = new System.Drawing.Size(75, 23);
            this.buttonLoadData.TabIndex = 7;
            this.buttonLoadData.Text = "Load Data";
            this.buttonLoadData.Click += new System.EventHandler(this.buttonLoadData_Click);
            // 
            // btnRegularExpression
            // 
            this.btnRegularExpression.Location = new System.Drawing.Point(16, 98);
            this.btnRegularExpression.Name = "btnRegularExpression";
            this.btnRegularExpression.Size = new System.Drawing.Size(75, 23);
            this.btnRegularExpression.TabIndex = 8;
            this.btnRegularExpression.Text = "Regular Exp";
            this.btnRegularExpression.Click += new System.EventHandler(this.btnRegularExpression_Click);
            // 
            // textBoxRegularExpression
            // 
            this.textBoxRegularExpression.Location = new System.Drawing.Point(112, 100);
            this.textBoxRegularExpression.Name = "textBoxRegularExpression";
            this.textBoxRegularExpression.Size = new System.Drawing.Size(100, 20);
            this.textBoxRegularExpression.TabIndex = 9;
            // 
            // Form1
            // 
            this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
            this.ClientSize = new System.Drawing.Size(292, 369);
            this.Controls.Add(this.textBoxRegularExpression);
            this.Controls.Add(this.btnRegularExpression);
            this.Controls.Add(this.buttonLoadData);
            this.Controls.Add(this.buttonOpenWindow);
            this.Controls.Add(this.textBoxDataEntry);
            this.Controls.Add(this.dg);
            this.Controls.Add(this.buttonChangeText);
            this.Controls.Add(this.labelMessage);
            this.Menu = this.mainMenu;
            this.Name = "Form1";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.SampleApp_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dg)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

		}
		#endregion

		/// <summary>
		/// The main entry point for the application.
		/// </summary>
		[STAThread]
		static void Main() 
		{
			Application.Run(new Form1());
		}

		/// <summary>
		/// Called when the button is pressed, if private reflection cannot see it, public it can
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void buttonChangeText_Click(object sender, System.EventArgs e)
		{
			ClickHereTest();
		}

		private void ClickHereTest() 
		{
			this.labelMessage.Text ="Pressed " + DateTime.Now.ToString();
			this.textBoxDataEntry.Text = "Hello World";
		}

		private void menuGenerateTestData_Click(object sender, System.EventArgs e)
		{
			GenerateTestData( xmlFilePath );
		}

		/// <summary>
		/// Generates an XML test block
		/// </summary>
		private void GenerateTestData(string filename) {
			ArrayList col = new ArrayList();
			col.Add(new TestData("Adam","Apple",new DateTime(1971,1,10)));
			col.Add(new TestData("Bill","Bloggs",new DateTime(1972,2,11)));
			col.Add(new TestData("Clare","Cat",new DateTime(1973,3,12)));
			col.Add(new TestData("Dawn","Dog",new DateTime(1974,4,13)));
			col.Add(new TestData("Edward","Egg",new DateTime(1975,5,14)));

			TestData[] data =(TestData[])col.ToArray(typeof (TestData));
			try 
			{
				XmlSerializer serializer = new XmlSerializer( typeof( TestData[] ) );
				TextWriter writer = new StreamWriter(filename);
				serializer.Serialize( writer, data );
				writer.Close();

				MessageBox.Show(this,"Saved test data file OK","Sample Application",MessageBoxButtons.OK,MessageBoxIcon.Information);			
			} 
			catch (Exception ex) 
			{
				MessageBox.Show(this,"Saved test data file failed \n" +ex.Message,"Sample Application",MessageBoxButtons.OK,MessageBoxIcon.Error);			
			}
		}

		/// <summary>
		/// The page load event, we add the datagrid loading here, as a test to 
		/// make sure that when we load we fire the even
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void SampleApp_Load(object sender, System.EventArgs e)
		{
			// do nothing
		}

		private void LoadXMLData() 
		{
			try 
			{
				// in the real world we would use this
				xmlFilePath = ConfigSettings.GetConfigSetting("DATAFILE");
				// but the method uses the settings of the test harness so
		
				// a rough and ready load
				if (File.Exists(xmlFilePath)==false ) 
				{
					this.GenerateTestData( xmlFilePath );
				}
				ds = new DataSet();
				ds.ReadXml( xmlFilePath);
				this.dg.DataSource = ds.Tables[0];
			} 
			catch (Exception ex) 
			{
				MessageBox.Show(this,"Load test data file failed \n" +ex.Message,"Sample Application",MessageBoxButtons.OK,MessageBoxIcon.Error);			
			}

		}


		private void button1_Click(object sender, System.EventArgs e)
		{
			new Form2().Show();
		}

		private void menuClickHere_Click(object sender, System.EventArgs e)
		{
			ClickHereTest();
		}

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

	
		private void menuAlertTest_Click(object sender, System.EventArgs e)
		{
			MessageBox.Show(this,"A test message","Message",MessageBoxButtons.OK,MessageBoxIcon.Information);
		}

		private void buttonLoadData_Click(object sender, System.EventArgs e)
		{
			this.LoadXMLData();
		}

        private void btnRegularExpression_Click(object sender, EventArgs e)
        {
            this.textBoxRegularExpression.Text = "fred@blackmarble.co.uk";
        }


	


	} // end class
} //end ns