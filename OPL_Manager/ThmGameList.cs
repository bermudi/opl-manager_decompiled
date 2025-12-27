using System;
using System.Drawing;
using System.Xml.Serialization;

namespace OPL_Manager;

[Serializable]
[XmlType(TypeName = "GameList")]
public class ThmGameList
{
	public int x;

	public int y;

	public int maxWidth;

	public int maxHeight;

	public int fontSize;

	public string text;

	public TextAlign align;

	[XmlIgnore]
	public Color color = Color.Black;

	[XmlIgnore]
	public Color colorSelected = Color.Black;

	[XmlElement("color")]
	public string colorHTML
	{
		get
		{
			return ColorTranslator.ToHtml(color);
		}
		set
		{
			color = ColorTranslator.FromHtml(value);
		}
	}

	[XmlElement("colorSelected")]
	public string colorSelectedHtml
	{
		get
		{
			return ColorTranslator.ToHtml(colorSelected);
		}
		set
		{
			colorSelected = ColorTranslator.FromHtml(value);
		}
	}
}
