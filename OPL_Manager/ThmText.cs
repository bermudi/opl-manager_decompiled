using System;
using System.Drawing;
using System.Xml.Serialization;

namespace OPL_Manager;

[Serializable]
[XmlType(TypeName = "Text")]
public class ThmText
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
}
