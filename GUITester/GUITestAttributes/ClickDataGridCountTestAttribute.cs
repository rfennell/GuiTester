using System;
using System.Reflection;
using System.Diagnostics;
using System.Windows.Forms;
using System.Data;

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
	public sealed class ClickDataGridCountTestAttribute: GuiTestAttribute
	{

		/// <summary>
		/// The name of the label field we are interested looking at
		/// </summary>
		private string _targetLabel="";

		/// <summary>
		/// The result we looking for
		/// </summary>
		private int _rowsAfter=-1;

		/// <summary>
		/// The result we looking for
		/// </summary>
		private int _rowsBefore=-1;

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
		/// The number of rows before test
		/// </summary>
		public int RowCountBefore
		{
			get 
			{
				return this._rowsBefore;
			}
		}

		/// <summary>
		/// The number of rows after test
		/// </summary>
		public int RowCountAfter
		{
			get 
			{
				return this._rowsAfter;
			}
		}

		/// <summary>
		/// Sets the attribute properties via a constructor
		/// </summary>
		/// <param name="testLabel">The meaningful name for a test</param>
		/// <param name="targetLabel">The target label for the control to look for the result in</param>
		/// <param name="rowsAfter">The result to look in</param>
		public ClickDataGridCountTestAttribute(string testLabel, string targetLabel, int rowsAfter):base(testLabel)
		{
			_targetLabel = targetLabel;
			_rowsAfter = rowsAfter;
		}

		/// <summary>
		/// Sets the attribute properties via a constructor
		/// </summary>
		/// <param name="testLabel">The meaningful name for a test</param>
		/// <param name="targetLabel">The target label for the control to look for the result in</param>
		/// <param name="rowsBefore">The state before the click</param>
		/// <param name="rowsAfter">The result to look in</param>
		public ClickDataGridCountTestAttribute(string testLabel, string targetLabel, int rowsBefore, int rowsAfter):base(testLabel)
		{
			_targetLabel = targetLabel;
			_rowsAfter = rowsAfter;
			_rowsBefore = rowsBefore;
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
			DataGrid testControl = (DataGrid)FindControl((Form)obj,this._targetLabel);  // and look at a control, anything with a .Text property

			if (testControl==null) 
			{
				throw new TestFailedException("Cannot find target item of name [" + this._targetLabel + "]");
			}

			// do we have to check the state before?
			if (this._rowsBefore!=-1) 
			{
				if (((DataTable)testControl.DataSource)==null) 
				{
					// we have no data source so by defination there are no rows
					if (this._rowsBefore !=0) 
					{
						System.Diagnostics.Trace.WriteLineIf(this.TraceSwitch.Level >= TraceLevel.Verbose,"Before pressing [" + ((Control)testControl).Name + "] showed [0]");
						return false;
					}
				} 
				else 
				{
					if (this._rowsBefore!=((DataTable)testControl.DataSource).Rows.Count) 
					{
						System.Diagnostics.Trace.WriteLineIf(this.TraceSwitch.Level >= TraceLevel.Verbose,"Before pressing [" + ((Control)testControl).Name + "] showed [" + ((DataTable)testControl.DataSource).Rows+"]");
						return false;
					} // if failed
				}
			} // end if before test

			//do the click
			InvokeEventOnObject(obj,mInfo,"OnClick");

			System.Diagnostics.Trace.WriteLineIf(this.TraceSwitch.Level >= TraceLevel.Verbose,"After pressing [" + ((Control)testControl).Name + "] showed [" + ((DataTable)testControl.DataSource).Rows+"]");
			if (this._rowsAfter==((DataTable)testControl.DataSource).Rows.Count) 
			{
				return true;
			} 
			else 
			{
				return false;
			}
		}

	} // end class
} // end ns
