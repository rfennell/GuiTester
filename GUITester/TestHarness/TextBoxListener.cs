	using System;

	namespace bm.utils
	{
		/// <summary>
		/// A helper class to provide a custom trace listerner
		/// 
		/// www.blackmarble.co.uk
		/// Copyright Black Marble 2005 
		/// Author: Richard Fennell
		/// </summary>
		internal class TextBoxListener : System.Diagnostics.TraceListener 
		{
			System.Windows.Forms.TextBox _textBox ;

			public TextBoxListener (System.Windows.Forms.TextBox textBox) 
			{
				_textBox = textBox;
			}

			/// <summary>
			/// Writes a line, appending a line feed
			/// </summary>
			/// <param name="message">The message</param>
			public override void Write (string message) 
			{
				try 
				{
					_textBox.AppendText (message.Replace("\n","\r\n"));
				} 
				catch 
				{
				}

			}

			/// <summary>
			/// Writes a line
			/// </summary>
			/// <param name="message">The message</param>
			public override void WriteLine(string message )
			{
				Write (message + "\n");
			}

		}
	}
