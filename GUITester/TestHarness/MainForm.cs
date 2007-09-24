using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using System.Data;
using System.Reflection;
using System.IO;
using System.Diagnostics;
using GuiTester.TestAttributes;
using GuiTester.TestFramework;
using MRUSample;
namespace GuiTester.GuiTestHarness
{
	/// <summary>
	/// The main form for the Graphics tools to exercise the GUITestHarness
	/// 
	/// www.blackmarble.co.uk
	/// Copyright Black Marble 2005 
	/// Author: Richard Fennell
	/// </summary>
	public class MainForm : System.Windows.Forms.Form, IMRUClient
	{

		/// <summary>
		/// Manages the recent file list
		/// </summary>
		private MRUManager _mruManager;
		/// <summary>
		/// Colour for tree icons
		/// </summary>
		private const int GREY = 0;
		/// <summary>
		/// Colour for tree icons
		/// </summary>
		private const int YELLOW = 1;
		/// <summary>
		/// Colour for tree icons
		/// </summary>
		private const int GREEN = 2;
		/// <summary>
		/// Colour for tree icons
		/// </summary>
		private const int RED = 3;

		/// <summary>
		/// Flag for debug logging
		/// </summary>
		private static readonly System.Diagnostics.TraceSwitch _traceSwitch = new System.Diagnostics.TraceSwitch("General", "Entire Application");

		private System.Windows.Forms.TabPage _tabLogger;
		private System.Windows.Forms.MenuItem _mnuRunTest;
		private System.Windows.Forms.MenuItem _mnuFile;
		private System.Windows.Forms.MenuItem _mnuView;
		private System.Windows.Forms.MenuItem _mnuClearLog;
		private System.Windows.Forms.MenuItem _mnuTest;
		private System.Windows.Forms.TextBox _txtLogger;
		private System.Windows.Forms.StatusBar _statusBar;
		private System.Windows.Forms.MainMenu _mainMenu;
		private System.Windows.Forms.TreeView _treeView;
		private System.Windows.Forms.Splitter _splitter;
		private System.Windows.Forms.TabControl _tabControl;
		private System.Windows.Forms.MenuItem _mnuHelp;
		private System.Windows.Forms.MenuItem _mnuAbout;
		private System.Windows.Forms.MenuItem _mnuLoad;
		private System.Windows.Forms.MenuItem _mnuSep1;
		private System.Windows.Forms.MenuItem _mnuExit;
		private System.Windows.Forms.OpenFileDialog _openFileDialog;
		private System.Windows.Forms.ImageList _imageList;
		private System.Windows.Forms.TabPage _tabSummary;
		private System.Windows.Forms.TextBox _textSummary;
		private System.Windows.Forms.Panel _resultPanel;
		private System.Windows.Forms.MenuItem _mnuRunSelectedTest;
		private System.Windows.Forms.Button _btnTest;
		private System.Windows.Forms.MenuItem _mnuMRU;
		private System.Windows.Forms.MenuItem _mnuSep2;
		private System.ComponentModel.IContainer components;

		/// <summary>
		/// Default constructor
		/// </summary>
		public MainForm()
		{
			//
			// Required for Windows Form Designer support
			//
			InitializeComponent();
			Trace.Listeners.Add( new bm.utils.TextBoxListener(this._txtLogger));
	
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
			System.Resources.ResourceManager resources = new System.Resources.ResourceManager(typeof(MainForm));
			this._statusBar = new System.Windows.Forms.StatusBar();
			this._mainMenu = new System.Windows.Forms.MainMenu();
			this._mnuFile = new System.Windows.Forms.MenuItem();
			this._mnuLoad = new System.Windows.Forms.MenuItem();
			this._mnuSep1 = new System.Windows.Forms.MenuItem();
			this._mnuExit = new System.Windows.Forms.MenuItem();
			this._mnuSep2 = new System.Windows.Forms.MenuItem();
			this._mnuMRU = new System.Windows.Forms.MenuItem();
			this._mnuView = new System.Windows.Forms.MenuItem();
			this._mnuClearLog = new System.Windows.Forms.MenuItem();
			this._mnuTest = new System.Windows.Forms.MenuItem();
			this._mnuRunTest = new System.Windows.Forms.MenuItem();
			this._mnuRunSelectedTest = new System.Windows.Forms.MenuItem();
			this._mnuHelp = new System.Windows.Forms.MenuItem();
			this._mnuAbout = new System.Windows.Forms.MenuItem();
			this._treeView = new System.Windows.Forms.TreeView();
			this._imageList = new System.Windows.Forms.ImageList(this.components);
			this._splitter = new System.Windows.Forms.Splitter();
			this._tabControl = new System.Windows.Forms.TabControl();
			this._tabSummary = new System.Windows.Forms.TabPage();
			this._btnTest = new System.Windows.Forms.Button();
			this._textSummary = new System.Windows.Forms.TextBox();
			this._resultPanel = new System.Windows.Forms.Panel();
			this._tabLogger = new System.Windows.Forms.TabPage();
			this._txtLogger = new System.Windows.Forms.TextBox();
			this._openFileDialog = new System.Windows.Forms.OpenFileDialog();
			this._tabControl.SuspendLayout();
			this._tabSummary.SuspendLayout();
			this._tabLogger.SuspendLayout();
			this.SuspendLayout();
			// 
			// _statusBar
			// 
			this._statusBar.Location = new System.Drawing.Point(0, 395);
			this._statusBar.Name = "_statusBar";
			this._statusBar.Size = new System.Drawing.Size(688, 22);
			this._statusBar.TabIndex = 4;
			// 
			// _mainMenu
			// 
			this._mainMenu.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
																					  this._mnuFile,
																					  this._mnuView,
																					  this._mnuTest,
																					  this._mnuHelp});
			// 
			// _mnuFile
			// 
			this._mnuFile.Index = 0;
			this._mnuFile.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
																					 this._mnuLoad,
																					 this._mnuSep1,
																					 this._mnuMRU,
																					 this._mnuSep2,
																					 this._mnuExit});
			this._mnuFile.Text = "&File";
			// 
			// _mnuLoad
			// 
			this._mnuLoad.Index = 0;
			this._mnuLoad.Text = "&Load";
			this._mnuLoad.Click += new System.EventHandler(this._mnuLoad_Click);
			// 
			// _mnuSep1
			// 
			this._mnuSep1.Index = 1;
			this._mnuSep1.Text = "-";
			// 
			// _mnuExit
			// 
			this._mnuExit.Index = 4;
			this._mnuExit.Text = "&Exit";
			this._mnuExit.Click += new System.EventHandler(this._mnuExit_Click);
			// 
			// _mnuSep2
			// 
			this._mnuSep2.Index = 3;
			this._mnuSep2.Text = "-";
			// 
			// _mnuMRU
			// 
			this._mnuMRU.Index = 2;
			this._mnuMRU.Text = "Recent Files";
			// 
			// _mnuView
			// 
			this._mnuView.Index = 1;
			this._mnuView.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
																					 this._mnuClearLog});
			this._mnuView.Text = "&View";
			// 
			// _mnuClearLog
			// 
			this._mnuClearLog.Index = 0;
			this._mnuClearLog.Text = "&Clear Log";
			this._mnuClearLog.Click += new System.EventHandler(this._mnuClearLog_Click);
			// 
			// _mnuTest
			// 
			this._mnuTest.Index = 2;
			this._mnuTest.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
																					 this._mnuRunTest,
																					 this._mnuRunSelectedTest});
			this._mnuTest.Text = "&Tests";
			// 
			// _mnuRunTest
			// 
			this._mnuRunTest.Index = 0;
			this._mnuRunTest.Text = "Run &All Test in Tree";
			this._mnuRunTest.Click += new System.EventHandler(this._mnuRunTest_Click);
			// 
			// _mnuRunSelectedTest
			// 
			this._mnuRunSelectedTest.Index = 1;
			this._mnuRunSelectedTest.Text = "&Run Highlighted Test (and below)";
			this._mnuRunSelectedTest.Click += new System.EventHandler(this._mnuRunSelectedTest_Click);
			// 
			// _mnuHelp
			// 
			this._mnuHelp.Index = 3;
			this._mnuHelp.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
																					 this._mnuAbout});
			this._mnuHelp.Text = "&Help";
			// 
			// _mnuAbout
			// 
			this._mnuAbout.Index = 0;
			this._mnuAbout.Text = "&About";
			this._mnuAbout.Click += new System.EventHandler(this._mnuAbout_Click);
			// 
			// _treeView
			// 
			this._treeView.Dock = System.Windows.Forms.DockStyle.Left;
			this._treeView.ImageList = this._imageList;
			this._treeView.Location = new System.Drawing.Point(0, 0);
			this._treeView.Name = "_treeView";
			this._treeView.Size = new System.Drawing.Size(200, 395);
			this._treeView.TabIndex = 5;
			// 
			// _imageList
			// 
			this._imageList.ImageSize = new System.Drawing.Size(16, 16);
			this._imageList.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("_imageList.ImageStream")));
			this._imageList.TransparentColor = System.Drawing.Color.Transparent;
			// 
			// _splitter
			// 
			this._splitter.Location = new System.Drawing.Point(200, 0);
			this._splitter.Name = "_splitter";
			this._splitter.Size = new System.Drawing.Size(3, 395);
			this._splitter.TabIndex = 6;
			this._splitter.TabStop = false;
			// 
			// _tabControl
			// 
			this._tabControl.Controls.Add(this._tabSummary);
			this._tabControl.Controls.Add(this._tabLogger);
			this._tabControl.Dock = System.Windows.Forms.DockStyle.Fill;
			this._tabControl.Location = new System.Drawing.Point(203, 0);
			this._tabControl.Name = "_tabControl";
			this._tabControl.SelectedIndex = 0;
			this._tabControl.Size = new System.Drawing.Size(485, 395);
			this._tabControl.TabIndex = 7;
			// 
			// _tabSummary
			// 
			this._tabSummary.Controls.Add(this._btnTest);
			this._tabSummary.Controls.Add(this._textSummary);
			this._tabSummary.Controls.Add(this._resultPanel);
			this._tabSummary.Location = new System.Drawing.Point(4, 22);
			this._tabSummary.Name = "_tabSummary";
			this._tabSummary.Size = new System.Drawing.Size(477, 369);
			this._tabSummary.TabIndex = 1;
			this._tabSummary.Text = "Test Summary";
			// 
			// _btnTest
			// 
			this._btnTest.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this._btnTest.Enabled = false;
			this._btnTest.Location = new System.Drawing.Point(376, 8);
			this._btnTest.Name = "_btnTest";
			this._btnTest.Size = new System.Drawing.Size(96, 32);
			this._btnTest.TabIndex = 2;
			this._btnTest.Text = "Run Test";
			this._btnTest.Click += new System.EventHandler(this._btnTest_Click);
			// 
			// _textSummary
			// 
			this._textSummary.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
				| System.Windows.Forms.AnchorStyles.Left) 
				| System.Windows.Forms.AnchorStyles.Right)));
			this._textSummary.Location = new System.Drawing.Point(8, 48);
			this._textSummary.Multiline = true;
			this._textSummary.Name = "_textSummary";
			this._textSummary.ReadOnly = true;
			this._textSummary.Size = new System.Drawing.Size(464, 312);
			this._textSummary.TabIndex = 1;
			this._textSummary.Text = "";
			// 
			// _resultPanel
			// 
			this._resultPanel.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
				| System.Windows.Forms.AnchorStyles.Right)));
			this._resultPanel.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
			this._resultPanel.Location = new System.Drawing.Point(8, 8);
			this._resultPanel.Name = "_resultPanel";
			this._resultPanel.Size = new System.Drawing.Size(360, 32);
			this._resultPanel.TabIndex = 0;
			// 
			// _tabLogger
			// 
			this._tabLogger.Controls.Add(this._txtLogger);
			this._tabLogger.Location = new System.Drawing.Point(4, 22);
			this._tabLogger.Name = "_tabLogger";
			this._tabLogger.Size = new System.Drawing.Size(477, 369);
			this._tabLogger.TabIndex = 0;
			this._tabLogger.Text = "Logger";
			// 
			// _txtLogger
			// 
			this._txtLogger.Dock = System.Windows.Forms.DockStyle.Fill;
			this._txtLogger.Location = new System.Drawing.Point(0, 0);
			this._txtLogger.Multiline = true;
			this._txtLogger.Name = "_txtLogger";
			this._txtLogger.ReadOnly = true;
			this._txtLogger.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
			this._txtLogger.Size = new System.Drawing.Size(477, 369);
			this._txtLogger.TabIndex = 4;
			this._txtLogger.Text = "";
			// 
			// MainForm
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			this.ClientSize = new System.Drawing.Size(688, 417);
			this.Controls.Add(this._tabControl);
			this.Controls.Add(this._splitter);
			this.Controls.Add(this._treeView);
			this.Controls.Add(this._statusBar);
			this.Menu = this._mainMenu;
			this.Name = "MainForm";
			this.Text = "Gui Test Harness";
			this.Load += new System.EventHandler(this.MainForm_Load);
			this._tabControl.ResumeLayout(false);
			this._tabSummary.ResumeLayout(false);
			this._tabLogger.ResumeLayout(false);
			this.ResumeLayout(false);

		}
		#endregion

		/// <summary>
		/// The main entry point for the application.
		/// </summary>
		[STAThread]
		static void Main() 
		{
			Application.Run(new MainForm());
		}

		/// <summary>
		/// Finds all the tests in a named assembly
		/// </summary>
		/// <param name="assemblyName">The full path to the file</param>
		private void FindTests(string assemblyName) 
		{
			this._treeView.Nodes.Clear();
				
			TestDataStore[] tests = TestControl.FindTests(assemblyName);

			ArrayList rootObjects = new ArrayList();
			foreach (TestDataStore test in tests)
			{
				if (rootObjects.Contains(test.ContainingClassType) ==false) 
				{
					// we have never seen this type before so add a root object
					// use a collection as it is quicker than a repeated for
					// let the compiler work to optimise it
					TreeNode testableClass = new TreeNode();
					testableClass.Text = test.ContainingClassType.ToString();
					testableClass.Tag = test.ContainingClassType;
					this._treeView.Nodes.Add(testableClass);
					rootObjects.Add(test.ContainingClassType);
				}
				// find the right root object
				foreach (TreeNode rootNode in this._treeView.Nodes) 
				{
					if (((Type)rootNode.Tag)==test.ContainingClassType)
					{
						// we found our root to added the test
						TreeNode testNode = new TreeNode();
						testNode.Text = test.TestAttribute.ToString();
						testNode.Tag = test;
						rootNode.Nodes.Add(testNode);
						break;
					} // end found it
				} // end for nodes

			} // end tests 

			this._treeView.ExpandAll();
			
		} // end method
		



		/// <summary>
		/// Run all tests logic
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void _mnuRunTest_Click(object sender, System.EventArgs e)
		{
			RunAllTestsFromTree();
		}

		/// <summary>
		/// Runs all the test shown in the tree
		/// </summary>
		private void RunAllTestsFromTree()
		{
			this._textSummary.Text="";
			_resultPanel.BackColor = Color.Gray;
			bool overallPass = true;

			foreach (TreeNode rootNode in this._treeView.Nodes) 
			{
				overallPass = RunAllTestsFromNode(rootNode);
			}

			this._textSummary.Text+="Overall return value for all classes under test was " + overallPass + "\r\n";

			// sort out the root icon
			if (overallPass ==true) 
			{
				_resultPanel.BackColor = Color.LightGreen;
			} 
			else 
			{
				_resultPanel.BackColor = Color.Red;
			}
		}

		/// <summary>
		/// Runs all the test from a given node in the tree
		/// </summary>
		/// <param name="rootNode">A node</param>
		/// <returns>True if all tests are past, false if any fail</returns>
		private bool RunAllTestsFromNode(TreeNode rootNode)
		{
			// we dynamically create object now, as we will need it
			object obj = Activator.CreateInstance((System.Type)rootNode.Tag);
			// show it to activate, to trigger the load event
			((Form)obj).Show();
			bool overallPass = true;

			foreach (TreeNode node in rootNode.Nodes)
			{
				if (false==RunATest(obj,node)) 
				{
					// if one failed the whole test fails for this branch
					overallPass = false;
				}
			} // end check for attributes

			this._textSummary.Text+="Overall test for [" + rootNode.Tag.ToString() + "] returned " + overallPass + "\r\n";

			// sort out the root icon
			if (overallPass ==true) 
			{
				SetAllNodeImages(rootNode,GREEN);
				_resultPanel.BackColor = Color.LightGreen;
			} 
			else 
			{
				SetAllNodeImages(rootNode,RED);
				_resultPanel.BackColor = Color.Red;
			}


			// dispose of the object
			((Form)obj).Close();
			obj=null;

			return overallPass;
		}

		/// <summary>
		/// Runs the test associated with a specific node
		/// </summary>
		/// <param name="obj"></param>
		/// <param name="node"></param>
		/// <returns></returns>
		private bool RunATest(object obj,TreeNode node) 
		{
			bool result = false;
			// pul the tag into a local object, purely to make the code readable
			TestDataStore testDetails = (TestDataStore)node.Tag;
			// currently we use a switch to call the correct test
			// but we might be able to do this via inheritance as we define the tests
			try 
			{
				if (testDetails.ControlInfo!=null) 
				{
					// it is a control based test
					result = ((GuiTestAttribute)testDetails.TestAttribute).DoTest(obj,testDetails.ControlInfo);
				} 
				else 
				{
					// it is a class wide test
					result = ((GuiTestAttribute)testDetails.TestAttribute).DoTest(obj);
				}
			} 
			catch (TestFailedException ex) 
			{
				System.Diagnostics.Trace.WriteLineIf(_traceSwitch.Level >= TraceLevel.Info,testDetails.TestAttribute.ToString() + " failed with " + ex.Message);
				result = false;
			}
			catch (Exception ex) 
			{
				System.Diagnostics.Trace.WriteLineIf(_traceSwitch.Level >= TraceLevel.Info,testDetails.TestAttribute.ToString() + " failed with a major error \r\n" + ex.Message);
				result = false;
			}
			if (result ==true) 
			{
				SetAllNodeImages(node,GREEN);
			} 
			else 
			{
				SetAllNodeImages(node,RED);
			}
			System.Diagnostics.Trace.WriteLineIf(_traceSwitch.Level >= TraceLevel.Verbose,testDetails.TestAttribute.ToString() + " returned " + result);
			this._textSummary.Text += "Test [" + testDetails.TestAttribute.ToString() + "] returned " + result +"\r\n";

			return result;

		}

		/// <summary>
		/// Sets all the images for a given node in the tree
		/// </summary>
		/// <param name="node">The tree node</param>
		/// <param name="index">The index into the image list</param>
		private void SetAllNodeImages (TreeNode node, int index) 
		{
			node.ImageIndex =index;
			node.SelectedImageIndex =index;
		}
			

		/// <summary>
		/// Clears the logger textbox
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void _mnuClearLog_Click(object sender, System.EventArgs e)
		{
			this._txtLogger.Clear();
		}

		/// <summary>
		/// The about screen for the harness
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void _mnuAbout_Click(object sender, System.EventArgs e)
		{
			MessageBox.Show(this,"Gui Test Harness\nFor Richard Fennell's Computer Science BSc Project 2004-5\nVersion " + Application.ProductVersion,"Help About",MessageBoxButtons.OK,MessageBoxIcon.Information);
		}

		/// <summary>
		/// Exits the application
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void _mnuExit_Click(object sender, System.EventArgs e)
		{
			this.Close();
		}

		/// <summary>
		/// Loads a DLL for testing
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void _mnuLoad_Click(object sender, System.EventArgs e)
		{
			this._openFileDialog.Filter = "EXE files (*.exe)|*.exe|DLL files (*.dll)|*.dll|All files (*.*)|*.*" ;
			this._openFileDialog.ShowDialog();
			string testAssembly = this._openFileDialog.FileName;
			if ((testAssembly!=null) && (testAssembly.Length>0)) 
			{
				LoadAssembly(testAssembly);
			}
		}

		/// <summary>
		/// Loads an assembly of a given name
		/// </summary>
		/// <param name="testAssembly">The name of the file</param>
		private void LoadAssembly(string testAssembly)
		{
			try 
			{
				FindTests(testAssembly); 				
				this._statusBar.Text=testAssembly;
				_mruManager.Add( testAssembly);
				EnableTests(true) ;
			}
			catch(Exception ex)
			{
				// Not a valid assembly
				// remove file from MRU list
				EnableTests(false) ;
				_mruManager.Remove( testAssembly);
				this._statusBar.Text="Error loading assembly";
				System.Diagnostics.Trace.WriteLine(ex.ToString());
				MessageBox.Show(this,ex.ToString(),"Error",MessageBoxButtons.OK,MessageBoxIcon.Error);

			}
			
		}

		/// <summary>
		/// Manages the state of the buttons etc that allow tests to be run
		/// </summary>
		/// <param name="state"></param>
		private void EnableTests(bool state) 
		{
			_btnTest.Enabled=state;
			_mnuTest.Enabled=state;

		}

		/// <summary>
		/// Clear the state colours for all the tree
		/// </summary>
		private void ClearTreeNodeIcon() 
		{
			
			foreach(TreeNode subNode in this._treeView.Nodes) 
			{
				ClearNodeIcon(subNode);
			}

		}
		
		/// <summary>
		/// Clears the state colour for a node in the tree and its children
		/// </summary>
		/// <param name="node"></param>
		private void ClearNodeIcon(TreeNode node) 
		{
			this.SetAllNodeImages(node,GREY);
			foreach(TreeNode subNode in node.Nodes) 
			{
				ClearNodeIcon(subNode);
			}
		}

		/// <summary>
		/// Runs the test from the current node in the tree
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void _mnuRunSelectedTest_Click(object sender, System.EventArgs e)
		{

			SelectiveTestRun();
		}

		/// <summary>
		/// Runs the test from the current node in the tree
		/// </summary>
		private void SelectiveTestRun()
		{

			TreeNode selectedNode = this._treeView.SelectedNode;
			if (selectedNode!=null) 
			{
				ClearTreeNodeIcon() ;
				this._textSummary.Text="";
				_resultPanel.BackColor = Color.Gray;

				// are we a root node
				if (selectedNode.Tag.GetType() != typeof(TestDataStore))
				{
					this.RunAllTestsFromNode(selectedNode);
				} 
				else 
				{

					// we dynamically create object now, as we will need it
					object obj = Activator.CreateInstance(((TestDataStore)selectedNode.Tag).ContainingClassType);
					// show it to activate, to trigger the load event
					((Form)obj).Show();
					bool overallPass =RunATest(obj,selectedNode);

					// sort out the root icon
					if (overallPass ==true) 
					{
						SetAllNodeImages(selectedNode,GREEN);
						_resultPanel.BackColor = Color.LightGreen;
					} 
					else 
					{
						SetAllNodeImages(selectedNode,RED);
						_resultPanel.BackColor = Color.Red;
					}
			

					// dispose of the object
					((Form)obj).Close();
					obj=null;
				}

			} // end if
		
		}

		/// <summary>
		/// Runs the test from the current node in the tree if selected
		/// else the whole tree
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void _btnTest_Click(object sender, System.EventArgs e)
		{
			if (this._treeView.SelectedNode!=null) 
			{
				SelectiveTestRun();
			} 
			else 
			{
				RunAllTestsFromTree();
			}
		}

		/// <summary>
		/// Default load event
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void MainForm_Load(object sender, System.EventArgs e)
		{
			
			// disable the test buttons
			EnableTests(false) ;
			// sort out the recent files
			_mruManager = new MRUManager();
			_mruManager.Initialize(this, this._mnuMRU, @"Software\Black Marble\GuiTester" );
			_mruManager.MaxMRULength = 4; 
			// Optional:
			// mruManager.CurrentDir = ".....";        // default is current directory
			// mruManager.MaxMRULength = ...;          // default 10
			// mruMamager.MaxDisplayNameLength = ...;  // default 40
		}

		/// <summary>
		/// Called when a MRU menu items is selected
		/// </summary>
		/// <param name="fileName"></param>
		public void OpenMRUFile(string fileName)
		{
			LoadAssembly(fileName)	;		
		}

	} // end class

} // end namespace
