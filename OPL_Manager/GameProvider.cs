using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using System.Xml.Linq;
using OPL_Manager.My.Resources;

namespace OPL_Manager;

public class GameProvider
{
	public delegate void ListUpdatedEventHandler();

	private static List<GameInfo> _gameList = new List<GameInfo>();

	private static string hdlOptionIpHdd;

	private static CommonFuncs.Modes Mode;

	public static string HdlIP => hdlOptionIpHdd;

	public static List<GameInfo> GameList => _gameList;

	public static event ListUpdatedEventHandler ListUpdated;

	public static void From(CommonFuncs.Modes _mode, string _ip = null, bool invalidate = false)
	{
		if (_mode == CommonFuncs.Modes.Ethernet || _mode == CommonFuncs.Modes.HDD)
		{
			Mode = _mode;
			hdlOptionIpHdd = _ip;
			OplFolders.CreateFolders();
			UpdateGameListHdl(invalidate);
		}
		else
		{
			Mode = _mode;
			UpdateGameListNormal(invalidate);
			SearchAppsPops();
		}
	}

	private static void SearchAppsPops()
	{
		List<KeyValuePair<string, string>> list = new List<KeyValuePair<string, string>>();
		string path = Path.Combine(OplFolders.Main, "conf_apps.cfg");
		if (File.Exists(path))
		{
			foreach (string item in File.ReadAllText(path).Split(Environment.NewLine).ToList())
			{
				string[] array = item.Split('=');
				if (array.Length == 2)
				{
					string key = array[0].Trim();
					string value = array[1].Trim();
					list.Add(new KeyValuePair<string, string>(key, value));
				}
			}
		}
		string[] files;
		if (Directory.Exists(OplFolders.APPS))
		{
			files = Directory.GetFiles(OplFolders.APPS, "*.elf");
			foreach (string app in files)
			{
				List<KeyValuePair<string, string>> list2 = list.FindAll((KeyValuePair<string, string> kv) => kv.Value.IndexOf(Path.GetFileNameWithoutExtension(app), StringComparison.OrdinalIgnoreCase) > -1);
				_gameList.Add((GameInfo)new GameInfo().FromAPP(app, (list2.Count == 1) ? list2[0].Key : null));
			}
			files = Directory.GetDirectories(OplFolders.APPS);
			foreach (string text in files)
			{
				string text2 = Path.Combine(text, "title.cfg");
				if (File.Exists(text2))
				{
					TextBox textboxxx = null;
					ConfigClass configClass = new ConfigClass(text2, skipCheck: true, ref textboxxx);
					string value2 = configClass.GetValue("boot=");
					if (!string.IsNullOrEmpty(value2))
					{
						string value3 = configClass.GetValue("title=", IgnoreCase: false);
						_gameList.Add((GameInfo)new GameInfo().FromAPP(text + Path.DirectorySeparatorChar + value2, string.IsNullOrEmpty(value3) ? value2 : value3, text2));
					}
				}
			}
		}
		string text3 = OplFolders.Main;
		DirectoryInfo parent = Directory.GetParent(text3);
		if (parent != null && parent.Root != null)
		{
			string fullName = parent.Root.FullName;
			if (Directory.Exists(Path.Combine(fullName, "POPS\\")))
			{
				text3 = fullName;
			}
		}
		files = new string[11]
		{
			"POPS\\", "POPS0\\", "POPS1\\", "POPS2\\", "POPS3\\", "POPS4\\", "POPS5\\", "POPS6\\", "POPS7\\", "POPS8\\",
			"POPS9\\"
		};
		foreach (string path2 in files)
		{
			string text4 = Path.Combine(text3, path2);
			if (!Directory.Exists(text4))
			{
				continue;
			}
			string[] files2 = Directory.GetFiles(text4, "*.VCD");
			foreach (string pops in files2)
			{
				KeyValuePair<string, string> keyValuePair = list.Find((KeyValuePair<string, string> kv) => kv.Value.IndexOf(Path.GetFileNameWithoutExtension(pops), StringComparison.OrdinalIgnoreCase) > -1);
				if (!keyValuePair.Equals(default(KeyValuePair<string, string>)))
				{
					string key2 = keyValuePair.Key;
					string fileName = Path.GetFileName(keyValuePair.Value);
					_gameList.Add((GameInfo)new GameInfo().FromPOPSWithConfApps(pops, key2, fileName));
					continue;
				}
				if (File.Exists(Path.Combine(text4, "SB." + Path.GetFileNameWithoutExtension(pops) + ".ELF")))
				{
					_gameList.Add((GameInfo)new GameInfo().FromPOPSWithSimpleMatchingELF(pops, "SB." + Path.GetFileNameWithoutExtension(pops) + ".ELF"));
					continue;
				}
				if (File.Exists(Path.Combine(text4, "XX." + Path.GetFileNameWithoutExtension(pops) + ".ELF")))
				{
					_gameList.Add((GameInfo)new GameInfo().FromPOPSWithSimpleMatchingELF(pops, "XX." + Path.GetFileNameWithoutExtension(pops) + ".ELF"));
					continue;
				}
				if (File.Exists(Path.Combine(text4, Path.GetFileNameWithoutExtension(pops) + ".ELF")))
				{
					_gameList.Add((GameInfo)new GameInfo().FromPOPSWithSimpleMatchingELF(pops, Path.GetFileNameWithoutExtension(pops) + ".ELF"));
					continue;
				}
				int num2 = _gameList.FindIndex((GameInfo x) => x.FileDiscFullPath.EndsWith("SB." + Path.GetFileNameWithoutExtension(pops) + ".ELF"));
				int num3 = _gameList.FindIndex((GameInfo x) => x.FileDiscFullPath.EndsWith("XX." + Path.GetFileNameWithoutExtension(pops) + ".ELF"));
				int num4 = _gameList.FindIndex((GameInfo x) => x.FileDiscFullPath.EndsWith(Path.GetFileNameWithoutExtension(pops) + ".ELF"));
				if (num2 != -1 || num3 != -1 || num4 != -1)
				{
					int index = ((num2 != -1) ? num2 : ((num3 != -1) ? num3 : num4));
					GameInfo appEntry = _gameList[index];
					_gameList.Add((GameInfo)new GameInfo().FromPOPSWithTitleCfg(pops, appEntry));
					_gameList.RemoveAt(index);
				}
				else
				{
					_gameList.Add((GameInfo)new GameInfo().FromPOPSWithOnlyVCDOldDBWay(pops));
				}
			}
		}
	}

	public static void UpdateGameListHdl(bool force_refresh = false)
	{
		//IL_0084: Unknown result type (might be due to invalid IL or missing references)
		//IL_008a: Invalid comparison between Unknown and I4
		//IL_00f0: Unknown result type (might be due to invalid IL or missing references)
		//IL_011c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0191: Unknown result type (might be due to invalid IL or missing references)
		//IL_013d: Unknown result type (might be due to invalid IL or missing references)
		//IL_015a: Unknown result type (might be due to invalid IL or missing references)
		//IL_02ec: Unknown result type (might be due to invalid IL or missing references)
		_gameList.Clear();
		bool flag = false;
		string text = "";
		if (Mode == CommonFuncs.Modes.Ethernet)
		{
			text = Path.Combine(Program.MainFormInst.app_folder, "cache_hdl_network.dat");
		}
		else if (Mode == CommonFuncs.Modes.HDD)
		{
			text = Path.Combine(Program.MainFormInst.app_folder, "cache_hdl_local.dat");
		}
		if (Mode == CommonFuncs.Modes.Ethernet && File.Exists(text))
		{
			flag = true;
		}
		else if (Mode == CommonFuncs.Modes.HDD && File.Exists(text))
		{
			flag = true;
		}
		if (!flag && (int)MessageBox.Show("No local game cache found. Get game list now?", Translated.Global_Question, (MessageBoxButtons)4, (MessageBoxIcon)32) == 7)
		{
			return;
		}
		if (flag && !force_refresh)
		{
			ReadCachedXmlHdl(text);
		}
		else
		{
			if (!(!flag || force_refresh))
			{
				return;
			}
			if (Mode == CommonFuncs.Modes.Ethernet)
			{
				MessageBox.Show("Ready to get game list from ps2!" + Environment.NewLine + "Please run hdl server on ps2 and press OK" + Environment.NewLine + "PS2 IP: " + hdlOptionIpHdd, Translated.Global_Information, (MessageBoxButtons)0, (MessageBoxIcon)64);
			}
			else if (Mode == CommonFuncs.Modes.HDD)
			{
				MessageBox.Show("Ready to get game list from ps2 HDD!" + Environment.NewLine + "Connect HDD to pc and click OK", Translated.Global_Information, (MessageBoxButtons)0, (MessageBoxIcon)64);
				List<string> list = HddLocalListDevices();
				if (list.Count == 0)
				{
					MessageBox.Show("No PS2 formatted HDD found on this PC!", Translated.Global_Error, (MessageBoxButtons)0, (MessageBoxIcon)16);
					return;
				}
				if (list.Count > 1)
				{
					MessageBox.Show("More than 1 PS2 HDD found! Using the 1st one.", Translated.Global_Information, (MessageBoxButtons)0, (MessageBoxIcon)64);
				}
				hdlOptionIpHdd = list[0];
			}
			string text2 = CommonFuncs.HDL_Dump_GetList(hdlOptionIpHdd).Trim();
			if (string.IsNullOrEmpty(text2))
			{
				MessageBox.Show("Error getting game list from ps2!", Translated.Global_Error, (MessageBoxButtons)0, (MessageBoxIcon)16);
				return;
			}
			foreach (string item in from x in text2.Split(Environment.NewLine)
				select x.Trim())
			{
				if (item.StartsWith("CD") | item.StartsWith("DVD"))
				{
					Match match = new Regex("(CD|DVD).*?(\\d+)KB.*?([A-Z][A-Z][A-Z][A-Z]_[0-9][0-9][0-9]\\.[0-9][0-9])(.*)").Match(item);
					if (match.Success)
					{
						string text3 = match.Groups[1].Value.Trim();
						long size = long.Parse(match.Groups[2].Value.Trim()) * 1000;
						string iD = match.Groups[3].Value.Trim();
						string title = match.Groups[4].Value.Trim();
						_gameList.Add((GameInfo)new GameInfo().FromCachedHdl(Mode, iD, title, (!(text3.Trim() == "CD")) ? CommonFuncs.DiscType.DVD : CommonFuncs.DiscType.CD, size));
					}
				}
			}
			MessageBox.Show("Got game list from PS2!", "Success", (MessageBoxButtons)0, (MessageBoxIcon)64);
			WriteCacheXmlHdl(text);
		}
	}

	public static void ReadCachedXmlHdl(string xmlFilePath)
	{
		if (!File.Exists(xmlFilePath))
		{
			return;
		}
		foreach (XElement item in from game in XDocument.Load(xmlFilePath).Element("Games").Descendants("Game")
			select (game))
		{
			string value = item.Element("ID").Value;
			string value2 = item.Element("Title").Value;
			string value3 = item.Element("Size").Value;
			CommonFuncs.DiscType discType = (CommonFuncs.DiscType)Enum.Parse(typeof(CommonFuncs.DiscType), item.Element("Media").Value);
			_gameList.Add((GameInfo)new GameInfo().FromCachedHdl(Mode, value, value2, discType, long.Parse(value3)));
		}
	}

	public static void WriteCacheXmlHdl(string xmlFilePath)
	{
		XDocument xDocument = new XDocument(new XDeclaration("1", "UTF-8", "YES"), new XComment("OPL MANAGER - Game Cache file"), new XElement("Games"));
		int num = 0;
		foreach (GameInfo game in _gameList)
		{
			if (game.NeedCacheGame)
			{
				XElement content = new XElement("Game", new XElement("ID", game.ID), new XElement("Title", game.Title), new XElement("Size", game.Size), new XElement("Media", (int)game.DiscType));
				xDocument.Element("Games").Add(content);
				num++;
			}
		}
		xDocument.Add(new XComment("Game count: " + num));
		xDocument.Save(xmlFilePath);
	}

	private static List<string> HddLocalListDevices()
	{
		//IL_00bd: Unknown result type (might be due to invalid IL or missing references)
		Process process = new Process();
		process.StartInfo.FileName = AppDomain.CurrentDomain.BaseDirectory + "\\lib\\" + OplmSettings.ReadString("HDL_VERSION");
		process.StartInfo.WorkingDirectory = AppDomain.CurrentDomain.BaseDirectory;
		process.StartInfo.Arguments = "query";
		process.StartInfo.UseShellExecute = false;
		process.StartInfo.RedirectStandardOutput = true;
		process.StartInfo.RedirectStandardError = true;
		process.StartInfo.CreateNoWindow = true;
		process.Start();
		string text = process.StandardOutput.ReadToEnd();
		string text2 = process.StandardError.ReadToEnd();
		process.WaitForExit();
		if (!string.IsNullOrEmpty(text2))
		{
			MessageBox.Show(text2.Trim(), "hdl_dump.exe ERROR", (MessageBoxButtons)0, (MessageBoxIcon)16);
		}
		using (StreamWriter streamWriter = new StreamWriter("hdl_log.txt", append: true))
		{
			streamWriter.WriteLine("hdl_log.txt", Environment.NewLine + "==== HddLocalListDevices() ====" + Environment.NewLine + text + Environment.NewLine + "==ERROR==" + text2 + Environment.NewLine + "===============" + Environment.NewLine);
		}
		List<string> list = new List<string>();
		foreach (string item in from x in text.Split(Environment.NewLine)
			select x.Trim())
		{
			Match match = new Regex("(hdd\\d+\\:).*Playstation").Match(item);
			if (match.Success)
			{
				list.Add(match.Groups[1].Value.Trim());
			}
		}
		return list;
	}

	public static void UpdateGameListNormal(bool invalidateCache = false)
	{
		DialogLoadingGames tmpProgressDialog = new DialogLoadingGames();
		((Control)Program.MainFormInst).Enabled = false;
		Application.DoEvents();
		string xmlFilePath = Path.Combine(Program.MainFormInst.app_folder, "cache_normal.dat");
		_gameList.Clear();
		if (!invalidateCache)
		{
			ReadCachedXmlNormal(xmlFilePath, ref tmpProgressDialog);
		}
		if (Directory.Exists(OplFolders.CD))
		{
			List<string> list = Directory.GetFiles(OplFolders.CD, "*.iso").ToList();
			list.AddRange(Directory.GetFiles(OplFolders.CD, "*.zso"));
			foreach (string item in list)
			{
				if (!GameAlreadyListed(item))
				{
					_gameList.Add((GameInfo)new GameInfo().FromNormalFile(item, CommonFuncs.DiscType.CD));
					tmpProgressDialog.IncrementCount();
				}
			}
		}
		if (Directory.Exists(OplFolders.DVD))
		{
			List<string> list2 = Directory.GetFiles(OplFolders.DVD, "*.iso").ToList();
			list2.AddRange(Directory.GetFiles(OplFolders.DVD, "*.zso"));
			foreach (string item2 in list2)
			{
				if (!GameAlreadyListed(item2))
				{
					_gameList.Add((GameInfo)new GameInfo().FromNormalFile(item2, CommonFuncs.DiscType.DVD));
					tmpProgressDialog.IncrementCount();
				}
			}
		}
		if (File.Exists(OplFolders.Main + "ul.cfg"))
		{
			using BinaryReader binaryReader = new BinaryReader(File.Open(OplFolders.Main + "ul.cfg", FileMode.Open));
			int num = (int)Math.Round((double)(int)binaryReader.BaseStream.Length / 64.0);
			int i = 1;
			for (int num2 = num; i <= num2; i++)
			{
				byte[] bytes = binaryReader.ReadBytes(32);
				string text = Encoding.ASCII.GetString(bytes).Replace("\0", "");
				string text2 = Encoding.ASCII.GetString(binaryReader.ReadBytes(15)).Trim().Replace("\0", "");
				short num3 = Convert.ToInt16(binaryReader.ReadByte());
				CommonFuncs.DiscType discType = ((Convert.ToInt16(binaryReader.ReadByte()) != 18) ? CommonFuncs.DiscType.DVD : CommonFuncs.DiscType.CD);
				long num4 = 0L;
				string text3 = CommonFuncs.CRC32StringToHexString(text);
				int j = 0;
				for (int num5 = num3 - 1; j <= num5; j++)
				{
					string text4 = OplFolders.Main + "ul." + text3 + "." + text2.Substring(3) + "." + j.ToString("00");
					if (File.Exists(text4))
					{
						num4 += new FileInfo(text4).Length;
					}
				}
				binaryReader.ReadBytes(15);
				_gameList.Add((GameInfo)new GameInfo().FromUlInfo(text2.Substring(3), text, discType, num4));
				tmpProgressDialog.IncrementCount();
			}
			binaryReader.Close();
		}
		WriteCacheXmlNormal(xmlFilePath);
		((Control)Program.MainFormInst).Enabled = true;
		((Form)tmpProgressDialog).Close();
		((Component)(object)tmpProgressDialog).Dispose();
		GameProvider.ListUpdated?.Invoke();
	}

	public static void ReadCachedXmlNormal(string xmlFilePath, ref DialogLoadingGames tmpProgressDialog)
	{
		if (!File.Exists(xmlFilePath))
		{
			return;
		}
		foreach (XElement item in from game in XDocument.Load(xmlFilePath).Element("Games").Descendants("Game")
			select (game))
		{
			string value = item.Element("ID").Value;
			string value2 = item.Element("Title").Value;
			string value3 = item.Element("File").Value;
			CommonFuncs.DiscType discType = (CommonFuncs.DiscType)Enum.Parse(typeof(CommonFuncs.DiscType), item.Element("Media").Value);
			string value4 = item.Element("Modified").Value;
			long size = long.Parse(item.Element("Size").Value);
			bool newformat = bool.Parse(item.Element("NewFormat").Value);
			if (value3.StartsWith(OplFolders.Main) && File.Exists(value3) && (File.GetLastWriteTime(value3).ToString() ?? "") == (value4 ?? ""))
			{
				tmpProgressDialog.IncrementCount();
				_gameList.Add((GameInfo)new GameInfo().FromNormalCached(value, value2, value3, size, discType, newformat));
			}
		}
	}

	public static void WriteCacheXmlNormal(string xmlFilePath)
	{
		if (_gameList.Where((GameInfo x) => x.NeedCacheGame).Count() <= 0)
		{
			if (File.Exists(xmlFilePath))
			{
				File.Delete(xmlFilePath);
			}
			return;
		}
		XDocument xDocument = new XDocument(new XDeclaration("1", "UTF-8", "YES"), new XComment("OPL MANAGER - Game Cache file"), new XElement("Games"));
		int num = 0;
		foreach (GameInfo game in _gameList)
		{
			if (game.NeedCacheGame)
			{
				XElement content = new XElement("Game", new XElement("ID", game.ID), new XElement("Title", game.Title), new XElement("File", game.FileDiscFullPath), new XElement("Media", game.DiscType.ToString()), new XElement("Modified", File.GetLastWriteTime(game.FileDiscFullPath).ToString()), new XElement("Size", new FileInfo(game.FileDiscFullPath).Length.ToString()), new XElement("NewFormat", game.IsNewFormat.ToString()));
				xDocument.Element("Games").Add(content);
				num++;
			}
		}
		xDocument.Add(new XComment("Game count: " + num));
		xDocument.Save(xmlFilePath);
	}

	public static bool GameAlreadyListed(string file)
	{
		foreach (GameInfo game in _gameList)
		{
			if ((game.FileDiscFullPath ?? "") == (file ?? ""))
			{
				return true;
			}
		}
		return false;
	}

	public static GameInfo get_GetGame(int indexID)
	{
		foreach (GameInfo game in _gameList)
		{
			if (game.ItemID == indexID)
			{
				return game;
			}
		}
		return null;
	}

	public static string get_TitleByGameID(string ID)
	{
		foreach (GameInfo game in _gameList)
		{
			if ((game.ID ?? "") == (ID ?? ""))
			{
				return game.Title;
			}
		}
		return "";
	}

	public static GameInfo get_ById(string ID)
	{
		foreach (GameInfo game in _gameList)
		{
			if ((game.ID ?? "") == (ID ?? ""))
			{
				return game;
			}
		}
		return null;
	}
}
