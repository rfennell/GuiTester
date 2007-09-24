using System;
using System.Reflection;

namespace GuiTester.TestAttributes
{
	/// <summary>
	/// The attribute that marks a class as testable by the GUITestHarness
	/// 
	/// www.blackmarble.co.uk
	/// Copyright Black Marble 2005 
	/// Author: Richard Fennell
	/// </summary>
	[AttributeUsage(AttributeTargets.Class, AllowMultiple=false)]
	public sealed class GuiTestableAttribute: Attribute
	{
		/// <summary>
		/// The default constructor
		/// </summary>
		public GuiTestableAttribute()
		{
		
		}



	} // end class
} // end ns
