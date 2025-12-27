using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace OPL_Manager;

public class OplmSettings
{
	private static string filename = Program.MainFormInst.app_folder + "\\OPLManager.ini";

	private static Dictionary<string, string> config = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase);

	public static HashSet<string> ignoredGameList = new HashSet<string>();

	public static Dictionary<string, string> defaultSettings = new Dictionary<string, string>
	{
		{
			"CFG_DEV",
			false.ToString()
		},
		{
			"ISO_USE_OLD_NAMING_FORMAT",
			true.ToString()
		},
		{
			"ISO_FORCE_NAMING_FORMAT",
			true.ToString()
		},
		{ "HDL_VERSION", "hdl_dump_092.exe" }
	};

	public static Dictionary<string, string> forceSettings = new Dictionary<string, string>();

	public static void Write(string Name, string Val)
	{
		if (config.ContainsKey(Name))
		{
			config[Name] = Val;
		}
		else
		{
			config.Add(Name, Val);
		}
	}

	public static void Delete(string Name)
	{
		if (config.ContainsKey(Name))
		{
			config.Remove(Name);
		}
	}

	public static bool ReadBool(string Name)
	{
		if (forceSettings.ContainsKey(Name))
		{
			return Convert.ToBoolean(forceSettings[Name]);
		}
		if (config.ContainsKey(Name))
		{
			return Convert.ToBoolean(config[Name]);
		}
		return Convert.ToBoolean(defaultSettings[Name]);
	}

	public static string ReadString(string Name)
	{
		if (forceSettings.ContainsKey(Name))
		{
			return forceSettings[Name];
		}
		if (config.ContainsKey(Name))
		{
			return config[Name];
		}
		return defaultSettings[Name];
	}

	public static string Read(string Name, string predef)
	{
		if (config.ContainsKey(Name))
		{
			return config[Name];
		}
		return predef;
	}

	public static int Read(string Name, int predef)
	{
		if (config.ContainsKey(Name))
		{
			return int.Parse(config[Name]);
		}
		return predef;
	}

	public static bool Read(string Name, bool predef)
	{
		if (config.ContainsKey(Name))
		{
			return Convert.ToBoolean(config[Name]);
		}
		return predef;
	}

	public static void SaveFile()
	{
		string text = "";
		foreach (KeyValuePair<string, string> item in config)
		{
			if (!string.IsNullOrEmpty(item.Key.Trim()) && !string.IsNullOrEmpty(item.Value.Trim()))
			{
				text = text + item.Key + "=" + item.Value + Environment.NewLine;
			}
		}
		text = text + "BATCH_ART_SHARE_IGNORED_GAMES=" + string.Join(";", ignoredGameList);
		File.WriteAllText(filename, text);
	}

	public static void ReadFile()
	{
		if (!File.Exists(filename))
		{
			return;
		}
		string[] array = File.ReadAllText(filename).Split(Environment.NewLine);
		foreach (string text in array)
		{
			if (!text.StartsWith("[") && text.Contains("="))
			{
				string text2 = text.Substring(0, text.IndexOf("=")).Trim();
				string text3 = text.Substring(text.IndexOf("=") + 1).Trim();
				if (text2.ToUpper() == "BATCH_ART_SHARE_IGNORED_GAMES")
				{
					ReadIgnoredGames(text3);
				}
				else
				{
					config.Add(text2, text3);
				}
			}
		}
	}

	private static void ReadIgnoredGames(string line)
	{
		foreach (string item in (from x in line.Split(';')
			select x.Trim()).ToList())
		{
			if (!string.IsNullOrEmpty(item))
			{
				ignoredGameList.Add(item);
			}
		}
	}
}
