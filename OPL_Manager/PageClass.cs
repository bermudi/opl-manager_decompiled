using System;
using System.Collections.Generic;
using System.Drawing;
using System.Xml.Serialization;

namespace OPL_Manager;

[Serializable]
public class PageClass
{
	[XmlIgnore]
	public Color BgColor = Color.Black;

	[XmlArrayItem(typeof(ThmImg))]
	[XmlArrayItem(typeof(ThmText))]
	[XmlArrayItem(typeof(ThmGameList))]
	public List<object> Elements = new List<object>();

	[XmlElement("BgColor")]
	public string BgColorHtml
	{
		get
		{
			return ColorTranslator.ToHtml(BgColor);
		}
		set
		{
			BgColor = ColorTranslator.FromHtml(value);
		}
	}
}
