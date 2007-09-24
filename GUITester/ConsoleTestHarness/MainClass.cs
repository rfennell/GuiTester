using System;
using GuiTester.TestFramework;
using System.Windows.Forms;
using GuiTester.TestAttributes;

namespace GuiTester.ConsoleTestHarness
{
	/// <summary>
	/// The main class for the command line tools to exercise the GUITestHarness
	/// 
	/// www.blackmarble.co.uk
	/// Copyright Black Marble 2005 
	/// Author: Richard Fennell
	/// </summary>

	class MainClass
	{
		/// <summary>
		/// The main entry point for the application.
		/// </summary>
		[STAThread]
		static int Main(string[] args)
		{
			if (args.GetLength(0)!=1) 
			{
				System.Console.WriteLine("An assembly name (.EXE or .DLL) must be provided. \nUsage: ConsoleTestHarness myFile.dll");
				return (-1);
			} 
			else 
			{
				bool overallPass = true;
				if (System.IO.File.Exists(args[0])==false) 
				{
					System.Console.WriteLine(args[0] + " not found");
					overallPass = false; 
				} 
				else 
				{
					TestDataStore[] tests = TestControl.FindTests(args[0]);
					foreach (TestDataStore test in tests) 
					{
						// we dynamically create object now, as we will need it
						object obj = Activator.CreateInstance(test.ContainingClassType);
						// show it to activate, to trigger the load event
						((Form)obj).Show();

						bool result = ((GuiTestAttribute)test.TestAttribute).DoTest(obj,test.ControlInfo);

						if (result==false) 
						{
							overallPass = false;
						}
						System.Console.WriteLine ("Test [" + test.TestAttribute.ToString() + "] returned " + result );

						// dispose of the object
						((Form)obj).Close();
						obj=null;
					}
				}


				System.Console.WriteLine("Overall test for all classes returned " + overallPass );

					// sort out the root icon
				if (overallPass ==true) 
				{
					return (0);
				} 
				else 
				{
					return (1);
				}

			} // end if
		} // end methods
	} // end class
} // end namespace
