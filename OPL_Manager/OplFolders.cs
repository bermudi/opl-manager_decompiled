using System.IO;

namespace OPL_Manager;

public class OplFolders
{
	public static string Main { get; set; }

	public static string CD { get; set; }

	public static string DVD { get; set; }

	public static string CFG { get; set; }

	public static string CHT { get; set; }

	public static string ART { get; set; }

	public static string VMC { get; set; }

	public static string APPS { get; set; }

	private static string POPS { get; set; }

	public static void CreateFolders()
	{
		string[] array = new string[9] { Main, CD, DVD, CFG, CHT, ART, VMC, APPS, POPS };
		foreach (string path in array)
		{
			if (!Directory.Exists(path))
			{
				Directory.CreateDirectory(path);
			}
		}
	}

	public static void From(string OPL_Root)
	{
		if (!OPL_Root.EndsWith("\\") | OPL_Root.EndsWith("/"))
		{
			OPL_Root += "\\";
		}
		Main = OPL_Root;
		ART = Main + "ART\\";
		CD = Main + "CD\\";
		if (OplmSettings.ReadBool("CFG_DEV"))
		{
			CFG = Main + "CFG-DEV\\";
		}
		else
		{
			CFG = Main + "CFG\\";
		}
		CHT = Main + "CHT\\";
		DVD = Main + "DVD\\";
		VMC = Main + "VMC\\";
		POPS = Main + "POPS\\";
		APPS = Main + "APPS\\";
	}
}
