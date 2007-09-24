using System;
using System.Reflection;
using System.Diagnostics;
using System.Windows.Forms;
using System.Data;

namespace GuiTester.TestAttributes
{
	/// <summary>
	/// A test that is written in an nUnit style by the developer during 
	/// the development process.
	/// 
	/// www.blackmarble.co.uk
	/// Copyright Black Marble 2005 
	/// Author: Richard Fennell
	/// </summary>
	[AttributeUsage(AttributeTargets.Class, AllowMultiple=true)]
	public sealed class UserDefinedTestAttribute: GuiTestAttribute
	{
		private string _methodName;
		

		/// <summary>
		/// The name of the method with the signature public void XXX() to call
		/// </summary>
		/// <param name="testLabel">The name if the test</param>
		/// <param name="methodName">The name of the method e.g. XXX</param>
		public UserDefinedTestAttribute(string testLabel, string methodName ):base(testLabel)
		{
			_methodName=methodName;
		}

		/// <summary>
		/// Called to run this test
		/// </summary>
		/// <param name="obj">The instance of the application</param>
		/// <returns>True if the test is passed</returns>
		public override bool DoTest(object obj)
		{
			
			// we look for the method containing the test, we pass the type to avoid having to crea
			MethodInfo mi = obj.GetType().GetMethod(this._methodName);

			if (mi==null) 
			{
				throw new TestFailedException("Cannot find public user defined test method named [" + this._methodName + "]");
			}
			// check the return type
			if (mi.ReturnType!=typeof(bool))
			{
				throw new TestFailedException("Test method named [" + this._methodName + "] does not return a boolean");
			}
			// check the parameters
			System.Reflection.ParameterInfo[] parameters = mi.GetParameters();
			if (parameters.GetLength(0) >0) 
			{
				throw new TestFailedException("Test method named [" + this._methodName + "] should not take any parameters");
			}

			// and run it							
			return (bool)mi.Invoke (obj,null);
		}


	} // end class
} // end ns
