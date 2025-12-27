using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using CsvHelper;
using OPL_Manager.My.Resources;
using OplManagerService;

namespace OPL_Manager;

public class OPLM_Main : BaseForm
{
	private OplManagerServiceSoapClient service = CommonFuncs.SoapAPI;

	public const int app_versionid = 37;

	public const string app_versiontext = "24";

	public const string app_version_date = "2024-09-15";

	public string app_folder = AppDomain.CurrentDomain.BaseDirectory;

	private int mode;

	private int SortColumn = -1;

	private SortOrder mSortDirection;

	public string UserID = "";

	public List<KeyValuePair<string, string>> LanguagesAvailable = new List<KeyValuePair<string, string>>(29)
	{
		new KeyValuePair<string, string>("en", "English (Default)"),
		new KeyValuePair<string, string>("ar", "العربية (Arabic) [93.4%]"),
		new KeyValuePair<string, string>("bg", "български (Bulgarian) [42.6%]"),
		new KeyValuePair<string, string>("ca-ES", "català (Catalan) [56.7%]"),
		new KeyValuePair<string, string>("cs", "čeština (Czech) [88.8%]"),
		new KeyValuePair<string, string>("da", "dansk (Danish) [27.1%]"),
		new KeyValuePair<string, string>("de", "Deutsch (German) [77.9%]"),
		new KeyValuePair<string, string>("el", "ελληνικά (Greek) [90.7%]"),
		new KeyValuePair<string, string>("es", "Español (Spanish) [100%]"),
		new KeyValuePair<string, string>("fa", "فارسى (Persian) [49.4%]"),
		new KeyValuePair<string, string>("fr", "Français (French) [77.7%]"),
		new KeyValuePair<string, string>("he", "ע\u05b4בר\u05b4ית  (Hebrew) [89.5%]"),
		new KeyValuePair<string, string>("hu", "magyar (Hungarian) [96.1%]"),
		new KeyValuePair<string, string>("id", "Bahasa Indonesia (Indonesian) [93.5%]"),
		new KeyValuePair<string, string>("it", "Italiano (Italian) [100%]"),
		new KeyValuePair<string, string>("ja", "日本語 (Japanese) [89.1%]"),
		new KeyValuePair<string, string>("ko", "한국어 (Korean) [100%]"),
		new KeyValuePair<string, string>("nl", "Nederlands (Dutch) [27.1%]"),
		new KeyValuePair<string, string>("no", "norsk (Norwegian) [4.8%]"),
		new KeyValuePair<string, string>("pl", "Polszczyzna (Polish) [100%]"),
		new KeyValuePair<string, string>("pt-BR", "Português Brazil (Portuguese Brazil) [100%]"),
		new KeyValuePair<string, string>("pt", "Português (Portuguese) [100%]"),
		new KeyValuePair<string, string>("ru", "русский (Russian) [100%]"),
		new KeyValuePair<string, string>("sk", "slovenčina (Slovak) [13.2%]"),
		new KeyValuePair<string, string>("sv", "svenska (Swedish) [93.2%]"),
		new KeyValuePair<string, string>("th-th", "ภาษาไทย (Thai) [84.7%]"),
		new KeyValuePair<string, string>("tr-TR", "Türkçe (Turkish) [98.6%]"),
		new KeyValuePair<string, string>("uk-UA", "Українська (Ukrainian) [90.4%]"),
		new KeyValuePair<string, string>("zh-CN", "中国简化 (Chinese Simplified) [47.6%]")
	};

	private IContainer components;

	internal SplitContainer SplitContainer1;

	internal PictureBox Disc_DiscIMG;

	internal PictureBox Cover_CoverIMG;

	internal TextBox GameDetails_TB_ID;

	internal Label GameDetails_Label_ID;

	internal CustomListView GameList;

	internal ColumnHeader Col_Game;

	internal TextBox GameDetails_TB_Path;

	internal Label GameDetails_Label_Path;

	internal ColumnHeader Col_ID;

	internal Button Operations_DeleteGame;

	internal GroupBox Operations;

	internal ToolStripContainer ToolStripContainer1;

	internal MenuStrip MenuStrip1;

	internal ToolStripMenuItem Menu_File;

	internal ToolStripMenuItem Menu_File_Exit;

	internal ToolStripMenuItem Menu_Settings;

	internal ToolStripMenuItem Menu_Help;

	internal ToolStripMenuItem Menu_File_Refresh;

	internal GroupBox GameDetails;

	internal ColumnHeader Col_Size;

	internal GroupBox GlobalStats;

	internal TableLayoutPanel TableLayoutPanel1;

	internal Label GlobalStats_PS2CD;

	internal Label GlobalStats_Total;

	internal Label GlobalStats_CountTotal;

	internal Label GlobalStats_PS2DVD;

	internal Label GlobalStats_PS2DVD_Count;

	internal Label GlobalStats_PS2CD_Count;

	internal Label GlobalStats_Count;

	internal Label GlobalStats_Size;

	internal Label GlobalStats_PS2CD_Size;

	internal Label GlobalStats_PS2DVD_Size;

	internal Label GlobalStats_SizeTotal;

	internal ColumnHeader Col_Medium;

	internal TabControl Tab_Main;

	internal TabPage TabHome;

	internal TabPage TabBadIsos;

	internal SplitContainer SplitContainer2;

	internal ListView BadIsoList;

	internal ColumnHeader ColumnHeader1;

	internal ColumnHeader ColumnHeader2;

	internal ColumnHeader ColumnHeader3;

	internal ColumnHeader ColumnHeader4;

	internal GroupBox GB_BadIsoDetails;

	internal TextBox BadIso_title;

	internal Label BadISO_GameDetails_Label_Size;

	internal Label BadISO_GameDetails_Label_Title;

	internal TextBox BadISO_ID;

	internal Label BadISO_GameDetails_Label_Path;

	internal Label BadISO_GameDetails_Label_ID;

	internal TextBox BadISO_Path;

	internal Button BadISO_UpdateFileName;

	internal TextBox BadISO_newfile;

	internal Label BadISO_GameDetails_Label_NewFile;

	internal TextBox BadISO_Size;

	internal Button Operations_UpdateTitle;

	internal TextBox BadISO_CurrentFileName;

	internal Label BadISO_GameDetails_Label_CurrentFile;

	internal ToolStripMenuItem Menu_Help_About;

	internal ToolStripMenuItem Menu_Help_ChangeLog;

	internal OpenFileDialog OpenFileDialog1;

	internal ToolStripMenuItem Menu_BatchActions;

	internal ToolStripMenuItem Menu_BatchActions_CoverAndIconDownload;

	internal Button Operations_EditCFG;

	internal ToolStripMenuItem Menu_BatchActions_ShareART;

	internal ColumnHeader Col_ArtCov;

	internal ColumnHeader Col_ArtIco;

	internal ToolStripMenuItem Menu_View;

	internal ToolStripMenuItem Menu_View_All;

	internal ToolStripMenuItem Menu_View_WithMissing;

	internal ToolStripMenuItem Menu_View_WithMissing_CoverArt;

	internal ToolStripMenuItem Menu_View_WithMissing_DiscArt;

	internal ToolStripMenuItem Menu_View_WithMissing_CoverAndOrDisc;

	internal ToolStripMenuItem Menu_View_With;

	internal ToolStripMenuItem Menu_View_With_Cover;

	internal ToolStripMenuItem Menu_View_With_Disc;

	internal ToolStripMenuItem Menu_View_With_CoverAndOrDisc;

	internal ToolStripMenuItem Menu_Settings_GameDoubleClickAction;

	internal ToolStripMenuItem Menu_BatchActions_UpgradeOldCFGs;

	internal ToolStripMenuItem Menu_OpenOPLFolder;

	internal ColumnHeader Col_CFG;

	internal ToolTip ToolTip1;

	internal Button BadISO_ChangeID;

	internal Button Operations_Hash;

	internal Timer Timer_VersionCheck;

	internal StatusStrip StatusStrip1;

	internal ToolStripStatusLabel StatusBar_Online;

	internal Timer TimerServerStatus;

	internal ToolStripMenuItem Menu_Settings_AutoUpdate;

	internal ToolStripStatusLabel StatusBar_CountArts;

	internal ToolStripStatusLabel StatusBar_ServerTime;

	internal ToolStripMenuItem Menu_Settings_ChangeLanguage;

	internal Button Operations_CheatEditor;

	internal ColumnHeader Col_Cht;

	internal ToolStripMenuItem Menu_Settings_Mode;

	internal ToolStripStatusLabel StatusBar_Mode;

	internal ToolStripMenuItem Menu_NetworkOptions;

	internal ToolStripMenuItem Menu_NetworkOptions_GetGames;

	internal ToolStripMenuItem Menu_NetworkOptions_Sync;

	internal ToolStripMenuItem Menu_Tools;

	internal ToolStripMenuItem Menu_Tools_IsoConv;

	internal TextBox GameDetails_TB_Title;

	internal Label GameDetails_Label_Title;

	internal Button Operations_ManageARTs;

	internal ToolStripMenuItem Menu_Help_Ps2HomeThread;

	internal ToolStripMenuItem Menu_File_ExportCsv;

	internal Button BadISO_GetTitleFromDB;

	internal ToolStripMenuItem Menu_Settings_CfgDevToggle;

	internal ToolStripMenuItem Menu_LocalHDDOptions;

	internal ToolStripMenuItem Menu_LocalHDDOptions_GetList;

	internal ToolStripMenuItem Menu_Settings_UseOldIsoFormat;

	internal ToolStripMenuItem Menu_Tools_OplSimulator;

	internal ColumnHeader Col_Type;

	internal GroupBox GroupBox1;

	internal CheckBox CB_Filter_APPS;

	internal CheckBox CB_Filter_POPS;

	internal CheckBox CB_Filter_PS2;

	internal ToolStripMenuItem Menu_Tools_APPInstaller;

	internal ToolStripMenuItem Menu_Tools_ConvertISONaming;

	internal ToolStripMenuItem Menu_Help_Facebook;

	internal ToolStripMenuItem Menu_Help_Homepage;

	internal Button BadISO_BatchRenameISO;

	internal ColumnHeader ColumnHeader5;

	internal Button BadISO_BatchRenameVCD;

	internal Label GlobalStats_PS2;

	internal Label GlobalStats_PS2_Count;

	internal Label GlobalStats_PS2_Size;

	internal Label GlobalStats_PS1;

	internal Label GlobalStats_APPS;

	internal Label GlobalStats_PS1_Count;

	internal Label GlobalStats_PS1_Size;

	internal Label GlobalStats_APP_Count;

	internal Label GlobalStats_APP_Size;

	internal ColumnHeader ColumnHeader6;

	internal GroupBox GroupBox2;

	internal Label BadISO_errorMsg;

	internal ToolStripMenuItem Menu_Tools_CleanFiles;

	internal ToolStripMenuItem Menu_File_InvalidateCacheAndRefresh;

	internal ToolStripMenuItem Menu_Help_PSXPlaceThread;

	internal ColumnHeader Col_ArtLab;

	internal ColumnHeader Col_ArtCov2;

	internal ColumnHeader Col_ArtScr;

	internal ColumnHeader Col_ArtLgo;

	internal ColumnHeader Col_ArtBg;

	internal ToolStripMenuItem Menu_Tools_ConvertIsoZso;

	internal ToolStripMenuItem Menu_Tools_DiskIsoZso;

	public GameInfo SelectedGame => GameList.SelectedGame;

	public int SelectedGameIndex => ((ListView)GameList).SelectedIndices[0];

	public bool CanMoveDown => ((ListView)GameList).SelectedIndices[0] < ((ListView)GameList).Items.Count - 1;

	public bool CanMoveUp => ((ListView)GameList).SelectedIndices[0] > 0;

	public OPLM_Main()
	{
		InitializeComponent();
	}

	private void SetLanguage()
	{
		//IL_0026: Unknown result type (might be due to invalid IL or missing references)
		string text = OplmSettings.Read("LANGUAGE", "");
		if (string.IsNullOrEmpty(text))
		{
			SettingsChangeLanguage settingsChangeLanguage = new SettingsChangeLanguage();
			try
			{
				((Form)settingsChangeLanguage).StartPosition = (FormStartPosition)4;
				((Form)settingsChangeLanguage).ShowDialog();
				SetLanguage();
				return;
			}
			finally
			{
				((IDisposable)settingsChangeLanguage)?.Dispose();
			}
		}
		try
		{
			CultureInfo.DefaultThreadCurrentUICulture = new CultureInfo(text);
		}
		catch (Exception)
		{
			CultureInfo.DefaultThreadCurrentUICulture = new CultureInfo("");
		}
		((ToolStripItem)Menu_File).Text = Translated.MAIN_TOOLBAR_FILE;
		((ToolStripItem)Menu_File_Refresh).Text = Translated.MAIN_TOOLBAR_FILE_REFRESH;
		((ToolStripItem)Menu_File_InvalidateCacheAndRefresh).Text = Translated.MAIN_TOOLBAR_FILE_REFRESHINVALIDATE;
		((ToolStripItem)Menu_File_ExportCsv).Text = Translated.MAIN_TOOLBAR_FILE_EXPORT_CSV;
		((ToolStripItem)Menu_File_Exit).Text = Translated.MAIN_TOOLBAR_FILE_EXIT;
		((ToolStripItem)Menu_BatchActions).Text = Translated.MAIN_TOOLBAR_BATCH;
		((ToolStripItem)Menu_BatchActions_CoverAndIconDownload).Text = Translated.MAIN_TOOLBAR_BATCH_COVER_DISC_DOWNLOAD;
		((ToolStripItem)Menu_Tools_CleanFiles).Text = Translated.MAIN_TOOLBAR_TOOLS_CLEAN_FILES;
		((ToolStripItem)Menu_BatchActions_ShareART).Text = Translated.MAIN_TOOLBAR_BATCH_ART_SHARE;
		((ToolStripItem)Menu_BatchActions_UpgradeOldCFGs).Text = Translated.MAIN_TOOLBAR_BATCH_UPGRADE_CFG;
		((ToolStripItem)Menu_View).Text = Translated.MAIN_TOOLBAR_VIEW;
		((ToolStripItem)Menu_View_All).Text = Translated.MAIN_TOOLBAR_VIEW_ALL;
		((ToolStripItem)Menu_View_WithMissing).Text = Translated.MAIN_TOOLBAR_VIEW_MISSING;
		((ToolStripItem)Menu_View_WithMissing_CoverArt).Text = Translated.MAIN_TOOLBAR_VIEW_MISSING_COVER;
		((ToolStripItem)Menu_View_WithMissing_DiscArt).Text = Translated.MAIN_TOOLBAR_VIEW_MISSING_DISC;
		((ToolStripItem)Menu_View_WithMissing_CoverAndOrDisc).Text = Translated.MAIN_TOOLBAR_VIEW_MISSING_COVER_OR_AND_DISC;
		((ToolStripItem)Menu_View_With).Text = Translated.MAIN_TOOLBAR_VIEW_WITH;
		((ToolStripItem)Menu_View_With_Cover).Text = Translated.MAIN_TOOLBAR_VIEW_WITH_COVER;
		((ToolStripItem)Menu_View_With_Disc).Text = Translated.MAIN_TOOLBAR_VIEW_WITH_DISC;
		((ToolStripItem)Menu_View_With_CoverAndOrDisc).Text = Translated.MAIN_TOOLBAR_VIEW_WITH_DISC_OR_AND_COVER;
		((ToolStripItem)Menu_Settings).Text = Translated.MAIN_TOOLBAR_SETTINGS;
		((ToolStripItem)Menu_Settings_Mode).Text = Translated.MAIN_TOOLBAR_SETTINGS_MODE;
		((ToolStripItem)Menu_Settings_GameDoubleClickAction).Text = Translated.MAIN_TOOLBAR_SETTINGS_DOUBLE_CLICK;
		((ToolStripItem)Menu_Settings_AutoUpdate).Text = Translated.MAIN_TOOLBAR_SETTINGS_AUTOUPDATE;
		((ToolStripItem)Menu_Settings_ChangeLanguage).Text = Translated.SettingsChangeLanguage_Title;
		((ToolStripItem)Menu_Settings_CfgDevToggle).Text = Translated.MAIN_TOOLBAR_SETTINGS_CFGDEV;
		((ToolStripItem)Menu_Settings_UseOldIsoFormat).Text = Translated.MAIN_TOOLBAR_SETTINGS_ISO_FORMAT;
		((ToolStripItem)Menu_NetworkOptions).Text = Translated.MAIN_TOOLBAR_NETWORKOPTIONS;
		((ToolStripItem)Menu_NetworkOptions_GetGames).Text = Translated.MAIN_TOOLBAR_NETWORKOPTIONS_GETGAMES;
		((ToolStripItem)Menu_NetworkOptions_Sync).Text = Translated.MAIN_TOOLBAR_NETWORKOPTIONS_SYNCFILES;
		((ToolStripItem)Menu_LocalHDDOptions).Text = Translated.MAIN_TOOLBAR_LOCALHDD;
		((ToolStripItem)Menu_LocalHDDOptions_GetList).Text = Translated.MAIN_TOOLBAR_LOCALHDD_GETGAMES;
		((ToolStripItem)Menu_Tools).Text = Translated.MAIN_TOOLBAR_TOOLS;
		((ToolStripItem)Menu_Tools_IsoConv).Text = Translated.MAIN_TOOLBAR_TOOLS_ISOCONV;
		((ToolStripItem)Menu_Tools_OplSimulator).Text = Translated.MAIN_TOOLBAR_TOOLS_GALLERY;
		((ToolStripItem)Menu_Tools_APPInstaller).Text = Translated.MAIN_TOOLBAR_TOOLS_APPINSTALLER;
		((ToolStripItem)Menu_Tools_ConvertISONaming).Text = Translated.MAIN_TOOLBAR_TOOLS_CONVERT_ISO_NAMING;
		((ToolStripItem)Menu_Tools_DiskIsoZso).Text = Translated.ToolsDiskToIsoZso_Title;
		((ToolStripItem)Menu_Tools_ConvertIsoZso).Text = Translated.ToolsConvertIsoZso_Title;
		((ToolStripItem)Menu_Help).Text = Translated.MAIN_TOOLBAR_HELP;
		((ToolStripItem)Menu_Help_ChangeLog).Text = Translated.MAIN_TOOLBAR_HELP_LOG;
		((ToolStripItem)Menu_Help_Ps2HomeThread).Text = Translated.MAIN_TOOLBAR_HELP_PS2HOME;
		((ToolStripItem)Menu_Help_PSXPlaceThread).Text = Translated.MAIN_TOOLBAR_HELP_PSXPLACE;
		((ToolStripItem)Menu_Help_About).Text = Translated.MAIN_TOOLBAR_HELP_ABOUT;
		((ToolStripItem)Menu_OpenOPLFolder).Text = Translated.MAIN_TOOLBAR_OPEN_OPL_FOLDER;
		((Control)TabHome).Text = Translated.MAIN_TAB_MAIN;
		Col_Game.Text = Translated.MAIN_TAB_MAIN_TABLE_COL_GAME;
		Col_ID.Text = Translated.MAIN_TAB_MAIN_TABLE_COL_ID;
		Col_Type.Text = Translated.MAIN_TAB_MAIN_TABLE_COL_TYPE;
		Col_Size.Text = Translated.MAIN_TAB_MAIN_TABLE_COL_SIZE;
		Col_Medium.Text = Translated.MAIN_TAB_MAIN_TABLE_COL_MEDIUM;
		Col_ArtIco.Text = Translated.GLOBAL_STRING_DISC;
		Col_ArtCov.Text = Translated.GLOBAL_STRING_COVER;
		Col_ArtLab.Text = Translated.GLOBAL_STRING_SPINE;
		Col_ArtCov2.Text = Translated.GLOBAL_STRING_BACK_COVER;
		Col_ArtScr.Text = Translated.GLOBAL_STRING_SCREENSHOTS;
		Col_ArtLgo.Text = Translated.GLOBAL_STRING_LOGO;
		Col_ArtBg.Text = Translated.GLOBAL_STRING_BACKGROUNDS;
		Col_CFG.Text = Translated.MAIN_TAB_MAIN_TABLE_COL_CFG;
		Col_Cht.Text = Translated.OpsCfgEditorCheats;
		((Control)GameDetails).Text = Translated.MAIN_TAB_MAIN_DETAIS;
		((Control)GameDetails_Label_Title).Text = Translated.MAIN_TAB_MAIN_DETAIS_TITLE;
		((Control)GameDetails_Label_Path).Text = Translated.MAIN_TAB_MAIN_DETAIS_PATH;
		((Control)GameDetails_Label_ID).Text = Translated.MAIN_TAB_MAIN_DETAIS_ID;
		((Control)Operations).Text = Translated.MAIN_TAB_MAIN_OPERATIONS;
		((Control)Operations_DeleteGame).Text = Translated.MAIN_TAB_MAIN_OPERATIONS_DELETE;
		((Control)Operations_UpdateTitle).Text = Translated.MAIN_TAB_MAIN_OPERATIONS_TITLE;
		((Control)Operations_Hash).Text = Translated.MAIN_TAB_MAIN_OPERATIONS_HASH;
		((Control)Operations_EditCFG).Text = Translated.MAIN_TAB_MAIN_OPERATIONS_EDIT_CFG;
		((Control)Operations_CheatEditor).Text = Translated.MAIN_TAB_MAIN_OPERATIONS_CHEAT_EDITOR;
		((Control)Operations_ManageARTs).Text = Translated.MAIN_TAB_MAIN_OPERATIONS_MANAGE_ARTS;
		((Control)GlobalStats).Text = Translated.MAIN_TAB_MAIN_GLOBALSTATS;
		((Control)GlobalStats_Count).Text = Translated.MAIN_TAB_MAIN_GLOBALSTATS_COUNT;
		((Control)GlobalStats_Size).Text = Translated.MAIN_TAB_MAIN_GLOBALSTATS_SIZE;
		((Control)GlobalStats_Total).Text = Translated.MAIN_TAB_MAIN_GLOBALSTATS_TOTAL;
		((Control)BadISO_GameDetails_Label_Title).Text = Translated.MAIN_TAB_MAIN_DETAIS_TITLE;
		((Control)BadISO_GameDetails_Label_Path).Text = Translated.MAIN_TAB_MAIN_DETAIS_PATH;
		((Control)BadISO_GameDetails_Label_Size).Text = Translated.MainBad_Size;
		((Control)BadISO_GameDetails_Label_CurrentFile).Text = Translated.MainBad_CurrentFile;
		((Control)BadISO_GameDetails_Label_NewFile).Text = Translated.MainBad_NewFile;
		((Control)BadISO_GetTitleFromDB).Text = Translated.MainBad_GetTitleFromDB;
		((Control)BadISO_UpdateFileName).Text = Translated.MainBad_TryUpdateFileName;
		((Control)BadISO_BatchRenameISO).Text = Translated.ToolsConvertIsoNames_Title;
		((Control)BadISO_BatchRenameVCD).Text = Translated.ToolsConvertVcdNames_Title;
		((Control)TabBadIsos).Text = Translated.MAIN_STRINGS_BAD_ISO_TAB;
	}

	public bool MoveTo(int index)
	{
		if (index < ((ListView)GameList).Items.Count)
		{
			((ListView)GameList).SelectedItems.Clear();
			((ListView)GameList).Items[index].Selected = true;
			return true;
		}
		return false;
	}

	public bool MoveUp()
	{
		if (((ListView)GameList).SelectedIndices[0] > 0)
		{
			int num = ((ListView)GameList).SelectedIndices[0];
			((ListView)GameList).SelectedItems.Clear();
			((ListView)GameList).Items[num - 1].Selected = true;
			return true;
		}
		return false;
	}

	public bool MoveDown()
	{
		if (((ListView)GameList).SelectedIndices[0] < ((ListView)GameList).Items.Count - 1)
		{
			int num = ((ListView)GameList).SelectedIndices[0];
			((ListView)GameList).SelectedItems.Clear();
			((ListView)GameList).Items[num + 1].Selected = true;
			return true;
		}
		return false;
	}

	public void MoveLast()
	{
		((ListView)GameList).SelectedItems.Clear();
		((ListView)GameList).Items[((ListView)GameList).Items.Count - 1].Selected = true;
	}

	public void MoveFirst()
	{
		((ListView)GameList).SelectedItems.Clear();
		((ListView)GameList).Items[0].Selected = true;
	}

	private void OplManagerStart(object sender, EventArgs e)
	{
		//IL_00ab: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b5: Expected O, but got Unknown
		OplmSettings.ReadFile();
		((ToolStripItem)Menu_NetworkOptions).Visible = false;
		((ToolStripItem)Menu_LocalHDDOptions).Visible = false;
		((Control)this).Text = "OPL Manager 24 | By danielb Homepage: http://oplmanager.com";
		Tab_Main.TabPages.Remove(TabBadIsos);
		((Control)Cover_CoverIMG).AllowDrop = true;
		((Control)Disc_DiscIMG).AllowDrop = true;
		UserID = OplmSettings.Read("TRACK_ID", "NEW");
		SetLanguage();
		VersionCheck();
		TrackAndRefreshServerStatus();
		setMode();
		int num = OplmSettings.Read("MainSplitterPos", 0);
		if (num != 0)
		{
			SplitContainer1.SplitterDistance = num;
		}
		SplitContainer1.SplitterMoved += new SplitterEventHandler(SplitContainer1_SplitterMoved);
		CB_Filter_APPS.CheckedChanged += FiltersChanged;
		CB_Filter_POPS.CheckedChanged += FiltersChanged;
		CB_Filter_PS2.CheckedChanged += FiltersChanged;
		SplitContainer2.SplitterDistance = ((Control)SplitContainer2).Width - ((Control)GB_BadIsoDetails).Size.Width;
	}

	public void setMode()
	{
		//IL_0064: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c1: Unknown result type (might be due to invalid IL or missing references)
		//IL_0207: Unknown result type (might be due to invalid IL or missing references)
		//IL_01ed: Unknown result type (might be due to invalid IL or missing references)
		((ToolStripItem)StatusBar_Mode).Text = "-";
		((ToolStripItem)Menu_NetworkOptions).Visible = false;
		((ToolStripItem)Menu_LocalHDDOptions).Visible = false;
		((ToolStripItem)Menu_Tools_ConvertISONaming).Visible = false;
		mode = int.Parse(OplmSettings.Read("MODE", "-1"));
		if (mode == 2)
		{
			MessageBox.Show("USB mode no longer exists, now to use USB Extreme you should use the normal mode. Updating your config.", Translated.Global_Information, (MessageBoxButtons)0, (MessageBoxIcon)64);
			OplmSettings.Write("MODE", 0.ToString());
			OplmSettings.Write("OPL_FOLDER", OplmSettings.Read("MODE_USB_DRIVE", OplmSettings.Read("OPL_FOLDER", "")));
			OplmSettings.Delete("MODE_USB_DRIVE");
			mode = 0;
		}
		if (mode == -1)
		{
			SettingModeSelect settingModeSelect = new SettingModeSelect();
			try
			{
				((Form)settingModeSelect).ShowDialog();
			}
			finally
			{
				((IDisposable)settingModeSelect)?.Dispose();
			}
			setMode();
			return;
		}
		if (mode == 1)
		{
			((ToolStripItem)Menu_NetworkOptions).Visible = true;
			((ToolStripItem)StatusBar_Mode).Text = Translated.GLOBAL_STRING_MODE + " Ethernet | PS2 IP: " + OplmSettings.Read("MODE_NET_IP", "");
			OplFolders.From(app_folder + "\\hdl\\");
			enableControlsNoGamesOrFolder(val: true);
			updateList();
			return;
		}
		if (mode == 3)
		{
			((ToolStripItem)Menu_LocalHDDOptions).Visible = true;
			((ToolStripItem)StatusBar_Mode).Text = Translated.GLOBAL_STRING_MODE + " PS2 HDD";
			OplFolders.From(app_folder + "\\hdl_hdd\\");
			enableControlsNoGamesOrFolder(val: true);
			updateList();
			return;
		}
		((ToolStripItem)Menu_Tools_ConvertISONaming).Visible = true;
		string folder = OplmSettings.Read("OPL_FOLDER", "");
		if (!string.IsNullOrEmpty(folder))
		{
			if (Directory.Exists(folder))
			{
				((ToolStripItem)StatusBar_Mode).Text = Translated.GLOBAL_STRING_MODE + " Local/USB Extreme";
				LoadOplFolder(ref folder);
			}
			else
			{
				MessageBox.Show(Translated.MAIN_STRINGS_OPL_FOLDER_SETTINGS_NOT_EXISTS, Translated.MAIN_STRINGS_OPL_FOLDER_SETTINGS_NOT_EXISTS_TITLE, (MessageBoxButtons)0, (MessageBoxIcon)16);
				chooseOplFolder();
			}
		}
		else
		{
			MessageBox.Show(Translated.MAIN_STRINGS_OPL_FOLDER_NOT_SET, Translated.MAIN_STRINGS_OPL_FOLDER_SETTINGS_NOT_EXISTS_TITLE, (MessageBoxButtons)0, (MessageBoxIcon)16);
			chooseOplFolder();
		}
	}

	private void LoadOplFolder(ref string folder)
	{
		//IL_00b1: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c4: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ca: Invalid comparison between Unknown and I4
		if (Directory.Exists(folder + "\\DVD") && Directory.Exists(folder + "\\CD") && Directory.Exists(folder + "\\ART"))
		{
			OplFolders.From(folder);
			if (mode != 2 && !((OplmSettings.Read("OPL_FOLDER", "") ?? "") == (OplFolders.Main ?? "")))
			{
				OplmSettings.Write("OPL_FOLDER", OplFolders.Main);
			}
			enableControlsNoGamesOrFolder(val: true);
			updateList();
		}
		else
		{
			MessageBox.Show(Translated.MAIN_STRINGS_OPL_FOLDER_LOAD_ERROR + Environment.NewLine + folder, Translated.Global_Error, (MessageBoxButtons)0, (MessageBoxIcon)16);
			if ((int)MessageBox.Show(Translated.MainCreateFoldersQuestion, Translated.Global_Question, (MessageBoxButtons)4, (MessageBoxIcon)32) == 6)
			{
				OplFolders.From(folder);
				OplFolders.CreateFolders();
				LoadOplFolder(ref folder);
			}
			else
			{
				enableControlsNoGamesOrFolder(val: false);
			}
		}
	}

	public void updateList(bool SkipMsgBoxBadIsosFound = false, bool invalidateCache = false)
	{
		//IL_02db: Unknown result type (might be due to invalid IL or missing references)
		//IL_02e2: Expected O, but got Unknown
		//IL_0148: Unknown result type (might be due to invalid IL or missing references)
		//IL_014f: Expected O, but got Unknown
		//IL_0786: Unknown result type (might be due to invalid IL or missing references)
		Application.DoEvents();
		((Control)GameList).Focus();
		string text = "";
		if (((ListView)GameList).SelectedItems.Count == 1)
		{
			text = ((ListView)GameList).SelectedItems[0].SubItems[1].Text;
		}
		Application.DoEvents();
		Tab_Main.TabPages.Remove(TabBadIsos);
		((ListView)GameList).Items.Clear();
		BadIsoList.Items.Clear();
		Application.DoEvents();
		if (mode == 1)
		{
			GameProvider.From(CommonFuncs.Modes.Ethernet, OplmSettings.Read("MODE_NET_IP", ""), invalidateCache);
		}
		else if (mode == 3)
		{
			GameProvider.From(CommonFuncs.Modes.HDD, null, invalidateCache);
		}
		else
		{
			GameProvider.From(CommonFuncs.Modes.Normal, null, invalidateCache);
		}
		List<ListViewItem> list = new List<ListViewItem>();
		List<ListViewItem> list2 = new List<ListViewItem>();
		foreach (GameInfo game in GameProvider.GameList)
		{
			if (!game.IsBad)
			{
				if ((game.Type != GameType.PS2 || CB_Filter_PS2.Checked) && (game.Type != GameType.POPS || CB_Filter_POPS.Checked) && (game.Type != GameType.APP || CB_Filter_APPS.Checked))
				{
					ListViewItem val = new ListViewItem();
					val.Text = game.Title;
					val.Tag = game.ItemID;
					val.SubItems.Add((((game.Type == GameType.PS2) | (game.Type == GameType.POPS)) && game.ID != null) ? game.ID : "");
					val.SubItems.Add(game.TypeText);
					val.SubItems.Add(CommonFuncs.FormatFileSize(game.Size)).Tag = game.Size;
					val.SubItems.Add(((game.Type == GameType.PS2) | (game.Type == GameType.POPS)) ? game.DiscTypeText : "");
					val.SubItems.Add(game.HasCFGtext);
					val.SubItems.Add(game.HasCHTtext);
					val.SubItems.Add(game.HasICOtext);
					val.SubItems.Add(game.HasCOVtext);
					val.SubItems.Add(game.HasLABtext);
					val.SubItems.Add(game.HasCOV2text);
					val.SubItems.Add(game.HasSCRtext);
					val.SubItems.Add(game.HasLGOtext);
					val.SubItems.Add(game.HasBGtext);
					list.Add(val);
				}
			}
			else
			{
				ListViewItem val2 = new ListViewItem();
				val2.Text = Path.GetFileNameWithoutExtension(game.FileDiscOnly);
				val2.Tag = game.ItemID;
				val2.SubItems.Add(game.ID);
				val2.SubItems.Add(game.TypeText);
				val2.SubItems.Add(CommonFuncs.FormatFileSize(game.Size));
				val2.SubItems.Add(game.DiscTypeText);
				val2.SubItems.Add(game.BadMsg);
				list2.Add(val2);
			}
		}
		if (list.Count > 0)
		{
			((ListView)GameList).BeginUpdate();
			((ListView)GameList).Items.AddRange(list.ToArray());
			((ListView)GameList).EndUpdate();
			Application.DoEvents();
		}
		if (list2.Count > 0)
		{
			BadIsoList.BeginUpdate();
			BadIsoList.Items.AddRange(list2.ToArray());
			BadIsoList.EndUpdate();
			Application.DoEvents();
		}
		IEnumerable<GameInfo> source = GameProvider.GameList.Where((GameInfo x) => !x.IsBad && x.Type == GameType.PS2 && x.DiscType == CommonFuncs.DiscType.CD);
		int num = source.Count();
		long num2 = source.Sum((GameInfo x) => x.Size);
		IEnumerable<GameInfo> source2 = GameProvider.GameList.Where((GameInfo x) => !x.IsBad && x.Type == GameType.PS2 && x.DiscType == CommonFuncs.DiscType.DVD);
		int num3 = source2.Count();
		long num4 = source2.Sum((GameInfo x) => x.Size);
		((Control)GlobalStats_PS2CD_Count).Text = num.ToString();
		((Control)GlobalStats_PS2CD_Size).Text = CommonFuncs.FormatFileSize(num2);
		((Control)GlobalStats_PS2DVD_Count).Text = num3.ToString();
		((Control)GlobalStats_PS2DVD_Size).Text = CommonFuncs.FormatFileSize(num4);
		((Control)GlobalStats_PS2_Count).Text = (num + num3).ToString();
		((Control)GlobalStats_PS2_Size).Text = CommonFuncs.FormatFileSize(num2 + num4);
		IEnumerable<GameInfo> source3 = GameProvider.GameList.Where((GameInfo x) => !x.IsBad && x.Type == GameType.POPS);
		((Control)GlobalStats_PS1_Count).Text = source3.Count().ToString();
		((Control)GlobalStats_PS1_Size).Text = CommonFuncs.FormatFileSize(source3.Sum((GameInfo x) => x.Size));
		IEnumerable<GameInfo> source4 = GameProvider.GameList.Where((GameInfo x) => !x.IsBad && x.Type == GameType.APP);
		((Control)GlobalStats_APP_Count).Text = source4.Count().ToString();
		((Control)GlobalStats_APP_Size).Text = CommonFuncs.FormatFileSize(source4.Sum((GameInfo x) => x.Size));
		IEnumerable<GameInfo> source5 = GameProvider.GameList.Where((GameInfo x) => !x.IsBad);
		((Control)GlobalStats_CountTotal).Text = source5.Count().ToString();
		((Control)GlobalStats_SizeTotal).Text = CommonFuncs.FormatFileSize(source5.Sum((GameInfo x) => x.Size));
		Application.DoEvents();
		UpdateOrder(0, (SortOrder)1);
		((ListView)GameList).AutoResizeColumns((ColumnHeaderAutoResizeStyle)1);
		if (((ListView)GameList).Items.Count > 0)
		{
			((ListView)GameList).Items[0].Selected = true;
		}
		Application.DoEvents();
		if (list2.Count > 0)
		{
			Tab_Main.TabPages.Insert(1, TabBadIsos);
			BadIsoList.ListViewItemSorter = new ListViewComparer(0, (SortOrder)1);
			BadIsoList.Columns[0].Width = -1;
			BadIsoList.Columns[1].Width = -1;
			BadIsoList.Columns[2].Width = -1;
			if (!SkipMsgBoxBadIsosFound)
			{
				MessageBox.Show(string.Format(Translated.MAIN_STRINGS_BAD_ISO_WARN, list2.Count, Translated.MAIN_STRINGS_BAD_ISO_TAB), Translated.Global_Information, (MessageBoxButtons)0, (MessageBoxIcon)64);
				Tab_Main.SelectTab(1);
			}
		}
		int num5 = 0;
		for (int num6 = ((ListView)GameList).Items.Count - 1; num5 <= num6; num5++)
		{
			if (GameProvider.get_GetGame((int)((ListView)GameList).Items[num5].Tag).ID == text)
			{
				((ListView)GameList).Items[num5].Selected = true;
			}
		}
		if (((ListView)GameList).Items.Count > 0)
		{
			enableControlsNoGamesOrFolder(val: true);
		}
		else
		{
			enableControlsNoGamesOrFolder(val: false);
		}
	}

	private void enableControlsNoGamesOrFolder(bool val)
	{
		((Control)GameDetails).Enabled = val;
		((ToolStripItem)Menu_BatchActions).Enabled = val;
		((ToolStripItem)Menu_View).Enabled = val;
		((ToolStripItem)Menu_OpenOPLFolder).Enabled = val;
		((ToolStripItem)Menu_File_Refresh).Enabled = val;
	}

	public void UpdateDetails()
	{
		GameInfo selectedGame = SelectedGame;
		selectedGame.UpdateGameInfo();
		((Control)GameDetails_TB_ID).Text = selectedGame.ID;
		((Control)GameDetails_TB_Path).Text = selectedGame.FileDiscFullPath;
		((Control)GameDetails_TB_Title).Text = selectedGame.Title;
		((Control)GameDetails_TB_Title).Enabled = selectedGame.Device == CommonFuncs.Modes.Normal;
		((Control)Operations_DeleteGame).Enabled = selectedGame.Device == CommonFuncs.Modes.Normal;
		((Control)Operations_UpdateTitle).Enabled = selectedGame.Device == CommonFuncs.Modes.Normal && selectedGame.Type != GameType.POPS;
		((Control)Operations_Hash).Enabled = selectedGame.Device == CommonFuncs.Modes.Normal && selectedGame.Type == GameType.PS2;
		((Control)Operations_CheatEditor).Enabled = (selectedGame.Type == GameType.PS2) | (selectedGame.Type == GameType.POPS);
		((Control)Operations_EditCFG).Enabled = !string.IsNullOrEmpty(selectedGame.ExpectedCFGFilePath);
		if (selectedGame.HasCOV)
		{
			Cover_CoverIMG.Image = (Image)(object)CommonFuncs.ReadImageToBitmap(selectedGame.FileArtCOV);
		}
		else if (selectedGame.Type == GameType.POPS)
		{
			Cover_CoverIMG.Image = (Image)(object)Resources.ps1_front_cover;
		}
		else
		{
			Cover_CoverIMG.Image = (Image)(object)Resources.art_front;
		}
		if (selectedGame.HasICO)
		{
			Disc_DiscIMG.Image = (Image)(object)CommonFuncs.ReadImageToBitmap(selectedGame.FileArtICO);
		}
		else if (selectedGame.Type == GameType.POPS)
		{
			Disc_DiscIMG.Image = (Image)(object)Resources.ps1_disc_cover_small;
		}
		else
		{
			Disc_DiscIMG.Image = (Image)(object)Resources.art_disc;
		}
	}

	private void GameList_LaunchApp(object sender, EventArgs e)
	{
		//IL_0103: Unknown result type (might be due to invalid IL or missing references)
		if (((ListView)GameList).SelectedItems.Count != 1)
		{
			return;
		}
		if (OplmSettings.Read("DoubleClickEnabled", predef: false))
		{
			string text = OplmSettings.Read("DoubleClickExe", "");
			string text2 = OplmSettings.Read("DoubleClickParams", "");
			if (!(File.Exists(text) & (text2.Length > 0)))
			{
				return;
			}
			GameInfo selectedGame = SelectedGame;
			if (selectedGame.Type == GameType.PS2)
			{
				text2 = text2.Replace("[FILE]", "\"" + selectedGame.FileDiscFullPath + "\"");
				text2 = text2.Replace("[ID]", selectedGame.ID);
				using Process process = new Process();
				process.StartInfo.FileName = text;
				process.StartInfo.Arguments = text2;
				process.StartInfo.UseShellExecute = false;
				process.StartInfo.RedirectStandardOutput = false;
				process.StartInfo.WindowStyle = ProcessWindowStyle.Normal;
				process.Start();
			}
		}
		else
		{
			MessageBox.Show(Translated.MAIN_STRINGS_DOUBLECLICK_HINTMSG, Translated.Global_Information, (MessageBoxButtons)0, (MessageBoxIcon)64);
		}
	}

	private void GameList_ItemSelectionChanged(object sender, ListViewItemSelectionChangedEventArgs e)
	{
		if (e.IsSelected & (((ListView)GameList).SelectedItems.Count == 1))
		{
			((Control)GameDetails).Enabled = true;
			UpdateDetails();
		}
		else
		{
			((Control)GameDetails).Enabled = false;
		}
	}

	private void chooseOplFolder()
	{
		//IL_000d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0013: Invalid comparison between Unknown and I4
		//IL_0033: Unknown result type (might be due to invalid IL or missing references)
		//IL_0039: Expected O, but got Unknown
		//IL_0041: Unknown result type (might be due to invalid IL or missing references)
		//IL_0047: Invalid comparison between Unknown and I4
		if ((int)MessageBox.Show(Translated.MAIN_STRINGS_CHOOSE_OPL_FOLDER, Translated.Global_Question, (MessageBoxButtons)4, (MessageBoxIcon)32) == 6)
		{
			string folder = CommonFuncs.ShowInputDialog(Translated.MAIN_STRINGS_CHOOSE_OPL_FOLDER_INPUT, "");
			LoadOplFolder(ref folder);
			return;
		}
		FolderBrowserDialog val = new FolderBrowserDialog();
		try
		{
			val.RootFolder = Environment.SpecialFolder.Desktop;
			if ((int)((CommonDialog)val).ShowDialog() == 1)
			{
				string folder2 = val.SelectedPath;
				LoadOplFolder(ref folder2);
				val.SelectedPath = folder2;
			}
		}
		finally
		{
			((IDisposable)val)?.Dispose();
		}
	}

	private bool TryRenameGameIsoOrZso(string Game_Title, string Game_ID, string fileOrigPath)
	{
		//IL_0148: Unknown result type (might be due to invalid IL or missing references)
		//IL_0033: Unknown result type (might be due to invalid IL or missing references)
		//IL_0078: Unknown result type (might be due to invalid IL or missing references)
		//IL_017f: Unknown result type (might be due to invalid IL or missing references)
		//IL_015f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0119: Unknown result type (might be due to invalid IL or missing references)
		//IL_011f: Invalid comparison between Unknown and I4
		string text = Path.GetExtension(fileOrigPath);
		if (string.IsNullOrEmpty(text))
		{
			text = ".iso";
		}
		text = text.ToLower();
		if (Game_Title.Length > 32)
		{
			MessageBox.Show(Translated.MAIN_STRINGS_RENAME_FILE_BIG_NAME, Translated.MAIN_STRINGS_RENAME_INVALID_TITLE, (MessageBoxButtons)0, (MessageBoxIcon)16);
			return false;
		}
		if ((CommonFuncs.SanitizeGameTitle(Game_Title.Trim()) ?? "") != (Game_Title ?? ""))
		{
			MessageBox.Show(Translated.MAIN_STRINGS_RENAME_INVALID_CHARS + " A-Z  a-z  0-9  -  _ () []", Translated.MAIN_STRINGS_RENAME_INVALID_TITLE, (MessageBoxButtons)0, (MessageBoxIcon)16);
			return false;
		}
		if (File.Exists(fileOrigPath))
		{
			string text2 = Path.Combine(path2: (!OplmSettings.ReadBool("ISO_USE_OLD_NAMING_FORMAT")) ? (Game_Title + text) : (Game_ID + "." + Game_Title + text), path1: new FileInfo(fileOrigPath).DirectoryName);
			if ((text2 ?? "") == (fileOrigPath ?? ""))
			{
				return true;
			}
			if (!File.Exists(text2))
			{
				if ((int)MessageBox.Show(fileOrigPath + Environment.NewLine + " => " + Environment.NewLine + text2, Translated.MAIN_STRINGS_RENAME_CONFIRM, (MessageBoxButtons)4, (MessageBoxIcon)32) == 6)
				{
					try
					{
						File.Move(fileOrigPath, text2);
					}
					catch (Exception ex)
					{
						MessageBox.Show(Translated.MAIN_STRINGS_RENAME_FILE_FAILED + Environment.NewLine + ex.Message, Translated.Global_Error, (MessageBoxButtons)0, (MessageBoxIcon)16);
					}
					return true;
				}
			}
			else
			{
				MessageBox.Show(Translated.MAIN_STRINGS_RENAME_FILE_EXISTS, Translated.Global_Error, (MessageBoxButtons)0, (MessageBoxIcon)16);
			}
		}
		else
		{
			MessageBox.Show(Translated.MAIN_STRINGS_RENAME_FILE_ORIGINAL + Environment.NewLine + fileOrigPath, Translated.Global_Error, (MessageBoxButtons)0, (MessageBoxIcon)16);
		}
		return false;
	}

	private bool TryRenameGameVCD(string Game_Title, string Game_ID, string fileOrigPath)
	{
		//IL_0017: Unknown result type (might be due to invalid IL or missing references)
		//IL_005c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0161: Unknown result type (might be due to invalid IL or missing references)
		//IL_0141: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d6: Unknown result type (might be due to invalid IL or missing references)
		//IL_00dc: Invalid comparison between Unknown and I4
		if (Game_Title.Length > 32)
		{
			MessageBox.Show(Translated.MAIN_STRINGS_RENAME_FILE_BIG_NAME, Translated.MAIN_STRINGS_RENAME_INVALID_TITLE, (MessageBoxButtons)0, (MessageBoxIcon)16);
			return false;
		}
		if ((CommonFuncs.SanitizeGameTitle(Game_Title.Trim()) ?? "") != (Game_Title ?? ""))
		{
			MessageBox.Show(Translated.MAIN_STRINGS_RENAME_INVALID_CHARS + " A-Z  a-z  0-9  -  _ () []", Translated.MAIN_STRINGS_RENAME_INVALID_TITLE, (MessageBoxButtons)0, (MessageBoxIcon)16);
			return false;
		}
		if (File.Exists(fileOrigPath))
		{
			string text = Game_ID + "." + Game_Title + ".VCD";
			string text2 = new FileInfo(fileOrigPath).DirectoryName + "\\" + text;
			if (!File.Exists(text2))
			{
				if ((int)MessageBox.Show("Rename: " + fileOrigPath + Environment.NewLine + "To:" + text2, "Rename confirm", (MessageBoxButtons)4, (MessageBoxIcon)32) == 6)
				{
					File.Move(fileOrigPath, text2);
					string text3 = Path.Combine(new FileInfo(fileOrigPath).DirectoryName, Path.GetFileNameWithoutExtension(fileOrigPath));
					string path = Path.Combine(new FileInfo(fileOrigPath).DirectoryName, Path.GetFileNameWithoutExtension(text));
					if (Directory.Exists(text3) && !Directory.Exists(path))
					{
						Directory.Move(text3, Path.GetFileNameWithoutExtension(text));
					}
					return true;
				}
			}
			else
			{
				MessageBox.Show(Translated.MAIN_STRINGS_RENAME_FILE_EXISTS, Translated.Global_Error, (MessageBoxButtons)0, (MessageBoxIcon)16);
			}
		}
		else
		{
			MessageBox.Show(Translated.MAIN_STRINGS_RENAME_FILE_ORIGINAL + Environment.NewLine + fileOrigPath, Translated.Global_Error, (MessageBoxButtons)0, (MessageBoxIcon)16);
		}
		return false;
	}

	private void BadISO_UpdateFileName_Click(object sender, EventArgs e)
	{
		GameInfo gameInfo = GameProvider.get_GetGame((int)BadIsoList.SelectedItems[0].Tag);
		bool flag = false;
		if (gameInfo.Type == GameType.PS2)
		{
			flag = TryRenameGameIsoOrZso(((Control)BadIso_title).Text, ((Control)BadISO_ID).Text, ((Control)BadISO_Path).Text);
		}
		else if (gameInfo.Type == GameType.POPS)
		{
			flag = TryRenameGameVCD(((Control)BadIso_title).Text, ((Control)BadISO_ID).Text, ((Control)BadISO_Path).Text);
		}
		if (flag)
		{
			((Control)BadISO_ID).Enabled = false;
			if (BadIsoList.Items.Count > 1)
			{
				updateList(SkipMsgBoxBadIsosFound: true);
				return;
			}
			Tab_Main.SelectTab(0);
			updateList();
		}
	}

	private void BadIsoList_ItemSelectionChanged(object sender, ListViewItemSelectionChangedEventArgs e)
	{
		if (e.IsSelected)
		{
			BadISO_updateDetails();
		}
	}

	private void BadISO_updateDetails()
	{
		GameInfo gameInfo = GameProvider.get_GetGame((int)BadIsoList.SelectedItems[0].Tag);
		((Control)BadISO_Path).Text = gameInfo.FileDiscFullPath;
		((Control)BadISO_CurrentFileName).Text = gameInfo.FileDiscOnly;
		((Control)BadISO_ID).Text = BadIsoList.SelectedItems[0].SubItems[1].Text;
		((Control)BadIso_title).Text = BadIsoList.SelectedItems[0].Text.Replace(((Control)BadISO_ID).Text + ".", "");
		((Control)BadIso_title).Tag = BadIsoList.SelectedItems[0].Text;
		((Control)BadISO_Size).Text = BadIsoList.SelectedItems[0].SubItems[3].Text;
		((Control)BadISO_errorMsg).Text = gameInfo.BadMsg;
		if (OplmSettings.ReadBool("ISO_USE_OLD_NAMING_FORMAT"))
		{
			((Control)BadISO_newfile).Text = ((Control)BadISO_ID).Text + "." + ((Control)BadIso_title).Text + Path.GetExtension(((Control)BadISO_CurrentFileName).Text);
		}
		else
		{
			((Control)BadISO_newfile).Text = ((Control)BadIso_title).Text + Path.GetExtension(((Control)BadISO_CurrentFileName).Text);
		}
		((Control)BadISO_BatchRenameVCD).BackColor = Color.Transparent;
		if (gameInfo.Type == GameType.POPS)
		{
			((Control)GB_BadIsoDetails).Enabled = true;
			((Control)BadISO_BatchRenameISO).Enabled = false;
			((Control)BadISO_BatchRenameVCD).Enabled = true;
			((Control)BadISO_BatchRenameVCD).BackColor = Color.LightGreen;
		}
		else if (gameInfo.Type == GameType.PS2)
		{
			((Control)GB_BadIsoDetails).Enabled = true;
			((Control)BadISO_BatchRenameISO).Enabled = true;
			((Control)BadISO_BatchRenameVCD).Enabled = false;
		}
		((TextBoxBase)BadISO_ID).ReadOnly = true;
	}

	private void BadIso_GameTitleChanged(object sender, EventArgs e)
	{
		((Control)BadISO_newfile).Text = ((Control)BadISO_ID).Text + "." + ((Control)BadIso_title).Text + Path.GetExtension(((Control)BadISO_CurrentFileName).Text);
	}

	private void BadISO_GetTitleFromDB_Click(object sender, EventArgs e)
	{
		//IL_0082: Unknown result type (might be due to invalid IL or missing references)
		//IL_0074: Unknown result type (might be due to invalid IL or missing references)
		try
		{
			GameInfo gameInfo = GameProvider.get_GetGame((int)BadIsoList.SelectedItems[0].Tag);
			((Control)BadIso_title).Text = CommonFuncs.SanitizeGameTitle(service.GetGameNameById(gameInfo.Type, ((Control)BadISO_ID).Text));
			if (((Control)BadIso_title).Text.Trim().Length > 32)
			{
				MessageBox.Show(Translated.MAIN_STRINGS_RENAME_FILE_BIG_NAME, Translated.MAIN_STRINGS_RENAME_INVALID_TITLE, (MessageBoxButtons)0, (MessageBoxIcon)16);
			}
		}
		catch (Exception)
		{
			MessageBox.Show("Error fetching game title From server!");
		}
	}

	private void BadISO_BatchRename_Click(object sender, EventArgs e)
	{
		//IL_0006: Unknown result type (might be due to invalid IL or missing references)
		//IL_000c: Invalid comparison between Unknown and I4
		ToolsConvertIsoNames toolsConvertIsoNames = new ToolsConvertIsoNames();
		if ((int)((Form)toolsConvertIsoNames).ShowDialog() == 1)
		{
			updateList();
		}
		((Component)(object)toolsConvertIsoNames).Dispose();
	}

	private void BatchCoverAndIconDownloadToolStripMenuItem_Click_1(object sender, EventArgs e)
	{
		//IL_000d: Unknown result type (might be due to invalid IL or missing references)
		BatchArtDownload batchArtDownload = new BatchArtDownload(UserID);
		try
		{
			((Form)batchArtDownload).ShowDialog();
			Application.DoEvents();
			updateList(SkipMsgBoxBadIsosFound: true);
		}
		finally
		{
			((IDisposable)batchArtDownload)?.Dispose();
		}
	}

	private void MoveToNextGame(bool direction)
	{
		int num = ((ListView)GameList).SelectedItems[0].Index;
		do
		{
			num = (direction ? ((num != ((ListView)GameList).Items.Count - 1) ? (num + 1) : 0) : ((num != 0) ? (num - 1) : (((ListView)GameList).Items.Count - 1)));
		}
		while (string.IsNullOrEmpty(GameProvider.get_GetGame((int)((ListView)GameList).Items[num].Tag).ExpectedCFGFilePath));
		((ListView)GameList).SelectedItems.Clear();
		((ListView)GameList).Items[num].Selected = true;
		((Control)GameList).Select();
		UpdateDetails();
	}

	private void SpawnCFGeditor()
	{
		//IL_0007: Unknown result type (might be due to invalid IL or missing references)
		//IL_000c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0019: Unknown result type (might be due to invalid IL or missing references)
		//IL_001b: Invalid comparison between Unknown and I4
		//IL_001d: Unknown result type (might be due to invalid IL or missing references)
		//IL_001f: Invalid comparison between Unknown and I4
		//IL_0022: Unknown result type (might be due to invalid IL or missing references)
		//IL_0024: Invalid comparison between Unknown and I4
		//IL_0029: Unknown result type (might be due to invalid IL or missing references)
		//IL_002b: Invalid comparison between Unknown and I4
		//IL_0036: Unknown result type (might be due to invalid IL or missing references)
		//IL_0038: Invalid comparison between Unknown and I4
		OpsCfgEditor opsCfgEditor = new OpsCfgEditor();
		DialogResult val;
		try
		{
			val = ((Form)opsCfgEditor).ShowDialog();
		}
		finally
		{
			((IDisposable)opsCfgEditor)?.Dispose();
		}
		if ((int)val == 6 || (int)val == 7 || (int)val == 4)
		{
			if ((int)val == 6)
			{
				MoveToNextGame(direction: true);
			}
			else if ((int)val == 7)
			{
				MoveToNextGame(direction: false);
			}
			UpdateDetails();
			SpawnCFGeditor();
		}
	}

	private void B_EditCFG_Click(object sender, EventArgs e)
	{
		SpawnCFGeditor();
		((Control)GameList).Focus();
	}

	private void ShareARTzipToolStripMenuItem_Click(object sender, EventArgs e)
	{
		//IL_0013: Unknown result type (might be due to invalid IL or missing references)
		BatchArtShare batchArtShare = new BatchArtShare(UserID);
		try
		{
			((Form)batchArtShare).ShowDialog((IWin32Window)(object)GameList);
		}
		finally
		{
			((IDisposable)batchArtShare)?.Dispose();
		}
	}

	private void ShowAllToolStripMenuItem_Click(object sender, EventArgs e)
	{
		updateList();
	}

	private void MissingCoverArt_Click(object sender, EventArgs e)
	{
		updateList();
		for (int num = ((ListView)GameList).Items.Count - 1; num >= 0; num--)
		{
			if (GameProvider.get_GetGame((int)((ListView)GameList).Items[num].Tag).HasCOV)
			{
				((ListView)GameList).Items.RemoveAt(num);
			}
		}
	}

	private void DiscArtToolStripMenuItem_Click(object sender, EventArgs e)
	{
		updateList();
		for (int num = ((ListView)GameList).Items.Count - 1; num >= 0; num--)
		{
			if (GameProvider.get_GetGame((int)((ListView)GameList).Items[num].Tag).HasICO)
			{
				((ListView)GameList).Items.RemoveAt(num);
			}
		}
	}

	private void CoverAndOrDiscArtToolStripMenuItem_Click(object sender, EventArgs e)
	{
		updateList();
		for (int num = ((ListView)GameList).Items.Count - 1; num >= 0; num--)
		{
			if (GameProvider.get_GetGame((int)((ListView)GameList).Items[num].Tag).HasICO && GameProvider.get_GetGame((int)((ListView)GameList).Items[num].Tag).HasCOV)
			{
				((ListView)GameList).Items.RemoveAt(num);
			}
		}
	}

	private void OnlyCoverArt_Click(object sender, EventArgs e)
	{
		updateList();
		for (int num = ((ListView)GameList).Items.Count - 1; num >= 0; num--)
		{
			if (!GameProvider.get_GetGame((int)((ListView)GameList).Items[num].Tag).HasCOV)
			{
				((ListView)GameList).Items.RemoveAt(num);
			}
		}
	}

	private void OnlyDiscArt_Click(object sender, EventArgs e)
	{
		updateList();
		for (int num = ((ListView)GameList).Items.Count - 1; num >= 0; num--)
		{
			if (!GameProvider.get_GetGame((int)((ListView)GameList).Items[num].Tag).HasICO)
			{
				((ListView)GameList).Items.RemoveAt(num);
			}
		}
	}

	private void OnlyCoverAndorDiscArt_Click(object sender, EventArgs e)
	{
		updateList();
		for (int num = ((ListView)GameList).Items.Count - 1; num >= 0; num--)
		{
			if (!GameProvider.get_GetGame((int)((ListView)GameList).Items[num].Tag).HasICO | !GameProvider.get_GetGame((int)((ListView)GameList).Items[num].Tag).HasCOV)
			{
				((ListView)GameList).Items.RemoveAt(num);
			}
		}
	}

	private void GameDoubleClickActionToolStripMenuItem_Click(object sender, EventArgs e)
	{
		//IL_0005: Unknown result type (might be due to invalid IL or missing references)
		((Form)new SettingsGameDoubleCLick()).ShowDialog();
	}

	private void UpgradeOldCFGsToolStripMenuItem_Click(object sender, EventArgs e)
	{
		//IL_0007: Unknown result type (might be due to invalid IL or missing references)
		BatchCfgUpdate batchCfgUpdate = new BatchCfgUpdate();
		try
		{
			((Form)batchCfgUpdate).ShowDialog();
		}
		finally
		{
			((IDisposable)batchCfgUpdate)?.Dispose();
		}
	}

	private void OpenOPLFolderToolStripMenuItem_Click(object sender, EventArgs e)
	{
		Process.Start("explorer.exe", OplFolders.Main);
	}

	private void GameList_ColumnClick(object sender, ColumnClickEventArgs e)
	{
		UpdateOrder(e.Column, (SortOrder)((SortColumn != e.Column) ? 2 : 0));
	}

	private void UpdateOrder(int newCol, SortOrder ReorderBy = (SortOrder)0)
	{
		//IL_000b: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e9: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ef: Invalid comparison between Unknown and I4
		//IL_0018: Unknown result type (might be due to invalid IL or missing references)
		//IL_002b: Unknown result type (might be due to invalid IL or missing references)
		//IL_002c: Unknown result type (might be due to invalid IL or missing references)
		//IL_007d: Unknown result type (might be due to invalid IL or missing references)
		//IL_007f: Invalid comparison between Unknown and I4
		//IL_00b1: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b3: Invalid comparison between Unknown and I4
		//IL_01b1: Unknown result type (might be due to invalid IL or missing references)
		//IL_01b7: Invalid comparison between Unknown and I4
		//IL_017e: Unknown result type (might be due to invalid IL or missing references)
		//IL_01ef: Unknown result type (might be due to invalid IL or missing references)
		//IL_01bb: Unknown result type (might be due to invalid IL or missing references)
		((ListView)GameList).BeginUpdate();
		if ((int)ReorderBy != 0)
		{
			((ListView)GameList).ListViewItemSorter = new ListViewComparer(newCol, ReorderBy);
			SortColumn = newCol;
			mSortDirection = ReorderBy;
			((ListView)GameList).Columns[SortColumn].Text = ((ListView)GameList).Columns[SortColumn].Text.TrimEnd(' ', ' ', ' ', '▲', '▼');
			if ((int)ReorderBy == 1)
			{
				ColumnHeader obj = ((ListView)GameList).Columns[SortColumn];
				obj.Text += " ▲";
			}
			else if ((int)ReorderBy == 2)
			{
				ColumnHeader obj2 = ((ListView)GameList).Columns[SortColumn];
				obj2.Text += " ▼";
			}
		}
		else
		{
			if ((int)mSortDirection == 1)
			{
				((ListView)GameList).ListViewItemSorter = new ListViewComparer(newCol, (SortOrder)2);
			}
			else
			{
				((ListView)GameList).ListViewItemSorter = new ListViewComparer(newCol, (SortOrder)1);
			}
			if (SortColumn != -1)
			{
				((ListView)GameList).Columns[SortColumn].Text = ((ListView)GameList).Columns[SortColumn].Text.TrimEnd(' ', ' ', ' ', '▲', '▼');
			}
			if (newCol != SortColumn)
			{
				SortColumn = newCol;
				mSortDirection = (SortOrder)1;
				ColumnHeader obj3 = ((ListView)GameList).Columns[SortColumn];
				obj3.Text += " ▲";
			}
			else if ((int)mSortDirection == 1)
			{
				mSortDirection = (SortOrder)2;
				ColumnHeader obj4 = ((ListView)GameList).Columns[SortColumn];
				obj4.Text += " ▼";
			}
			else
			{
				mSortDirection = (SortOrder)1;
				ColumnHeader obj5 = ((ListView)GameList).Columns[SortColumn];
				obj5.Text += " ▲";
			}
		}
		((ListView)GameList).EndUpdate();
	}

	private void Button1_Click(object sender, EventArgs e)
	{
		((TextBoxBase)BadISO_ID).ReadOnly = false;
	}

	private void B_Hash_Click(object sender, EventArgs e)
	{
		//IL_001d: Unknown result type (might be due to invalid IL or missing references)
		OpsGameHash opsGameHash = new OpsGameHash();
		try
		{
			opsGameHash.ShowDialog(((Control)GameDetails_TB_Path).Text, ((Control)GameDetails_TB_ID).Text);
		}
		finally
		{
			((IDisposable)opsGameHash)?.Dispose();
		}
		((Control)GameList).Focus();
	}

	private void B_DeleteGame_Click(object sender, EventArgs e)
	{
		//IL_0028: Unknown result type (might be due to invalid IL or missing references)
		//IL_002e: Invalid comparison between Unknown and I4
		if ((int)MessageBox.Show(string.Format(Translated.MAIN_STRINGS_DELETE_GAME_PROMPT, SelectedGame.Title, SelectedGame.FileDiscFullPath), Translated.MAIN_TAB_MAIN_OPERATIONS_DELETE, (MessageBoxButtons)4, (MessageBoxIcon)64) == 6)
		{
			File.Delete(SelectedGame.FileDiscFullPath);
			updateList();
		}
	}

	private void RefreshListToolStripMenuItem_Click(object sender, EventArgs e)
	{
		updateList();
	}

	private void InvalidateCacheAndRefreshToolStripMenuItem_Click(object sender, EventArgs e)
	{
		updateList(SkipMsgBoxBadIsosFound: false, invalidateCache: true);
	}

	private void ExitToolStripMenuItem_Click(object sender, EventArgs e)
	{
		((Form)this).Close();
	}

	private void AboutToolStripMenuItem1_Click(object sender, EventArgs e)
	{
		//IL_0007: Unknown result type (might be due to invalid IL or missing references)
		HelpAboutOPLManager helpAboutOPLManager = new HelpAboutOPLManager();
		try
		{
			((Form)helpAboutOPLManager).ShowDialog();
		}
		finally
		{
			((IDisposable)helpAboutOPLManager)?.Dispose();
		}
	}

	private void ChangeLogToolStripMenuItem_Click(object sender, EventArgs e)
	{
		//IL_0007: Unknown result type (might be due to invalid IL or missing references)
		HelpChangeLogWindow helpChangeLogWindow = new HelpChangeLogWindow();
		try
		{
			((Form)helpChangeLogWindow).ShowDialog();
		}
		finally
		{
			((IDisposable)helpChangeLogWindow)?.Dispose();
		}
	}

	private void B_UpdateTitle_Click(object sender, EventArgs e)
	{
		//IL_010c: Unknown result type (might be due to invalid IL or missing references)
		//IL_00dd: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e3: Invalid comparison between Unknown and I4
		string text = CommonFuncs.ShowInputDialog(Translated.GLOBAL_TITLE, Translated.MAIN_STRINGS_RENAME_PROMPT, ((Control)GameDetails_TB_Title).Text);
		string fileDiscFullPath = SelectedGame.FileDiscFullPath;
		if (string.IsNullOrEmpty(text) || string.IsNullOrEmpty(fileDiscFullPath) || !File.Exists(fileDiscFullPath))
		{
			return;
		}
		if (SelectedGame.Type == GameType.PS2)
		{
			if (TryRenameGameIsoOrZso(text, ((Control)GameDetails_TB_ID).Text, fileDiscFullPath))
			{
				updateList();
			}
		}
		else
		{
			if (SelectedGame.Type == GameType.POPS)
			{
				return;
			}
			string text2 = Path.Combine(Path.GetDirectoryName(fileDiscFullPath), text + Path.GetExtension(SelectedGame.FileDiscFullPath));
			if ((int)MessageBox.Show(fileDiscFullPath + Environment.NewLine + " => " + Environment.NewLine + text2, Translated.MAIN_STRINGS_RENAME_CONFIRM, (MessageBoxButtons)4, (MessageBoxIcon)32) == 6)
			{
				try
				{
					File.Move(fileDiscFullPath, text2);
				}
				catch (Exception ex)
				{
					MessageBox.Show(Translated.MAIN_STRINGS_RENAME_FILE_FAILED + Environment.NewLine + ex.Message, Translated.Global_Error, (MessageBoxButtons)0, (MessageBoxIcon)16);
				}
				updateList();
			}
		}
	}

	private void TimerServerStatus_tick(object sender, EventArgs e)
	{
		TrackAndRefreshServerStatus();
	}

	public async void TrackAndRefreshServerStatus()
	{
		bool success = false;
		try
		{
			ServiceStatusResponse serviceStatusResponse = await service.ServiceStatusAsync(UserID, 37);
			if (serviceStatusResponse != null)
			{
				success = true;
				ServerStatus serviceStatusResult = serviceStatusResponse.Body.ServiceStatusResult;
				if (!string.IsNullOrEmpty(serviceStatusResult.userID) && (serviceStatusResult.userID ?? "") != (UserID ?? ""))
				{
					UserID = serviceStatusResult.userID;
					OplmSettings.Write("TRACK_ID", UserID);
				}
				((ToolStripItem)StatusBar_Online).Text = Translated.MAIN_STRINGS_USERS_ONLINE + " " + serviceStatusResult.usersOnline;
				((ToolStripItem)StatusBar_CountArts).Text = "ICO=" + serviceStatusResult.countIcos + "    COV=" + serviceStatusResult.countCov + "    LAB=" + serviceStatusResult.countLab + "    COV2=" + serviceStatusResult.countCov2 + "    SCR=" + serviceStatusResult.countScr + "    BG=" + serviceStatusResult.countBg;
				((ToolStripItem)StatusBar_ServerTime).Text = Translated.MAIN_STRINGS_SERVER_TIME + " " + serviceStatusResult.serverTime;
			}
		}
		catch (Exception)
		{
		}
		if (!success)
		{
			((ToolStripItem)StatusBar_Online).Text = Translated.MAIN_STRINGS_USERS_ONLINE + " " + Translated.MAIN_STRINGS_NOT_AVAILABLE;
			((ToolStripItem)StatusBar_CountArts).Text = Translated.MAIN_STRINGS_NOT_AVAILABLE;
			((ToolStripItem)StatusBar_ServerTime).Text = Translated.MAIN_STRINGS_SERVER_TIME + " " + Translated.MAIN_STRINGS_NOT_AVAILABLE;
		}
	}

	private void AutoUpdateCheckToolStripMenuItem_Click(object sender, EventArgs e)
	{
		//IL_0007: Unknown result type (might be due to invalid IL or missing references)
		SettingAutoUpdate settingAutoUpdate = new SettingAutoUpdate();
		try
		{
			((Form)settingAutoUpdate).ShowDialog();
		}
		finally
		{
			((IDisposable)settingAutoUpdate)?.Dispose();
		}
	}

	public void VersionCheck()
	{
		//IL_0024: Unknown result type (might be due to invalid IL or missing references)
		string text = OplmSettings.Read("AUTOUPDATE", "NA");
		if (text == "NA")
		{
			SettingAutoUpdate settingAutoUpdate = new SettingAutoUpdate();
			try
			{
				((Form)settingAutoUpdate).ShowDialog();
				return;
			}
			finally
			{
				((IDisposable)settingAutoUpdate)?.Dispose();
			}
		}
		if (bool.Parse(text) && bool.Parse(text))
		{
			DoVersionCheck();
		}
	}

	private async void DoVersionCheck()
	{
		try
		{
			GetDesktopAppVersionResponse getDesktopAppVersionResponse = await service.GetDesktopAppVersionAsync(37);
			if (getDesktopAppVersionResponse != null)
			{
				VersionDesktopInfo getDesktopAppVersionResult = getDesktopAppVersionResponse.Body.GetDesktopAppVersionResult;
				if (37 < getDesktopAppVersionResult.versionid && (int)MessageBox.Show("Your version: 24" + Environment.NewLine + "New version: " + getDesktopAppVersionResult.major + "." + getDesktopAppVersionResult.minor + " buit in " + getDesktopAppVersionResult.date + Environment.NewLine + "Click yes to visit download site." + Environment.NewLine + Environment.NewLine + "Changes:" + Environment.NewLine + getDesktopAppVersionResult.changes, "Update available!", (MessageBoxButtons)4, (MessageBoxIcon)32) == 6)
				{
					CommonFuncs.OpenURL(getDesktopAppVersionResult.url);
				}
			}
		}
		catch (Exception)
		{
		}
	}

	private void Timer_VersionCheck_Tick(object sender, EventArgs e)
	{
		VersionCheck();
	}

	private void Menu_Settings_ChangeLanguage_Click(object sender, EventArgs e)
	{
		//IL_0019: Unknown result type (might be due to invalid IL or missing references)
		//IL_001f: Invalid comparison between Unknown and I4
		SettingsChangeLanguage settingsChangeLanguage = new SettingsChangeLanguage();
		try
		{
			((Form)settingsChangeLanguage).StartPosition = (FormStartPosition)0;
			((Form)settingsChangeLanguage).Location = Cursor.Position;
			if ((int)((Form)settingsChangeLanguage).ShowDialog() == 6)
			{
				SetLanguage();
			}
		}
		finally
		{
			((IDisposable)settingsChangeLanguage)?.Dispose();
		}
	}

	private void Button1_Click_1(object sender, EventArgs e)
	{
		//IL_001b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0021: Invalid comparison between Unknown and I4
		//IL_008c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0092: Invalid comparison between Unknown and I4
		if (SelectedGame.Type == GameType.PS2)
		{
			OpsCheatEditor2 opsCheatEditor = new OpsCheatEditor2();
			try
			{
				if ((int)opsCheatEditor.ShowDialog(SelectedGame) == 6)
				{
					SelectedGame.UpdateGameInfoArts();
					GameList.SelectedItem.SubItems[8].Text = SelectedGame.HasCHTtext;
				}
				else
				{
					((Control)GameList).Focus();
				}
				return;
			}
			finally
			{
				((IDisposable)opsCheatEditor)?.Dispose();
			}
		}
		if (SelectedGame.Type != GameType.POPS)
		{
			return;
		}
		OpsCheatEditor1 opsCheatEditor2 = new OpsCheatEditor1();
		try
		{
			if ((int)opsCheatEditor2.ShowDialog(SelectedGame) == 6)
			{
				SelectedGame.UpdateGameInfoArts();
				GameList.SelectedItem.SubItems[8].Text = SelectedGame.HasCHTtext;
			}
			else
			{
				((Control)GameList).Focus();
			}
		}
		finally
		{
			((IDisposable)opsCheatEditor2)?.Dispose();
		}
	}

	private void Menu_Settings_Mode_Click(object sender, EventArgs e)
	{
		//IL_0007: Unknown result type (might be due to invalid IL or missing references)
		SettingModeSelect settingModeSelect = new SettingModeSelect();
		try
		{
			((Form)settingModeSelect).ShowDialog();
		}
		finally
		{
			((IDisposable)settingModeSelect)?.Dispose();
		}
	}

	private void Menu_NetworkOptions_GetGames_Click(object sender, EventArgs e)
	{
		GameProvider.UpdateGameListHdl(force_refresh: true);
		updateList(SkipMsgBoxBadIsosFound: true);
	}

	private void Menu_NetworkOptions_Sync_Click(object sender, EventArgs e)
	{
		//IL_0007: Unknown result type (might be due to invalid IL or missing references)
		Ethernet_Sync ethernet_Sync = new Ethernet_Sync();
		try
		{
			((Form)ethernet_Sync).ShowDialog();
		}
		finally
		{
			((IDisposable)ethernet_Sync)?.Dispose();
		}
	}

	private void CreateConvertToISOToolStripMenuItem_Click(object sender, EventArgs e)
	{
		//IL_0007: Unknown result type (might be due to invalid IL or missing references)
		ToolsIsoCreator toolsIsoCreator = new ToolsIsoCreator();
		try
		{
			((Form)toolsIsoCreator).ShowDialog();
		}
		finally
		{
			((IDisposable)toolsIsoCreator)?.Dispose();
		}
	}

	private void Button1_Click_2(object sender, EventArgs e)
	{
		//IL_0007: Unknown result type (might be due to invalid IL or missing references)
		OpsArtDownloadNew opsArtDownloadNew = new OpsArtDownloadNew();
		try
		{
			((Form)opsArtDownloadNew).ShowDialog();
		}
		finally
		{
			((IDisposable)opsArtDownloadNew)?.Dispose();
		}
	}

	private void Menu_Help_Ps2HomeThread_Click(object sender, EventArgs e)
	{
		CommonFuncs.OpenURL("http://www.ps2-home.com/forum/viewtopic.php?f=13&t=189");
	}

	private void Menu_Help_PSXPlaceThread_Click(object sender, EventArgs e)
	{
		CommonFuncs.OpenURL("https://www.psx-place.com/threads/opl-manager-tool-to-manage-your-games.19055");
	}

	private void Menu_Help_Facebook_Click(object sender, EventArgs e)
	{
		CommonFuncs.OpenURL("https://www.facebook.com/OPLMANAGER/");
	}

	private void Menu_Help_HomePage_Click(object sender, EventArgs e)
	{
		CommonFuncs.OpenURL("http://oplmanager.com");
	}

	private void Menu_File_ExportCsv_Click(object sender, EventArgs e)
	{
		//IL_0000: Unknown result type (might be due to invalid IL or missing references)
		//IL_0006: Expected O, but got Unknown
		//IL_002f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0035: Invalid comparison between Unknown and I4
		SaveFileDialog val = new SaveFileDialog();
		((FileDialog)val).Filter = "*.csv|CSV Files";
		((FileDialog)val).AddExtension = true;
		((FileDialog)val).DefaultExt = ".csv";
		((FileDialog)val).FileName = "list.csv";
		if ((int)((CommonDialog)val).ShowDialog() != 1)
		{
			return;
		}
		using TextWriter textWriter = new StreamWriter(((FileDialog)val).FileName, append: false, Encoding.UTF8);
		CsvWriter csvWriter = new CsvWriter(textWriter, CultureInfo.InvariantCulture);
		csvWriter.WriteField("sep=,");
		csvWriter.NextRecord();
		csvWriter.WriteField(Translated.MAIN_TAB_MAIN_TABLE_COL_GAME);
		csvWriter.WriteField(Translated.MAIN_TAB_MAIN_TABLE_COL_ID);
		csvWriter.WriteField(Translated.MAIN_TAB_MAIN_TABLE_COL_TYPE);
		csvWriter.WriteField(Translated.MAIN_TAB_MAIN_TABLE_COL_SIZE);
		csvWriter.WriteField(Translated.MAIN_TAB_MAIN_TABLE_COL_MEDIUM);
		csvWriter.NextRecord();
		foreach (GameInfo game in GameProvider.GameList)
		{
			csvWriter.WriteField(game.Title);
			csvWriter.WriteField(game.ID);
			csvWriter.WriteField(game.TypeText);
			csvWriter.WriteField(game.Size);
			csvWriter.WriteField(game.DiscTypeText);
			csvWriter.NextRecord();
		}
		textWriter.Flush();
		textWriter.Close();
	}

	private void Menu_Settings_CfgDevToggle_Click(object sender, EventArgs e)
	{
		//IL_004c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0028: Unknown result type (might be due to invalid IL or missing references)
		if (!OplmSettings.ReadBool("CFG_DEV"))
		{
			OplmSettings.Write("CFG_DEV", "True");
			MessageBox.Show(Translated.MAIN_STRINGS_CFGDEV_ACTIVATED, Translated.Global_Information, (MessageBoxButtons)0, (MessageBoxIcon)64);
		}
		else
		{
			OplmSettings.Write("CFG_DEV", "False");
			MessageBox.Show(Translated.MAIN_STRINGS_CFGDEV_DISABLED, Translated.Global_Information, (MessageBoxButtons)0, (MessageBoxIcon)64);
		}
		setMode();
	}

	private void Menu_LocalHDDOptions_GetList_Click(object sender, EventArgs e)
	{
		GameProvider.UpdateGameListHdl(force_refresh: true);
		updateList(SkipMsgBoxBadIsosFound: true);
	}

	private void Menu_Settings_UseOldIsoFormat_Click(object sender, EventArgs e)
	{
		//IL_0007: Unknown result type (might be due to invalid IL or missing references)
		//IL_000d: Invalid comparison between Unknown and I4
		SettingsIsoNaming settingsIsoNaming = new SettingsIsoNaming();
		try
		{
			if ((int)((Form)settingsIsoNaming).ShowDialog() == 1 && mode == 0 && settingsIsoNaming.changes)
			{
				GameProvider.UpdateGameListNormal(invalidateCache: true);
				updateList();
			}
		}
		finally
		{
			((IDisposable)settingsIsoNaming)?.Dispose();
		}
	}

	private void Menu_Tools_GameGallery_Click(object sender, EventArgs e)
	{
		//IL_000d: Unknown result type (might be due to invalid IL or missing references)
		ToolsOplSimulator toolsOplSimulator = new ToolsOplSimulator();
		try
		{
			((Control)this).Hide();
			((Form)toolsOplSimulator).ShowDialog();
		}
		finally
		{
			((IDisposable)toolsOplSimulator)?.Dispose();
		}
		((Control)this).Show();
	}

	private void OPL_Manager_FormClosing(object sender, FormClosingEventArgs e)
	{
		OplmSettings.SaveFile();
	}

	private void Button1_Click_3(object sender, EventArgs e)
	{
		MoveUp();
	}

	private void Button3_Click(object sender, EventArgs e)
	{
		MoveDown();
	}

	private void FiltersChanged(object sender, EventArgs e)
	{
		updateList();
	}

	private void APPInstallerToolStripMenuItem_Click(object sender, EventArgs e)
	{
		//IL_0006: Unknown result type (might be due to invalid IL or missing references)
		ToolsAppInstaller toolsAppInstaller = new ToolsAppInstaller();
		((Form)toolsAppInstaller).ShowDialog();
		((Component)(object)toolsAppInstaller).Dispose();
	}

	private void Menu_Tools_ConvertISONaming_Click(object sender, EventArgs e)
	{
		//IL_0006: Unknown result type (might be due to invalid IL or missing references)
		//IL_000c: Invalid comparison between Unknown and I4
		ToolsConvertIsoNames toolsConvertIsoNames = new ToolsConvertIsoNames();
		if ((int)((Form)toolsConvertIsoNames).ShowDialog() == 1)
		{
			updateList();
		}
		((Component)(object)toolsConvertIsoNames).Dispose();
	}

	private void SplitContainer1_SplitterMoved(object sender, SplitterEventArgs e)
	{
		OplmSettings.Write("MainSplitterPos", SplitContainer1.SplitterDistance.ToString());
	}

	private void BadISO_BatchRenameVCD_Click(object sender, EventArgs e)
	{
		//IL_000c: Unknown result type (might be due to invalid IL or missing references)
		ToolsConvertVcdNames toolsConvertVcdNames = new ToolsConvertVcdNames();
		((Control)this).Hide();
		((Form)toolsConvertVcdNames).ShowDialog();
		updateList();
		((Component)(object)toolsConvertVcdNames).Dispose();
		((Control)this).Show();
	}

	private void Menu_Tools_CleanFiles_Click(object sender, EventArgs e)
	{
		//IL_0007: Unknown result type (might be due to invalid IL or missing references)
		ToolsCleanFiles toolsCleanFiles = new ToolsCleanFiles();
		try
		{
			((Form)toolsCleanFiles).ShowDialog();
		}
		finally
		{
			((IDisposable)toolsCleanFiles)?.Dispose();
		}
	}

	private void Menu_Tools_ConvertIsoZso_Click(object sender, EventArgs e)
	{
		//IL_0007: Unknown result type (might be due to invalid IL or missing references)
		ToolsConvertIsoZso toolsConvertIsoZso = new ToolsConvertIsoZso();
		try
		{
			((Form)toolsConvertIsoZso).ShowDialog();
			if (toolsConvertIsoZso.flagShouldReload)
			{
				updateList();
			}
		}
		finally
		{
			((IDisposable)toolsConvertIsoZso)?.Dispose();
		}
	}

	private void Menu_Tools_DiskIsoZso_Click(object sender, EventArgs e)
	{
		//IL_0007: Unknown result type (might be due to invalid IL or missing references)
		ToolsDiskToIsoZso toolsDiskToIsoZso = new ToolsDiskToIsoZso();
		try
		{
			((Form)toolsDiskToIsoZso).ShowDialog();
			if (toolsDiskToIsoZso.flagShouldReload)
			{
				updateList();
			}
		}
		finally
		{
			((IDisposable)toolsDiskToIsoZso)?.Dispose();
		}
	}

	[DebuggerNonUserCode]
	protected override void Dispose(bool disposing)
	{
		try
		{
			if (disposing && components != null)
			{
				components.Dispose();
			}
		}
		finally
		{
			((Form)this).Dispose(disposing);
		}
	}

	[DebuggerStepThrough]
	private void InitializeComponent()
	{
		//IL_001c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0026: Expected O, but got Unknown
		//IL_0032: Unknown result type (might be due to invalid IL or missing references)
		//IL_003c: Expected O, but got Unknown
		//IL_003d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0047: Expected O, but got Unknown
		//IL_0048: Unknown result type (might be due to invalid IL or missing references)
		//IL_0052: Expected O, but got Unknown
		//IL_0053: Unknown result type (might be due to invalid IL or missing references)
		//IL_005d: Expected O, but got Unknown
		//IL_005e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0068: Expected O, but got Unknown
		//IL_0069: Unknown result type (might be due to invalid IL or missing references)
		//IL_0073: Expected O, but got Unknown
		//IL_0074: Unknown result type (might be due to invalid IL or missing references)
		//IL_007e: Expected O, but got Unknown
		//IL_007f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0089: Expected O, but got Unknown
		//IL_008a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0094: Expected O, but got Unknown
		//IL_0095: Unknown result type (might be due to invalid IL or missing references)
		//IL_009f: Expected O, but got Unknown
		//IL_00a0: Unknown result type (might be due to invalid IL or missing references)
		//IL_00aa: Expected O, but got Unknown
		//IL_00ab: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b5: Expected O, but got Unknown
		//IL_00b6: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c0: Expected O, but got Unknown
		//IL_00c1: Unknown result type (might be due to invalid IL or missing references)
		//IL_00cb: Expected O, but got Unknown
		//IL_00cc: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d6: Expected O, but got Unknown
		//IL_00d7: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e1: Expected O, but got Unknown
		//IL_00e2: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ec: Expected O, but got Unknown
		//IL_00ed: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f7: Expected O, but got Unknown
		//IL_00f8: Unknown result type (might be due to invalid IL or missing references)
		//IL_0102: Expected O, but got Unknown
		//IL_0103: Unknown result type (might be due to invalid IL or missing references)
		//IL_010d: Expected O, but got Unknown
		//IL_010e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0118: Expected O, but got Unknown
		//IL_0119: Unknown result type (might be due to invalid IL or missing references)
		//IL_0123: Expected O, but got Unknown
		//IL_0124: Unknown result type (might be due to invalid IL or missing references)
		//IL_012e: Expected O, but got Unknown
		//IL_012f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0139: Expected O, but got Unknown
		//IL_013a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0144: Expected O, but got Unknown
		//IL_0145: Unknown result type (might be due to invalid IL or missing references)
		//IL_014f: Expected O, but got Unknown
		//IL_0150: Unknown result type (might be due to invalid IL or missing references)
		//IL_015a: Expected O, but got Unknown
		//IL_015b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0165: Expected O, but got Unknown
		//IL_0166: Unknown result type (might be due to invalid IL or missing references)
		//IL_0170: Expected O, but got Unknown
		//IL_0171: Unknown result type (might be due to invalid IL or missing references)
		//IL_017b: Expected O, but got Unknown
		//IL_017c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0186: Expected O, but got Unknown
		//IL_0187: Unknown result type (might be due to invalid IL or missing references)
		//IL_0191: Expected O, but got Unknown
		//IL_0192: Unknown result type (might be due to invalid IL or missing references)
		//IL_019c: Expected O, but got Unknown
		//IL_019d: Unknown result type (might be due to invalid IL or missing references)
		//IL_01a7: Expected O, but got Unknown
		//IL_01a8: Unknown result type (might be due to invalid IL or missing references)
		//IL_01b2: Expected O, but got Unknown
		//IL_01b3: Unknown result type (might be due to invalid IL or missing references)
		//IL_01bd: Expected O, but got Unknown
		//IL_01be: Unknown result type (might be due to invalid IL or missing references)
		//IL_01c8: Expected O, but got Unknown
		//IL_01c9: Unknown result type (might be due to invalid IL or missing references)
		//IL_01d3: Expected O, but got Unknown
		//IL_01d4: Unknown result type (might be due to invalid IL or missing references)
		//IL_01de: Expected O, but got Unknown
		//IL_01df: Unknown result type (might be due to invalid IL or missing references)
		//IL_01e9: Expected O, but got Unknown
		//IL_01ea: Unknown result type (might be due to invalid IL or missing references)
		//IL_01f4: Expected O, but got Unknown
		//IL_01f5: Unknown result type (might be due to invalid IL or missing references)
		//IL_01ff: Expected O, but got Unknown
		//IL_0200: Unknown result type (might be due to invalid IL or missing references)
		//IL_020a: Expected O, but got Unknown
		//IL_020b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0215: Expected O, but got Unknown
		//IL_0216: Unknown result type (might be due to invalid IL or missing references)
		//IL_0220: Expected O, but got Unknown
		//IL_0221: Unknown result type (might be due to invalid IL or missing references)
		//IL_022b: Expected O, but got Unknown
		//IL_022c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0236: Expected O, but got Unknown
		//IL_0237: Unknown result type (might be due to invalid IL or missing references)
		//IL_0241: Expected O, but got Unknown
		//IL_0242: Unknown result type (might be due to invalid IL or missing references)
		//IL_024c: Expected O, but got Unknown
		//IL_024d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0257: Expected O, but got Unknown
		//IL_0258: Unknown result type (might be due to invalid IL or missing references)
		//IL_0262: Expected O, but got Unknown
		//IL_0263: Unknown result type (might be due to invalid IL or missing references)
		//IL_026d: Expected O, but got Unknown
		//IL_026e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0278: Expected O, but got Unknown
		//IL_0279: Unknown result type (might be due to invalid IL or missing references)
		//IL_0283: Expected O, but got Unknown
		//IL_0284: Unknown result type (might be due to invalid IL or missing references)
		//IL_028e: Expected O, but got Unknown
		//IL_028f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0299: Expected O, but got Unknown
		//IL_029a: Unknown result type (might be due to invalid IL or missing references)
		//IL_02a4: Expected O, but got Unknown
		//IL_02a5: Unknown result type (might be due to invalid IL or missing references)
		//IL_02af: Expected O, but got Unknown
		//IL_02b0: Unknown result type (might be due to invalid IL or missing references)
		//IL_02ba: Expected O, but got Unknown
		//IL_02bb: Unknown result type (might be due to invalid IL or missing references)
		//IL_02c5: Expected O, but got Unknown
		//IL_02c6: Unknown result type (might be due to invalid IL or missing references)
		//IL_02d0: Expected O, but got Unknown
		//IL_02d1: Unknown result type (might be due to invalid IL or missing references)
		//IL_02db: Expected O, but got Unknown
		//IL_02dc: Unknown result type (might be due to invalid IL or missing references)
		//IL_02e6: Expected O, but got Unknown
		//IL_02e7: Unknown result type (might be due to invalid IL or missing references)
		//IL_02f1: Expected O, but got Unknown
		//IL_02f2: Unknown result type (might be due to invalid IL or missing references)
		//IL_02fc: Expected O, but got Unknown
		//IL_02fd: Unknown result type (might be due to invalid IL or missing references)
		//IL_0307: Expected O, but got Unknown
		//IL_0308: Unknown result type (might be due to invalid IL or missing references)
		//IL_0312: Expected O, but got Unknown
		//IL_0313: Unknown result type (might be due to invalid IL or missing references)
		//IL_031d: Expected O, but got Unknown
		//IL_031e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0328: Expected O, but got Unknown
		//IL_0329: Unknown result type (might be due to invalid IL or missing references)
		//IL_0333: Expected O, but got Unknown
		//IL_0334: Unknown result type (might be due to invalid IL or missing references)
		//IL_033e: Expected O, but got Unknown
		//IL_033f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0349: Expected O, but got Unknown
		//IL_034a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0354: Expected O, but got Unknown
		//IL_0355: Unknown result type (might be due to invalid IL or missing references)
		//IL_035f: Expected O, but got Unknown
		//IL_0360: Unknown result type (might be due to invalid IL or missing references)
		//IL_036a: Expected O, but got Unknown
		//IL_036b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0375: Expected O, but got Unknown
		//IL_0376: Unknown result type (might be due to invalid IL or missing references)
		//IL_0380: Expected O, but got Unknown
		//IL_0381: Unknown result type (might be due to invalid IL or missing references)
		//IL_038b: Expected O, but got Unknown
		//IL_038c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0396: Expected O, but got Unknown
		//IL_0397: Unknown result type (might be due to invalid IL or missing references)
		//IL_03a1: Expected O, but got Unknown
		//IL_03a2: Unknown result type (might be due to invalid IL or missing references)
		//IL_03ac: Expected O, but got Unknown
		//IL_03ad: Unknown result type (might be due to invalid IL or missing references)
		//IL_03b7: Expected O, but got Unknown
		//IL_03b8: Unknown result type (might be due to invalid IL or missing references)
		//IL_03c2: Expected O, but got Unknown
		//IL_03c3: Unknown result type (might be due to invalid IL or missing references)
		//IL_03cd: Expected O, but got Unknown
		//IL_03ce: Unknown result type (might be due to invalid IL or missing references)
		//IL_03d8: Expected O, but got Unknown
		//IL_03d9: Unknown result type (might be due to invalid IL or missing references)
		//IL_03e3: Expected O, but got Unknown
		//IL_03e4: Unknown result type (might be due to invalid IL or missing references)
		//IL_03ee: Expected O, but got Unknown
		//IL_03ef: Unknown result type (might be due to invalid IL or missing references)
		//IL_03f9: Expected O, but got Unknown
		//IL_03fa: Unknown result type (might be due to invalid IL or missing references)
		//IL_0404: Expected O, but got Unknown
		//IL_0405: Unknown result type (might be due to invalid IL or missing references)
		//IL_040f: Expected O, but got Unknown
		//IL_0410: Unknown result type (might be due to invalid IL or missing references)
		//IL_041a: Expected O, but got Unknown
		//IL_041b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0425: Expected O, but got Unknown
		//IL_0426: Unknown result type (might be due to invalid IL or missing references)
		//IL_0430: Expected O, but got Unknown
		//IL_0431: Unknown result type (might be due to invalid IL or missing references)
		//IL_043b: Expected O, but got Unknown
		//IL_043c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0446: Expected O, but got Unknown
		//IL_0447: Unknown result type (might be due to invalid IL or missing references)
		//IL_0451: Expected O, but got Unknown
		//IL_0452: Unknown result type (might be due to invalid IL or missing references)
		//IL_045c: Expected O, but got Unknown
		//IL_045d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0467: Expected O, but got Unknown
		//IL_0468: Unknown result type (might be due to invalid IL or missing references)
		//IL_0472: Expected O, but got Unknown
		//IL_0473: Unknown result type (might be due to invalid IL or missing references)
		//IL_047d: Expected O, but got Unknown
		//IL_047e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0488: Expected O, but got Unknown
		//IL_0489: Unknown result type (might be due to invalid IL or missing references)
		//IL_0493: Expected O, but got Unknown
		//IL_0494: Unknown result type (might be due to invalid IL or missing references)
		//IL_049e: Expected O, but got Unknown
		//IL_049f: Unknown result type (might be due to invalid IL or missing references)
		//IL_04a9: Expected O, but got Unknown
		//IL_04aa: Unknown result type (might be due to invalid IL or missing references)
		//IL_04b4: Expected O, but got Unknown
		//IL_04b5: Unknown result type (might be due to invalid IL or missing references)
		//IL_04bf: Expected O, but got Unknown
		//IL_04c0: Unknown result type (might be due to invalid IL or missing references)
		//IL_04ca: Expected O, but got Unknown
		//IL_04cb: Unknown result type (might be due to invalid IL or missing references)
		//IL_04d5: Expected O, but got Unknown
		//IL_04d6: Unknown result type (might be due to invalid IL or missing references)
		//IL_04e0: Expected O, but got Unknown
		//IL_04e1: Unknown result type (might be due to invalid IL or missing references)
		//IL_04eb: Expected O, but got Unknown
		//IL_04ec: Unknown result type (might be due to invalid IL or missing references)
		//IL_04f6: Expected O, but got Unknown
		//IL_04f7: Unknown result type (might be due to invalid IL or missing references)
		//IL_0501: Expected O, but got Unknown
		//IL_0502: Unknown result type (might be due to invalid IL or missing references)
		//IL_050c: Expected O, but got Unknown
		//IL_050d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0517: Expected O, but got Unknown
		//IL_0518: Unknown result type (might be due to invalid IL or missing references)
		//IL_0522: Expected O, but got Unknown
		//IL_0523: Unknown result type (might be due to invalid IL or missing references)
		//IL_052d: Expected O, but got Unknown
		//IL_052e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0538: Expected O, but got Unknown
		//IL_0539: Unknown result type (might be due to invalid IL or missing references)
		//IL_0543: Expected O, but got Unknown
		//IL_0544: Unknown result type (might be due to invalid IL or missing references)
		//IL_054e: Expected O, but got Unknown
		//IL_054f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0559: Expected O, but got Unknown
		//IL_055a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0564: Expected O, but got Unknown
		//IL_0565: Unknown result type (might be due to invalid IL or missing references)
		//IL_056f: Expected O, but got Unknown
		//IL_0570: Unknown result type (might be due to invalid IL or missing references)
		//IL_057a: Expected O, but got Unknown
		//IL_057b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0585: Expected O, but got Unknown
		//IL_0586: Unknown result type (might be due to invalid IL or missing references)
		//IL_0590: Expected O, but got Unknown
		//IL_0591: Unknown result type (might be due to invalid IL or missing references)
		//IL_059b: Expected O, but got Unknown
		//IL_059c: Unknown result type (might be due to invalid IL or missing references)
		//IL_05a6: Expected O, but got Unknown
		//IL_05a7: Unknown result type (might be due to invalid IL or missing references)
		//IL_05b1: Expected O, but got Unknown
		//IL_05b2: Unknown result type (might be due to invalid IL or missing references)
		//IL_05bc: Expected O, but got Unknown
		//IL_05bd: Unknown result type (might be due to invalid IL or missing references)
		//IL_05c7: Expected O, but got Unknown
		//IL_05c8: Unknown result type (might be due to invalid IL or missing references)
		//IL_05d2: Expected O, but got Unknown
		//IL_05d3: Unknown result type (might be due to invalid IL or missing references)
		//IL_05dd: Expected O, but got Unknown
		//IL_05de: Unknown result type (might be due to invalid IL or missing references)
		//IL_05e8: Expected O, but got Unknown
		//IL_05e9: Unknown result type (might be due to invalid IL or missing references)
		//IL_05f3: Expected O, but got Unknown
		//IL_05f4: Unknown result type (might be due to invalid IL or missing references)
		//IL_05fe: Expected O, but got Unknown
		//IL_05ff: Unknown result type (might be due to invalid IL or missing references)
		//IL_0609: Expected O, but got Unknown
		//IL_060a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0614: Expected O, but got Unknown
		//IL_0615: Unknown result type (might be due to invalid IL or missing references)
		//IL_061f: Expected O, but got Unknown
		//IL_0620: Unknown result type (might be due to invalid IL or missing references)
		//IL_062a: Expected O, but got Unknown
		//IL_062b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0635: Expected O, but got Unknown
		//IL_0636: Unknown result type (might be due to invalid IL or missing references)
		//IL_0640: Expected O, but got Unknown
		//IL_0641: Unknown result type (might be due to invalid IL or missing references)
		//IL_064b: Expected O, but got Unknown
		//IL_0652: Unknown result type (might be due to invalid IL or missing references)
		//IL_065c: Expected O, but got Unknown
		//IL_0663: Unknown result type (might be due to invalid IL or missing references)
		//IL_066d: Expected O, but got Unknown
		//IL_0674: Unknown result type (might be due to invalid IL or missing references)
		//IL_067e: Expected O, but got Unknown
		//IL_0a14: Unknown result type (might be due to invalid IL or missing references)
		//IL_0a1e: Expected O, but got Unknown
		//IL_0a2b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0a35: Expected O, but got Unknown
		//IL_0e69: Unknown result type (might be due to invalid IL or missing references)
		//IL_0e73: Expected O, but got Unknown
		//IL_0e7f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0e89: Expected O, but got Unknown
		//IL_0e95: Unknown result type (might be due to invalid IL or missing references)
		//IL_0e9f: Expected O, but got Unknown
		//IL_0eb1: Unknown result type (might be due to invalid IL or missing references)
		//IL_0ebb: Expected O, but got Unknown
		//IL_110b: Unknown result type (might be due to invalid IL or missing references)
		//IL_1115: Expected O, but got Unknown
		//IL_1127: Unknown result type (might be due to invalid IL or missing references)
		//IL_1131: Expected O, but got Unknown
		//IL_1143: Unknown result type (might be due to invalid IL or missing references)
		//IL_114d: Expected O, but got Unknown
		//IL_115f: Unknown result type (might be due to invalid IL or missing references)
		//IL_1169: Expected O, but got Unknown
		//IL_117b: Unknown result type (might be due to invalid IL or missing references)
		//IL_1185: Expected O, but got Unknown
		//IL_1197: Unknown result type (might be due to invalid IL or missing references)
		//IL_11a1: Expected O, but got Unknown
		//IL_11b3: Unknown result type (might be due to invalid IL or missing references)
		//IL_11bd: Expected O, but got Unknown
		//IL_122d: Unknown result type (might be due to invalid IL or missing references)
		//IL_12ca: Unknown result type (might be due to invalid IL or missing references)
		//IL_135a: Unknown result type (might be due to invalid IL or missing references)
		//IL_13e6: Unknown result type (might be due to invalid IL or missing references)
		//IL_1472: Unknown result type (might be due to invalid IL or missing references)
		//IL_150f: Unknown result type (might be due to invalid IL or missing references)
		//IL_166d: Unknown result type (might be due to invalid IL or missing references)
		//IL_170b: Unknown result type (might be due to invalid IL or missing references)
		//IL_17ac: Unknown result type (might be due to invalid IL or missing references)
		//IL_1839: Unknown result type (might be due to invalid IL or missing references)
		//IL_18c6: Unknown result type (might be due to invalid IL or missing references)
		//IL_1964: Unknown result type (might be due to invalid IL or missing references)
		//IL_1a02: Unknown result type (might be due to invalid IL or missing references)
		//IL_1aa0: Unknown result type (might be due to invalid IL or missing references)
		//IL_1b3e: Unknown result type (might be due to invalid IL or missing references)
		//IL_1bcb: Unknown result type (might be due to invalid IL or missing references)
		//IL_1c79: Unknown result type (might be due to invalid IL or missing references)
		//IL_1d17: Unknown result type (might be due to invalid IL or missing references)
		//IL_1eac: Unknown result type (might be due to invalid IL or missing references)
		//IL_1eb6: Expected O, but got Unknown
		//IL_1f4b: Unknown result type (might be due to invalid IL or missing references)
		//IL_1f55: Expected O, but got Unknown
		//IL_28bb: Unknown result type (might be due to invalid IL or missing references)
		//IL_2947: Unknown result type (might be due to invalid IL or missing references)
		//IL_2b4c: Unknown result type (might be due to invalid IL or missing references)
		//IL_2b56: Expected O, but got Unknown
		//IL_45f7: Unknown result type (might be due to invalid IL or missing references)
		//IL_4601: Expected O, but got Unknown
		//IL_460d: Unknown result type (might be due to invalid IL or missing references)
		//IL_4617: Expected O, but got Unknown
		//IL_4648: Unknown result type (might be due to invalid IL or missing references)
		//IL_4652: Expected O, but got Unknown
		components = new Container();
		ComponentResourceManager componentResourceManager = new ComponentResourceManager(typeof(OPLM_Main));
		SplitContainer1 = new SplitContainer();
		GameList = new CustomListView();
		Col_Game = new ColumnHeader();
		Col_ID = new ColumnHeader();
		Col_Type = new ColumnHeader();
		Col_Size = new ColumnHeader();
		Col_Medium = new ColumnHeader();
		Col_CFG = new ColumnHeader();
		Col_Cht = new ColumnHeader();
		Col_ArtIco = new ColumnHeader();
		Col_ArtCov = new ColumnHeader();
		Col_ArtLab = new ColumnHeader();
		Col_ArtCov2 = new ColumnHeader();
		Col_ArtScr = new ColumnHeader();
		Col_ArtLgo = new ColumnHeader();
		Col_ArtBg = new ColumnHeader();
		GroupBox1 = new GroupBox();
		CB_Filter_APPS = new CheckBox();
		CB_Filter_PS2 = new CheckBox();
		CB_Filter_POPS = new CheckBox();
		GlobalStats = new GroupBox();
		TableLayoutPanel1 = new TableLayoutPanel();
		GlobalStats_CountTotal = new Label();
		GlobalStats_PS2DVD_Count = new Label();
		GlobalStats_Total = new Label();
		GlobalStats_PS2DVD = new Label();
		GlobalStats_PS2CD = new Label();
		GlobalStats_PS2CD_Count = new Label();
		GlobalStats_Count = new Label();
		GlobalStats_Size = new Label();
		GlobalStats_PS2CD_Size = new Label();
		GlobalStats_PS2DVD_Size = new Label();
		GlobalStats_SizeTotal = new Label();
		GlobalStats_PS1 = new Label();
		GlobalStats_APPS = new Label();
		GlobalStats_PS1_Count = new Label();
		GlobalStats_PS1_Size = new Label();
		GlobalStats_APP_Count = new Label();
		GlobalStats_APP_Size = new Label();
		GlobalStats_PS2 = new Label();
		GlobalStats_PS2_Count = new Label();
		GlobalStats_PS2_Size = new Label();
		GameDetails = new GroupBox();
		Cover_CoverIMG = new PictureBox();
		Disc_DiscIMG = new PictureBox();
		Operations = new GroupBox();
		Operations_ManageARTs = new Button();
		Operations_CheatEditor = new Button();
		Operations_Hash = new Button();
		Operations_EditCFG = new Button();
		Operations_UpdateTitle = new Button();
		Operations_DeleteGame = new Button();
		GameDetails_TB_Title = new TextBox();
		GameDetails_Label_Title = new Label();
		GameDetails_TB_ID = new TextBox();
		GameDetails_Label_Path = new Label();
		GameDetails_Label_ID = new Label();
		GameDetails_TB_Path = new TextBox();
		ToolStripContainer1 = new ToolStripContainer();
		StatusStrip1 = new StatusStrip();
		StatusBar_Online = new ToolStripStatusLabel();
		StatusBar_CountArts = new ToolStripStatusLabel();
		StatusBar_ServerTime = new ToolStripStatusLabel();
		StatusBar_Mode = new ToolStripStatusLabel();
		Tab_Main = new TabControl();
		TabHome = new TabPage();
		TabBadIsos = new TabPage();
		SplitContainer2 = new SplitContainer();
		BadIsoList = new ListView();
		ColumnHeader1 = new ColumnHeader();
		ColumnHeader2 = new ColumnHeader();
		ColumnHeader5 = new ColumnHeader();
		ColumnHeader3 = new ColumnHeader();
		ColumnHeader4 = new ColumnHeader();
		ColumnHeader6 = new ColumnHeader();
		BadISO_BatchRenameVCD = new Button();
		BadISO_BatchRenameISO = new Button();
		GB_BadIsoDetails = new GroupBox();
		GroupBox2 = new GroupBox();
		BadISO_errorMsg = new Label();
		BadISO_GetTitleFromDB = new Button();
		BadISO_ChangeID = new Button();
		BadISO_CurrentFileName = new TextBox();
		BadISO_GameDetails_Label_CurrentFile = new Label();
		BadISO_Size = new TextBox();
		BadISO_newfile = new TextBox();
		BadISO_GameDetails_Label_NewFile = new Label();
		BadISO_UpdateFileName = new Button();
		BadIso_title = new TextBox();
		BadISO_GameDetails_Label_Size = new Label();
		BadISO_GameDetails_Label_Title = new Label();
		BadISO_ID = new TextBox();
		BadISO_GameDetails_Label_Path = new Label();
		BadISO_GameDetails_Label_ID = new Label();
		BadISO_Path = new TextBox();
		MenuStrip1 = new MenuStrip();
		Menu_File = new ToolStripMenuItem();
		Menu_File_Refresh = new ToolStripMenuItem();
		Menu_File_InvalidateCacheAndRefresh = new ToolStripMenuItem();
		Menu_File_ExportCsv = new ToolStripMenuItem();
		Menu_File_Exit = new ToolStripMenuItem();
		Menu_BatchActions = new ToolStripMenuItem();
		Menu_BatchActions_CoverAndIconDownload = new ToolStripMenuItem();
		Menu_BatchActions_ShareART = new ToolStripMenuItem();
		Menu_BatchActions_UpgradeOldCFGs = new ToolStripMenuItem();
		Menu_View = new ToolStripMenuItem();
		Menu_View_All = new ToolStripMenuItem();
		Menu_View_WithMissing = new ToolStripMenuItem();
		Menu_View_WithMissing_CoverArt = new ToolStripMenuItem();
		Menu_View_WithMissing_DiscArt = new ToolStripMenuItem();
		Menu_View_WithMissing_CoverAndOrDisc = new ToolStripMenuItem();
		Menu_View_With = new ToolStripMenuItem();
		Menu_View_With_Cover = new ToolStripMenuItem();
		Menu_View_With_Disc = new ToolStripMenuItem();
		Menu_View_With_CoverAndOrDisc = new ToolStripMenuItem();
		Menu_Settings = new ToolStripMenuItem();
		Menu_Settings_ChangeLanguage = new ToolStripMenuItem();
		Menu_Settings_Mode = new ToolStripMenuItem();
		Menu_Settings_GameDoubleClickAction = new ToolStripMenuItem();
		Menu_Settings_AutoUpdate = new ToolStripMenuItem();
		Menu_Settings_CfgDevToggle = new ToolStripMenuItem();
		Menu_Settings_UseOldIsoFormat = new ToolStripMenuItem();
		Menu_NetworkOptions = new ToolStripMenuItem();
		Menu_NetworkOptions_GetGames = new ToolStripMenuItem();
		Menu_NetworkOptions_Sync = new ToolStripMenuItem();
		Menu_Tools = new ToolStripMenuItem();
		Menu_Tools_IsoConv = new ToolStripMenuItem();
		Menu_Tools_OplSimulator = new ToolStripMenuItem();
		Menu_Tools_APPInstaller = new ToolStripMenuItem();
		Menu_Tools_ConvertISONaming = new ToolStripMenuItem();
		Menu_Tools_CleanFiles = new ToolStripMenuItem();
		Menu_Tools_ConvertIsoZso = new ToolStripMenuItem();
		Menu_Tools_DiskIsoZso = new ToolStripMenuItem();
		Menu_LocalHDDOptions = new ToolStripMenuItem();
		Menu_LocalHDDOptions_GetList = new ToolStripMenuItem();
		Menu_Help = new ToolStripMenuItem();
		Menu_Help_ChangeLog = new ToolStripMenuItem();
		Menu_Help_Homepage = new ToolStripMenuItem();
		Menu_Help_Ps2HomeThread = new ToolStripMenuItem();
		Menu_Help_PSXPlaceThread = new ToolStripMenuItem();
		Menu_Help_Facebook = new ToolStripMenuItem();
		Menu_Help_About = new ToolStripMenuItem();
		Menu_OpenOPLFolder = new ToolStripMenuItem();
		OpenFileDialog1 = new OpenFileDialog();
		ToolTip1 = new ToolTip(components);
		Timer_VersionCheck = new Timer(components);
		TimerServerStatus = new Timer(components);
		((ISupportInitialize)SplitContainer1).BeginInit();
		((Control)SplitContainer1.Panel1).SuspendLayout();
		((Control)SplitContainer1.Panel2).SuspendLayout();
		((Control)SplitContainer1).SuspendLayout();
		((Control)GroupBox1).SuspendLayout();
		((Control)GlobalStats).SuspendLayout();
		((Control)TableLayoutPanel1).SuspendLayout();
		((Control)GameDetails).SuspendLayout();
		((ISupportInitialize)Cover_CoverIMG).BeginInit();
		((ISupportInitialize)Disc_DiscIMG).BeginInit();
		((Control)Operations).SuspendLayout();
		((Control)ToolStripContainer1.BottomToolStripPanel).SuspendLayout();
		((Control)ToolStripContainer1.ContentPanel).SuspendLayout();
		((Control)ToolStripContainer1.TopToolStripPanel).SuspendLayout();
		((Control)ToolStripContainer1).SuspendLayout();
		((Control)StatusStrip1).SuspendLayout();
		((Control)Tab_Main).SuspendLayout();
		((Control)TabHome).SuspendLayout();
		((Control)TabBadIsos).SuspendLayout();
		((ISupportInitialize)SplitContainer2).BeginInit();
		((Control)SplitContainer2.Panel1).SuspendLayout();
		((Control)SplitContainer2.Panel2).SuspendLayout();
		((Control)SplitContainer2).SuspendLayout();
		((Control)GB_BadIsoDetails).SuspendLayout();
		((Control)GroupBox2).SuspendLayout();
		((Control)MenuStrip1).SuspendLayout();
		((Control)this).SuspendLayout();
		SplitContainer1.Dock = (DockStyle)5;
		SplitContainer1.FixedPanel = (FixedPanel)2;
		((Control)SplitContainer1).Location = new Point(3, 3);
		((Control)SplitContainer1).Name = "SplitContainer1";
		((Control)SplitContainer1.Panel1).Controls.Add((Control)(object)GameList);
		SplitContainer1.Panel1MinSize = 100;
		((Control)SplitContainer1.Panel2).BackColor = Color.Transparent;
		((Control)SplitContainer1.Panel2).Controls.Add((Control)(object)GroupBox1);
		((Control)SplitContainer1.Panel2).Controls.Add((Control)(object)GlobalStats);
		((Control)SplitContainer1.Panel2).Controls.Add((Control)(object)GameDetails);
		SplitContainer1.Panel2MinSize = 320;
		((Control)SplitContainer1).Size = new Size(850, 583);
		SplitContainer1.SplitterDistance = 520;
		((Control)SplitContainer1).TabIndex = 0;
		((ListView)GameList).Columns.AddRange((ColumnHeader[])(object)new ColumnHeader[14]
		{
			Col_Game, Col_ID, Col_Type, Col_Size, Col_Medium, Col_CFG, Col_Cht, Col_ArtIco, Col_ArtCov, Col_ArtLab,
			Col_ArtCov2, Col_ArtScr, Col_ArtLgo, Col_ArtBg
		});
		((Control)GameList).Dock = (DockStyle)5;
		((ListView)GameList).FullRowSelect = true;
		((Control)GameList).Location = new Point(0, 0);
		((ListView)GameList).MultiSelect = false;
		((Control)GameList).Name = "GameList";
		((Control)GameList).Size = new Size(520, 583);
		((Control)GameList).TabIndex = 0;
		ToolTip1.SetToolTip((Control)(object)GameList, "Click the columns header to order!");
		((ListView)GameList).UseCompatibleStateImageBehavior = false;
		((ListView)GameList).View = (View)1;
		((ListView)GameList).ColumnClick += new ColumnClickEventHandler(GameList_ColumnClick);
		((ListView)GameList).ItemSelectionChanged += new ListViewItemSelectionChangedEventHandler(GameList_ItemSelectionChanged);
		((Control)GameList).DoubleClick += GameList_LaunchApp;
		Col_Game.Text = "Game";
		Col_Game.Width = 41;
		Col_ID.Text = "ID";
		Col_ID.Width = 41;
		Col_Type.Text = "Type";
		Col_Type.Width = 43;
		Col_Size.Text = "Size";
		Col_Size.Width = 33;
		Col_Medium.Text = "Medium";
		Col_Medium.Width = 51;
		Col_CFG.Text = "CFG";
		Col_Cht.Text = "Cheat";
		Col_ArtIco.Text = "Disc";
		Col_ArtIco.Width = 38;
		Col_ArtCov.Text = "Cover";
		Col_ArtCov.Width = 44;
		Col_ArtLab.Text = "Label";
		Col_ArtCov2.Text = "Back cover";
		Col_ArtScr.Text = "Screen";
		Col_ArtLgo.Text = "Logo";
		Col_ArtBg.Text = "Background";
		((Control)GroupBox1).Controls.Add((Control)(object)CB_Filter_APPS);
		((Control)GroupBox1).Controls.Add((Control)(object)CB_Filter_PS2);
		((Control)GroupBox1).Controls.Add((Control)(object)CB_Filter_POPS);
		((Control)GroupBox1).Location = new Point(3, -2);
		((Control)GroupBox1).Name = "GroupBox1";
		((Control)GroupBox1).Size = new Size(316, 47);
		((Control)GroupBox1).TabIndex = 19;
		GroupBox1.TabStop = false;
		((Control)GroupBox1).Text = "Filters";
		((Control)CB_Filter_APPS).AutoSize = true;
		CB_Filter_APPS.Checked = true;
		CB_Filter_APPS.CheckState = (CheckState)1;
		((Control)CB_Filter_APPS).Location = new Point(216, 19);
		((Control)CB_Filter_APPS).Name = "CB_Filter_APPS";
		((Control)CB_Filter_APPS).Size = new Size(54, 17);
		((Control)CB_Filter_APPS).TabIndex = 2;
		((Control)CB_Filter_APPS).Text = "APPS";
		((ButtonBase)CB_Filter_APPS).UseVisualStyleBackColor = true;
		((Control)CB_Filter_PS2).AutoSize = true;
		CB_Filter_PS2.Checked = true;
		CB_Filter_PS2.CheckState = (CheckState)1;
		((Control)CB_Filter_PS2).Location = new Point(13, 19);
		((Control)CB_Filter_PS2).Name = "CB_Filter_PS2";
		((Control)CB_Filter_PS2).Size = new Size(46, 17);
		((Control)CB_Filter_PS2).TabIndex = 0;
		((Control)CB_Filter_PS2).Text = "PS2";
		((ButtonBase)CB_Filter_PS2).UseVisualStyleBackColor = true;
		((Control)CB_Filter_POPS).AutoSize = true;
		CB_Filter_POPS.Checked = true;
		CB_Filter_POPS.CheckState = (CheckState)1;
		((Control)CB_Filter_POPS).Location = new Point(118, 19);
		((Control)CB_Filter_POPS).Name = "CB_Filter_POPS";
		((Control)CB_Filter_POPS).Size = new Size(55, 17);
		((Control)CB_Filter_POPS).TabIndex = 1;
		((Control)CB_Filter_POPS).Text = "POPS";
		((ButtonBase)CB_Filter_POPS).UseVisualStyleBackColor = true;
		((Control)GlobalStats).Controls.Add((Control)(object)TableLayoutPanel1);
		((Control)GlobalStats).Location = new Point(8, 404);
		((Control)GlobalStats).Name = "GlobalStats";
		((Control)GlobalStats).Size = new Size(311, 178);
		((Control)GlobalStats).TabIndex = 16;
		GlobalStats.TabStop = false;
		((Control)GlobalStats).Text = "Global Stats";
		((Control)TableLayoutPanel1).AutoSize = true;
		((Panel)TableLayoutPanel1).AutoSizeMode = (AutoSizeMode)0;
		TableLayoutPanel1.ColumnCount = 4;
		TableLayoutPanel1.ColumnStyles.Add(new ColumnStyle());
		TableLayoutPanel1.ColumnStyles.Add(new ColumnStyle());
		TableLayoutPanel1.ColumnStyles.Add(new ColumnStyle());
		TableLayoutPanel1.ColumnStyles.Add(new ColumnStyle((SizeType)1, 191f));
		TableLayoutPanel1.Controls.Add((Control)(object)GlobalStats_CountTotal, 1, 6);
		TableLayoutPanel1.Controls.Add((Control)(object)GlobalStats_PS2DVD_Count, 1, 3);
		TableLayoutPanel1.Controls.Add((Control)(object)GlobalStats_Total, 0, 6);
		TableLayoutPanel1.Controls.Add((Control)(object)GlobalStats_PS2DVD, 0, 3);
		TableLayoutPanel1.Controls.Add((Control)(object)GlobalStats_PS2CD, 0, 2);
		TableLayoutPanel1.Controls.Add((Control)(object)GlobalStats_PS2CD_Count, 1, 2);
		TableLayoutPanel1.Controls.Add((Control)(object)GlobalStats_Count, 1, 0);
		TableLayoutPanel1.Controls.Add((Control)(object)GlobalStats_Size, 2, 0);
		TableLayoutPanel1.Controls.Add((Control)(object)GlobalStats_PS2CD_Size, 2, 2);
		TableLayoutPanel1.Controls.Add((Control)(object)GlobalStats_PS2DVD_Size, 2, 3);
		TableLayoutPanel1.Controls.Add((Control)(object)GlobalStats_SizeTotal, 2, 6);
		TableLayoutPanel1.Controls.Add((Control)(object)GlobalStats_PS1, 0, 4);
		TableLayoutPanel1.Controls.Add((Control)(object)GlobalStats_APPS, 0, 5);
		TableLayoutPanel1.Controls.Add((Control)(object)GlobalStats_PS1_Count, 1, 4);
		TableLayoutPanel1.Controls.Add((Control)(object)GlobalStats_PS1_Size, 2, 4);
		TableLayoutPanel1.Controls.Add((Control)(object)GlobalStats_APP_Count, 1, 5);
		TableLayoutPanel1.Controls.Add((Control)(object)GlobalStats_APP_Size, 2, 5);
		TableLayoutPanel1.Controls.Add((Control)(object)GlobalStats_PS2, 0, 1);
		TableLayoutPanel1.Controls.Add((Control)(object)GlobalStats_PS2_Count, 1, 1);
		TableLayoutPanel1.Controls.Add((Control)(object)GlobalStats_PS2_Size, 2, 1);
		((Control)TableLayoutPanel1).Dock = (DockStyle)5;
		TableLayoutPanel1.GrowStyle = (TableLayoutPanelGrowStyle)0;
		((Control)TableLayoutPanel1).Location = new Point(3, 16);
		((Control)TableLayoutPanel1).MinimumSize = new Size(200, 100);
		((Control)TableLayoutPanel1).Name = "TableLayoutPanel1";
		TableLayoutPanel1.RowCount = 7;
		TableLayoutPanel1.RowStyles.Add(new RowStyle((SizeType)2, 14.28571f));
		TableLayoutPanel1.RowStyles.Add(new RowStyle((SizeType)2, 14.28572f));
		TableLayoutPanel1.RowStyles.Add(new RowStyle((SizeType)2, 14.28572f));
		TableLayoutPanel1.RowStyles.Add(new RowStyle((SizeType)2, 14.28572f));
		TableLayoutPanel1.RowStyles.Add(new RowStyle((SizeType)2, 14.28572f));
		TableLayoutPanel1.RowStyles.Add(new RowStyle((SizeType)2, 14.28572f));
		TableLayoutPanel1.RowStyles.Add(new RowStyle((SizeType)2, 14.28572f));
		((Control)TableLayoutPanel1).Size = new Size(305, 159);
		((Control)TableLayoutPanel1).TabIndex = 0;
		((Control)GlobalStats_CountTotal).AutoSize = true;
		((Control)GlobalStats_CountTotal).BackColor = Color.Transparent;
		((Control)GlobalStats_CountTotal).Dock = (DockStyle)5;
		((Control)GlobalStats_CountTotal).Location = new Point(44, 134);
		((Control)GlobalStats_CountTotal).Margin = new Padding(3, 2, 3, 2);
		((Control)GlobalStats_CountTotal).Name = "GlobalStats_CountTotal";
		((Control)GlobalStats_CountTotal).Size = new Size(35, 23);
		((Control)GlobalStats_CountTotal).TabIndex = 1;
		((Control)GlobalStats_CountTotal).Text = "0";
		GlobalStats_CountTotal.TextAlign = (ContentAlignment)64;
		((Control)GlobalStats_PS2DVD_Count).AutoSize = true;
		((Control)GlobalStats_PS2DVD_Count).BackColor = Color.Transparent;
		((Control)GlobalStats_PS2DVD_Count).Dock = (DockStyle)5;
		((Control)GlobalStats_PS2DVD_Count).Location = new Point(44, 68);
		((Control)GlobalStats_PS2DVD_Count).Margin = new Padding(3, 2, 3, 2);
		((Control)GlobalStats_PS2DVD_Count).Name = "GlobalStats_PS2DVD_Count";
		((Control)GlobalStats_PS2DVD_Count).Size = new Size(35, 18);
		((Control)GlobalStats_PS2DVD_Count).TabIndex = 13;
		((Control)GlobalStats_PS2DVD_Count).Text = "0";
		GlobalStats_PS2DVD_Count.TextAlign = (ContentAlignment)64;
		((Control)GlobalStats_Total).AutoSize = true;
		((Control)GlobalStats_Total).Dock = (DockStyle)5;
		((Control)GlobalStats_Total).Location = new Point(3, 134);
		((Control)GlobalStats_Total).Margin = new Padding(3, 2, 3, 2);
		((Control)GlobalStats_Total).Name = "GlobalStats_Total";
		((Control)GlobalStats_Total).Size = new Size(35, 23);
		((Control)GlobalStats_Total).TabIndex = 0;
		((Control)GlobalStats_Total).Text = "Total";
		GlobalStats_Total.TextAlign = (ContentAlignment)64;
		((Control)GlobalStats_PS2DVD).AutoSize = true;
		((Control)GlobalStats_PS2DVD).Dock = (DockStyle)5;
		((Control)GlobalStats_PS2DVD).Location = new Point(3, 68);
		((Control)GlobalStats_PS2DVD).Margin = new Padding(3, 2, 3, 2);
		((Control)GlobalStats_PS2DVD).Name = "GlobalStats_PS2DVD";
		((Control)GlobalStats_PS2DVD).Size = new Size(35, 18);
		((Control)GlobalStats_PS2DVD).TabIndex = 3;
		((Control)GlobalStats_PS2DVD).Text = "DVD";
		GlobalStats_PS2DVD.TextAlign = (ContentAlignment)64;
		((Control)GlobalStats_PS2CD).AutoSize = true;
		((Control)GlobalStats_PS2CD).Dock = (DockStyle)5;
		((Control)GlobalStats_PS2CD).Location = new Point(3, 46);
		((Control)GlobalStats_PS2CD).Margin = new Padding(3, 2, 3, 2);
		((Control)GlobalStats_PS2CD).Name = "GlobalStats_PS2CD";
		((Control)GlobalStats_PS2CD).Size = new Size(35, 18);
		((Control)GlobalStats_PS2CD).TabIndex = 2;
		((Control)GlobalStats_PS2CD).Text = "CD";
		GlobalStats_PS2CD.TextAlign = (ContentAlignment)64;
		((Control)GlobalStats_PS2CD_Count).AutoSize = true;
		((Control)GlobalStats_PS2CD_Count).BackColor = Color.Transparent;
		((Control)GlobalStats_PS2CD_Count).Dock = (DockStyle)5;
		((Control)GlobalStats_PS2CD_Count).Location = new Point(44, 46);
		((Control)GlobalStats_PS2CD_Count).Margin = new Padding(3, 2, 3, 2);
		((Control)GlobalStats_PS2CD_Count).Name = "GlobalStats_PS2CD_Count";
		((Control)GlobalStats_PS2CD_Count).Size = new Size(35, 18);
		((Control)GlobalStats_PS2CD_Count).TabIndex = 12;
		((Control)GlobalStats_PS2CD_Count).Text = "0";
		GlobalStats_PS2CD_Count.TextAlign = (ContentAlignment)64;
		((Control)GlobalStats_Count).AutoSize = true;
		((Control)GlobalStats_Count).Location = new Point(44, 0);
		((Control)GlobalStats_Count).Name = "GlobalStats_Count";
		((Control)GlobalStats_Count).Size = new Size(35, 13);
		((Control)GlobalStats_Count).TabIndex = 14;
		((Control)GlobalStats_Count).Text = "Count";
		((Control)GlobalStats_Size).AutoSize = true;
		((Control)GlobalStats_Size).Location = new Point(85, 0);
		((Control)GlobalStats_Size).Name = "GlobalStats_Size";
		((Control)GlobalStats_Size).Size = new Size(27, 13);
		((Control)GlobalStats_Size).TabIndex = 15;
		((Control)GlobalStats_Size).Text = "Size";
		((Control)GlobalStats_PS2CD_Size).AutoSize = true;
		((Control)GlobalStats_PS2CD_Size).BackColor = Color.Transparent;
		((Control)GlobalStats_PS2CD_Size).Dock = (DockStyle)5;
		((Control)GlobalStats_PS2CD_Size).Location = new Point(85, 46);
		((Control)GlobalStats_PS2CD_Size).Margin = new Padding(3, 2, 3, 2);
		((Control)GlobalStats_PS2CD_Size).Name = "GlobalStats_PS2CD_Size";
		((Control)GlobalStats_PS2CD_Size).Size = new Size(27, 18);
		((Control)GlobalStats_PS2CD_Size).TabIndex = 16;
		((Control)GlobalStats_PS2CD_Size).Text = "0";
		GlobalStats_PS2CD_Size.TextAlign = (ContentAlignment)64;
		((Control)GlobalStats_PS2DVD_Size).AutoSize = true;
		((Control)GlobalStats_PS2DVD_Size).BackColor = Color.Transparent;
		((Control)GlobalStats_PS2DVD_Size).Dock = (DockStyle)5;
		((Control)GlobalStats_PS2DVD_Size).Location = new Point(85, 68);
		((Control)GlobalStats_PS2DVD_Size).Margin = new Padding(3, 2, 3, 2);
		((Control)GlobalStats_PS2DVD_Size).Name = "GlobalStats_PS2DVD_Size";
		((Control)GlobalStats_PS2DVD_Size).Size = new Size(27, 18);
		((Control)GlobalStats_PS2DVD_Size).TabIndex = 17;
		((Control)GlobalStats_PS2DVD_Size).Text = "0";
		GlobalStats_PS2DVD_Size.TextAlign = (ContentAlignment)64;
		((Control)GlobalStats_SizeTotal).AutoSize = true;
		((Control)GlobalStats_SizeTotal).BackColor = Color.Transparent;
		((Control)GlobalStats_SizeTotal).Dock = (DockStyle)5;
		((Control)GlobalStats_SizeTotal).Location = new Point(85, 134);
		((Control)GlobalStats_SizeTotal).Margin = new Padding(3, 2, 3, 2);
		((Control)GlobalStats_SizeTotal).Name = "GlobalStats_SizeTotal";
		((Control)GlobalStats_SizeTotal).Size = new Size(27, 23);
		((Control)GlobalStats_SizeTotal).TabIndex = 18;
		((Control)GlobalStats_SizeTotal).Text = "0";
		GlobalStats_SizeTotal.TextAlign = (ContentAlignment)64;
		((Control)GlobalStats_PS1).AutoSize = true;
		((Control)GlobalStats_PS1).Dock = (DockStyle)5;
		((Control)GlobalStats_PS1).Location = new Point(3, 90);
		((Control)GlobalStats_PS1).Margin = new Padding(3, 2, 3, 2);
		((Control)GlobalStats_PS1).Name = "GlobalStats_PS1";
		((Control)GlobalStats_PS1).Size = new Size(35, 18);
		((Control)GlobalStats_PS1).TabIndex = 23;
		((Control)GlobalStats_PS1).Text = "PS1";
		GlobalStats_PS1.TextAlign = (ContentAlignment)64;
		((Control)GlobalStats_APPS).AutoSize = true;
		((Control)GlobalStats_APPS).Dock = (DockStyle)5;
		((Control)GlobalStats_APPS).Location = new Point(3, 112);
		((Control)GlobalStats_APPS).Margin = new Padding(3, 2, 3, 2);
		((Control)GlobalStats_APPS).Name = "GlobalStats_APPS";
		((Control)GlobalStats_APPS).Size = new Size(35, 18);
		((Control)GlobalStats_APPS).TabIndex = 24;
		((Control)GlobalStats_APPS).Text = "APPS";
		GlobalStats_APPS.TextAlign = (ContentAlignment)64;
		((Control)GlobalStats_PS1_Count).AutoSize = true;
		((Control)GlobalStats_PS1_Count).BackColor = Color.Transparent;
		((Control)GlobalStats_PS1_Count).Dock = (DockStyle)5;
		((Control)GlobalStats_PS1_Count).Location = new Point(44, 90);
		((Control)GlobalStats_PS1_Count).Margin = new Padding(3, 2, 3, 2);
		((Control)GlobalStats_PS1_Count).Name = "GlobalStats_PS1_Count";
		((Control)GlobalStats_PS1_Count).Size = new Size(35, 18);
		((Control)GlobalStats_PS1_Count).TabIndex = 25;
		((Control)GlobalStats_PS1_Count).Text = "0";
		GlobalStats_PS1_Count.TextAlign = (ContentAlignment)64;
		((Control)GlobalStats_PS1_Size).AutoSize = true;
		((Control)GlobalStats_PS1_Size).BackColor = Color.Transparent;
		((Control)GlobalStats_PS1_Size).Dock = (DockStyle)5;
		((Control)GlobalStats_PS1_Size).Location = new Point(85, 90);
		((Control)GlobalStats_PS1_Size).Margin = new Padding(3, 2, 3, 2);
		((Control)GlobalStats_PS1_Size).Name = "GlobalStats_PS1_Size";
		((Control)GlobalStats_PS1_Size).Size = new Size(27, 18);
		((Control)GlobalStats_PS1_Size).TabIndex = 26;
		((Control)GlobalStats_PS1_Size).Text = "0";
		GlobalStats_PS1_Size.TextAlign = (ContentAlignment)64;
		((Control)GlobalStats_APP_Count).AutoSize = true;
		((Control)GlobalStats_APP_Count).BackColor = Color.Transparent;
		((Control)GlobalStats_APP_Count).Dock = (DockStyle)5;
		((Control)GlobalStats_APP_Count).Location = new Point(44, 112);
		((Control)GlobalStats_APP_Count).Margin = new Padding(3, 2, 3, 2);
		((Control)GlobalStats_APP_Count).Name = "GlobalStats_APP_Count";
		((Control)GlobalStats_APP_Count).Size = new Size(35, 18);
		((Control)GlobalStats_APP_Count).TabIndex = 27;
		((Control)GlobalStats_APP_Count).Text = "0";
		GlobalStats_APP_Count.TextAlign = (ContentAlignment)64;
		((Control)GlobalStats_APP_Size).AutoSize = true;
		((Control)GlobalStats_APP_Size).BackColor = Color.Transparent;
		((Control)GlobalStats_APP_Size).Dock = (DockStyle)5;
		((Control)GlobalStats_APP_Size).Location = new Point(85, 112);
		((Control)GlobalStats_APP_Size).Margin = new Padding(3, 2, 3, 2);
		((Control)GlobalStats_APP_Size).Name = "GlobalStats_APP_Size";
		((Control)GlobalStats_APP_Size).Size = new Size(27, 18);
		((Control)GlobalStats_APP_Size).TabIndex = 28;
		((Control)GlobalStats_APP_Size).Text = "0";
		GlobalStats_APP_Size.TextAlign = (ContentAlignment)64;
		((Control)GlobalStats_PS2).AutoSize = true;
		((Control)GlobalStats_PS2).Dock = (DockStyle)5;
		((Control)GlobalStats_PS2).Location = new Point(3, 24);
		((Control)GlobalStats_PS2).Margin = new Padding(3, 2, 3, 2);
		((Control)GlobalStats_PS2).Name = "GlobalStats_PS2";
		((Control)GlobalStats_PS2).Size = new Size(35, 18);
		((Control)GlobalStats_PS2).TabIndex = 20;
		((Control)GlobalStats_PS2).Text = "PS2";
		GlobalStats_PS2.TextAlign = (ContentAlignment)64;
		((Control)GlobalStats_PS2_Count).AutoSize = true;
		((Control)GlobalStats_PS2_Count).BackColor = Color.Transparent;
		((Control)GlobalStats_PS2_Count).Dock = (DockStyle)5;
		((Control)GlobalStats_PS2_Count).ForeColor = SystemColors.ControlText;
		((Control)GlobalStats_PS2_Count).Location = new Point(44, 24);
		((Control)GlobalStats_PS2_Count).Margin = new Padding(3, 2, 3, 2);
		((Control)GlobalStats_PS2_Count).Name = "GlobalStats_PS2_Count";
		((Control)GlobalStats_PS2_Count).Size = new Size(35, 18);
		((Control)GlobalStats_PS2_Count).TabIndex = 21;
		((Control)GlobalStats_PS2_Count).Text = "0";
		GlobalStats_PS2_Count.TextAlign = (ContentAlignment)64;
		((Control)GlobalStats_PS2_Size).AutoSize = true;
		((Control)GlobalStats_PS2_Size).BackColor = Color.Transparent;
		((Control)GlobalStats_PS2_Size).Dock = (DockStyle)5;
		((Control)GlobalStats_PS2_Size).Location = new Point(85, 24);
		((Control)GlobalStats_PS2_Size).Margin = new Padding(3, 2, 3, 2);
		((Control)GlobalStats_PS2_Size).Name = "GlobalStats_PS2_Size";
		((Control)GlobalStats_PS2_Size).Size = new Size(27, 18);
		((Control)GlobalStats_PS2_Size).TabIndex = 22;
		((Control)GlobalStats_PS2_Size).Text = "0";
		GlobalStats_PS2_Size.TextAlign = (ContentAlignment)64;
		((Control)GameDetails).Controls.Add((Control)(object)Cover_CoverIMG);
		((Control)GameDetails).Controls.Add((Control)(object)Disc_DiscIMG);
		((Control)GameDetails).Controls.Add((Control)(object)Operations);
		((Control)GameDetails).Controls.Add((Control)(object)GameDetails_TB_Title);
		((Control)GameDetails).Controls.Add((Control)(object)GameDetails_Label_Title);
		((Control)GameDetails).Controls.Add((Control)(object)GameDetails_TB_ID);
		((Control)GameDetails).Controls.Add((Control)(object)GameDetails_Label_Path);
		((Control)GameDetails).Controls.Add((Control)(object)GameDetails_Label_ID);
		((Control)GameDetails).Controls.Add((Control)(object)GameDetails_TB_Path);
		((Control)GameDetails).Location = new Point(3, 51);
		((Control)GameDetails).Name = "GameDetails";
		((Control)GameDetails).Size = new Size(316, 347);
		((Control)GameDetails).TabIndex = 15;
		GameDetails.TabStop = false;
		((Control)GameDetails).Text = "Game Details";
		Cover_CoverIMG.Image = (Image)componentResourceManager.GetObject("Cover_CoverIMG.Image");
		((Control)Cover_CoverIMG).Location = new Point(165, 137);
		((Control)Cover_CoverIMG).Name = "Cover_CoverIMG";
		((Control)Cover_CoverIMG).Size = new Size(140, 200);
		Cover_CoverIMG.SizeMode = (PictureBoxSizeMode)4;
		Cover_CoverIMG.TabIndex = 2;
		Cover_CoverIMG.TabStop = false;
		((Control)Disc_DiscIMG).BackColor = Color.Gray;
		((Control)Disc_DiscIMG).BackgroundImageLayout = (ImageLayout)3;
		Disc_DiscIMG.Image = (Image)componentResourceManager.GetObject("Disc_DiscIMG.Image");
		((Control)Disc_DiscIMG).Location = new Point(206, 67);
		((Control)Disc_DiscIMG).Name = "Disc_DiscIMG";
		((Control)Disc_DiscIMG).Size = new Size(64, 64);
		Disc_DiscIMG.SizeMode = (PictureBoxSizeMode)1;
		Disc_DiscIMG.TabIndex = 5;
		Disc_DiscIMG.TabStop = false;
		((Control)Operations).Controls.Add((Control)(object)Operations_ManageARTs);
		((Control)Operations).Controls.Add((Control)(object)Operations_CheatEditor);
		((Control)Operations).Controls.Add((Control)(object)Operations_Hash);
		((Control)Operations).Controls.Add((Control)(object)Operations_EditCFG);
		((Control)Operations).Controls.Add((Control)(object)Operations_UpdateTitle);
		((Control)Operations).Controls.Add((Control)(object)Operations_DeleteGame);
		((Control)Operations).Location = new Point(13, 93);
		((Control)Operations).Name = "Operations";
		((Control)Operations).Size = new Size(139, 248);
		((Control)Operations).TabIndex = 14;
		Operations.TabStop = false;
		((Control)Operations).Text = "Operations";
		((Control)Operations_ManageARTs).Location = new Point(6, 180);
		((Control)Operations_ManageARTs).Name = "Operations_ManageARTs";
		((Control)Operations_ManageARTs).Size = new Size(122, 23);
		((Control)Operations_ManageARTs).TabIndex = 17;
		((Control)Operations_ManageARTs).Text = "Manage ARTs";
		((ButtonBase)Operations_ManageARTs).UseVisualStyleBackColor = true;
		((Control)Operations_ManageARTs).Click += Button1_Click_2;
		((Control)Operations_CheatEditor).Location = new Point(6, 220);
		((Control)Operations_CheatEditor).Name = "Operations_CheatEditor";
		((Control)Operations_CheatEditor).Size = new Size(122, 23);
		((Control)Operations_CheatEditor).TabIndex = 16;
		((Control)Operations_CheatEditor).Text = "Cheat Editor";
		((ButtonBase)Operations_CheatEditor).UseVisualStyleBackColor = true;
		((Control)Operations_CheatEditor).Click += Button1_Click_1;
		((Control)Operations_Hash).Location = new Point(6, 100);
		((Control)Operations_Hash).Name = "Operations_Hash";
		((Control)Operations_Hash).Size = new Size(122, 23);
		((Control)Operations_Hash).TabIndex = 15;
		((Control)Operations_Hash).Text = "Check Game Hash";
		((ButtonBase)Operations_Hash).UseVisualStyleBackColor = true;
		((Control)Operations_Hash).Click += B_Hash_Click;
		((Control)Operations_EditCFG).Location = new Point(6, 140);
		((Control)Operations_EditCFG).Name = "Operations_EditCFG";
		((Control)Operations_EditCFG).Size = new Size(122, 23);
		((Control)Operations_EditCFG).TabIndex = 14;
		((Control)Operations_EditCFG).Text = "Edit CFG";
		((ButtonBase)Operations_EditCFG).UseVisualStyleBackColor = true;
		((Control)Operations_EditCFG).Click += B_EditCFG_Click;
		((Control)Operations_UpdateTitle).Location = new Point(6, 60);
		((Control)Operations_UpdateTitle).Name = "Operations_UpdateTitle";
		((Control)Operations_UpdateTitle).Size = new Size(122, 23);
		((Control)Operations_UpdateTitle).TabIndex = 12;
		((Control)Operations_UpdateTitle).Text = "Rename";
		((ButtonBase)Operations_UpdateTitle).UseVisualStyleBackColor = true;
		((Control)Operations_UpdateTitle).Click += B_UpdateTitle_Click;
		((Control)Operations_DeleteGame).Location = new Point(6, 20);
		((Control)Operations_DeleteGame).Name = "Operations_DeleteGame";
		((Control)Operations_DeleteGame).Size = new Size(123, 23);
		((Control)Operations_DeleteGame).TabIndex = 11;
		((Control)Operations_DeleteGame).Text = "Delete game";
		((ButtonBase)Operations_DeleteGame).UseVisualStyleBackColor = true;
		((Control)Operations_DeleteGame).Click += B_DeleteGame_Click;
		((Control)GameDetails_TB_Title).Location = new Point(59, 17);
		((TextBoxBase)GameDetails_TB_Title).MaxLength = 32;
		((Control)GameDetails_TB_Title).Name = "GameDetails_TB_Title";
		((TextBoxBase)GameDetails_TB_Title).ReadOnly = true;
		((Control)GameDetails_TB_Title).Size = new Size(251, 20);
		((Control)GameDetails_TB_Title).TabIndex = 16;
		((Control)GameDetails_Label_Title).Location = new Point(2, 16);
		((Control)GameDetails_Label_Title).Name = "GameDetails_Label_Title";
		((Control)GameDetails_Label_Title).Size = new Size(57, 20);
		((Control)GameDetails_Label_Title).TabIndex = 15;
		((Control)GameDetails_Label_Title).Text = "Title:";
		GameDetails_Label_Title.TextAlign = (ContentAlignment)64;
		((Control)GameDetails_TB_ID).Location = new Point(59, 67);
		((Control)GameDetails_TB_ID).Name = "GameDetails_TB_ID";
		((TextBoxBase)GameDetails_TB_ID).ReadOnly = true;
		((Control)GameDetails_TB_ID).Size = new Size(98, 20);
		((Control)GameDetails_TB_ID).TabIndex = 1;
		((Control)GameDetails_Label_Path).Location = new Point(2, 41);
		((Control)GameDetails_Label_Path).Name = "GameDetails_Label_Path";
		((Control)GameDetails_Label_Path).Size = new Size(57, 20);
		((Control)GameDetails_Label_Path).TabIndex = 7;
		((Control)GameDetails_Label_Path).Text = "Path:";
		GameDetails_Label_Path.TextAlign = (ContentAlignment)64;
		((Control)GameDetails_Label_ID).Location = new Point(6, 66);
		((Control)GameDetails_Label_ID).Name = "GameDetails_Label_ID";
		((Control)GameDetails_Label_ID).Size = new Size(53, 20);
		((Control)GameDetails_Label_ID).TabIndex = 0;
		((Control)GameDetails_Label_ID).Text = "ID:";
		GameDetails_Label_ID.TextAlign = (ContentAlignment)64;
		((Control)GameDetails_TB_Path).Location = new Point(59, 41);
		((Control)GameDetails_TB_Path).Name = "GameDetails_TB_Path";
		((TextBoxBase)GameDetails_TB_Path).ReadOnly = true;
		((Control)GameDetails_TB_Path).Size = new Size(251, 20);
		((Control)GameDetails_TB_Path).TabIndex = 8;
		((Control)ToolStripContainer1.BottomToolStripPanel).Controls.Add((Control)(object)StatusStrip1);
		((ScrollableControl)ToolStripContainer1.ContentPanel).AutoScroll = true;
		((Control)ToolStripContainer1.ContentPanel).Controls.Add((Control)(object)Tab_Main);
		((Control)ToolStripContainer1.ContentPanel).Size = new Size(864, 615);
		((Control)ToolStripContainer1).Dock = (DockStyle)5;
		ToolStripContainer1.LeftToolStripPanelVisible = false;
		((Control)ToolStripContainer1).Location = new Point(0, 0);
		((Control)ToolStripContainer1).Name = "ToolStripContainer1";
		ToolStripContainer1.RightToolStripPanelVisible = false;
		((Control)ToolStripContainer1).Size = new Size(864, 661);
		((Control)ToolStripContainer1).TabIndex = 2;
		((Control)ToolStripContainer1.TopToolStripPanel).Controls.Add((Control)(object)MenuStrip1);
		((Control)StatusStrip1).Dock = (DockStyle)0;
		((ToolStrip)StatusStrip1).Items.AddRange((ToolStripItem[])(object)new ToolStripItem[4]
		{
			(ToolStripItem)StatusBar_Online,
			(ToolStripItem)StatusBar_CountArts,
			(ToolStripItem)StatusBar_ServerTime,
			(ToolStripItem)StatusBar_Mode
		});
		((Control)StatusStrip1).Location = new Point(0, 0);
		((Control)StatusStrip1).Name = "StatusStrip1";
		((Control)StatusStrip1).Size = new Size(864, 22);
		((Control)StatusStrip1).TabIndex = 1;
		((Control)StatusStrip1).Text = "StatusStrip1";
		((ToolStripItem)StatusBar_Online).Name = "StatusBar_Online";
		((ToolStripItem)StatusBar_Online).Size = new Size(101, 17);
		((ToolStripItem)StatusBar_Online).Text = "Users Online: N/A";
		((ToolStripItem)StatusBar_CountArts).Name = "StatusBar_CountArts";
		((ToolStripItem)StatusBar_CountArts).Size = new Size(89, 17);
		((ToolStripItem)StatusBar_CountArts).Text = "Total ARTS: N/A";
		((ToolStripItem)StatusBar_ServerTime).Name = "StatusBar_ServerTime";
		((ToolStripItem)StatusBar_ServerTime).Size = new Size(96, 17);
		((ToolStripItem)StatusBar_ServerTime).Text = "Server Time: N/A";
		((ToolStripItem)StatusBar_Mode).Name = "StatusBar_Mode";
		((ToolStripItem)StatusBar_Mode).Size = new Size(119, 17);
		((ToolStripItem)StatusBar_Mode).Text = "ToolStripStatusLabel1";
		((Control)Tab_Main).Controls.Add((Control)(object)TabHome);
		((Control)Tab_Main).Controls.Add((Control)(object)TabBadIsos);
		((Control)Tab_Main).Dock = (DockStyle)5;
		((Control)Tab_Main).Location = new Point(0, 0);
		((Control)Tab_Main).Name = "Tab_Main";
		Tab_Main.SelectedIndex = 0;
		((Control)Tab_Main).Size = new Size(864, 615);
		((Control)Tab_Main).TabIndex = 4;
		((Control)TabHome).Controls.Add((Control)(object)SplitContainer1);
		TabHome.Location = new Point(4, 22);
		((Control)TabHome).Name = "TabHome";
		((Control)TabHome).Padding = new Padding(3);
		((Control)TabHome).Size = new Size(856, 589);
		TabHome.TabIndex = 0;
		((Control)TabHome).Text = "Home";
		TabHome.UseVisualStyleBackColor = true;
		((Control)TabBadIsos).Controls.Add((Control)(object)SplitContainer2);
		TabBadIsos.Location = new Point(4, 24);
		((Control)TabBadIsos).Name = "TabBadIsos";
		((Control)TabBadIsos).Padding = new Padding(3);
		((Control)TabBadIsos).Size = new Size(856, 587);
		TabBadIsos.TabIndex = 1;
		((Control)TabBadIsos).Text = "Bad ISO's";
		TabBadIsos.UseVisualStyleBackColor = true;
		SplitContainer2.Dock = (DockStyle)5;
		SplitContainer2.FixedPanel = (FixedPanel)2;
		((Control)SplitContainer2).Location = new Point(3, 3);
		((Control)SplitContainer2).Name = "SplitContainer2";
		((Control)SplitContainer2.Panel1).Controls.Add((Control)(object)BadIsoList);
		((Control)SplitContainer2.Panel2).Controls.Add((Control)(object)BadISO_BatchRenameVCD);
		((Control)SplitContainer2.Panel2).Controls.Add((Control)(object)BadISO_BatchRenameISO);
		((Control)SplitContainer2.Panel2).Controls.Add((Control)(object)GB_BadIsoDetails);
		((Control)SplitContainer2).Size = new Size(850, 581);
		SplitContainer2.SplitterDistance = 460;
		((Control)SplitContainer2).TabIndex = 1;
		BadIsoList.Columns.AddRange((ColumnHeader[])(object)new ColumnHeader[6] { ColumnHeader1, ColumnHeader2, ColumnHeader5, ColumnHeader3, ColumnHeader4, ColumnHeader6 });
		((Control)BadIsoList).Dock = (DockStyle)5;
		BadIsoList.FullRowSelect = true;
		((Control)BadIsoList).Location = new Point(0, 0);
		BadIsoList.MultiSelect = false;
		((Control)BadIsoList).Name = "BadIsoList";
		((Control)BadIsoList).Size = new Size(460, 581);
		((Control)BadIsoList).TabIndex = 0;
		BadIsoList.UseCompatibleStateImageBehavior = false;
		BadIsoList.View = (View)1;
		BadIsoList.ItemSelectionChanged += new ListViewItemSelectionChangedEventHandler(BadIsoList_ItemSelectionChanged);
		ColumnHeader1.Text = "Game";
		ColumnHeader1.Width = 93;
		ColumnHeader2.Text = "ID";
		ColumnHeader5.Text = "Type";
		ColumnHeader3.Text = "Size";
		ColumnHeader4.Text = "Type";
		ColumnHeader4.Width = 75;
		ColumnHeader6.Text = "Error";
		((Control)BadISO_BatchRenameVCD).Location = new Point(4, 290);
		((Control)BadISO_BatchRenameVCD).Name = "BadISO_BatchRenameVCD";
		((Control)BadISO_BatchRenameVCD).Size = new Size(378, 23);
		((Control)BadISO_BatchRenameVCD).TabIndex = 26;
		((Control)BadISO_BatchRenameVCD).Text = "Batch Convert VCD Naming";
		((ButtonBase)BadISO_BatchRenameVCD).UseVisualStyleBackColor = true;
		((Control)BadISO_BatchRenameVCD).Click += BadISO_BatchRenameVCD_Click;
		((Control)BadISO_BatchRenameISO).Location = new Point(3, 261);
		((Control)BadISO_BatchRenameISO).Name = "BadISO_BatchRenameISO";
		((Control)BadISO_BatchRenameISO).Size = new Size(378, 23);
		((Control)BadISO_BatchRenameISO).TabIndex = 25;
		((Control)BadISO_BatchRenameISO).Text = "Convert between ISO naming formats";
		((ButtonBase)BadISO_BatchRenameISO).UseVisualStyleBackColor = true;
		((Control)BadISO_BatchRenameISO).Click += BadISO_BatchRename_Click;
		((Control)GB_BadIsoDetails).Controls.Add((Control)(object)GroupBox2);
		((Control)GB_BadIsoDetails).Controls.Add((Control)(object)BadISO_GetTitleFromDB);
		((Control)GB_BadIsoDetails).Controls.Add((Control)(object)BadISO_ChangeID);
		((Control)GB_BadIsoDetails).Controls.Add((Control)(object)BadISO_CurrentFileName);
		((Control)GB_BadIsoDetails).Controls.Add((Control)(object)BadISO_GameDetails_Label_CurrentFile);
		((Control)GB_BadIsoDetails).Controls.Add((Control)(object)BadISO_Size);
		((Control)GB_BadIsoDetails).Controls.Add((Control)(object)BadISO_newfile);
		((Control)GB_BadIsoDetails).Controls.Add((Control)(object)BadISO_GameDetails_Label_NewFile);
		((Control)GB_BadIsoDetails).Controls.Add((Control)(object)BadISO_UpdateFileName);
		((Control)GB_BadIsoDetails).Controls.Add((Control)(object)BadIso_title);
		((Control)GB_BadIsoDetails).Controls.Add((Control)(object)BadISO_GameDetails_Label_Size);
		((Control)GB_BadIsoDetails).Controls.Add((Control)(object)BadISO_GameDetails_Label_Title);
		((Control)GB_BadIsoDetails).Controls.Add((Control)(object)BadISO_ID);
		((Control)GB_BadIsoDetails).Controls.Add((Control)(object)BadISO_GameDetails_Label_Path);
		((Control)GB_BadIsoDetails).Controls.Add((Control)(object)BadISO_GameDetails_Label_ID);
		((Control)GB_BadIsoDetails).Controls.Add((Control)(object)BadISO_Path);
		((Control)GB_BadIsoDetails).Location = new Point(3, 3);
		((Control)GB_BadIsoDetails).Name = "GB_BadIsoDetails";
		((Control)GB_BadIsoDetails).Size = new Size(378, 252);
		((Control)GB_BadIsoDetails).TabIndex = 15;
		GB_BadIsoDetails.TabStop = false;
		((Control)GB_BadIsoDetails).Text = "Game Details";
		((Control)GroupBox2).Controls.Add((Control)(object)BadISO_errorMsg);
		((Control)GroupBox2).Location = new Point(12, 181);
		((Control)GroupBox2).Name = "GroupBox2";
		((Control)GroupBox2).Size = new Size(360, 65);
		((Control)GroupBox2).TabIndex = 25;
		GroupBox2.TabStop = false;
		((Control)GroupBox2).Text = "Error";
		((Control)BadISO_errorMsg).Dock = (DockStyle)5;
		((Control)BadISO_errorMsg).Location = new Point(3, 16);
		((Control)BadISO_errorMsg).Name = "BadISO_errorMsg";
		((Control)BadISO_errorMsg).Size = new Size(354, 46);
		((Control)BadISO_errorMsg).TabIndex = 27;
		((Control)BadISO_GetTitleFromDB).Location = new Point(6, 148);
		((Control)BadISO_GetTitleFromDB).Name = "BadISO_GetTitleFromDB";
		((Control)BadISO_GetTitleFromDB).Size = new Size(124, 23);
		((Control)BadISO_GetTitleFromDB).TabIndex = 24;
		((Control)BadISO_GetTitleFromDB).Text = "Get title from DB";
		((ButtonBase)BadISO_GetTitleFromDB).UseVisualStyleBackColor = true;
		((Control)BadISO_GetTitleFromDB).Click += BadISO_GetTitleFromDB_Click;
		((Control)BadISO_ChangeID).Location = new Point(160, 60);
		((Control)BadISO_ChangeID).Name = "BadISO_ChangeID";
		((Control)BadISO_ChangeID).Size = new Size(59, 23);
		((Control)BadISO_ChangeID).TabIndex = 23;
		((Control)BadISO_ChangeID).Text = "Change";
		((ButtonBase)BadISO_ChangeID).UseVisualStyleBackColor = true;
		((Control)BadISO_ChangeID).Click += Button1_Click;
		((Control)BadISO_CurrentFileName).Location = new Point(101, 86);
		((Control)BadISO_CurrentFileName).Name = "BadISO_CurrentFileName";
		((TextBoxBase)BadISO_CurrentFileName).ReadOnly = true;
		((Control)BadISO_CurrentFileName).Size = new Size(271, 20);
		((Control)BadISO_CurrentFileName).TabIndex = 22;
		((Control)BadISO_GameDetails_Label_CurrentFile).Location = new Point(6, 89);
		((Control)BadISO_GameDetails_Label_CurrentFile).Name = "BadISO_GameDetails_Label_CurrentFile";
		((Control)BadISO_GameDetails_Label_CurrentFile).Size = new Size(89, 13);
		((Control)BadISO_GameDetails_Label_CurrentFile).TabIndex = 21;
		((Control)BadISO_GameDetails_Label_CurrentFile).Text = "Current file name:";
		((Control)BadISO_Size).Location = new Point(292, 60);
		((Control)BadISO_Size).Name = "BadISO_Size";
		((TextBoxBase)BadISO_Size).ReadOnly = true;
		((Control)BadISO_Size).Size = new Size(80, 20);
		((Control)BadISO_Size).TabIndex = 20;
		((Control)BadISO_newfile).Location = new Point(101, 112);
		((Control)BadISO_newfile).Name = "BadISO_newfile";
		((TextBoxBase)BadISO_newfile).ReadOnly = true;
		((Control)BadISO_newfile).Size = new Size(271, 20);
		((Control)BadISO_newfile).TabIndex = 19;
		((Control)BadISO_GameDetails_Label_NewFile).Location = new Point(6, 115);
		((Control)BadISO_GameDetails_Label_NewFile).Name = "BadISO_GameDetails_Label_NewFile";
		((Control)BadISO_GameDetails_Label_NewFile).Size = new Size(89, 13);
		((Control)BadISO_GameDetails_Label_NewFile).TabIndex = 18;
		((Control)BadISO_GameDetails_Label_NewFile).Text = "New file name:";
		((Control)BadISO_UpdateFileName).Location = new Point(183, 148);
		((Control)BadISO_UpdateFileName).Name = "BadISO_UpdateFileName";
		((Control)BadISO_UpdateFileName).Size = new Size(189, 23);
		((Control)BadISO_UpdateFileName).TabIndex = 17;
		((Control)BadISO_UpdateFileName).Text = "Try update file name!";
		((ButtonBase)BadISO_UpdateFileName).UseVisualStyleBackColor = true;
		((Control)BadISO_UpdateFileName).Click += BadISO_UpdateFileName_Click;
		((Control)BadIso_title).Location = new Point(74, 13);
		((TextBoxBase)BadIso_title).MaxLength = 32;
		((Control)BadIso_title).Name = "BadIso_title";
		((Control)BadIso_title).Size = new Size(298, 20);
		((Control)BadIso_title).TabIndex = 16;
		((Control)BadIso_title).TextChanged += BadIso_GameTitleChanged;
		((Control)BadISO_GameDetails_Label_Size).Location = new Point(225, 65);
		((Control)BadISO_GameDetails_Label_Size).Name = "BadISO_GameDetails_Label_Size";
		((Control)BadISO_GameDetails_Label_Size).Size = new Size(61, 13);
		((Control)BadISO_GameDetails_Label_Size).TabIndex = 15;
		((Control)BadISO_GameDetails_Label_Size).Text = "Size:";
		((Control)BadISO_GameDetails_Label_Title).Location = new Point(6, 16);
		((Control)BadISO_GameDetails_Label_Title).Name = "BadISO_GameDetails_Label_Title";
		((Control)BadISO_GameDetails_Label_Title).Size = new Size(62, 13);
		((Control)BadISO_GameDetails_Label_Title).TabIndex = 15;
		((Control)BadISO_GameDetails_Label_Title).Text = "Title:";
		((Control)BadISO_ID).Location = new Point(53, 62);
		((Control)BadISO_ID).Name = "BadISO_ID";
		((TextBoxBase)BadISO_ID).ReadOnly = true;
		((Control)BadISO_ID).Size = new Size(101, 20);
		((Control)BadISO_ID).TabIndex = 1;
		((Control)BadISO_ID).TextChanged += BadIso_GameTitleChanged;
		((Control)BadISO_GameDetails_Label_Path).Location = new Point(6, 39);
		((Control)BadISO_GameDetails_Label_Path).Name = "BadISO_GameDetails_Label_Path";
		((Control)BadISO_GameDetails_Label_Path).Size = new Size(62, 13);
		((Control)BadISO_GameDetails_Label_Path).TabIndex = 7;
		((Control)BadISO_GameDetails_Label_Path).Text = "Path:";
		((Control)BadISO_GameDetails_Label_ID).Location = new Point(9, 67);
		((Control)BadISO_GameDetails_Label_ID).Name = "BadISO_GameDetails_Label_ID";
		((Control)BadISO_GameDetails_Label_ID).Size = new Size(38, 13);
		((Control)BadISO_GameDetails_Label_ID).TabIndex = 0;
		((Control)BadISO_GameDetails_Label_ID).Text = "ID:";
		((Control)BadISO_Path).Location = new Point(74, 36);
		((Control)BadISO_Path).Name = "BadISO_Path";
		((TextBoxBase)BadISO_Path).ReadOnly = true;
		((Control)BadISO_Path).Size = new Size(298, 20);
		((Control)BadISO_Path).TabIndex = 8;
		((Control)MenuStrip1).Dock = (DockStyle)0;
		((ToolStrip)MenuStrip1).Items.AddRange((ToolStripItem[])(object)new ToolStripItem[9]
		{
			(ToolStripItem)Menu_File,
			(ToolStripItem)Menu_BatchActions,
			(ToolStripItem)Menu_View,
			(ToolStripItem)Menu_Settings,
			(ToolStripItem)Menu_NetworkOptions,
			(ToolStripItem)Menu_Tools,
			(ToolStripItem)Menu_LocalHDDOptions,
			(ToolStripItem)Menu_Help,
			(ToolStripItem)Menu_OpenOPLFolder
		});
		((Control)MenuStrip1).Location = new Point(0, 0);
		((Control)MenuStrip1).Name = "MenuStrip1";
		((Control)MenuStrip1).Size = new Size(864, 24);
		((Control)MenuStrip1).TabIndex = 3;
		((Control)MenuStrip1).Text = "MenuStrip1";
		((ToolStripDropDownItem)Menu_File).DropDownItems.AddRange((ToolStripItem[])(object)new ToolStripItem[4]
		{
			(ToolStripItem)Menu_File_Refresh,
			(ToolStripItem)Menu_File_InvalidateCacheAndRefresh,
			(ToolStripItem)Menu_File_ExportCsv,
			(ToolStripItem)Menu_File_Exit
		});
		((ToolStripItem)Menu_File).Name = "Menu_File";
		((ToolStripItem)Menu_File).Size = new Size(37, 20);
		((ToolStripItem)Menu_File).Text = "File";
		((ToolStripItem)Menu_File_Refresh).Name = "Menu_File_Refresh";
		((ToolStripItem)Menu_File_Refresh).Size = new Size(239, 22);
		((ToolStripItem)Menu_File_Refresh).Text = "Refresh list";
		((ToolStripItem)Menu_File_Refresh).Click += RefreshListToolStripMenuItem_Click;
		((ToolStripItem)Menu_File_InvalidateCacheAndRefresh).Name = "Menu_File_InvalidateCacheAndRefresh";
		((ToolStripItem)Menu_File_InvalidateCacheAndRefresh).Size = new Size(239, 22);
		((ToolStripItem)Menu_File_InvalidateCacheAndRefresh).Text = "Invalidate cache and refresh list";
		((ToolStripItem)Menu_File_InvalidateCacheAndRefresh).Click += InvalidateCacheAndRefreshToolStripMenuItem_Click;
		((ToolStripItem)Menu_File_ExportCsv).Name = "Menu_File_ExportCsv";
		((ToolStripItem)Menu_File_ExportCsv).Size = new Size(239, 22);
		((ToolStripItem)Menu_File_ExportCsv).Text = "Export game list to CSV";
		((ToolStripItem)Menu_File_ExportCsv).Click += Menu_File_ExportCsv_Click;
		((ToolStripItem)Menu_File_Exit).Name = "Menu_File_Exit";
		((ToolStripItem)Menu_File_Exit).Size = new Size(239, 22);
		((ToolStripItem)Menu_File_Exit).Text = "Exit";
		((ToolStripItem)Menu_File_Exit).Click += ExitToolStripMenuItem_Click;
		((ToolStripDropDownItem)Menu_BatchActions).DropDownItems.AddRange((ToolStripItem[])(object)new ToolStripItem[3]
		{
			(ToolStripItem)Menu_BatchActions_CoverAndIconDownload,
			(ToolStripItem)Menu_BatchActions_ShareART,
			(ToolStripItem)Menu_BatchActions_UpgradeOldCFGs
		});
		((ToolStripItem)Menu_BatchActions).Name = "Menu_BatchActions";
		((ToolStripItem)Menu_BatchActions).Size = new Size(92, 20);
		((ToolStripItem)Menu_BatchActions).Text = "Batch Actions";
		((ToolStripItem)Menu_BatchActions_CoverAndIconDownload).Name = "Menu_BatchActions_CoverAndIconDownload";
		((ToolStripItem)Menu_BatchActions_CoverAndIconDownload).Size = new Size(172, 22);
		((ToolStripItem)Menu_BatchActions_CoverAndIconDownload).Text = "ART Download";
		((ToolStripItem)Menu_BatchActions_CoverAndIconDownload).Click += BatchCoverAndIconDownloadToolStripMenuItem_Click_1;
		((ToolStripItem)Menu_BatchActions_ShareART).Name = "Menu_BatchActions_ShareART";
		((ToolStripItem)Menu_BatchActions_ShareART).Size = new Size(172, 22);
		((ToolStripItem)Menu_BatchActions_ShareART).Text = "Share all ARTs";
		((ToolStripItem)Menu_BatchActions_ShareART).Click += ShareARTzipToolStripMenuItem_Click;
		((ToolStripItem)Menu_BatchActions_UpgradeOldCFGs).Name = "Menu_BatchActions_UpgradeOldCFGs";
		((ToolStripItem)Menu_BatchActions_UpgradeOldCFGs).Size = new Size(172, 22);
		((ToolStripItem)Menu_BatchActions_UpgradeOldCFGs).Text = "Upgrade old CFG's";
		((ToolStripItem)Menu_BatchActions_UpgradeOldCFGs).Click += UpgradeOldCFGsToolStripMenuItem_Click;
		((ToolStripDropDownItem)Menu_View).DropDownItems.AddRange((ToolStripItem[])(object)new ToolStripItem[3]
		{
			(ToolStripItem)Menu_View_All,
			(ToolStripItem)Menu_View_WithMissing,
			(ToolStripItem)Menu_View_With
		});
		((ToolStripItem)Menu_View).Name = "Menu_View";
		((ToolStripItem)Menu_View).Size = new Size(44, 20);
		((ToolStripItem)Menu_View).Text = "View";
		((ToolStripItem)Menu_View_All).Name = "Menu_View_All";
		((ToolStripItem)Menu_View_All).Size = new Size(246, 22);
		((ToolStripItem)Menu_View_All).Text = "Show all";
		((ToolStripItem)Menu_View_All).Click += ShowAllToolStripMenuItem_Click;
		((ToolStripDropDownItem)Menu_View_WithMissing).DropDownItems.AddRange((ToolStripItem[])(object)new ToolStripItem[3]
		{
			(ToolStripItem)Menu_View_WithMissing_CoverArt,
			(ToolStripItem)Menu_View_WithMissing_DiscArt,
			(ToolStripItem)Menu_View_WithMissing_CoverAndOrDisc
		});
		((ToolStripItem)Menu_View_WithMissing).Name = "Menu_View_WithMissing";
		((ToolStripItem)Menu_View_WithMissing).Size = new Size(246, 22);
		((ToolStripItem)Menu_View_WithMissing).Text = "Show only games with missing...";
		((ToolStripItem)Menu_View_WithMissing_CoverArt).Name = "Menu_View_WithMissing_CoverArt";
		((ToolStripItem)Menu_View_WithMissing_CoverArt).Size = new Size(192, 22);
		((ToolStripItem)Menu_View_WithMissing_CoverArt).Text = "Cover Art";
		((ToolStripItem)Menu_View_WithMissing_CoverArt).Click += MissingCoverArt_Click;
		((ToolStripItem)Menu_View_WithMissing_DiscArt).Name = "Menu_View_WithMissing_DiscArt";
		((ToolStripItem)Menu_View_WithMissing_DiscArt).Size = new Size(192, 22);
		((ToolStripItem)Menu_View_WithMissing_DiscArt).Text = "Disc Art";
		((ToolStripItem)Menu_View_WithMissing_DiscArt).Click += DiscArtToolStripMenuItem_Click;
		((ToolStripItem)Menu_View_WithMissing_CoverAndOrDisc).Name = "Menu_View_WithMissing_CoverAndOrDisc";
		((ToolStripItem)Menu_View_WithMissing_CoverAndOrDisc).Size = new Size(192, 22);
		((ToolStripItem)Menu_View_WithMissing_CoverAndOrDisc).Text = "Cover And/Or Disc Art";
		((ToolStripItem)Menu_View_WithMissing_CoverAndOrDisc).Click += CoverAndOrDiscArtToolStripMenuItem_Click;
		((ToolStripDropDownItem)Menu_View_With).DropDownItems.AddRange((ToolStripItem[])(object)new ToolStripItem[3]
		{
			(ToolStripItem)Menu_View_With_Cover,
			(ToolStripItem)Menu_View_With_Disc,
			(ToolStripItem)Menu_View_With_CoverAndOrDisc
		});
		((ToolStripItem)Menu_View_With).Name = "Menu_View_With";
		((ToolStripItem)Menu_View_With).Size = new Size(246, 22);
		((ToolStripItem)Menu_View_With).Text = "Show only games with...";
		((ToolStripItem)Menu_View_With_Cover).Name = "Menu_View_With_Cover";
		((ToolStripItem)Menu_View_With_Cover).Size = new Size(188, 22);
		((ToolStripItem)Menu_View_With_Cover).Text = "Cover Art";
		((ToolStripItem)Menu_View_With_Cover).Click += OnlyCoverArt_Click;
		((ToolStripItem)Menu_View_With_Disc).Name = "Menu_View_With_Disc";
		((ToolStripItem)Menu_View_With_Disc).Size = new Size(188, 22);
		((ToolStripItem)Menu_View_With_Disc).Text = "Disc Art";
		((ToolStripItem)Menu_View_With_Disc).Click += OnlyDiscArt_Click;
		((ToolStripItem)Menu_View_With_CoverAndOrDisc).Name = "Menu_View_With_CoverAndOrDisc";
		((ToolStripItem)Menu_View_With_CoverAndOrDisc).Size = new Size(188, 22);
		((ToolStripItem)Menu_View_With_CoverAndOrDisc).Text = "Cover and/or Disc Art";
		((ToolStripItem)Menu_View_With_CoverAndOrDisc).Click += OnlyCoverAndorDiscArt_Click;
		((ToolStripDropDownItem)Menu_Settings).DropDownItems.AddRange((ToolStripItem[])(object)new ToolStripItem[6]
		{
			(ToolStripItem)Menu_Settings_ChangeLanguage,
			(ToolStripItem)Menu_Settings_Mode,
			(ToolStripItem)Menu_Settings_GameDoubleClickAction,
			(ToolStripItem)Menu_Settings_AutoUpdate,
			(ToolStripItem)Menu_Settings_CfgDevToggle,
			(ToolStripItem)Menu_Settings_UseOldIsoFormat
		});
		((ToolStripItem)Menu_Settings).Name = "Menu_Settings";
		((ToolStripItem)Menu_Settings).Size = new Size(61, 20);
		((ToolStripItem)Menu_Settings).Text = "Settings";
		((ToolStripItem)Menu_Settings_ChangeLanguage).Name = "Menu_Settings_ChangeLanguage";
		((ToolStripItem)Menu_Settings_ChangeLanguage).Size = new Size(272, 22);
		((ToolStripItem)Menu_Settings_ChangeLanguage).Text = "Change Language";
		((ToolStripItem)Menu_Settings_ChangeLanguage).Click += Menu_Settings_ChangeLanguage_Click;
		((ToolStripItem)Menu_Settings_Mode).Name = "Menu_Settings_Mode";
		((ToolStripItem)Menu_Settings_Mode).Size = new Size(272, 22);
		((ToolStripItem)Menu_Settings_Mode).Text = "Change Mode/OPL Folder";
		((ToolStripItem)Menu_Settings_Mode).Click += Menu_Settings_Mode_Click;
		((ToolStripItem)Menu_Settings_GameDoubleClickAction).Name = "Menu_Settings_GameDoubleClickAction";
		((ToolStripItem)Menu_Settings_GameDoubleClickAction).Size = new Size(272, 22);
		((ToolStripItem)Menu_Settings_GameDoubleClickAction).Text = "Set Game double click action";
		((ToolStripItem)Menu_Settings_GameDoubleClickAction).Click += GameDoubleClickActionToolStripMenuItem_Click;
		((ToolStripItem)Menu_Settings_AutoUpdate).Name = "Menu_Settings_AutoUpdate";
		((ToolStripItem)Menu_Settings_AutoUpdate).Size = new Size(272, 22);
		((ToolStripItem)Menu_Settings_AutoUpdate).Text = "Auto-Update Check";
		((ToolStripItem)Menu_Settings_AutoUpdate).Click += AutoUpdateCheckToolStripMenuItem_Click;
		((ToolStripItem)Menu_Settings_CfgDevToggle).Name = "Menu_Settings_CfgDevToggle";
		((ToolStripItem)Menu_Settings_CfgDevToggle).Size = new Size(272, 22);
		((ToolStripItem)Menu_Settings_CfgDevToggle).Text = "Enable/Disable use of CFG-DEV folder";
		((ToolStripItem)Menu_Settings_CfgDevToggle).Click += Menu_Settings_CfgDevToggle_Click;
		((ToolStripItem)Menu_Settings_UseOldIsoFormat).Name = "Menu_Settings_UseOldIsoFormat";
		((ToolStripItem)Menu_Settings_UseOldIsoFormat).Size = new Size(272, 22);
		((ToolStripItem)Menu_Settings_UseOldIsoFormat).Text = "Change ISO naming format";
		((ToolStripItem)Menu_Settings_UseOldIsoFormat).Click += Menu_Settings_UseOldIsoFormat_Click;
		((ToolStripDropDownItem)Menu_NetworkOptions).DropDownItems.AddRange((ToolStripItem[])(object)new ToolStripItem[2]
		{
			(ToolStripItem)Menu_NetworkOptions_GetGames,
			(ToolStripItem)Menu_NetworkOptions_Sync
		});
		((ToolStripItem)Menu_NetworkOptions).Name = "Menu_NetworkOptions";
		((ToolStripItem)Menu_NetworkOptions).Size = new Size(109, 20);
		((ToolStripItem)Menu_NetworkOptions).Text = "Network Options";
		((ToolStripItem)Menu_NetworkOptions_GetGames).Name = "Menu_NetworkOptions_GetGames";
		((ToolStripItem)Menu_NetworkOptions_GetGames).Size = new Size(194, 22);
		((ToolStripItem)Menu_NetworkOptions_GetGames).Text = "Get game list from ps2";
		((ToolStripItem)Menu_NetworkOptions_GetGames).Click += Menu_NetworkOptions_GetGames_Click;
		((ToolStripItem)Menu_NetworkOptions_Sync).Name = "Menu_NetworkOptions_Sync";
		((ToolStripItem)Menu_NetworkOptions_Sync).Size = new Size(194, 22);
		((ToolStripItem)Menu_NetworkOptions_Sync).Text = "Sync files to ps2 via ftp";
		((ToolStripItem)Menu_NetworkOptions_Sync).Click += Menu_NetworkOptions_Sync_Click;
		((ToolStripDropDownItem)Menu_Tools).DropDownItems.AddRange((ToolStripItem[])(object)new ToolStripItem[7]
		{
			(ToolStripItem)Menu_Tools_IsoConv,
			(ToolStripItem)Menu_Tools_OplSimulator,
			(ToolStripItem)Menu_Tools_APPInstaller,
			(ToolStripItem)Menu_Tools_ConvertISONaming,
			(ToolStripItem)Menu_Tools_CleanFiles,
			(ToolStripItem)Menu_Tools_ConvertIsoZso,
			(ToolStripItem)Menu_Tools_DiskIsoZso
		});
		((ToolStripItem)Menu_Tools).Name = "Menu_Tools";
		((ToolStripItem)Menu_Tools).Size = new Size(46, 20);
		((ToolStripItem)Menu_Tools).Text = "Tools";
		((ToolStripItem)Menu_Tools_IsoConv).Name = "Menu_Tools_IsoConv";
		((ToolStripItem)Menu_Tools_IsoConv).Size = new Size(225, 22);
		((ToolStripItem)Menu_Tools_IsoConv).Text = "Disc/Convert to ISO";
		((ToolStripItem)Menu_Tools_IsoConv).Click += CreateConvertToISOToolStripMenuItem_Click;
		((ToolStripItem)Menu_Tools_OplSimulator).Name = "Menu_Tools_OplSimulator";
		((ToolStripItem)Menu_Tools_OplSimulator).Size = new Size(225, 22);
		((ToolStripItem)Menu_Tools_OplSimulator).Text = "Game Gallery";
		((ToolStripItem)Menu_Tools_OplSimulator).Click += Menu_Tools_GameGallery_Click;
		((ToolStripItem)Menu_Tools_APPInstaller).Name = "Menu_Tools_APPInstaller";
		((ToolStripItem)Menu_Tools_APPInstaller).Size = new Size(225, 22);
		((ToolStripItem)Menu_Tools_APPInstaller).Text = "APP installer";
		((ToolStripItem)Menu_Tools_APPInstaller).Click += APPInstallerToolStripMenuItem_Click;
		((ToolStripItem)Menu_Tools_ConvertISONaming).Name = "Menu_Tools_ConvertISONaming";
		((ToolStripItem)Menu_Tools_ConvertISONaming).Size = new Size(225, 22);
		((ToolStripItem)Menu_Tools_ConvertISONaming).Text = "Convert ISO naming formats";
		((ToolStripItem)Menu_Tools_ConvertISONaming).Click += Menu_Tools_ConvertISONaming_Click;
		((ToolStripItem)Menu_Tools_CleanFiles).Name = "Menu_Tools_CleanFiles";
		((ToolStripItem)Menu_Tools_CleanFiles).Size = new Size(225, 22);
		((ToolStripItem)Menu_Tools_CleanFiles).Text = "Clean files";
		((ToolStripItem)Menu_Tools_CleanFiles).Click += Menu_Tools_CleanFiles_Click;
		((ToolStripItem)Menu_Tools_ConvertIsoZso).Name = "Menu_Tools_ConvertIsoZso";
		((ToolStripItem)Menu_Tools_ConvertIsoZso).Size = new Size(225, 22);
		((ToolStripItem)Menu_Tools_ConvertIsoZso).Text = "Convert ISO/ZSO";
		((ToolStripItem)Menu_Tools_ConvertIsoZso).Click += Menu_Tools_ConvertIsoZso_Click;
		((ToolStripItem)Menu_Tools_DiskIsoZso).Name = "Menu_Tools_DiskIsoZso";
		((ToolStripItem)Menu_Tools_DiskIsoZso).Size = new Size(225, 22);
		((ToolStripItem)Menu_Tools_DiskIsoZso).Text = "Disk to ISO/ZSO";
		((ToolStripItem)Menu_Tools_DiskIsoZso).Click += Menu_Tools_DiskIsoZso_Click;
		((ToolStripDropDownItem)Menu_LocalHDDOptions).DropDownItems.AddRange((ToolStripItem[])(object)new ToolStripItem[1] { (ToolStripItem)Menu_LocalHDDOptions_GetList });
		((ToolStripItem)Menu_LocalHDDOptions).Name = "Menu_LocalHDDOptions";
		((ToolStripItem)Menu_LocalHDDOptions).Size = new Size(120, 20);
		((ToolStripItem)Menu_LocalHDDOptions).Text = "Local HDD Options";
		((ToolStripItem)Menu_LocalHDDOptions).Visible = false;
		((ToolStripItem)Menu_LocalHDDOptions_GetList).Name = "Menu_LocalHDDOptions_GetList";
		((ToolStripItem)Menu_LocalHDDOptions_GetList).Size = new Size(143, 22);
		((ToolStripItem)Menu_LocalHDDOptions_GetList).Text = "Get game list";
		((ToolStripItem)Menu_LocalHDDOptions_GetList).Click += Menu_LocalHDDOptions_GetList_Click;
		((ToolStripDropDownItem)Menu_Help).DropDownItems.AddRange((ToolStripItem[])(object)new ToolStripItem[6]
		{
			(ToolStripItem)Menu_Help_ChangeLog,
			(ToolStripItem)Menu_Help_Homepage,
			(ToolStripItem)Menu_Help_Ps2HomeThread,
			(ToolStripItem)Menu_Help_PSXPlaceThread,
			(ToolStripItem)Menu_Help_Facebook,
			(ToolStripItem)Menu_Help_About
		});
		((ToolStripItem)Menu_Help).Name = "Menu_Help";
		((ToolStripItem)Menu_Help).Size = new Size(44, 20);
		((ToolStripItem)Menu_Help).Text = "Help";
		((ToolStripItem)Menu_Help_ChangeLog).Name = "Menu_Help_ChangeLog";
		((ToolStripItem)Menu_Help_ChangeLog).Size = new Size(208, 22);
		((ToolStripItem)Menu_Help_ChangeLog).Text = "ChangeLog";
		((ToolStripItem)Menu_Help_ChangeLog).Click += ChangeLogToolStripMenuItem_Click;
		((ToolStripItem)Menu_Help_Homepage).Name = "Menu_Help_Homepage";
		((ToolStripItem)Menu_Help_Homepage).Size = new Size(208, 22);
		((ToolStripItem)Menu_Help_Homepage).Text = "OPL Manager Homepage";
		((ToolStripItem)Menu_Help_Homepage).Click += Menu_Help_HomePage_Click;
		((ToolStripItem)Menu_Help_Ps2HomeThread).Name = "Menu_Help_Ps2HomeThread";
		((ToolStripItem)Menu_Help_Ps2HomeThread).Size = new Size(208, 22);
		((ToolStripItem)Menu_Help_Ps2HomeThread).Text = "PS2 Home Thread";
		((ToolStripItem)Menu_Help_Ps2HomeThread).Click += Menu_Help_Ps2HomeThread_Click;
		((ToolStripItem)Menu_Help_PSXPlaceThread).Name = "Menu_Help_PSXPlaceThread";
		((ToolStripItem)Menu_Help_PSXPlaceThread).Size = new Size(208, 22);
		((ToolStripItem)Menu_Help_PSXPlaceThread).Text = "PSX-Place Thread";
		((ToolStripItem)Menu_Help_PSXPlaceThread).Click += Menu_Help_PSXPlaceThread_Click;
		((ToolStripItem)Menu_Help_Facebook).Name = "Menu_Help_Facebook";
		((ToolStripItem)Menu_Help_Facebook).Size = new Size(208, 22);
		((ToolStripItem)Menu_Help_Facebook).Text = "Facebook";
		((ToolStripItem)Menu_Help_Facebook).Click += Menu_Help_Facebook_Click;
		((ToolStripItem)Menu_Help_About).Name = "Menu_Help_About";
		((ToolStripItem)Menu_Help_About).Size = new Size(208, 22);
		((ToolStripItem)Menu_Help_About).Text = "About";
		((ToolStripItem)Menu_Help_About).Click += AboutToolStripMenuItem1_Click;
		((ToolStripItem)Menu_OpenOPLFolder).Name = "Menu_OpenOPLFolder";
		((ToolStripItem)Menu_OpenOPLFolder).Size = new Size(107, 20);
		((ToolStripItem)Menu_OpenOPLFolder).Text = "Open OPL folder";
		((ToolStripItem)Menu_OpenOPLFolder).Click += OpenOPLFolderToolStripMenuItem_Click;
		((FileDialog)OpenFileDialog1).Filter = "Image Files|*.png;";
		Timer_VersionCheck.Interval = 300000;
		Timer_VersionCheck.Tick += Timer_VersionCheck_Tick;
		TimerServerStatus.Interval = 30000;
		TimerServerStatus.Tick += TimerServerStatus_tick;
		((ContainerControl)this).AutoScaleDimensions = new SizeF(6f, 13f);
		((ContainerControl)this).AutoScaleMode = (AutoScaleMode)1;
		((Form)this).ClientSize = new Size(864, 661);
		((Control)this).Controls.Add((Control)(object)ToolStripContainer1);
		((Control)this).Font = new Font("Microsoft Sans Serif", 8f, (FontStyle)0, (GraphicsUnit)3);
		((Form)this).Icon = (Icon)componentResourceManager.GetObject("$this.Icon");
		((Form)this).MainMenuStrip = MenuStrip1;
		((Control)this).Name = "OPLM_Main";
		((Form)this).StartPosition = (FormStartPosition)1;
		((Control)this).Text = "OPL Manager";
		((Form)this).FormClosing += new FormClosingEventHandler(OPL_Manager_FormClosing);
		((Form)this).Shown += OplManagerStart;
		((Control)SplitContainer1.Panel1).ResumeLayout(false);
		((Control)SplitContainer1.Panel2).ResumeLayout(false);
		((ISupportInitialize)SplitContainer1).EndInit();
		((Control)SplitContainer1).ResumeLayout(false);
		((Control)GroupBox1).ResumeLayout(false);
		((Control)GroupBox1).PerformLayout();
		((Control)GlobalStats).ResumeLayout(false);
		((Control)GlobalStats).PerformLayout();
		((Control)TableLayoutPanel1).ResumeLayout(false);
		((Control)TableLayoutPanel1).PerformLayout();
		((Control)GameDetails).ResumeLayout(false);
		((Control)GameDetails).PerformLayout();
		((ISupportInitialize)Cover_CoverIMG).EndInit();
		((ISupportInitialize)Disc_DiscIMG).EndInit();
		((Control)Operations).ResumeLayout(false);
		((Control)ToolStripContainer1.BottomToolStripPanel).ResumeLayout(false);
		((Control)ToolStripContainer1.BottomToolStripPanel).PerformLayout();
		((Control)ToolStripContainer1.ContentPanel).ResumeLayout(false);
		((Control)ToolStripContainer1.TopToolStripPanel).ResumeLayout(false);
		((Control)ToolStripContainer1.TopToolStripPanel).PerformLayout();
		((Control)ToolStripContainer1).ResumeLayout(false);
		((Control)ToolStripContainer1).PerformLayout();
		((Control)StatusStrip1).ResumeLayout(false);
		((Control)StatusStrip1).PerformLayout();
		((Control)Tab_Main).ResumeLayout(false);
		((Control)TabHome).ResumeLayout(false);
		((Control)TabBadIsos).ResumeLayout(false);
		((Control)SplitContainer2.Panel1).ResumeLayout(false);
		((Control)SplitContainer2.Panel2).ResumeLayout(false);
		((ISupportInitialize)SplitContainer2).EndInit();
		((Control)SplitContainer2).ResumeLayout(false);
		((Control)GB_BadIsoDetails).ResumeLayout(false);
		((Control)GB_BadIsoDetails).PerformLayout();
		((Control)GroupBox2).ResumeLayout(false);
		((Control)MenuStrip1).ResumeLayout(false);
		((Control)MenuStrip1).PerformLayout();
		((Control)this).ResumeLayout(false);
	}
}
