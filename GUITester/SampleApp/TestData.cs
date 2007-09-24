using System;

namespace GuiTester.SampleApp
{
	/// <summary>
	/// A simple class, a strucutre really that we will use to generate an XML file
	/// We can then load this into a Dataset, to simulate a database
	/// 
	/// www.blackmarble.co.uk
	/// Copyright Black Marble 2005 
	/// Author: Richard Fennell
	/// </summary>
	public class TestData
	{
		#region Members
		/// <summary>
		/// The first name of the customer
		/// </summary>
		private string _firstname;

		/// <summary>
		/// The surname of the customer
		/// </summary>
		private string _surname;

		/// <summary>
		/// Thier date of birth
		/// </summary>
		private DateTime _dob;
#endregion

		#region Properties
		/// <summary>
		/// The first name of the customer
		/// </summary>
		public string Firstname 
		{
			get 
			{
				return _firstname;
			}
			set 
			{
				this._firstname = value;
			}
		}

		/// <summary>
		/// The surname of the customer
		/// </summary>
		public string Surname 
		{
			get 
			{
				return _surname;
			}
			set 
			{
				this._surname = value;
			}
		}


		/// <summary>
		/// The date of birth of the customer
		/// </summary>
		public DateTime DOB
		{
			get 
			{
				return _dob;
			}
			set 
			{
				this._dob = value;
			}
		}
		#endregion

		#region Constructor
		/// <summary>
		/// We must have an empty constructor for XML serialisation to work
		/// </summary>
		public TestData()
		{
		}

		/// <summary>
		/// Main constructor used for data creation
		/// </summary>
		/// <param name="firstname">A customer first name</param>
		/// <param name="surname">A customer surname</param>
		/// <param name="dob">A date of birth</param>
		public TestData(string firstname,string surname,DateTime dob):this()
		{
			_firstname = firstname;
			_surname=surname;
			_dob=dob;
		}

		#endregion

	}
}
