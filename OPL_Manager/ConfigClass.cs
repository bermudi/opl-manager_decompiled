using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using OPL_Manager.My.Resources;

namespace OPL_Manager;

public class ConfigClass
{
	private string CFGFile;

	private List<string> CFG_Text = new List<string>();

	private TextBox LeTextBox;

	private bool unsavedChanges;

	private const int CFG_Version = 8;

	public static Dictionary<string, List<GameRatingClass>> StaticRatings = new Dictionary<string, List<GameRatingClass>>
	{
		{
			"BBFC",
			new List<GameRatingClass>
			{
				new GameRatingClass("", "", Resources.NoRating),
				new GameRatingClass("PG", "bbfc/pg", Resources.BBFC_PG),
				new GameRatingClass("U", "bbfc/u", Resources.BBFC_U),
				new GameRatingClass("12", "bbfc/12", Resources.BBFC_12),
				new GameRatingClass("15", "bbfc/15", Resources.BBFC_15),
				new GameRatingClass("18", "bbfc/18", Resources.BBFC_18)
			}
		},
		{
			"CERO",
			new List<GameRatingClass>
			{
				new GameRatingClass("", "", Resources.NoRating),
				new GameRatingClass("A", "cero/a", Resources.CERO_A),
				new GameRatingClass("B", "cero/b", Resources.CERO_B),
				new GameRatingClass("C", "cero/c", Resources.CERO_C),
				new GameRatingClass("D", "cero/d", Resources.CERO_D),
				new GameRatingClass("Z", "cero/z", Resources.CERO_Z),
				new GameRatingClass("Demo", "cero/demo", Resources.CERO_demo),
				new GameRatingClass("Pending", "cero/pending", Resources.CERO_Pending)
			}
		},
		{
			"DEJUS",
			new List<GameRatingClass>
			{
				new GameRatingClass("", "", Resources.NoRating),
				new GameRatingClass("L", "dejus/l", Resources.DEJUS_L),
				new GameRatingClass("10", "dejus/10", Resources.DEJUS_10),
				new GameRatingClass("12", "dejus/12", Resources.DEJUS_12),
				new GameRatingClass("14", "dejus/14", Resources.DEJUS_14),
				new GameRatingClass("16", "dejus/16", Resources.DEJUS_16),
				new GameRatingClass("18", "dejus/18", Resources.DEJUS_18)
			}
		},
		{
			"ELSPA",
			new List<GameRatingClass>
			{
				new GameRatingClass("", "", Resources.NoRating),
				new GameRatingClass("3", "elspa/3", Resources.ELSPA_3),
				new GameRatingClass("3a", "elspa/3a", Resources.ELSPA_3a),
				new GameRatingClass("11", "elspa/11", Resources.ELSPA_11),
				new GameRatingClass("11a", "elspa/11a", Resources.ELSPA_11a),
				new GameRatingClass("15", "elspa/15", Resources.ELSPA_15),
				new GameRatingClass("15a", "elspa/15a", Resources.ELSPA_15a),
				new GameRatingClass("18", "elspa/18", Resources.ELSPA_18),
				new GameRatingClass("18a", "elspa/18a", Resources.ELSPA_18a)
			}
		},
		{
			"ESRA",
			new List<GameRatingClass>
			{
				new GameRatingClass("", "", Resources.NoRating),
				new GameRatingClass("3", "esra/3", Resources.ESRA_3),
				new GameRatingClass("7", "esra/7", Resources.ESRA_7),
				new GameRatingClass("12", "esra/12", Resources.ESRA_12),
				new GameRatingClass("15", "esra/15", Resources.ESRA_15),
				new GameRatingClass("18", "esra/18", Resources.ESRA_18)
			}
		},
		{
			"ESRB",
			new List<GameRatingClass>
			{
				new GameRatingClass("", "", Resources.ESRB_NoRating),
				new GameRatingClass("C", "esrb/3", Resources.ESRB_EarlyChildhood),
				new GameRatingClass("E", "esrb/e", Resources.ESRB_Everyone),
				new GameRatingClass("E+", "esrb/10", Resources.ESRB_Everyone10),
				new GameRatingClass("T", "esrb/teen", Resources.ESRB_Teen),
				new GameRatingClass("M", "esrb/17", Resources.ESRB_Mature),
				new GameRatingClass("A", "esrb/18", Resources.ESRB_Adults)
			}
		},
		{
			"OFLC",
			new List<GameRatingClass>
			{
				new GameRatingClass("", "", Resources.NoRating),
				new GameRatingClass("E", "ofcl/e", Resources.OFLC_E),
				new GameRatingClass("G", "ofcl/g", Resources.OFLC_G),
				new GameRatingClass("PG", "ofcl/pg", Resources.OFLC_PG),
				new GameRatingClass("M", "ofcl/m", Resources.OFLC_M),
				new GameRatingClass("MA", "ofcl/ma", Resources.OFLC_MA),
				new GameRatingClass("R", "ofcl/r", Resources.OFLC_R),
				new GameRatingClass("X", "ofcl/x", Resources.OFLC_X)
			}
		},
		{
			"PEGI",
			new List<GameRatingClass>
			{
				new GameRatingClass("", "", Resources.PEGI_0),
				new GameRatingClass("3", "pegi/3", Resources.PEGI_3),
				new GameRatingClass("7", "pegi/7", Resources.PEGI_7),
				new GameRatingClass("12", "pegi/12", Resources.PEGI_12),
				new GameRatingClass("16", "pegi/16", Resources.PEGI_16),
				new GameRatingClass("18", "pegi/18", Resources.PEGI_18)
			}
		},
		{
			"USK",
			new List<GameRatingClass>
			{
				new GameRatingClass("", "", Resources.USK_No),
				new GameRatingClass("0", "usk/0", Resources.USK_0),
				new GameRatingClass("6", "usk/6", Resources.USK_6),
				new GameRatingClass("12", "usk/12", Resources.USK_12),
				new GameRatingClass("16", "usk/16", Resources.USK_16),
				new GameRatingClass("18", "usk/18", Resources.USK_18)
			}
		}
	};

	public static List<ComboCfgDescValTextClass> StaticAspect = new List<ComboCfgDescValTextClass>
	{
		new ComboCfgDescValTextClass("", "", Translated.OpsCfgEditor_ComboNotSetUnknown),
		new ComboCfgDescValTextClass("4:3", "aspect/s", "Standard - (4:3)"),
		new ComboCfgDescValTextClass("16:9", "aspect/w", "Widescreen - (16:9)"),
		new ComboCfgDescValTextClass("16:9 (ps2rd)", "aspect/w1", "Widescreen - (ps2rd Hack)"),
		new ComboCfgDescValTextClass("16:9 (HexISO)", "aspect/w2", "Widescreen - (HEX ISO Hack)")
	};

	public static List<ComboCfgDescValTextClass> StaticPlayers = new List<ComboCfgDescValTextClass>
	{
		new ComboCfgDescValTextClass("", "", Translated.OpsCfgEditor_ComboNotSet),
		new ComboCfgDescValTextClass("1", "players/1", Translated.OpsCfgEditor_Players1),
		new ComboCfgDescValTextClass("2", "players/2", Translated.OpsCfgEditor_Players2),
		new ComboCfgDescValTextClass("3", "players/3", Translated.OpsCfgEditor_Players3),
		new ComboCfgDescValTextClass("4", "players/4", Translated.OpsCfgEditor_Players4)
	};

	public static List<ComboCfgDescValTextClass> StaticScan = new List<ComboCfgDescValTextClass>
	{
		new ComboCfgDescValTextClass("", "", Translated.OpsCfgEditor_ComboNotSetUnknown),
		new ComboCfgDescValTextClass("240p", "scan/240p", Translated.OpsCfgEditor_Scan_240pDefault),
		new ComboCfgDescValTextClass("240p (HexISO)", "scan/240p1", Translated.OpsCfgEditor_Scan_240pHex),
		new ComboCfgDescValTextClass("480i", "scan/480i", Translated.OpsCfgEditor_Scan_480iNTSC),
		new ComboCfgDescValTextClass("480p", "scan/480p", Translated.OpsCfgEditor_Scan_480pGame),
		new ComboCfgDescValTextClass("480p (Triangle + Cross)", "scan/480p1", Translated.OpsCfgEditor_Scan_480pTriangleCross),
		new ComboCfgDescValTextClass("480p (Circle + Cross)", "scan/480p2", Translated.OpsCfgEditor_Scan_480pCircleCross),
		new ComboCfgDescValTextClass("480p (GSM)", "scan/480p3", Translated.OpsCfgEditor_Scan_480pGSM),
		new ComboCfgDescValTextClass("480p (ps2rd)", "scan/480p4", Translated.OpsCfgEditor_Scan_480pPs2rd),
		new ComboCfgDescValTextClass("480p (HexISO)", "scan/480p5", Translated.OpsCfgEditor_Scan_480pHex),
		new ComboCfgDescValTextClass("576i", "scan/576i", Translated.OpsCfgEditor_Scan_576iPAL),
		new ComboCfgDescValTextClass("576p (GSM)", "scan/576p", Translated.OpsCfgEditor_Scan_576pGSM),
		new ComboCfgDescValTextClass("720p (GSM)", "scan/720p", Translated.OpsCfgEditor_Scan_720pGSM),
		new ComboCfgDescValTextClass("1080i", "scan/1080i", Translated.OpsCfgEditor_Scan_1080iGame),
		new ComboCfgDescValTextClass("1080i (GSM)", "scan/1080i2", Translated.OpsCfgEditor_Scan_1080iGSM),
		new ComboCfgDescValTextClass("1080p (GSM)", "scan/1080p", Translated.OpsCfgEditor_Scan_1080pGSM)
	};

	public static List<ComboCfgDescValTextClass> StaticVmode = new List<ComboCfgDescValTextClass>
	{
		new ComboCfgDescValTextClass("", "", Translated.OpsCfgEditor_ComboNotSetUnknown),
		new ComboCfgDescValTextClass("PAL", "vmode/pal", "PAL"),
		new ComboCfgDescValTextClass("NTSC", "vmode/ntsc", "NTSC"),
		new ComboCfgDescValTextClass("MULTI", "vmode/multi", "MULTI")
	};

	public static List<ComboCfgDescValTextClass> StaticDma = new List<ComboCfgDescValTextClass>
	{
		new ComboCfgDescValTextClass("", "0", "MDMA 0"),
		new ComboCfgDescValTextClass("", "1", "MDMA 1"),
		new ComboCfgDescValTextClass("", "2", "MDMA 2"),
		new ComboCfgDescValTextClass("", "3", "UDMA 0"),
		new ComboCfgDescValTextClass("", "4", "UDMA 1"),
		new ComboCfgDescValTextClass("", "5", "UDMA 2"),
		new ComboCfgDescValTextClass("", "6", "UDMA 3"),
		new ComboCfgDescValTextClass("", "", "UDMA 4")
	};

	public static List<ComboCfgDescValTextClass> StaticGsm = new List<ComboCfgDescValTextClass>
	{
		new ComboCfgDescValTextClass("", "", "NTSC"),
		new ComboCfgDescValTextClass("", "1", "NTSC Non Interlaced"),
		new ComboCfgDescValTextClass("", "2", "PAL"),
		new ComboCfgDescValTextClass("", "3", "PAL Non Interlaced"),
		new ComboCfgDescValTextClass("", "4", "PAL @60hz"),
		new ComboCfgDescValTextClass("", "5", "PAL @60hz Non Interlaced"),
		new ComboCfgDescValTextClass("", "6", "PS1 NTSC (HDTV 480p @60hz)"),
		new ComboCfgDescValTextClass("", "7", "PS1 PAL (HDTV 576p @50hz)"),
		new ComboCfgDescValTextClass("", "8", "HDTV 480p @60hz"),
		new ComboCfgDescValTextClass("", "9", "HDTV 576p @50hz"),
		new ComboCfgDescValTextClass("", "10", "HDTV 720p @60hz"),
		new ComboCfgDescValTextClass("", "11", "HDTV 1080i @60hz"),
		new ComboCfgDescValTextClass("", "12", "HDTV 1080i @60hz Non Interlaced"),
		new ComboCfgDescValTextClass("", "13", "HDTV 1080p @60hz"),
		new ComboCfgDescValTextClass("", "14", "VGA 640x480p @60hz"),
		new ComboCfgDescValTextClass("", "15", "VGA 640x480p @72hz"),
		new ComboCfgDescValTextClass("", "16", "VGA 640x480p @75hz"),
		new ComboCfgDescValTextClass("", "17", "VGA 640x480p @85hz"),
		new ComboCfgDescValTextClass("", "18", "VGA 640x480i @60hz")
	};

	public static Dictionary<string, string> StaticDevice = new Dictionary<string, string>
	{
		{ "device/1", "USB" },
		{ "device/2", "USB + ETH" },
		{ "device/3", "USB + HDD" },
		{ "device/4", "ETH + HDD" },
		{ "device/5", "ETH" },
		{ "device/6", "HDD" },
		{ "device/all", "USB + ETH + HDD" }
	};

	public static Dictionary<string, string> StaticRating = new Dictionary<string, string>
	{
		{ "rating/1", "1" },
		{ "rating/2", "2" },
		{ "rating/3", "3" },
		{ "rating/4", "4" },
		{ "rating/5", "5" }
	};

	public bool Changed => unsavedChanges;

	private static T InlineAssignHelper<T>(ref T target, T value)
	{
		target = value;
		return value;
	}

	public ConfigClass(string _CFGFile, bool skipCheck = false, [Optional] ref TextBox textboxxx)
	{
		CFGFile = _CFGFile;
		LeTextBox = textboxxx;
		string target = "";
		if (!File.Exists(CFGFile))
		{
			target = "CfgVersion=" + 8;
		}
		else
		{
			StreamReader streamReader = new StreamReader(CFGFile);
			while (InlineAssignHelper(ref target, streamReader.ReadLine()) != null)
			{
				if (!string.IsNullOrEmpty(target.Trim()))
				{
					CFG_Text.Add(target.Trim());
				}
			}
			streamReader.Close();
		}
		if (skipCheck)
		{
			CheckVersion();
		}
		TextBoxDump();
	}

	public string CheckVersion()
	{
		string text = "";
		List<KeyValuePair<TwoStringPair, List<string>>> list = new List<KeyValuePair<TwoStringPair, List<string>>>();
		list.Add(new KeyValuePair<TwoStringPair, List<string>>(new TwoStringPair("Aspect=", "aspect/"), new List<string> { "s", "w", "w1", "w2" }));
		list.Add(new KeyValuePair<TwoStringPair, List<string>>(new TwoStringPair("Device=", "device/"), new List<string> { "1", "2", "3", "4", "5", "6", "All" }));
		list.Add(new KeyValuePair<TwoStringPair, List<string>>(new TwoStringPair("Players=", "players/"), new List<string> { "1", "2", "3", "4" }));
		list.Add(new KeyValuePair<TwoStringPair, List<string>>(new TwoStringPair("Rating=", "rating/"), new List<string> { "0", "1", "2", "3", "4", "5" }));
		list.Add(new KeyValuePair<TwoStringPair, List<string>>(new TwoStringPair("Scan=", "scan/"), new List<string> { "480i", "480p", "480p1", "480p2", "576i", "1080i", "1080p" }));
		list.Add(new KeyValuePair<TwoStringPair, List<string>>(new TwoStringPair("Compatibility=", "device/"), new List<string> { "1", "2", "3", "4", "5", "6", "all" }));
		list.Add(new KeyValuePair<TwoStringPair, List<string>>(new TwoStringPair("Esrb=", "esrb/"), new List<string> { "everyone", "3", "10", "teen", "17", "18" }));
		list.Add(new KeyValuePair<TwoStringPair, List<string>>(new TwoStringPair("Region=", "region/"), new List<string> { "ntsc", "pal", "multi" }));
		bool flag = false;
		if ((GetValue("CfgVersion=").ToLower() ?? "") == (8.ToString() ?? ""))
		{
			return text + Translated.ConfigIO_AlreadyLastVersion + " V." + 8;
		}
		if (string.IsNullOrEmpty(GetValue("CfgVersion=").ToLower()))
		{
			text = text + Translated.ConfigIO_VersionMissing + Environment.NewLine;
			flag = true;
			foreach (KeyValuePair<TwoStringPair, List<string>> item in list)
			{
				string text2 = GetValue(item.Key.String1).Trim();
				string @string = item.Key.String1;
				if (!string.IsNullOrEmpty(text2))
				{
					foreach (string item2 in item.Value)
					{
						if (!((text2.ToLower() ?? "") == (item2.ToLower() ?? "")))
						{
							continue;
						}
						if (@string.ToLower() == "compatibility=")
						{
							text = text + Translated.ConfigIO_Upgrading + " Compatibility => Device  (" + @string + text2 + ") => (Device=" + item.Key.String2 + text2 + ")" + Environment.NewLine;
							SetValueInternal(@string, "");
							SetValueInternal("Device=", item.Key.String2 + item2);
						}
						else if (@string.ToLower() == "esrb=")
						{
							string text3 = item2;
							if (text3.Trim().ToLower() == "everyone")
							{
								text3 = "e";
							}
							text = text + Translated.ConfigIO_Upgrading + " ESRB => Parental (" + @string + text2 + ") => (Parental=" + item.Key.String2 + text3 + ")" + Environment.NewLine;
							SetValueInternal(@string, "");
							SetValueInternal("Parental=", item.Key.String2 + text3);
						}
						else
						{
							text = text + Translated.ConfigIO_Upgrading + " (" + @string + text2 + ") => (" + @string + item.Key.String2 + item2 + ")" + Environment.NewLine;
							SetValueInternal(@string, item.Key.String2 + item2);
						}
						break;
					}
				}
				else
				{
					SetValueInternal(item.Key.String1, "");
				}
			}
			SetValueInternal("CfgVersion=", 1.ToString());
		}
		if (GetValue("CfgVersion=").ToLower() == "1")
		{
			text = text + string.Format(Translated.ConfigIO_UpgradingFromTo, "1", "2") + Environment.NewLine;
			string value = GetValue("region=");
			if (!string.IsNullOrEmpty(value))
			{
				flag = true;
				string text4 = value.ToLower().Replace("region", "vmode");
				text = text + Translated.ConfigIO_Upgrading + " Region => V-Mode (region=" + value + ") => (Vmode=" + text4 + ")" + Environment.NewLine;
				SetValueInternal("region=", "");
				SetValueInternal("Region=", "");
				SetValueInternal("Vmode=", text4);
			}
			SetValueInternal("CfgVersion=", 2.ToString());
		}
		if (GetValue("CfgVersion=").ToLower() == "2")
		{
			text = text + string.Format(Translated.ConfigIO_UpgradingFromTo, "2", "3") + Environment.NewLine;
			string value2 = GetValue("GSM=");
			if (!string.IsNullOrEmpty(value2))
			{
				flag = true;
				text = text + Translated.ConfigIO_Upgrading + " GSM => Notes (GSM=" + value2 + ") => (NOTES=" + value2 + ")" + Environment.NewLine;
				SetValueInternal("GSM=", "");
				SetValueInternal("Notes=", value2);
			}
			SetValueInternal("CfgVersion=", 3.ToString());
		}
		if (GetValue("CfgVersion=").ToLower() == "3")
		{
			text = text + string.Format(Translated.ConfigIO_UpgradingFromTo, "3", "4") + Environment.NewLine;
			string value3 = GetValue("$Compatibility=");
			if (!string.IsNullOrEmpty(value3))
			{
				char[] array = Convert.ToString(int.Parse(value3), 2).PadLeft(8, '0').Reverse()
					.ToArray();
				List<string> list2 = new List<string>();
				for (int i = 0; i <= 7; i++)
				{
					if (array[i] == '1')
					{
						list2.Add((i + 1).ToString());
					}
				}
				SetValueInternal("Modes=", string.Join("+", list2));
			}
			SetValueInternal("CfgVersion=", 4.ToString());
		}
		if (GetValue("CfgVersion=").ToLower() == "4")
		{
			text = text + string.Format(Translated.ConfigIO_UpgradingFromTo, "4", "5") + Environment.NewLine;
			if (!string.IsNullOrEmpty(GetValue("Cheat=").Trim()))
			{
				SetValueInternal("$EnableCheat=", 1.ToString());
			}
			SetValueInternal("CfgVersion=", 5.ToString());
		}
		if (GetValue("CfgVersion=").ToLower() == "5")
		{
			text = text + string.Format(Translated.ConfigIO_UpgradingFromTo, "5", "6") + Environment.NewLine;
			string tmpParental = GetValue("Parental=").ToLower();
			if (!string.IsNullOrEmpty(tmpParental))
			{
				foreach (KeyValuePair<string, List<GameRatingClass>> staticRating in StaticRatings)
				{
					IEnumerable<GameRatingClass> source = staticRating.Value.Where((GameRatingClass x) => (x.Value ?? "") == (tmpParental ?? ""));
					if (source.Count() == 1)
					{
						SetValueInternal("ParentalText=", source.ElementAtOrDefault(0).ValueText);
					}
				}
			}
			string text5 = GetValue("Device=").ToLower();
			if (!string.IsNullOrEmpty(text5) && StaticDevice.ContainsKey(text5))
			{
				SetValueInternal("DeviceText=", StaticDevice[text5]);
			}
			string text6 = GetValue("Rating=").ToLower();
			if (!string.IsNullOrEmpty(text6) && StaticRating.ContainsKey(text6))
			{
				SetValueInternal("RatingText=", StaticRating[text6]);
			}
			Upgrade5_6Helper1("Aspect=", "AspectText=", StaticAspect);
			Upgrade5_6Helper1("Players=", "PlayersText=", StaticPlayers);
			Upgrade5_6Helper1("Scan=", "ScanText=", StaticScan);
			Upgrade5_6Helper1("Vmode=", "VmodeText=", StaticVmode);
			flag = true;
			SetValueInternal("CfgVersion=", 6.ToString());
		}
		if (GetValue("CfgVersion=").ToLower() == "6")
		{
			text = text + string.Format(Translated.ConfigIO_UpgradingFromTo, "6", "7") + Environment.NewLine;
			SetValueInternal("PS1ID=", "");
			SetValueInternal("CfgVersion=", 7.ToString());
		}
		if (GetValue("CfgVersion=").ToLower() == "7")
		{
			text = text + string.Format(Translated.ConfigIO_UpgradingFromTo, "7", "8") + Environment.NewLine;
			if (GetValue("$EnableCheat=") == "1")
			{
				SetValueInternal("$CheatsSource=", 1.ToString());
			}
			if (GetValue("$EnableGSM=") == "1")
			{
				SetValueInternal("$GSMSource=", 1.ToString());
			}
			if (GetValue("$EnablePadEmu=") == "1")
			{
				SetValueInternal("$PADEMUSource=", 1.ToString());
			}
			SetValueInternal("$ConfigSource=", 1.ToString());
			SetValueInternal("CfgVersion=", 8.ToString());
		}
		if (flag)
		{
			unsavedChanges = true;
			text += string.Format(Translated.ConfigIO_UpdateComplete, 8);
		}
		return text;
	}

	private void Upgrade5_6Helper1(string key, string keyText, List<ComboCfgDescValTextClass> list)
	{
		string tmpVal = GetValue(key).ToLower();
		if (!string.IsNullOrEmpty(tmpVal))
		{
			IEnumerable<ComboCfgDescValTextClass> source = list.Where((ComboCfgDescValTextClass x) => (x.Value.ToLower() ?? "") == (tmpVal ?? ""));
			if (source.Count() == 1)
			{
				SetValueInternal(keyText, source.ElementAtOrDefault(0).ValueText);
			}
		}
	}

	public bool WriteCFG()
	{
		//IL_00cc: Unknown result type (might be due to invalid IL or missing references)
		if (GetValue("$ConfigSource=") != "1")
		{
			SetValueInternal("$ConfigSource=", 1.ToString());
		}
		if (File.Exists(CFGFile))
		{
			File.Copy(CFGFile, CFGFile + ".bak", overwrite: true);
		}
		bool flag;
		try
		{
			using StreamWriter streamWriter = new StreamWriter(CFGFile, append: false);
			foreach (string item in CFG_Text)
			{
				if (!string.IsNullOrEmpty(item.Trim()))
				{
					streamWriter.WriteLine(item.Trim());
				}
			}
			flag = true;
		}
		catch
		{
			MessageBox.Show(Translated.ConfigIO_FileWriteFail, Translated.Global_Error, (MessageBoxButtons)0, (MessageBoxIcon)16);
			File.Copy(CFGFile + ".bak", CFGFile, overwrite: true);
			flag = false;
		}
		if (File.Exists(CFGFile + ".back"))
		{
			File.Delete(CFGFile + ".bak");
		}
		if (flag)
		{
			unsavedChanges = false;
		}
		return flag;
	}

	public string GetValue(string key, bool IgnoreCase = true)
	{
		foreach (string item in CFG_Text)
		{
			if (item.StartsWith(key, IgnoreCase, null))
			{
				string text = item.Remove(0, key.Length).Trim();
				if (string.IsNullOrEmpty(text))
				{
					SetValue(key, "");
				}
				return text;
			}
		}
		return "";
	}

	public int GetValueInt(string key, int defaultVal = 0, bool IgnoreCase = true)
	{
		int result = defaultVal;
		foreach (string item in CFG_Text)
		{
			if (item.StartsWith(key, IgnoreCase, null))
			{
				string text = item.Remove(0, key.Length).Trim();
				if (string.IsNullOrEmpty(text))
				{
					SetValue(key, "");
				}
				else
				{
					int.TryParse(text, out result);
				}
				return result;
			}
		}
		return result;
	}

	public void WriteFromTextBoxAndSave()
	{
		if (LeTextBox == null)
		{
			return;
		}
		CFG_Text.Clear();
		string[] array = ((Control)LeTextBox).Text.Split(Environment.NewLine);
		foreach (string text in array)
		{
			if (!string.IsNullOrEmpty(text.Trim()))
			{
				CFG_Text.Add(text.Trim());
			}
		}
		WriteCFG();
	}

	private void SetValueInternal(string key, string val)
	{
		SetValue(key, val, internalCall: true);
	}

	public void SetValue(string key, string val, bool internalCall = false)
	{
		if (!internalCall)
		{
			unsavedChanges = true;
		}
		bool flag = false;
		if (CFG_Text.Count > 0)
		{
			int i = 0;
			for (int num = CFG_Text.Count - 1; i <= num; i++)
			{
				if (CFG_Text[i].ToLower().StartsWith(key.ToLower()))
				{
					if (string.IsNullOrEmpty(val))
					{
						CFG_Text.RemoveAt(i);
					}
					else
					{
						CFG_Text[i] = key + val;
					}
					flag = true;
					break;
				}
			}
		}
		if (!flag && !string.IsNullOrEmpty(val))
		{
			CFG_Text.Add(key + val);
		}
		TextBoxDump();
	}

	private void TextBoxDump()
	{
		if (LeTextBox == null)
		{
			return;
		}
		((Control)LeTextBox).Text = "";
		foreach (string item in CFG_Text)
		{
			TextBox leTextBox = LeTextBox;
			((Control)leTextBox).Text = ((Control)leTextBox).Text + item + Environment.NewLine;
		}
	}
}
