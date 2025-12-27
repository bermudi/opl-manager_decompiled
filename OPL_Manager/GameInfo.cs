using System;
using System.IO;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using OPL_Manager.My.Resources;
using OplManagerService;

namespace OPL_Manager;

public class GameInfo
{
	private string GameID = "";

	private string GameArtID;

	private long GameSize;

	private string GameTitle = "";

	private string GameTitleFromCfg;

	private string GameFile = "";

	private CommonFuncs.DiscType GameDiscType;

	private GameType GameType = GameType.PS2;

	private string GameArtICO = "";

	private string GameArtCOV = "";

	private string GameArtLAB = "";

	private string GameArtLGO = "";

	private string GameArtCOV2 = "";

	private string GameArtSCR = "";

	private string GameArtSCR2 = "";

	private string GameArtBG = "";

	private string OverrideGameCFGPath = "";

	private string GameCFG = "";

	private string GameCHT = "";

	private ConfigClass GameCfgData;

	private bool GameNewFormat;

	private bool CacheGame;

	private bool GameBad;

	private string GameBadMsg;

	private CommonFuncs.Modes GameDevice;

	private static int counter;

	private int _itemID;

	public ConfigClass CFG
	{
		get
		{
			if (GameCfgData == null && HasCFG)
			{
				TextBox textboxxx = null;
				GameCfgData = new ConfigClass(FileCFG, skipCheck: false, ref textboxxx);
			}
			return GameCfgData;
		}
	}

	public int ItemID => _itemID;

	public string Title => GameTitle;

	public bool IsBad => GameBad;

	public string BadMsg => GameBadMsg;

	public bool IsNewFormat => GameNewFormat;

	public string ID => GameID;

	public string ArtID
	{
		get
		{
			if (GameArtID == null)
			{
				return GameID;
			}
			return GameArtID;
		}
	}

	public long Size => GameSize;

	public GameType Type => GameType;

	public string TypeText => Enum.GetName(typeof(GameType), GameType);

	public CommonFuncs.DiscType DiscType => GameDiscType;

	public string DiscTypeText => Enum.GetName(typeof(CommonFuncs.DiscType), GameDiscType);

	public CommonFuncs.Modes Device => GameDevice;

	public bool NeedCacheGame => CacheGame;

	public string FileArtICO => GameArtICO;

	public string FileArtCOV => GameArtCOV;

	public string FileArtCOV2 => GameArtCOV2;

	public string FileArtLAB => GameArtLAB;

	public string FileArtLGO => GameArtLGO;

	public string FileArtSCR => GameArtSCR;

	public string FileArtSCR2 => GameArtSCR2;

	public string FileArtBG => GameArtBG;

	public string FileCHT => GameCHT;

	public string FileCFG => GameCFG;

	public string ExpectedCFGFilePath
	{
		get
		{
			if (Type == GameType.PS2)
			{
				return Path.Combine(OplFolders.CFG, GameID + ".cfg");
			}
			return OverrideGameCFGPath;
		}
	}

	public string FileDiscFullPath => GameFile;

	public string FileDiscOnly
	{
		get
		{
			if (GameDevice == CommonFuncs.Modes.USB)
			{
				return GameFile;
			}
			return Path.GetFileName(GameFile);
		}
	}

	public string FileDiscFolderPath => Path.GetDirectoryName(GameFile) + Path.DirectorySeparatorChar;

	public bool HasICO => !string.IsNullOrEmpty(GameArtICO);

	public bool HasCOV => !string.IsNullOrEmpty(GameArtCOV);

	public bool HasCOV2 => !string.IsNullOrEmpty(GameArtCOV2);

	public bool HasLAB => !string.IsNullOrEmpty(GameArtLAB);

	public bool HasLGO => !string.IsNullOrEmpty(GameArtLGO);

	public bool HasSCR => !string.IsNullOrEmpty(GameArtSCR);

	public bool HasSCR2 => !string.IsNullOrEmpty(GameArtSCR2);

	public bool HasBG => !string.IsNullOrEmpty(GameArtBG);

	public bool HasCFG => !string.IsNullOrEmpty(GameCFG);

	public bool HasCHT => !string.IsNullOrEmpty(GameCHT);

	public string HasICOtext
	{
		get
		{
			if (string.IsNullOrEmpty(GameArtICO))
			{
				return Translated.GLOBAL_STRING_NO;
			}
			return Translated.GLOBAL_STRING_YES;
		}
	}

	public string HasCOVtext
	{
		get
		{
			if (string.IsNullOrEmpty(GameArtCOV))
			{
				return Translated.GLOBAL_STRING_NO;
			}
			return Translated.GLOBAL_STRING_YES;
		}
	}

	public string HasCOV2text
	{
		get
		{
			if (string.IsNullOrEmpty(GameArtCOV2))
			{
				return Translated.GLOBAL_STRING_NO;
			}
			return Translated.GLOBAL_STRING_YES;
		}
	}

	public string HasLABtext
	{
		get
		{
			if (string.IsNullOrEmpty(GameArtLAB))
			{
				return Translated.GLOBAL_STRING_NO;
			}
			return Translated.GLOBAL_STRING_YES;
		}
	}

	public string HasLGOtext
	{
		get
		{
			if (string.IsNullOrEmpty(GameArtLGO))
			{
				return Translated.GLOBAL_STRING_NO;
			}
			return Translated.GLOBAL_STRING_YES;
		}
	}

	public string HasSCRtext
	{
		get
		{
			if (!(!string.IsNullOrEmpty(GameArtSCR) | !string.IsNullOrEmpty(GameArtSCR2)))
			{
				return Translated.GLOBAL_STRING_NO;
			}
			return Translated.GLOBAL_STRING_YES;
		}
	}

	public string HasBGtext
	{
		get
		{
			if (string.IsNullOrEmpty(GameArtBG))
			{
				return Translated.GLOBAL_STRING_NO;
			}
			return Translated.GLOBAL_STRING_YES;
		}
	}

	public string HasCFGtext
	{
		get
		{
			if (string.IsNullOrEmpty(GameCFG))
			{
				return Translated.GLOBAL_STRING_NO;
			}
			return Translated.GLOBAL_STRING_YES;
		}
	}

	public string HasCHTtext
	{
		get
		{
			if (string.IsNullOrEmpty(GameCHT))
			{
				return Translated.GLOBAL_STRING_NO;
			}
			return Translated.GLOBAL_STRING_YES;
		}
	}

	public GameInfo()
	{
		_itemID = counter;
		counter++;
	}

	public object FromNormalFile(string File, CommonFuncs.DiscType DiscType)
	{
		GameDevice = CommonFuncs.Modes.Normal;
		GameFile = File;
		GameDiscType = DiscType;
		CacheGame = true;
		UpdateGameInfo();
		return this;
	}

	public object FromPOPSWithConfApps(string File, string _title, string _elf)
	{
		GameDevice = CommonFuncs.Modes.Normal;
		GameFile = File;
		GameDiscType = CommonFuncs.DiscType.CD;
		GameType = GameType.POPS;
		GameTitleFromCfg = _title;
		GameArtID = _elf;
		OverrideGameCFGPath = Path.Combine(OplFolders.CFG, Path.GetFileNameWithoutExtension(_elf) + ".cfg");
		UpdateGameInfo();
		return this;
	}

	public object FromPOPSWithSimpleMatchingELF(string File, string _elf)
	{
		GameDevice = CommonFuncs.Modes.Normal;
		GameFile = File;
		GameDiscType = CommonFuncs.DiscType.CD;
		GameType = GameType.POPS;
		GameArtID = _elf;
		OverrideGameCFGPath = Path.Combine(OplFolders.CFG, Path.GetFileNameWithoutExtension(_elf) + ".cfg");
		UpdateGameInfo();
		return this;
	}

	public object FromPOPSWithTitleCfg(string VCDFile, GameInfo AppEntry)
	{
		GameDevice = CommonFuncs.Modes.Normal;
		GameFile = VCDFile;
		GameDiscType = CommonFuncs.DiscType.CD;
		GameType = GameType.POPS;
		GameTitleFromCfg = AppEntry.Title;
		GameArtID = Path.GetFileName(AppEntry.FileDiscFullPath);
		if (!string.IsNullOrEmpty(AppEntry.OverrideGameCFGPath))
		{
			OverrideGameCFGPath = AppEntry.OverrideGameCFGPath;
		}
		UpdateGameInfo();
		return this;
	}

	public object FromPOPSWithOnlyVCDOldDBWay(string File)
	{
		GameDevice = CommonFuncs.Modes.Normal;
		GameFile = File;
		GameDiscType = CommonFuncs.DiscType.CD;
		GameType = GameType.POPS;
		UpdateGameInfo();
		return this;
	}

	public object FromAPP(string File, string _title, string cfgPath = null)
	{
		GameDevice = CommonFuncs.Modes.Normal;
		GameFile = File;
		GameDiscType = CommonFuncs.DiscType.CD;
		GameType = GameType.APP;
		GameTitleFromCfg = _title;
		if (!string.IsNullOrEmpty(cfgPath))
		{
			OverrideGameCFGPath = cfgPath;
		}
		UpdateGameInfo();
		return this;
	}

	public object FromNormalCached(string ID, string Title, string File, long Size, CommonFuncs.DiscType DiscType, bool newformat)
	{
		GameDevice = CommonFuncs.Modes.Normal;
		GameID = ID;
		GameTitle = Title;
		GameDiscType = DiscType;
		GameSize = Size;
		GameFile = File;
		GameNewFormat = newformat;
		CacheGame = true;
		UpdateGameInfo();
		return this;
	}

	public object FromCachedHdl(CommonFuncs.Modes Device, string ID, string Title, CommonFuncs.DiscType DiscType, long Size)
	{
		GameDevice = Device;
		GameID = ID;
		GameTitle = Title;
		GameDiscType = DiscType;
		GameSize = Size;
		CacheGame = true;
		UpdateGameInfoArts();
		return this;
	}

	public object FromUlInfo(string ID, string Title, CommonFuncs.DiscType DiscType, long Size)
	{
		GameDevice = CommonFuncs.Modes.USB;
		GameID = ID;
		GameTitle = Title;
		GameDiscType = DiscType;
		GameSize = Size;
		GameFile = "ul." + GameTitle;
		UpdateGameInfoArts();
		return this;
	}

	public void UpdateGameInfo()
	{
		if (GameDevice != CommonFuncs.Modes.Normal)
		{
			return;
		}
		GameBad = false;
		if (GameType == GameType.PS2)
		{
			bool flag = OplmSettings.ReadBool("ISO_USE_OLD_NAMING_FORMAT");
			bool flag2 = OplmSettings.ReadBool("ISO_FORCE_NAMING_FORMAT");
			if (new Regex("[A-Z][A-Z][A-Z][A-Z]_[0-9][0-9][0-9]\\.[0-9][0-9]\\.").Match(Path.GetFileName(GameFile)).Success)
			{
				GameTitle = ((!string.IsNullOrEmpty(GameTitle)) ? GameTitle : Path.GetFileNameWithoutExtension(Path.GetFileName(GameFile).Substring(12)));
				if (GameTitle.Length > 32)
				{
					GameBad = true;
					GameBadMsg = "The game title is too long! >32.";
				}
				if ((CommonFuncs.SanitizeGameTitle(GameTitle, removeMultipleSpaces: false) ?? "") != (GameTitle.Trim() ?? ""))
				{
					GameBad = true;
					GameBadMsg = Translated.MAIN_STRINGS_RENAME_INVALID_CHARS + " A-Z  a-z  0-9  -  _ () []";
				}
				if (!flag && flag2)
				{
					GameBad = true;
					GameBadMsg = "Using incorrect naming format (OLD).";
				}
				GameID = ((!string.IsNullOrEmpty(GameID)) ? GameID : Path.GetFileName(GameFile).Substring(0, 11));
				GameSize = ((GameSize > 0) ? GameSize : new FileInfo(GameFile).Length);
				GameNewFormat = false;
				UpdateGameInfoArts();
			}
			else
			{
				if (flag && flag2)
				{
					GameBad = true;
					GameBadMsg = "Using incorrect naming format (NEW).";
				}
				string text = GameID;
				if (string.IsNullOrEmpty(text) && !CommonFuncs.IsFileOpen(GameFile))
				{
					text = CommonFuncs.GetGameIdFromISO(GameFile);
				}
				if (!string.IsNullOrEmpty(text))
				{
					GameID = text;
					GameTitle = ((!string.IsNullOrEmpty(GameTitle)) ? GameTitle : Path.GetFileNameWithoutExtension(Path.GetFileName(GameFile)));
					GameSize = ((GameSize > 0) ? GameSize : new FileInfo(GameFile).Length);
					UpdateGameInfoArts();
					CacheGame = true;
					GameNewFormat = true;
				}
				else
				{
					GameBad = true;
					GameBadMsg = "Failed to extract game ID. Invalid ISO?";
				}
			}
		}
		else
		{
			if (!((GameType == GameType.POPS) | (GameType == GameType.APP)))
			{
				return;
			}
			string text2 = Path.GetFileNameWithoutExtension(GameFile);
			if (GameType == GameType.POPS)
			{
				GameSize = new FileInfo(GameFile).Length;
				if (new Regex("[A-Z][A-Z][A-Z][A-Z]_[0-9][0-9][0-9]\\.[0-9][0-9]\\.").Match(text2).Success)
				{
					GameID = text2.Substring(0, 11);
					text2 = text2.Substring(12);
					if (text2.Length > 32)
					{
						GameBad = true;
						GameBadMsg = "The game title is too long! >32.";
					}
					if ((CommonFuncs.SanitizeGameTitle(text2, removeMultipleSpaces: false) ?? "") != (text2.Trim() ?? ""))
					{
						GameBad = true;
						GameBadMsg = Translated.MAIN_STRINGS_RENAME_INVALID_CHARS + " A-Z  a-z  0-9  -  _ () []";
					}
				}
				else if (File.Exists(GameFile))
				{
					string text3 = CommonFuncs.Ps1GameGetID(GameFile);
					if (text3 != null)
					{
						GameID = text3;
					}
					else
					{
						GameBad = true;
						GameBadMsg = "No game ID found.";
					}
				}
			}
			else
			{
				GameID = Path.GetFileName(GameFile);
				GameSize = new FileInfo(GameFile).Length;
			}
			GameTitle = ((GameTitleFromCfg != null) ? GameTitleFromCfg : text2);
			UpdateGameInfoArts();
		}
	}

	public void UpdateGameInfoArts()
	{
		GameArtICO = "";
		GameArtCOV = "";
		GameArtCOV2 = "";
		GameArtLAB = "";
		GameArtLGO = "";
		GameArtSCR = "";
		GameArtSCR2 = "";
		GameArtBG = "";
		GameCFG = "";
		GameCHT = "";
		string artID = ArtID;
		CommonFuncs.TryArtPngThenJpgIfExistsAssign(OplFolders.ART, artID, "_ICO", ref GameArtICO);
		CommonFuncs.TryArtPngThenJpgIfExistsAssign(OplFolders.ART, artID, "_COV", ref GameArtCOV);
		CommonFuncs.TryArtPngThenJpgIfExistsAssign(OplFolders.ART, artID, "_COV2", ref GameArtCOV2);
		CommonFuncs.TryArtPngThenJpgIfExistsAssign(OplFolders.ART, artID, "_LAB", ref GameArtLAB);
		CommonFuncs.TryArtPngThenJpgIfExistsAssign(OplFolders.ART, artID, "_LGO", ref GameArtLGO);
		CommonFuncs.TryArtPngThenJpgIfExistsAssign(OplFolders.ART, artID, "_SCR", ref GameArtSCR);
		CommonFuncs.TryArtPngThenJpgIfExistsAssign(OplFolders.ART, artID, "_SCR2", ref GameArtSCR2);
		CommonFuncs.TryArtPngThenJpgIfExistsAssign(OplFolders.ART, artID, "_BG", ref GameArtBG);
		CommonFuncs.FileExistsAssign(OverrideGameCFGPath, ref GameCFG);
		if (GameType == GameType.POPS)
		{
			CommonFuncs.FileExistsAssign(FileDiscFolderPath + Path.GetFileNameWithoutExtension(FileDiscOnly) + "/CHEATS.TXT", ref GameCHT);
		}
		else
		{
			CommonFuncs.FileExistsAssign(OplFolders.CHT + artID + ".cht", ref GameCHT);
		}
	}
}
