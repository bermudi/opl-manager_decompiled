using System;
using System.Xml.Serialization;

namespace OPL_Manager;

[Serializable]
[XmlType(TypeName = "Image")]
public class ThmImg
{
	[XmlAttribute]
	public ImageType type;

	public int x;

	public int y;

	public int widthX;

	public int heightY;

	public string source;

	public string fallback;

	public ImageOrigin origin;
}
