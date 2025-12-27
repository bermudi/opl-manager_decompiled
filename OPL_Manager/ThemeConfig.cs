using System;
using System.Xml.Serialization;

namespace OPL_Manager;

[Serializable]
[XmlInclude(typeof(ThmImg))]
public class ThemeConfig
{
	public string Name = "Default Theme";

	public string Author;

	public bool HasInfoPage;

	public int Width = 640;

	public int Height = 480;

	public bool Debug;

	public PageClass MainPage = new PageClass();

	public PageClass InfoPage = new PageClass();
}
