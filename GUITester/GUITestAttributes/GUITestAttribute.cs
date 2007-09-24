using System;
using System.Reflection;
using System.Windows.Forms;

namespace GuiTester.TestAttributes
{
	/// <summary>
	/// The base class for all test attributes in the GUITestHarness
	/// 
	/// www.blackmarble.co.uk
	/// Copyright Black Marble 2005 
	/// Author: Richard Fennell
	/// </summary>
	public abstract class GuiTestAttribute: Attribute
	{

		/// <summary>
		/// Flag for debug logging
		/// </summary>
		private static readonly System.Diagnostics.TraceSwitch _traceSwitch = new System.Diagnostics.TraceSwitch("General", "Entire Application");


		/// <summary>
		/// The name for the test
		/// </summary>
		private string _testName;

		/// <summary>
		/// Constructor, as abstract never called
		/// </summary>
		/// <param name="label">A name for the test</param>
		protected GuiTestAttribute(string label)
		{
			_testName=label;
		}

		/// <summary>
		/// The current applciations trace setting
		/// </summary>
		protected System.Diagnostics.TraceSwitch TraceSwitch
		{
			get 
			{
				return _traceSwitch ;
			}
		}


		/// <summary>
		/// The name for the test
		/// </summary>
		public string TestName
		{
			get 
			{
				return this._testName;
			}
		}

		/// <summary>
		/// The name for the test
		/// </summary>
		public override string ToString()
		{
			return this.TestName;
		}

		/// <summary>
		/// Called to run this test on a control
		/// </summary>
		/// <param name="obj">The instance of the application</param>
		/// <param name="fieldInfo">The item to test</param>
		/// <returns>True if the test is passed</returns>
		public virtual bool DoTest(object obj,FieldInfo fieldInfo)
		{
			return false;
		}

		/// <summary>
		/// Called to run this test on a class
		/// </summary>
		/// <param name="obj">The instance of the application</param>
		/// <returns>True if the test is passed</returns>
		public virtual bool DoTest(object obj)
		{
			return false;
		}


		/// <summary>
		/// Finds a control on the given form
		/// </summary>
		/// <param name="form">The form</param>
		/// <param name="name">The name of the control instance</param>
		/// <returns></returns>
		protected Control FindControl (Form form,string name) 
		{
			return FindControl(form.Controls,name);
		}

		/// <summary>
		/// Finds a control on the given control collection e.g myForm.Controls
		/// This can be used recursivally to find things in tab controls or groupboxes
		/// </summary>
		/// <param name="collection">The collection</param>
		/// <param name="name">The name of the conrol instance</param>
		/// <returns></returns>
		protected Control FindControl (System.Windows.Forms.Control.ControlCollection collection, string name) 
		{
			foreach (Control control in collection) 
			{
				// you would have expected you could get the field
				// the attribute is linked to directly, but it seems 
				// you cannot, we have to pass in the instance from
				// object
				if (control.Name==name) 
				{
					return (Control)control;
				} // end if
				// Ok we did not find it, but it is possible there is a sub item
				Control retValue=null;
				// use  a switch as I expect others to be added to the list
				// assume the compiler will optimise this out
				switch (control.GetType().ToString()) 
				{
					case "System.Windows.Forms.GroupBox":
						retValue = FindControl(control.Controls,name);
						if (retValue !=null) 
						{
							return retValue;
						}
						break;
				} // end switch
			} // end for
			return null;
		} // end method


		/// <summary>
		/// Find an object and tries to click it
		/// </summary>
		/// <param name="obj">The applicaton object</param>
		/// <param name="fieldInfo">Info on the item to run the event on</param>
		/// <param name="eventName">The name of the event e.g. OnClick</param>
		protected void InvokeEventOnObject (object obj,FieldInfo fieldInfo, string eventName)
		{
			// we cannot use a [Control] as it might be a MenuItem which is not in this branch of the object tree
			// not this might not be a button now
			object testButton = fieldInfo.GetValue(obj);
			// it is for this reason we don't use the following form, as we cannot find MenuItems
			// and the fact this is a far neater solution!
			// 1. because they are not in the right bit of the tree
			// 2. more important they don't have a runtime name to match
			//object testButton = FindControl((Form)obj,mInfo.Name); // we know we press a button

			// saftey check
			if (testButton==null) 
			{
				throw new TestFailedException("Cannot find item to [Click] of name [" + fieldInfo.Name + "]");
			}

			// we found it so create a handle to the method
			MethodInfo mi = fieldInfo.FieldType.GetMethod(eventName,BindingFlags.Instance|BindingFlags.NonPublic);
			// and run it							
			mi.Invoke(testButton,new object[]{new EventArgs()});
		}

		
	} // end class
} // end ns
