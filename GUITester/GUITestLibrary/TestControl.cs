using System;
using System.Reflection;
using System.IO;
using System.Diagnostics;
using GuiTester.TestAttributes;

namespace GuiTester.TestFramework
{
	/// <summary>
	/// A collection of static methods to manage the loading of tests from assemblies
	/// 
	/// www.blackmarble.co.uk
	/// Copyright Black Marble 2005 
	/// Author: Richard Fennell
	/// </summary>
	public sealed class TestControl
	{
		/// <summary>
		/// Flag for debug logging
		/// </summary>
		private static readonly System.Diagnostics.TraceSwitch _traceSwitch = new System.Diagnostics.TraceSwitch("General", "Entire Application");

		/// <summary>
		/// Empty construtor as can never be called
		/// </summary>
		private TestControl(){}

		/// <summary>
		/// Loads an assembly, a DLL or EXE to test
		/// </summary>
		/// <param name="assemblyName">The name of the assembly</param>
		private static Assembly LoadAssembly(string assemblyName) 
		{
			return LoadAssembly(assemblyName,assemblyName.Substring(0,assemblyName.LastIndexOf(".")) + ".pdb");
		}

		/// <summary>
		/// Loads an assembly, a DLL or EXE to test
		/// </summary>
		/// <param name="assemblyName">The name of the assembly</param>
		/// <param name="symbolsName">The name of an associated PDB file of debug information, if this is not present no debugging is possible</param>
		/// <returns></returns>
		private static Assembly LoadAssembly(string assemblyName, string symbolsName) 
		{
			Assembly asm;
			FileStream fs;
			byte[] assemblyStream=null;
			byte[] symbolsStream=null;

			// open the DLL or EXE using a memory byte stream, if we use a direct open we get a lock
			// on the file that we cannot release without cycling the the application domain, which is impossible
			// if the use the default (i.e. the one we are in) and loads of work to create another on the fly
			// this is quicker
			if (File.Exists(assemblyName)==true) 
			{
				fs = new FileStream(assemblyName,FileMode.Open);
				assemblyStream = new byte[fs.Length];
				fs.Read(assemblyStream,0,(int)fs.Length);
				fs.Close();
			} 
			else 
			{
				System.Diagnostics.Trace.WriteLineIf(_traceSwitch.Level >= TraceLevel.Error,"Main assembly file " + assemblyName + " does not exist");
				return null;
			}

			if ((symbolsName.Length>0) && (File.Exists(symbolsName)==true)) 
			{
				fs = new FileStream(symbolsName,FileMode.Open);
				symbolsStream = new byte[fs.Length];
				fs.Read(symbolsStream,0,(int)fs.Length);
				fs.Close();
			}

			if (symbolsStream!=null) 
			{
				asm = Assembly.Load(assemblyStream,symbolsStream);
			} 
			else 
			{
				asm = Assembly.Load(assemblyStream);
			}
			return asm;
		
		} // end method


		/// <summary>
		/// Scans an assembly and retuns a holder full of the tests avaialble
		/// </summary>
		/// <param name="assemblyName"></param>
		/// <returns></returns>
		public static TestDataStore[] FindTests(string assemblyName) 
		{

			Assembly asm = TestControl.LoadAssembly( assemblyName); 
			// get the types in the assmembly
			Type[] types = asm.GetTypes();
			System.Collections.ArrayList testsCollection = new System.Collections.ArrayList();

			// check each type, in turn to see if it has our custom attribute
			System.Diagnostics.Trace.WriteLineIf(_traceSwitch.Level >= TraceLevel.Verbose,"The type(s) in the assembly " + assemblyName + " are:");
			foreach ( Type type in types )
			{
				System.Diagnostics.Trace.WriteLineIf(_traceSwitch.Level >= TraceLevel.Verbose,type.ToString());
				// look for our attributes
				Object[] classAttributes= type.GetCustomAttributes(typeof(GuiTestableAttribute),false);
				// this attribute if present can only be present once
				if ((classAttributes!=null) && (classAttributes.GetLength(0)>0))
				{
					// we know we have one of our testable classes
					// check to see if there are class wide tests
					Object[] classWideAttributes= type.GetCustomAttributes(typeof(GuiTestAttribute),true);
					foreach (GuiTestAttribute test in classWideAttributes) 
					{
						System.Diagnostics.Trace.WriteLineIf(_traceSwitch.Level >= TraceLevel.Verbose,"We found a class wide test of  type  " + test.GetType().ToString());
						testsCollection.Add( new TestDataStore(type,test));
					}

					// look for all the fields on the class
					foreach (FieldInfo mInfo in type.GetFields(BindingFlags.Instance|BindingFlags.NonPublic)) 
					{
						// look for the Gui items marked with our atributes e.g. buttons we have marked for testing via reflection
						foreach (object customAttrib in mInfo.GetCustomAttributes(typeof(GuiTestAttribute),true))
						{
							// we found one we need to test
							System.Diagnostics.Trace.WriteLineIf(_traceSwitch.Level >= TraceLevel.Verbose,"We found the testable control " + mInfo.Name + " and it allows " + customAttrib.GetType().ToString());
							testsCollection.Add( new TestDataStore(type,mInfo,(GuiTestAttribute)customAttrib));
								
						} // end check for attributes
					}  // end check the fields
				}
			} // end check the types
			// put them in alphabetical order
			testsCollection.Sort();
			// return a fixed array
			return (TestDataStore[])testsCollection.ToArray(typeof(TestDataStore));
		}



	} // end class
} // end ns
