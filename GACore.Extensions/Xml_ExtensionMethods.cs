﻿using System.Xml;
using System.Xml.Linq;

namespace GACore.Extensions
{
	public static class Xml_ExtensionMethods
	{
		public static XmlDocument ToXmlDocument(this XDocument xDocument)
		{
			XmlDocument xmlDocument = new XmlDocument();
			using (XmlReader reader = xDocument.CreateReader())
			{
				xmlDocument.Load(reader);
			}
			return xmlDocument;
		}

		public static XDocument ToXDocument(this XmlDocument xmlDocument)
		{
			using (XmlNodeReader reader = new XmlNodeReader(xmlDocument))
			{
				reader.MoveToContent();
				return XDocument.Load(reader);
			}
		}
	}
}