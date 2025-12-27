using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using System.Xml.Serialization;
using OPL_Manager.My.Resources;
using OplManagerService;

namespace OPL_Manager;

public class ToolsOplSimulator : BaseForm
{
	private class BitmapCacheClass
	{
		public bool Keep;

		public Bitmap Bitmap;

		public BitmapCacheClass(Bitmap BitMap, bool Keep)
		{
			Bitmap = BitMap;
			this.Keep = Keep;
		}
	}

	public string ThmFolder;

	public string GameID;

	public PageClass currentPage;

	private double aspectTheme;

	private double aspectScreen;

	private int offSetW;

	private int offSetH;

	private int newW;

	private int newH;

	private bool fullScreen;

	private int perPage;

	public bool bitmapsLoaded;

	private int currentGameListPage;

	private ThemeConfig themeCfg;

	private ConfigClass gameCfg;

	private GameInfo gameInfo;

	private List<string> availableThms = new List<string>();

	private int currentThm;

	private PageType currentPageType;

	private Dictionary<string, BitmapCacheClass> bitmapCache = new Dictionary<string, BitmapCacheClass>();

	private readonly OPLM_Main MainF = Program.MainFormInst;

	private IContainer components;

	public ToolsOplSimulator()
	{
		InitializeComponent();
	}

	private void Form1_Shown(object sender, EventArgs e)
	{
		//IL_007a: Unknown result type (might be due to invalid IL or missing references)
		if (Directory.Exists(MainF.app_folder + "\\opl_simulator_themes\\"))
		{
			string[] directories = Directory.GetDirectories(MainF.app_folder + "\\opl_simulator_themes\\");
			foreach (string text in directories)
			{
				availableThms.Add(text + "\\");
			}
		}
		if (availableThms.Count <= 0)
		{
			MessageBox.Show("No themes found in 'opl_simulator_themes' folder!", Translated.Global_Error, (MessageBoxButtons)0, (MessageBoxIcon)16);
			((Form)this).Close();
		}
		else
		{
			((Form)this).ClientSize = new Size(640, 480);
			LoadTheme();
			LoadGame();
		}
	}

	private void LoadGame()
	{
		BitmapCachePurge();
		currentGameListPage = 0;
		if (themeCfg.HasInfoPage && currentPageType == PageType.Info)
		{
			currentPage = themeCfg.InfoPage;
			currentPageType = PageType.Info;
		}
		else
		{
			currentPage = themeCfg.MainPage;
			currentPageType = PageType.Main;
		}
		gameCfg = null;
		gameInfo = MainF.SelectedGame;
		if (gameInfo.HasCFG)
		{
			TextBox textboxxx = null;
			gameCfg = new ConfigClass(gameInfo.FileCFG, skipCheck: true, ref textboxxx);
		}
		GameID = gameInfo.ID;
		((Control)this).Text = gameInfo.Title + "  |  " + GameID;
		((Control)this).Refresh();
	}

	private void Form1_KeyDown(object sender, KeyEventArgs e)
	{
		//IL_0001: Unknown result type (might be due to invalid IL or missing references)
		//IL_0008: Invalid comparison between Unknown and I4
		//IL_000b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0012: Invalid comparison between Unknown and I4
		//IL_004c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0053: Invalid comparison between Unknown and I4
		//IL_0056: Unknown result type (might be due to invalid IL or missing references)
		//IL_005d: Invalid comparison between Unknown and I4
		//IL_0061: Unknown result type (might be due to invalid IL or missing references)
		//IL_0067: Invalid comparison between Unknown and I4
		//IL_0095: Unknown result type (might be due to invalid IL or missing references)
		//IL_009c: Invalid comparison between Unknown and I4
		//IL_00c6: Unknown result type (might be due to invalid IL or missing references)
		//IL_00cd: Invalid comparison between Unknown and I4
		//IL_00e3: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ea: Invalid comparison between Unknown and I4
		//IL_00b3: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b9: Invalid comparison between Unknown and I4
		//IL_0121: Unknown result type (might be due to invalid IL or missing references)
		//IL_0128: Invalid comparison between Unknown and I4
		//IL_012b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0132: Invalid comparison between Unknown and I4
		//IL_0192: Unknown result type (might be due to invalid IL or missing references)
		//IL_0199: Invalid comparison between Unknown and I4
		//IL_019c: Unknown result type (might be due to invalid IL or missing references)
		//IL_01a3: Invalid comparison between Unknown and I4
		//IL_0138: Unknown result type (might be due to invalid IL or missing references)
		//IL_013f: Invalid comparison between Unknown and I4
		//IL_020d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0214: Invalid comparison between Unknown and I4
		//IL_01a9: Unknown result type (might be due to invalid IL or missing references)
		//IL_01b0: Invalid comparison between Unknown and I4
		//IL_0162: Unknown result type (might be due to invalid IL or missing references)
		//IL_0169: Invalid comparison between Unknown and I4
		//IL_024a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0251: Invalid comparison between Unknown and I4
		//IL_01de: Unknown result type (might be due to invalid IL or missing references)
		//IL_01e5: Invalid comparison between Unknown and I4
		//IL_02aa: Unknown result type (might be due to invalid IL or missing references)
		//IL_02b1: Invalid comparison between Unknown and I4
		//IL_02c6: Unknown result type (might be due to invalid IL or missing references)
		//IL_02cd: Invalid comparison between Unknown and I4
		//IL_02e2: Unknown result type (might be due to invalid IL or missing references)
		//IL_02e9: Invalid comparison between Unknown and I4
		//IL_040f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0416: Invalid comparison between Unknown and I4
		//IL_0407: Unknown result type (might be due to invalid IL or missing references)
		if ((((int)e.KeyCode == 50) | ((int)e.KeyCode == 13)) && currentPageType == PageType.Main && themeCfg.HasInfoPage)
		{
			currentPage = themeCfg.InfoPage;
			currentPageType = PageType.Info;
			((Control)this).Refresh();
		}
		else if ((((int)e.KeyCode == 49) | ((int)e.KeyCode == 27) | ((int)e.KeyCode == 8)) && currentPageType == PageType.Info)
		{
			currentPageType = PageType.Main;
			currentPage = themeCfg.MainPage;
			((Control)this).Refresh();
		}
		else if ((int)e.KeyCode == 27 && currentPageType == PageType.Main)
		{
			if ((int)MessageBox.Show("You sure?", "Exit?", (MessageBoxButtons)4, (MessageBoxIcon)32) == 6)
			{
				((Form)this).Close();
			}
		}
		else if ((int)e.KeyCode == 82)
		{
			LoadTheme(currentPageType);
			((Control)this).Refresh();
		}
		else if ((int)e.KeyCode == 70)
		{
			if (fullScreen)
			{
				((Form)this).FormBorderStyle = (FormBorderStyle)4;
				((Form)this).WindowState = (FormWindowState)0;
				fullScreen = false;
			}
			else
			{
				((Form)this).FormBorderStyle = (FormBorderStyle)0;
				((Form)this).WindowState = (FormWindowState)2;
				fullScreen = true;
			}
		}
		else if (((int)e.KeyCode == 40) | ((int)e.KeyCode == 38))
		{
			if ((int)e.KeyCode == 40 && MainF.CanMoveDown)
			{
				MainF.MoveDown();
				LoadGame();
			}
			else if ((int)e.KeyCode == 38 && MainF.CanMoveUp)
			{
				MainF.MoveUp();
				LoadGame();
			}
		}
		else if (((int)e.KeyCode == 37) | ((int)e.KeyCode == 39))
		{
			if ((int)e.KeyCode == 39 && currentThm < availableThms.Count - 1)
			{
				currentThm++;
				LoadTheme();
			}
			else if ((int)e.KeyCode == 37 && currentThm > 0)
			{
				currentThm--;
				LoadTheme();
			}
		}
		else if ((int)e.KeyCode == 33 && currentPageType == PageType.Main && currentGameListPage > 0)
		{
			MainF.MoveTo(perPage * (currentGameListPage - 1));
			LoadGame();
		}
		else if ((int)e.KeyCode == 34 && currentPageType == PageType.Main && (double)currentGameListPage < Math.Floor((double)((ListView)MainF.GameList).Items.Count / (double)perPage))
		{
			MainF.MoveTo(perPage * (currentGameListPage + 1));
			LoadGame();
		}
		else if ((int)e.KeyCode == 36)
		{
			MainF.MoveFirst();
			LoadGame();
		}
		else if ((int)e.KeyCode == 35)
		{
			MainF.MoveLast();
			LoadGame();
		}
		else if ((int)e.KeyCode == 72)
		{
			MessageBox.Show("On both pages:" + Environment.NewLine + "[F]= Toggle Full Screen" + Environment.NewLine + "[HOME]= Go to first game" + Environment.NewLine + "[END]= Go to last game" + Environment.NewLine + "[1]= Show main page" + Environment.NewLine + "[2]= Show info page (if any)" + Environment.NewLine + Environment.NewLine + "[LEFT ARROW] [RIGHT ARROW]= Change theme (if more than 1)" + Environment.NewLine + Environment.NewLine + "On main page:" + Environment.NewLine + "[ENTER]= Go to info page (if any) OR launch game" + Environment.NewLine + "[PAGE UP]= Previous game list page" + Environment.NewLine + "[PAGE DOWN]= Next game list page" + Environment.NewLine + Environment.NewLine + "On info page (if any):" + Environment.NewLine + "[ESCAPE/BACK SPACE]= Back to main page" + Environment.NewLine + "[ENTER]= Launch game (if double click action is set in OPLM Settings)", "Keyboard shortcut help", (MessageBoxButtons)0, (MessageBoxIcon)64);
		}
		else
		{
			if (!(((int)e.KeyCode == 13 && currentPageType == PageType.Info) & bool.Parse(OplmSettings.Read("DoubleClickEnabled", "False"))))
			{
				return;
			}
			string text = OplmSettings.Read("DoubleClickExe", "");
			string text2 = OplmSettings.Read("DoubleClickParams", "");
			if (File.Exists(text) & (text2.Length > 0))
			{
				text2 = text2.Replace("[FILE]", "\"" + gameInfo.FileDiscFullPath + "\"");
				text2 = text2.Replace("[ID]", gameInfo.ID);
				using Process process = new Process();
				process.StartInfo.FileName = text;
				process.StartInfo.Arguments = text2;
				process.StartInfo.UseShellExecute = false;
				process.StartInfo.RedirectStandardOutput = false;
				process.StartInfo.WindowStyle = ProcessWindowStyle.Normal;
				process.Start();
			}
		}
	}

	private Bitmap LoadBitmap(string file, bool keep = false)
	{
		//IL_003f: Unknown result type (might be due to invalid IL or missing references)
		//IL_004a: Expected O, but got Unknown
		if (bitmapCache.ContainsKey(file))
		{
			return bitmapCache[file].Bitmap;
		}
		if (File.Exists(file))
		{
			bitmapCache.Add(file, new BitmapCacheClass((Bitmap)Image.FromStream((Stream)new MemoryStream(File.ReadAllBytes(file))), keep));
			return bitmapCache[file].Bitmap;
		}
		return null;
	}

	private void BitmapCachePurge()
	{
		List<string> list = new List<string>();
		foreach (KeyValuePair<string, BitmapCacheClass> item in bitmapCache)
		{
			if (!item.Value.Keep)
			{
				list.Add(item.Key);
			}
		}
		foreach (string item2 in list)
		{
			bitmapCache.Remove(item2);
		}
	}

	private void LoadTheme(PageType page = PageType.Main)
	{
		ThmFolder = availableThms[currentThm];
		XmlSerializer xmlSerializer = new XmlSerializer(typeof(ThemeConfig));
		using (TextReader textReader = new StringReader(File.ReadAllText(ThmFolder + "conf_theme.xml")))
		{
			themeCfg = (ThemeConfig)xmlSerializer.Deserialize(textReader);
		}
		currentPage = ((page == PageType.Main) ? themeCfg.MainPage : themeCfg.InfoPage);
		aspectTheme = (double)themeCfg.Width / (double)themeCfg.Height;
		ResizeTheme();
	}

	protected override void OnResize(EventArgs e)
	{
		((Form)this).OnResize(e);
		ResizeTheme();
	}

	private void ResizeTheme()
	{
		if (themeCfg != null)
		{
			aspectScreen = (double)((Form)this).ClientSize.Width / (double)((Form)this).ClientSize.Height;
			if (aspectScreen > aspectTheme)
			{
				newW = (int)Math.Round((double)(themeCfg.Width * ((Form)this).ClientSize.Height) / (double)themeCfg.Height);
				newH = ((Form)this).ClientSize.Height;
			}
			else
			{
				newW = ((Form)this).ClientSize.Width;
				newH = (int)Math.Round((double)(themeCfg.Height * ((Form)this).ClientSize.Width) / (double)themeCfg.Width);
			}
			offSetW = (int)Math.Round((double)(((Form)this).ClientSize.Width - newW) / 2.0);
			offSetH = (int)Math.Round((double)(((Form)this).ClientSize.Height - newH) / 2.0);
			((Control)this).Refresh();
		}
	}

	protected override void OnPaint(PaintEventArgs e)
	{
		if ((themeCfg == null) | (gameInfo == null))
		{
			return;
		}
		e.Graphics.Clear(currentPage.BgColor);
		foreach (object element in currentPage.Elements)
		{
			if ((object)element.GetType() == typeof(ThmImg))
			{
				bool flag = false;
				ThmImg thmImg = (ThmImg)element;
				if (thmImg.type == ImageType.FILE)
				{
					Graphics formGraphics = e.Graphics;
					DrawImage(ref formGraphics, (Image)(object)LoadBitmap(ThmFolder + thmImg.source, keep: true), thmImg);
					flag = true;
				}
				else if (thmImg.type == ImageType.ICO && gameInfo.HasICO)
				{
					Graphics formGraphics2 = e.Graphics;
					DrawImage(ref formGraphics2, (Image)(object)LoadBitmap(gameInfo.FileArtICO), thmImg);
					flag = true;
				}
				else if (thmImg.type == ImageType.COV && gameInfo.HasCOV)
				{
					Graphics formGraphics3 = e.Graphics;
					DrawImage(ref formGraphics3, (Image)(object)LoadBitmap(gameInfo.FileArtCOV), thmImg);
					flag = true;
				}
				else if (thmImg.type == ImageType.COV2 && gameInfo.HasCOV2)
				{
					Graphics formGraphics4 = e.Graphics;
					DrawImage(ref formGraphics4, (Image)(object)LoadBitmap(gameInfo.FileArtCOV2), thmImg);
					flag = true;
				}
				else if (thmImg.type == ImageType.LAB && gameInfo.HasLAB)
				{
					Graphics formGraphics5 = e.Graphics;
					DrawImage(ref formGraphics5, (Image)(object)LoadBitmap(gameInfo.FileArtLAB), thmImg);
					flag = true;
				}
				else if (thmImg.type == ImageType.SCR && gameInfo.HasSCR)
				{
					Graphics formGraphics6 = e.Graphics;
					DrawImage(ref formGraphics6, (Image)(object)LoadBitmap(gameInfo.FileArtSCR), thmImg);
					flag = true;
				}
				else if (thmImg.type == ImageType.SCR2 && gameInfo.HasSCR2)
				{
					Graphics formGraphics7 = e.Graphics;
					DrawImage(ref formGraphics7, (Image)(object)LoadBitmap(gameInfo.FileArtSCR2), thmImg);
					flag = true;
				}
				else if (thmImg.type == ImageType.BG && gameInfo.HasBG)
				{
					Graphics formGraphics8 = e.Graphics;
					DrawImage(ref formGraphics8, (Image)(object)LoadBitmap(gameInfo.FileArtBG), thmImg);
					flag = true;
				}
				else if (thmImg.type == ImageType.CFG && gameCfg != null && !string.IsNullOrEmpty(thmImg.source))
				{
					string source = thmImg.source;
					string value = gameCfg.GetValue(source + "=");
					string result = "";
					if (CommonFuncs.FileExistsAssign(ThmFolder + value + "_" + source + ".png", ref result))
					{
						Graphics formGraphics9 = e.Graphics;
						DrawImage(ref formGraphics9, (Image)(object)LoadBitmap(result, keep: true), thmImg);
						flag = true;
					}
				}
				else if (thmImg.type == ImageType.OPLM && !string.IsNullOrEmpty(thmImg.source))
				{
					if (thmImg.source == "#Media")
					{
						string text = "";
						if (gameInfo.Type == GameType.PS2)
						{
							text = ThmFolder + gameInfo.DiscTypeText + "_#Media.png";
						}
						else if (gameInfo.Type == GameType.APP)
						{
							text = ThmFolder + "ELF_#Media.png";
						}
						else if (gameInfo.Type == GameType.POPS)
						{
							text = ThmFolder + "ELM_#Media.png";
						}
						if (File.Exists(text))
						{
							Graphics formGraphics10 = e.Graphics;
							DrawImage(ref formGraphics10, (Image)(object)LoadBitmap(text, keep: true), thmImg);
							flag = true;
						}
					}
					else if (thmImg.source == "#Format")
					{
						string text2 = "";
						if (gameInfo.Device == CommonFuncs.Modes.Normal)
						{
							text2 = "ISO";
						}
						else if ((gameInfo.Device == CommonFuncs.Modes.HDD) | (gameInfo.Device == CommonFuncs.Modes.Ethernet))
						{
							text2 = "HDL";
						}
						else if (gameInfo.Device == CommonFuncs.Modes.USB)
						{
							text2 = "UL";
						}
						if (gameInfo.Type == GameType.POPS)
						{
							text2 = "VCD";
						}
						string text3 = ThmFolder + text2 + "_#Format.png";
						if (File.Exists(text3))
						{
							Graphics formGraphics11 = e.Graphics;
							DrawImage(ref formGraphics11, (Image)(object)LoadBitmap(text3, keep: true), thmImg);
							flag = true;
						}
					}
				}
				if (!flag && !string.IsNullOrEmpty(thmImg.fallback))
				{
					string text4 = ThmFolder + thmImg.fallback;
					if (File.Exists(text4))
					{
						Graphics formGraphics12 = e.Graphics;
						DrawImage(ref formGraphics12, (Image)(object)LoadBitmap(text4, keep: true), thmImg);
					}
				}
			}
			else if ((object)element.GetType() == typeof(ThmText))
			{
				ThmText thmText = (ThmText)element;
				string text5 = thmText.text;
				foreach (Match item in Regex.Matches(text5, "\\{(.*?)\\}"))
				{
					string value2 = item.Groups[1].Value;
					if (value2.StartsWith("OPLM:"))
					{
						value2 = value2.Remove(0, 5);
						string newValue = "";
						switch ((TextType)Enum.Parse(typeof(TextType), value2))
						{
						case TextType.GameID:
							newValue = GameID;
							break;
						case TextType.MediaType:
							newValue = gameInfo.DiscType.ToString();
							break;
						case TextType.Size:
							newValue = CommonFuncs.FormatFileSize(gameInfo.Size);
							break;
						case TextType.Help:
							if (currentPageType == PageType.Main)
							{
								newValue = "Press [H] to show help";
							}
							break;
						}
						text5 = text5.Replace("{OPLM:" + value2 + "}", newValue);
					}
					else if (value2.StartsWith("CFG:"))
					{
						value2 = value2.Remove(0, 4);
						string text6 = ((gameCfg != null) ? gameCfg.GetValue(value2 + "=") : "");
						if (string.IsNullOrEmpty(text6) && value2.ToLower() == "title")
						{
							text6 = gameInfo.Title;
						}
						text5 = text5.Replace("{CFG:" + value2 + "}", text6);
					}
				}
				Graphics formGraphics13 = e.Graphics;
				DrawString(ref formGraphics13, text5, ScalePosX(thmText.x), ScalePosY(thmText.y), ScaleWidth(thmText.maxWidth), ScaleHeight(thmText.maxHeight), ScaleHeight(thmText.fontSize), thmText.align, thmText.color);
			}
			else if ((object)element.GetType() == typeof(ThmGameList))
			{
				ThmGameList thmGameList = (ThmGameList)element;
				Graphics formGraphics14 = e.Graphics;
				DrawGameList(ref formGraphics14, ScalePosX(thmGameList.x), ScalePosY(thmGameList.y), ScaleWidth(thmGameList.maxWidth), ScaleHeight(thmGameList.maxHeight), ScaleHeight(thmGameList.fontSize), thmGameList.align, thmGameList.color, thmGameList.colorSelected);
			}
		}
	}

	private void DrawImage(ref Graphics formGraphics, Image image, ThmImg artInfo)
	{
		int num = artInfo.x;
		int num2 = artInfo.y;
		int num3 = artInfo.widthX;
		int num4 = artInfo.heightY;
		ImageOrigin origin = artInfo.origin;
		if (num3 == 0 && num4 == 0)
		{
			num3 = image.Width;
			num4 = image.Height;
		}
		else if (num3 == 0)
		{
			num3 = (int)Math.Round((double)image.Width / (double)image.Height * (double)num4);
		}
		else if (num4 == 0)
		{
			num4 = (int)Math.Round((double)image.Height / (double)image.Width * (double)num3);
		}
		switch (origin)
		{
		case ImageOrigin.TopRight:
			num -= num3;
			break;
		case ImageOrigin.BottomLeft:
			num2 -= num4;
			break;
		case ImageOrigin.BottomRight:
			num -= num3;
			num2 -= num4;
			break;
		case ImageOrigin.Center:
			num = (int)Math.Round((double)num - (double)num3 / 2.0);
			num2 = (int)Math.Round((double)num2 - (double)num4 / 2.0);
			break;
		case ImageOrigin.MiddleLeft:
			num2 = (int)Math.Round((double)num2 - (double)num4 / 2.0);
			break;
		case ImageOrigin.MiddleRight:
			num -= num3;
			num2 = (int)Math.Round((double)num2 - (double)num4 / 2.0);
			break;
		case ImageOrigin.MiddleTop:
			num = (int)Math.Round((double)num - (double)num3 / 2.0);
			break;
		case ImageOrigin.MiddleBottom:
			num = (int)Math.Round((double)num - (double)num3 / 2.0);
			num2 -= num4;
			break;
		}
		num = ScalePosX(num);
		num2 = ScalePosY(num2);
		num3 = ScaleWidth(num3);
		num4 = ScaleHeight(num4);
		formGraphics.DrawImage(image, num, num2, num3, num4);
	}

	public void DrawGameList(ref Graphics formGraphics, int x, int y, int w, int h, int size, TextAlign align, Color color, Color colorSelected)
	{
		//IL_001c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0022: Expected O, but got Unknown
		//IL_00b7: Unknown result type (might be due to invalid IL or missing references)
		//IL_00bd: Expected O, but got Unknown
		//IL_00bf: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c5: Expected O, but got Unknown
		//IL_00c5: Unknown result type (might be due to invalid IL or missing references)
		//IL_00cc: Expected O, but got Unknown
		//IL_00a9: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b5: Expected O, but got Unknown
		if (themeCfg == null || size == 0)
		{
			return;
		}
		Font val = new Font("Arial", (float)size, (FontStyle)1, (GraphicsUnit)2);
		int num = y;
		if (themeCfg.Debug)
		{
			Rectangle rectangle = new Rectangle(x, y, w, h);
			dynamic val2 = Pens.Red.Clone();
			val2.Width = 2;
			formGraphics.DrawRectangle((Pen)val2, rectangle);
		}
		SolidBrush val3 = new SolidBrush(color);
		SolidBrush val4 = new SolidBrush(colorSelected);
		StringFormat val5 = new StringFormat();
		switch (align)
		{
		case TextAlign.Left:
			val5.Alignment = (StringAlignment)0;
			break;
		case TextAlign.Right:
			val5.Alignment = (StringAlignment)2;
			break;
		default:
			val5.Alignment = (StringAlignment)1;
			break;
		}
		val5.LineAlignment = (StringAlignment)0;
		perPage = (int)Math.Round(Math.Floor((double)h / (double)(size + 4)));
		currentGameListPage = (int)Math.Round(Math.Floor((double)MainF.SelectedGameIndex / (double)perPage));
		int i = perPage * currentGameListPage;
		for (int num2 = ((ListView)MainF.GameList).Items.Count - 1; i <= num2; i++)
		{
			Rectangle rectangle2 = new Rectangle(x, y, w, size + 4);
			formGraphics.DrawString(GameProvider.get_GetGame((int)((ListView)MainF.GameList).Items[i].Tag).Title, val, (Brush)(object)((MainF.SelectedGameIndex == i) ? val4 : val3), (RectangleF)rectangle2, val5);
			y += size + 4;
			if (y + size > h + num)
			{
				break;
			}
		}
		val.Dispose();
		((Brush)val3).Dispose();
		((Brush)val4).Dispose();
	}

	public void DrawString(ref Graphics formGraphics, string txt, int x, int y, int w, int h, int size, TextAlign align, Color color)
	{
		//IL_000a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0010: Expected O, but got Unknown
		//IL_0012: Unknown result type (might be due to invalid IL or missing references)
		//IL_0018: Expected O, but got Unknown
		//IL_0026: Unknown result type (might be due to invalid IL or missing references)
		//IL_002c: Expected O, but got Unknown
		//IL_00d5: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e0: Expected O, but got Unknown
		Font val = new Font("Arial", (float)size, (FontStyle)1, (GraphicsUnit)2);
		SolidBrush val2 = new SolidBrush(color);
		Rectangle rectangle = new Rectangle(x, y, w, h);
		StringFormat val3 = new StringFormat();
		switch (align)
		{
		case TextAlign.Left:
			val3.Alignment = (StringAlignment)0;
			break;
		case TextAlign.Right:
			val3.Alignment = (StringAlignment)2;
			break;
		default:
			val3.Alignment = (StringAlignment)1;
			break;
		}
		val3.LineAlignment = (StringAlignment)0;
		if (themeCfg != null && themeCfg.Debug)
		{
			dynamic val4 = Pens.Red.Clone();
			val4.Width = 2;
			formGraphics.DrawRectangle((Pen)val4, rectangle);
		}
		formGraphics.DrawString(txt, val, (Brush)(object)val2, (RectangleF)rectangle, val3);
		val.Dispose();
		((Brush)val2).Dispose();
	}

	private int ScalePosX(int val)
	{
		return (int)Math.Round((double)(newW * val) / (double)themeCfg.Width + (double)offSetW);
	}

	private int ScalePosY(int val)
	{
		return (int)Math.Round((double)(newH * val) / (double)themeCfg.Height + (double)offSetH);
	}

	private int ScaleWidth(int val)
	{
		return (int)Math.Round((double)(newW * val) / (double)themeCfg.Width);
	}

	private int ScaleHeight(int val)
	{
		return (int)Math.Round((double)(newH * val) / (double)themeCfg.Height);
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
		//IL_005f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0069: Expected O, but got Unknown
		//IL_0075: Unknown result type (might be due to invalid IL or missing references)
		//IL_007f: Expected O, but got Unknown
		//IL_00b6: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c0: Expected O, but got Unknown
		ComponentResourceManager componentResourceManager = new ComponentResourceManager(typeof(ToolsOplSimulator));
		((Control)this).SuspendLayout();
		((ContainerControl)this).AutoScaleDimensions = new SizeF(6f, 13f);
		((ContainerControl)this).AutoScaleMode = (AutoScaleMode)1;
		((Control)this).BackColor = SystemColors.Control;
		((Form)this).ClientSize = new Size(784, 562);
		((Control)this).Font = new Font("Microsoft Sans Serif", 8f, (FontStyle)0, (GraphicsUnit)3);
		((Form)this).Icon = (Icon)componentResourceManager.GetObject("$this.Icon");
		((Control)this).Name = "ToolsOplSimulator";
		((Form)this).StartPosition = (FormStartPosition)4;
		((Control)this).Text = "OpsShowCase";
		((Form)this).Shown += Form1_Shown;
		((Control)this).KeyDown += new KeyEventHandler(Form1_KeyDown);
		((Control)this).ResumeLayout(false);
	}
}
