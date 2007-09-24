using System;
using System.Reflection;
using GuiTester.TestAttributes;

namespace GuiTester.TestFramework
{
	/// <summary>
	/// Small helper class, little more than structure to store the relavent data for a 
	/// test within the treeview iteself
	/// 
	/// www.blackmarble.co.uk
	/// Copyright Black Marble 2005 
	/// Author: Richard Fennell
	/// </summary>
	public class TestDataStore : object , System.IComparable
	{
		private Type _containingClassType;
		private FieldInfo _controlInfo;
		private GuiTestAttribute _testAttribute;

		/// <summary>
		/// The name of the control under test
		/// </summary>
		public Type ContainingClassType 
		{
			get 
			{
				return _containingClassType;
			}
		}

		/// <summary>
		/// The name of the control under test
		/// </summary>
		public FieldInfo ControlInfo 
		{
			get 
			{
				return _controlInfo;
			}
		}

		/// <summary>
		/// The actual test details
		/// </summary>
		public GuiTestAttribute TestAttribute
		{
			get 
			{
				return _testAttribute;
			}
		}

		/// <summary>
		/// The constructor for field based tests
		/// </summary>
		/// <param name="containingClassType">The typeof object conatining this test</param>
		/// <param name="controlInfo">The control we are talking about</param>
		/// <param name="testAttribute">The attribute on that control</param>
		public TestDataStore (Type containingClassType,FieldInfo controlInfo,GuiTestAttribute testAttribute)
		{
			_containingClassType=containingClassType;
			_controlInfo = controlInfo;
			_testAttribute = testAttribute;
		}

		/// <summary>
		/// The constructor for class based test
		/// </summary>
		/// <param name="containingClassType">The typeof object conatining this test</param>
		/// <param name="testAttribute">The attribute on that control</param>
		public TestDataStore (Type containingClassType,GuiTestAttribute testAttribute)
		{
			_containingClassType=containingClassType;
			_controlInfo = null;
			_testAttribute = testAttribute;
		}

		/// <summary>
		/// Omitting Equals violates FxCop rule: IComparableImplementationsOverrideEquals.
		/// </summary>
		/// <param name="obj"></param>
		/// <returns></returns>
		public override bool Equals(object obj)
		{
			return (this.CompareTo(obj)== 0);

		}

		/// <summary>
		/// Omitting getHashCode violates FxCop rule: EqualsOverridesRequireGetHashCodeOverride.
     	/// </summary>
		/// <returns></returns>
		public override int GetHashCode()
		{
			return _containingClassType.GetHashCode () ^ this._controlInfo.GetHashCode() ^ this._testAttribute.GetHashCode();
		}


		/// <summary>
		/// Omitting any of the following operator overloads
		/// violates FxCop rule: IComparableImplementationsOverrideOperators.
		/// </summary>
		/// <param name="t1"></param>
		/// <param name="t2"></param>
		/// <returns></returns>
		public static bool operator == (TestDataStore t1, TestDataStore t2)
		{
			return t1.Equals(t2);
		}  

		/// <summary>
		/// Omitting any of the following operator overloads
		/// violates FxCop rule: IComparableImplementationsOverrideOperators.
		/// </summary>
		/// <param name="t1"></param>
		/// <param name="t2"></param>
		/// <returns></returns>
		public static bool operator != (TestDataStore t1, TestDataStore t2)
		{
			return !(t1==t2);
		}  
		
		/// <summary>
		/// Omitting any of the following operator overloads
		/// violates FxCop rule: IComparableImplementationsOverrideOperators.
		/// </summary>
		/// <param name="t1"></param>
		/// <param name="t2"></param>
		/// <returns></returns>
		public static bool operator < (TestDataStore t1, TestDataStore t2)
		{
			return (t1.CompareTo(t2) < 0);
		}  
		
		/// <summary>
		/// Omitting any of the following operator overloads
		/// violates FxCop rule: IComparableImplementationsOverrideOperators.
		/// </summary>
		/// <param name="t1"></param>
		/// <param name="t2"></param>
		/// <returns></returns>
		public static bool operator > (TestDataStore t1, TestDataStore t2)
		{
			return (t1.CompareTo(t2) > 0);
		}  

		/// <summary>
		/// The default sort on the test name
		/// </summary>
		/// <param name="obj"></param>
		/// <returns></returns>
		public int CompareTo(object obj)
		{

			return this._testAttribute.TestName.CompareTo(((TestDataStore)obj)._testAttribute.TestName);
		}

	} // end class
} // ns
