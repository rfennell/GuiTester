using System;
using System.Xml;  
using System.Configuration;
using System.Reflection;

namespace GuiTester.SampleApp
{
/// <summary>
/// A set of helper functions that allow the sample application to 
/// read from it APP.CONGIG file even if loaded as an assembly 
/// in the test harness
/// </summary>
	public class ConfigSettings
	{
		/// <summary>
		/// Constructor
		/// </summary>
		private ConfigSettings() {}

		/// <summary>
		/// Loads a configuration document
		/// </summary>
		/// <param name="path">File path</param>
		/// <returns>The XML document</returns>
		private static XmlDocument LoadConfigDocument(string path)
		{
			XmlDocument doc = null;
			try
			{
				doc = new XmlDocument();
				doc.Load(path);
				return doc;
			}
			catch (System.IO.FileNotFoundException e)
			{
				throw new Exception("No configuration file found.", e);
			}
		}

		/// <summary>
		/// Reads the given appsettings key
		/// </summary>
		/// <param name="key">The key name</param>
		/// <returns>The value</returns>
		public static string GetConfigSetting(string key) 
		{
			if (System.Configuration.ConfigurationSettings.AppSettings[key]!=null)
			{
				return System.Configuration.ConfigurationSettings.AppSettings[key];
			} 
			else 
			{
				// would be nice to find this automatically, but all accessing of the assembly information
				// returns stuff on the calling assembly not the containing one
                string path = @"C:\projects\GUITester\SampleApp\bin\Debug\SampleApp.exe.config";

				XmlDocument doc = LoadConfigDocument(path);

				// retrieve appSettings node
				XmlNode node =  doc.SelectSingleNode("//appSettings");

				if (node != null) 
				{
					try
					{
						// select the 'add' element that contains the key
						XmlElement elem = (XmlElement)node.SelectSingleNode(string.Format("//add[@key='{0}']", key));

						if (elem != null)
						{
							// add value for key
							return elem.GetAttribute("value");
						}
					}
					catch{}
				}
				return "";
			}
		}
	}
} // ns