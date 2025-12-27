using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using OPL_Manager.My.Resources;
using OplManagerService;

namespace OPL_Manager;

public class BatchArtDownload : BaseForm
{
	private const int REQUEST_MAX = 500;

	private readonly string UserID;

	private bool cancel;

	private IContainer components;

	internal ProgressBar ProgressBarStatus;

	internal GroupBox GroupLastGame;

	internal TextBox L_StatusTitle;

	internal Label GroupLastGame_Title;

	internal TextBox L_StatusID;

	internal Label Label2;

	internal GroupBox GroupStatus;

	internal TextBox TB_Log;

	internal Button B_Start;

	internal GroupBox GB_BG;

	internal PictureBox ImgBackground;

	internal GroupBox GB_Screens;

	internal PictureBox ImgScreen2;

	internal PictureBox ImgScreen1;

	internal PictureBox ImgBack;

	internal GroupBox GB_Disc;

	internal PictureBox ImgDisc;

	internal PictureBox ImgCover;

	internal GroupBox GB_OptDownloads;

	internal CheckBox CB_RandScr;

	internal CheckBox CB_RandBG;

	internal CheckBox CB_Back;

	internal CheckBox CB_Disc;

	internal CheckBox CB_Cover;

	internal Label L_Status;

	internal CheckBox CB_Spine;

	internal PictureBox ImgSpine;

	internal GroupBox GB_Logo;

	internal PictureBox ImgLogo;

	internal CheckBox CB_Lgo;

	internal CheckBox CB_RandScrReplace;

	internal CheckBox CB_RandBGREplace;

	internal CheckBox CB_BackReplace;

	internal CheckBox CB_LgoReplace;

	internal CheckBox CB_SpineReplace;

	internal CheckBox CB_DiscReplace;

	internal CheckBox CB_CoverReplace;

	internal Button B_Cancel;

	public BatchArtDownload()
	{
		InitializeComponent();
	}

	public BatchArtDownload(string UserID)
	{
		this.UserID = UserID;
		InitializeComponent();
	}

	private void SetLanguage()
	{
		((Control)this).Text = Translated.BatchArtDownload_WindowTitle;
		((Control)GroupLastGame).Text = Translated.BatchArtDownload_Last;
		((Control)GroupLastGame_Title).Text = Translated.BatchArtDownload_Title;
		((Control)GroupStatus).Text = Translated.Global_String_Progress;
		((Control)B_Start).Text = Translated.GLOBAL_BUTTON_START;
		((Control)B_Cancel).Text = Translated.GLOBAL_BUTTON_CANCEL;
		((Control)GB_OptDownloads).Text = Translated.BatchArtDownload_OptExtra;
		((Control)CB_Cover).Text = Translated.BatchArtDownload_OptExtraCov;
		((Control)CB_Back).Text = Translated.BatchArtDownload_OptExtraBack;
		((Control)CB_Disc).Text = Translated.BatchArtDownload_OptExtraDisc;
		((Control)CB_Spine).Text = Translated.BatchArtDownload_OptExtraSpine;
		((Control)CB_RandBG).Text = Translated.BatchArtDownload_OptExtraBG;
		((Control)CB_RandScr).Text = Translated.BatchArtDownload_OptExtraScr;
		((Control)CB_Lgo).Text = Translated.BatchArtDownload_OptExtraLogo;
		((Control)CB_CoverReplace).Text = Translated.BatchArtDownload_ReplaceIfExists;
		((Control)CB_BackReplace).Text = Translated.BatchArtDownload_ReplaceIfExists;
		((Control)CB_DiscReplace).Text = Translated.BatchArtDownload_ReplaceIfExists;
		((Control)CB_SpineReplace).Text = Translated.BatchArtDownload_ReplaceIfExists;
		((Control)CB_RandBGREplace).Text = Translated.BatchArtDownload_ReplaceIfExists;
		((Control)CB_RandScrReplace).Text = Translated.BatchArtDownload_ReplaceIfExists;
		((Control)CB_LgoReplace).Text = Translated.BatchArtDownload_ReplaceIfExists;
		((Control)GB_Disc).Text = Translated.GLOBAL_STRING_DISC;
		((Control)GB_Screens).Text = Translated.GLOBAL_STRING_SCREENSHOTS;
		((Control)GB_BG).Text = Translated.GLOBAL_STRING_BACKGROUNDS;
		((Control)GB_Logo).Text = Translated.GLOBAL_STRING_LOGO;
	}

	private void BatchArtDownload_Shown(object sender, EventArgs e)
	{
		SetLanguage();
		CB_Cover.Checked = OplmSettings.Read("BATCH_DOWNLOAD_COVER", CB_Cover.Checked);
		CB_Back.Checked = OplmSettings.Read("BATCH_DOWNLOAD_BACK", CB_Back.Checked);
		CB_Disc.Checked = OplmSettings.Read("BATCH_DOWNLOAD_DISC", CB_Disc.Checked);
		CB_Spine.Checked = OplmSettings.Read("BATCH_DOWNLOAD_SPINE", CB_Spine.Checked);
		CB_RandBG.Checked = OplmSettings.Read("BATCH_DOWNLOAD_RANDBG", CB_RandBG.Checked);
		CB_RandScr.Checked = OplmSettings.Read("BATCH_DOWNLOAD_RANDSCR", CB_RandScr.Checked);
		CB_Lgo.Checked = OplmSettings.Read("BATCH_DOWNLOAD_LGO", CB_RandScr.Checked);
		CB_Cover.CheckedChanged += DownloadOptionsCheckedChanged;
		CB_Back.CheckedChanged += DownloadOptionsCheckedChanged;
		CB_Disc.CheckedChanged += DownloadOptionsCheckedChanged;
		CB_Spine.CheckedChanged += DownloadOptionsCheckedChanged;
		CB_RandBG.CheckedChanged += DownloadOptionsCheckedChanged;
		CB_RandScr.CheckedChanged += DownloadOptionsCheckedChanged;
		CB_Lgo.CheckedChanged += DownloadOptionsCheckedChanged;
	}

	private void B_Start_Click(object sender, EventArgs e)
	{
		Queue<ArtSearchBatchRequestClass> queue = new Queue<ArtSearchBatchRequestClass>();
		foreach (GameInfo item in GameProvider.GameList.Where((GameInfo x) => (x.Type == GameType.PS2) | (x.Type == GameType.POPS)))
		{
			queue.Enqueue(new ArtSearchBatchRequestClass
			{
				GameID = item.ID,
				GameType = item.Type
			});
		}
		((Control)B_Start).Enabled = false;
		((Control)B_Cancel).Enabled = true;
		((Control)GB_OptDownloads).Enabled = false;
		ArtSearchLoop(queue);
	}

	private void B_Cancel_Click(object sender, EventArgs e)
	{
		cancel = true;
		((Control)B_Cancel).Enabled = false;
	}

	private async void ArtSearchLoop(Queue<ArtSearchBatchRequestClass> queue)
	{
		if (queue.Count == 0)
		{
			MessageBox.Show(Translated.GlobalString_OperationComplete, Translated.Global_Information, (MessageBoxButtons)0, (MessageBoxIcon)64);
		}
		int requestsTotal = 0;
		int requestsCalculated = (int)Math.Round(Math.Ceiling((double)queue.Count / 500.0));
		List<GameART> responses = new List<GameART>();
		while (queue.Count > 0)
		{
			requestsTotal++;
			ArtSearchBatchRequestClass[] array = queue.DequeueChunk(500).ToArray();
			writeTextBox($"Sending request {requestsTotal} of {requestsCalculated} with {array.Length} games.");
			ArtSearchBatchResponse artSearchBatchResponse = await CommonFuncs.SoapAPI.ArtSearchBatchAsync(UserID, array);
			if (artSearchBatchResponse?.Body?.ArtSearchBatchResult == null || artSearchBatchResponse.Body.ArtSearchBatchResult.Length == 0)
			{
				MessageBox.Show(Translated.BatchArtDownload_ServerReplyError, Translated.Global_Error, (MessageBoxButtons)0, (MessageBoxIcon)16);
				((Form)this).Close();
				return;
			}
			writeTextBox(string.Format("Got reply to request {0}" + Environment.NewLine, requestsTotal));
			responses.AddRange(artSearchBatchResponse.Body.ArtSearchBatchResult);
			if (cancel)
			{
				MessageBox.Show(Translated.Global_String_OperationCanceled, Translated.Global_Information, (MessageBoxButtons)0, (MessageBoxIcon)64);
				((Form)this).Close();
				return;
			}
		}
		int count = responses.Count;
		ProgressBarStatus.Value = 0;
		ProgressBarStatus.Maximum = count;
		foreach (GameART item in responses)
		{
			if (cancel)
			{
				MessageBox.Show(Translated.Global_String_OperationCanceled, Translated.Global_Information, (MessageBoxButtons)0, (MessageBoxIcon)64);
				((Form)this).Close();
				return;
			}
			GameInfo gameInfo = GameProvider.get_ById(item.ID);
			((Control)L_StatusTitle).Text = gameInfo.Title;
			((Control)L_StatusID).Text = item.ID;
			writeTextBox(Translated.BatchArtDownload_Game + " " + ((Control)L_StatusTitle).Text);
			Image image = (Image)(object)Resources.art_disc;
			Image image2 = (Image)(object)Resources.art_front;
			Image image3 = (Image)(object)Resources.art_spine;
			Image image4 = (Image)(object)Resources.art_back;
			Image image5 = (Image)(object)Resources.art_bg;
			Image image6 = (Image)(object)Resources.art_logo;
			Image image7 = (Image)(object)Resources.art_scr1;
			Image image8 = (Image)(object)Resources.art_scr2;
			if (CB_Disc.Checked && !string.IsNullOrEmpty(item.ICO) && (!ExistsImg(gameInfo.ArtID, ArtType.ICO) | CB_DiscReplace.Checked))
			{
				byte[] array2 = CommonFuncs.HttpGetImgToByteArray(item.ICO);
				if (array2 != null)
				{
					image = CommonFuncs.ByteArrayToImage(array2);
					writeImg(gameInfo.ArtID, "ICO", array2, Path.GetExtension(item.ICO));
				}
			}
			if (CB_Cover.Checked && !string.IsNullOrEmpty(item.COV) && (!ExistsImg(gameInfo.ArtID, ArtType.COV) | CB_CoverReplace.Checked))
			{
				byte[] array2 = CommonFuncs.HttpGetImgToByteArray(item.COV);
				if (array2 != null)
				{
					image2 = CommonFuncs.ByteArrayToImage(array2);
					writeImg(gameInfo.ArtID, "COV", array2, Path.GetExtension(item.COV));
				}
			}
			if (CB_Back.Checked && !string.IsNullOrEmpty(item.COV2) && (!ExistsImg(gameInfo.ArtID, ArtType.COV2) | CB_BackReplace.Checked))
			{
				byte[] array2 = CommonFuncs.HttpGetImgToByteArray(item.COV2);
				if (array2 != null)
				{
					image4 = CommonFuncs.ByteArrayToImage(array2);
					writeImg(gameInfo.ArtID, "COV2", array2, Path.GetExtension(item.COV2));
				}
			}
			if (CB_Spine.Checked && !string.IsNullOrEmpty(item.LAB) && (!ExistsImg(gameInfo.ArtID, ArtType.LAB) | CB_SpineReplace.Checked))
			{
				byte[] array2 = CommonFuncs.HttpGetImgToByteArray(item.LAB);
				if (array2 != null)
				{
					image3 = CommonFuncs.ByteArrayToImage(array2);
					writeImg(gameInfo.ArtID, "LAB", array2, Path.GetExtension(item.LAB));
				}
			}
			if (CB_Lgo.Checked && !string.IsNullOrEmpty(item.LGO) && (!ExistsImg(gameInfo.ArtID, ArtType.LGO) | CB_LgoReplace.Checked))
			{
				byte[] array2 = CommonFuncs.HttpGetImgToByteArray(item.LGO);
				if (array2 != null)
				{
					image6 = CommonFuncs.ByteArrayToImage(array2);
					writeImg(gameInfo.ArtID, "LGO", array2, Path.GetExtension(item.LGO));
				}
			}
			if (CB_RandBG.Checked && item.BG.Count > 0 && (!ExistsImg(gameInfo.ArtID, ArtType.BG) | CB_RandBGREplace.Checked))
			{
				Random random = new Random();
				string text = item.BG[random.Next(0, item.BG.Count)];
				byte[] array2 = CommonFuncs.HttpGetImgToByteArray(text);
				if (array2 != null)
				{
					image5 = CommonFuncs.ByteArrayToImage(array2);
					writeImg(gameInfo.ArtID, "BG", array2, Path.GetExtension(text));
				}
			}
			if (CB_RandScr.Checked && item.SCR.Count > 0)
			{
				Random random2 = new Random();
				if (!ExistsImg(gameInfo.ArtID, ArtType.SCR) | CB_RandScrReplace.Checked)
				{
					string text2 = item.SCR[random2.Next(0, item.SCR.Count)];
					item.SCR.Remove(text2);
					byte[] array2 = CommonFuncs.HttpGetImgToByteArray(text2);
					if (array2 != null)
					{
						image7 = CommonFuncs.ByteArrayToImage(array2);
						writeImg(gameInfo.ArtID, "SCR", array2, Path.GetExtension(text2));
					}
				}
				if (item.SCR.Count > 0 && (!ExistsImg(gameInfo.ArtID, ArtType.SCR2) | CB_RandScrReplace.Checked))
				{
					string text3 = item.SCR[random2.Next(0, item.SCR.Count)];
					byte[] array2 = CommonFuncs.HttpGetImgToByteArray(text3);
					if (array2 != null)
					{
						image8 = CommonFuncs.ByteArrayToImage(array2);
						writeImg(gameInfo.ArtID, "SCR2", array2, Path.GetExtension(text3));
					}
				}
			}
			ProgressBar progressBarStatus = ProgressBarStatus;
			progressBarStatus.Value += 1;
			((Control)L_Status).Text = ProgressBarStatus.Value + " / " + count;
			ImgDisc.Image = image;
			ImgCover.Image = image2;
			ImgSpine.Image = image3;
			ImgBack.Image = image4;
			ImgBackground.Image = image5;
			ImgLogo.Image = image6;
			ImgScreen1.Image = image7;
			ImgScreen2.Image = image8;
			Application.DoEvents();
		}
		MessageBox.Show(Translated.GlobalString_OperationComplete, Translated.Global_Information, (MessageBoxButtons)0, (MessageBoxIcon)64);
		((Control)B_Cancel).Enabled = false;
	}

	private void writeTextBox(string text)
	{
		((TextBoxBase)TB_Log).AppendText(text + Environment.NewLine);
		((TextBoxBase)TB_Log).Select(((TextBoxBase)TB_Log).TextLength, 0);
		((TextBoxBase)TB_Log).ScrollToCaret();
	}

	private void writeImg(string ID, string type, byte[] data, string extension)
	{
		CommonFuncs.WriteByteArrayToFile(data, OplFolders.ART + ID + "_" + type + extension);
	}

	public bool ExistsImg(string ID, ArtType tipo)
	{
		return File.Exists(OplFolders.ART + ID + "_" + tipo.ToString() + ".png");
	}

	private void DownloadOptionsCheckedChanged(object sender, EventArgs e)
	{
		OplmSettings.Write("BATCH_DOWNLOAD_COVER", CB_Cover.Checked.ToString());
		OplmSettings.Write("BATCH_DOWNLOAD_BACK", CB_Back.Checked.ToString());
		OplmSettings.Write("BATCH_DOWNLOAD_DISC", CB_Disc.Checked.ToString());
		OplmSettings.Write("BATCH_DOWNLOAD_SPINE", CB_Spine.Checked.ToString());
		OplmSettings.Write("BATCH_DOWNLOAD_RANDBG", CB_RandBG.Checked.ToString());
		OplmSettings.Write("BATCH_DOWNLOAD_RANDSCR", CB_RandScr.Checked.ToString());
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
		//IL_0011: Unknown result type (might be due to invalid IL or missing references)
		//IL_001b: Expected O, but got Unknown
		//IL_001c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0026: Expected O, but got Unknown
		//IL_0027: Unknown result type (might be due to invalid IL or missing references)
		//IL_0031: Expected O, but got Unknown
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
		//IL_048a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0494: Expected O, but got Unknown
		//IL_0503: Unknown result type (might be due to invalid IL or missing references)
		//IL_050d: Expected O, but got Unknown
		//IL_0583: Unknown result type (might be due to invalid IL or missing references)
		//IL_058d: Expected O, but got Unknown
		//IL_0600: Unknown result type (might be due to invalid IL or missing references)
		//IL_060a: Expected O, but got Unknown
		//IL_0628: Unknown result type (might be due to invalid IL or missing references)
		//IL_070e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0718: Expected O, but got Unknown
		//IL_0823: Unknown result type (might be due to invalid IL or missing references)
		//IL_082d: Expected O, but got Unknown
		//IL_08a4: Unknown result type (might be due to invalid IL or missing references)
		//IL_08ae: Expected O, but got Unknown
		//IL_09ab: Unknown result type (might be due to invalid IL or missing references)
		//IL_09b5: Expected O, but got Unknown
		//IL_169d: Unknown result type (might be due to invalid IL or missing references)
		//IL_16a7: Expected O, but got Unknown
		//IL_16ba: Unknown result type (might be due to invalid IL or missing references)
		//IL_16c4: Expected O, but got Unknown
		ComponentResourceManager componentResourceManager = new ComponentResourceManager(typeof(BatchArtDownload));
		ProgressBarStatus = new ProgressBar();
		GroupLastGame = new GroupBox();
		GB_Logo = new GroupBox();
		ImgLogo = new PictureBox();
		ImgCover = new PictureBox();
		ImgBack = new PictureBox();
		ImgSpine = new PictureBox();
		GB_BG = new GroupBox();
		ImgBackground = new PictureBox();
		GB_Screens = new GroupBox();
		ImgScreen2 = new PictureBox();
		ImgScreen1 = new PictureBox();
		GB_Disc = new GroupBox();
		ImgDisc = new PictureBox();
		L_StatusTitle = new TextBox();
		GroupLastGame_Title = new Label();
		L_StatusID = new TextBox();
		Label2 = new Label();
		GroupStatus = new GroupBox();
		L_Status = new Label();
		TB_Log = new TextBox();
		B_Start = new Button();
		GB_OptDownloads = new GroupBox();
		CB_RandScrReplace = new CheckBox();
		CB_RandBGREplace = new CheckBox();
		CB_BackReplace = new CheckBox();
		CB_LgoReplace = new CheckBox();
		CB_SpineReplace = new CheckBox();
		CB_DiscReplace = new CheckBox();
		CB_CoverReplace = new CheckBox();
		CB_Lgo = new CheckBox();
		CB_Spine = new CheckBox();
		CB_Back = new CheckBox();
		CB_Disc = new CheckBox();
		CB_Cover = new CheckBox();
		CB_RandScr = new CheckBox();
		CB_RandBG = new CheckBox();
		B_Cancel = new Button();
		((Control)GroupLastGame).SuspendLayout();
		((Control)GB_Logo).SuspendLayout();
		((ISupportInitialize)ImgLogo).BeginInit();
		((ISupportInitialize)ImgCover).BeginInit();
		((ISupportInitialize)ImgBack).BeginInit();
		((ISupportInitialize)ImgSpine).BeginInit();
		((Control)GB_BG).SuspendLayout();
		((ISupportInitialize)ImgBackground).BeginInit();
		((Control)GB_Screens).SuspendLayout();
		((ISupportInitialize)ImgScreen2).BeginInit();
		((ISupportInitialize)ImgScreen1).BeginInit();
		((Control)GB_Disc).SuspendLayout();
		((ISupportInitialize)ImgDisc).BeginInit();
		((Control)GroupStatus).SuspendLayout();
		((Control)GB_OptDownloads).SuspendLayout();
		((Control)this).SuspendLayout();
		((Control)ProgressBarStatus).Location = new Point(85, 16);
		((Control)ProgressBarStatus).Name = "ProgressBarStatus";
		((Control)ProgressBarStatus).Size = new Size(109, 23);
		((Control)ProgressBarStatus).TabIndex = 1;
		((Control)GroupLastGame).Controls.Add((Control)(object)GB_Logo);
		((Control)GroupLastGame).Controls.Add((Control)(object)ImgCover);
		((Control)GroupLastGame).Controls.Add((Control)(object)ImgBack);
		((Control)GroupLastGame).Controls.Add((Control)(object)ImgSpine);
		((Control)GroupLastGame).Controls.Add((Control)(object)GB_BG);
		((Control)GroupLastGame).Controls.Add((Control)(object)GB_Screens);
		((Control)GroupLastGame).Controls.Add((Control)(object)GB_Disc);
		((Control)GroupLastGame).Controls.Add((Control)(object)L_StatusTitle);
		((Control)GroupLastGame).Controls.Add((Control)(object)GroupLastGame_Title);
		((Control)GroupLastGame).Controls.Add((Control)(object)L_StatusID);
		((Control)GroupLastGame).Controls.Add((Control)(object)Label2);
		((Control)GroupLastGame).Location = new Point(12, 12);
		((Control)GroupLastGame).Name = "GroupLastGame";
		((Control)GroupLastGame).Size = new Size(893, 580);
		((Control)GroupLastGame).TabIndex = 5;
		GroupLastGame.TabStop = false;
		((Control)GroupLastGame).Text = "Last game processed";
		((Control)GB_Logo).Controls.Add((Control)(object)ImgLogo);
		((Control)GB_Logo).Location = new Point(530, 43);
		((Control)GB_Logo).Name = "GB_Logo";
		((Control)GB_Logo).Size = new Size(320, 158);
		((Control)GB_Logo).TabIndex = 26;
		GB_Logo.TabStop = false;
		((Control)GB_Logo).Text = "Logo";
		ImgLogo.Image = (Image)componentResourceManager.GetObject("ImgLogo.Image");
		((Control)ImgLogo).Location = new Point(6, 22);
		((Control)ImgLogo).Name = "ImgLogo";
		((Control)ImgLogo).Size = new Size(300, 125);
		ImgLogo.SizeMode = (PictureBoxSizeMode)4;
		ImgLogo.TabIndex = 0;
		ImgLogo.TabStop = false;
		ImgCover.Image = (Image)componentResourceManager.GetObject("ImgCover.Image");
		((Control)ImgCover).Location = new Point(201, 42);
		((Control)ImgCover).Name = "ImgCover";
		((Control)ImgCover).Size = new Size(168, 240);
		ImgCover.SizeMode = (PictureBoxSizeMode)4;
		ImgCover.TabIndex = 3;
		ImgCover.TabStop = false;
		ImgBack.Image = (Image)componentResourceManager.GetObject("ImgBack.Image");
		((Control)ImgBack).Location = new Point(9, 42);
		((Control)ImgBack).Name = "ImgBack";
		((Control)ImgBack).Size = new Size(168, 240);
		ImgBack.SizeMode = (PictureBoxSizeMode)4;
		ImgBack.TabIndex = 3;
		ImgBack.TabStop = false;
		ImgSpine.Image = (Image)componentResourceManager.GetObject("ImgSpine.Image");
		((Control)ImgSpine).Location = new Point(180, 42);
		((Control)ImgSpine).Margin = new Padding(0);
		((Control)ImgSpine).Name = "ImgSpine";
		((Control)ImgSpine).Size = new Size(18, 240);
		ImgSpine.SizeMode = (PictureBoxSizeMode)4;
		ImgSpine.TabIndex = 3;
		ImgSpine.TabStop = false;
		((Control)GB_BG).Controls.Add((Control)(object)ImgBackground);
		((Control)GB_BG).Location = new Point(9, 296);
		((Control)GB_BG).Name = "GB_BG";
		((Control)GB_BG).Size = new Size(333, 269);
		((Control)GB_BG).TabIndex = 25;
		GB_BG.TabStop = false;
		((Control)GB_BG).Text = "Background";
		ImgBackground.Image = (Image)componentResourceManager.GetObject("ImgBackground.Image");
		((Control)ImgBackground).Location = new Point(6, 19);
		((Control)ImgBackground).Name = "ImgBackground";
		((Control)ImgBackground).Size = new Size(320, 240);
		ImgBackground.SizeMode = (PictureBoxSizeMode)4;
		ImgBackground.TabIndex = 0;
		ImgBackground.TabStop = false;
		((Control)GB_Screens).Controls.Add((Control)(object)ImgScreen2);
		((Control)GB_Screens).Controls.Add((Control)(object)ImgScreen1);
		((Control)GB_Screens).Location = new Point(348, 296);
		((Control)GB_Screens).Name = "GB_Screens";
		((Control)GB_Screens).Size = new Size(535, 269);
		((Control)GB_Screens).TabIndex = 24;
		GB_Screens.TabStop = false;
		((Control)GB_Screens).Text = "Game ScreenShots";
		ImgScreen2.Image = (Image)componentResourceManager.GetObject("ImgScreen2.Image");
		((Control)ImgScreen2).Location = new Point(273, 45);
		((Control)ImgScreen2).Name = "ImgScreen2";
		((Control)ImgScreen2).Size = new Size(250, 188);
		ImgScreen2.SizeMode = (PictureBoxSizeMode)4;
		ImgScreen2.TabIndex = 41;
		ImgScreen2.TabStop = false;
		ImgScreen1.Image = (Image)componentResourceManager.GetObject("ImgScreen1.Image");
		((Control)ImgScreen1).Location = new Point(17, 45);
		((Control)ImgScreen1).Name = "ImgScreen1";
		((Control)ImgScreen1).Size = new Size(250, 188);
		ImgScreen1.SizeMode = (PictureBoxSizeMode)4;
		ImgScreen1.TabIndex = 40;
		ImgScreen1.TabStop = false;
		((Control)GB_Disc).Controls.Add((Control)(object)ImgDisc);
		((Control)GB_Disc).Location = new Point(375, 43);
		((Control)GB_Disc).Name = "GB_Disc";
		((Control)GB_Disc).Size = new Size(149, 107);
		((Control)GB_Disc).TabIndex = 22;
		GB_Disc.TabStop = false;
		((Control)GB_Disc).Text = "Disc";
		((Control)ImgDisc).BackgroundImageLayout = (ImageLayout)3;
		ImgDisc.Image = (Image)componentResourceManager.GetObject("ImgDisc.Image");
		((Control)ImgDisc).Location = new Point(36, 12);
		((Control)ImgDisc).Name = "ImgDisc";
		((Control)ImgDisc).Size = new Size(80, 80);
		ImgDisc.SizeMode = (PictureBoxSizeMode)1;
		ImgDisc.TabIndex = 6;
		ImgDisc.TabStop = false;
		((Control)L_StatusTitle).Location = new Point(42, 16);
		((Control)L_StatusTitle).Name = "L_StatusTitle";
		((TextBoxBase)L_StatusTitle).ReadOnly = true;
		((Control)L_StatusTitle).Size = new Size(425, 20);
		((Control)L_StatusTitle).TabIndex = 9;
		((Control)GroupLastGame_Title).AutoSize = true;
		((Control)GroupLastGame_Title).Location = new Point(6, 19);
		((Control)GroupLastGame_Title).Name = "GroupLastGame_Title";
		((Control)GroupLastGame_Title).Size = new Size(30, 13);
		((Control)GroupLastGame_Title).TabIndex = 8;
		((Control)GroupLastGame_Title).Text = "Title:";
		((Control)L_StatusID).Location = new Point(500, 16);
		((Control)L_StatusID).Name = "L_StatusID";
		((TextBoxBase)L_StatusID).ReadOnly = true;
		((Control)L_StatusID).Size = new Size(138, 20);
		((Control)L_StatusID).TabIndex = 7;
		((Control)Label2).AutoSize = true;
		((Control)Label2).Location = new Point(473, 19);
		((Control)Label2).Name = "Label2";
		((Control)Label2).Size = new Size(21, 13);
		((Control)Label2).TabIndex = 6;
		((Control)Label2).Text = "ID:";
		((Control)GroupStatus).Controls.Add((Control)(object)L_Status);
		((Control)GroupStatus).Controls.Add((Control)(object)ProgressBarStatus);
		((Control)GroupStatus).Location = new Point(908, 389);
		((Control)GroupStatus).Name = "GroupStatus";
		((Control)GroupStatus).Size = new Size(200, 50);
		((Control)GroupStatus).TabIndex = 7;
		GroupStatus.TabStop = false;
		((Control)GroupStatus).Text = "Status";
		((Control)L_Status).Location = new Point(6, 19);
		((Control)L_Status).Name = "L_Status";
		((Control)L_Status).Size = new Size(73, 17);
		((Control)L_Status).TabIndex = 26;
		((Control)L_Status).Text = "0 / 0";
		L_Status.TextAlign = (ContentAlignment)64;
		((Control)TB_Log).Location = new Point(908, 444);
		((TextBoxBase)TB_Log).Multiline = true;
		((Control)TB_Log).Name = "TB_Log";
		((TextBoxBase)TB_Log).ReadOnly = true;
		TB_Log.ScrollBars = (ScrollBars)2;
		((Control)TB_Log).Size = new Size(201, 148);
		((Control)TB_Log).TabIndex = 8;
		((Control)B_Start).Location = new Point(908, 353);
		((Control)B_Start).Name = "B_Start";
		((Control)B_Start).Size = new Size(91, 30);
		((Control)B_Start).TabIndex = 10;
		((Control)B_Start).Text = "Start!";
		((ButtonBase)B_Start).UseVisualStyleBackColor = true;
		((Control)B_Start).Click += B_Start_Click;
		((Control)GB_OptDownloads).Controls.Add((Control)(object)CB_RandScrReplace);
		((Control)GB_OptDownloads).Controls.Add((Control)(object)CB_RandBGREplace);
		((Control)GB_OptDownloads).Controls.Add((Control)(object)CB_BackReplace);
		((Control)GB_OptDownloads).Controls.Add((Control)(object)CB_LgoReplace);
		((Control)GB_OptDownloads).Controls.Add((Control)(object)CB_SpineReplace);
		((Control)GB_OptDownloads).Controls.Add((Control)(object)CB_DiscReplace);
		((Control)GB_OptDownloads).Controls.Add((Control)(object)CB_CoverReplace);
		((Control)GB_OptDownloads).Controls.Add((Control)(object)CB_Lgo);
		((Control)GB_OptDownloads).Controls.Add((Control)(object)CB_Spine);
		((Control)GB_OptDownloads).Controls.Add((Control)(object)CB_Back);
		((Control)GB_OptDownloads).Controls.Add((Control)(object)CB_Disc);
		((Control)GB_OptDownloads).Controls.Add((Control)(object)CB_Cover);
		((Control)GB_OptDownloads).Controls.Add((Control)(object)CB_RandScr);
		((Control)GB_OptDownloads).Controls.Add((Control)(object)CB_RandBG);
		((Control)GB_OptDownloads).Location = new Point(908, 12);
		((Control)GB_OptDownloads).Name = "GB_OptDownloads";
		((Control)GB_OptDownloads).Size = new Size(200, 335);
		((Control)GB_OptDownloads).TabIndex = 13;
		GB_OptDownloads.TabStop = false;
		((Control)GB_OptDownloads).Text = "Download options:";
		((Control)CB_RandScrReplace).AutoSize = true;
		((Control)CB_RandScrReplace).Location = new Point(26, 310);
		((Control)CB_RandScrReplace).Name = "CB_RandScrReplace";
		((Control)CB_RandScrReplace).Size = new Size(103, 17);
		((Control)CB_RandScrReplace).TabIndex = 15;
		((Control)CB_RandScrReplace).Text = "Replace if exists";
		((ButtonBase)CB_RandScrReplace).UseVisualStyleBackColor = true;
		((Control)CB_RandBGREplace).AutoSize = true;
		((Control)CB_RandBGREplace).Location = new Point(26, 265);
		((Control)CB_RandBGREplace).Name = "CB_RandBGREplace";
		((Control)CB_RandBGREplace).Size = new Size(103, 17);
		((Control)CB_RandBGREplace).TabIndex = 14;
		((Control)CB_RandBGREplace).Text = "Replace if exists";
		((ButtonBase)CB_RandBGREplace).UseVisualStyleBackColor = true;
		((Control)CB_BackReplace).AutoSize = true;
		((Control)CB_BackReplace).Location = new Point(26, 85);
		((Control)CB_BackReplace).Name = "CB_BackReplace";
		((Control)CB_BackReplace).Size = new Size(103, 17);
		((Control)CB_BackReplace).TabIndex = 13;
		((Control)CB_BackReplace).Text = "Replace if exists";
		((ButtonBase)CB_BackReplace).UseVisualStyleBackColor = true;
		((Control)CB_LgoReplace).AutoSize = true;
		((Control)CB_LgoReplace).Location = new Point(26, 220);
		((Control)CB_LgoReplace).Name = "CB_LgoReplace";
		((Control)CB_LgoReplace).Size = new Size(103, 17);
		((Control)CB_LgoReplace).TabIndex = 12;
		((Control)CB_LgoReplace).Text = "Replace if exists";
		((ButtonBase)CB_LgoReplace).UseVisualStyleBackColor = true;
		((Control)CB_SpineReplace).AutoSize = true;
		((Control)CB_SpineReplace).Location = new Point(26, 175);
		((Control)CB_SpineReplace).Name = "CB_SpineReplace";
		((Control)CB_SpineReplace).Size = new Size(103, 17);
		((Control)CB_SpineReplace).TabIndex = 11;
		((Control)CB_SpineReplace).Text = "Replace if exists";
		((ButtonBase)CB_SpineReplace).UseVisualStyleBackColor = true;
		((Control)CB_DiscReplace).AutoSize = true;
		((Control)CB_DiscReplace).Location = new Point(26, 130);
		((Control)CB_DiscReplace).Name = "CB_DiscReplace";
		((Control)CB_DiscReplace).Size = new Size(103, 17);
		((Control)CB_DiscReplace).TabIndex = 10;
		((Control)CB_DiscReplace).Text = "Replace if exists";
		((ButtonBase)CB_DiscReplace).UseVisualStyleBackColor = true;
		((Control)CB_CoverReplace).AutoSize = true;
		((Control)CB_CoverReplace).Location = new Point(26, 40);
		((Control)CB_CoverReplace).Name = "CB_CoverReplace";
		((Control)CB_CoverReplace).Size = new Size(103, 17);
		((Control)CB_CoverReplace).TabIndex = 9;
		((Control)CB_CoverReplace).Text = "Replace if exists";
		((ButtonBase)CB_CoverReplace).UseVisualStyleBackColor = true;
		((Control)CB_Lgo).AutoSize = true;
		((Control)CB_Lgo).Location = new Point(5, 200);
		((Control)CB_Lgo).Name = "CB_Lgo";
		((Control)CB_Lgo).Size = new Size(101, 17);
		((Control)CB_Lgo).TabIndex = 8;
		((Control)CB_Lgo).Text = "Download Logo";
		((ButtonBase)CB_Lgo).UseVisualStyleBackColor = true;
		((Control)CB_Spine).AutoSize = true;
		CB_Spine.Checked = true;
		CB_Spine.CheckState = (CheckState)1;
		((Control)CB_Spine).Location = new Point(5, 155);
		((Control)CB_Spine).Name = "CB_Spine";
		((Control)CB_Spine).Size = new Size(104, 17);
		((Control)CB_Spine).TabIndex = 7;
		((Control)CB_Spine).Text = "Download Spine";
		((ButtonBase)CB_Spine).UseVisualStyleBackColor = true;
		((Control)CB_Back).AutoSize = true;
		CB_Back.Checked = true;
		CB_Back.CheckState = (CheckState)1;
		((Control)CB_Back).Location = new Point(5, 65);
		((Control)CB_Back).Name = "CB_Back";
		((Control)CB_Back).Size = new Size(133, 17);
		((Control)CB_Back).TabIndex = 6;
		((Control)CB_Back).Text = "Download Back Cover";
		((ButtonBase)CB_Back).UseVisualStyleBackColor = true;
		((Control)CB_Disc).AutoSize = true;
		CB_Disc.Checked = true;
		CB_Disc.CheckState = (CheckState)1;
		((Control)CB_Disc).Location = new Point(5, 110);
		((Control)CB_Disc).Name = "CB_Disc";
		((Control)CB_Disc).Size = new Size(98, 17);
		((Control)CB_Disc).TabIndex = 5;
		((Control)CB_Disc).Text = "Download Disc";
		((ButtonBase)CB_Disc).UseVisualStyleBackColor = true;
		((Control)CB_Cover).AutoSize = true;
		CB_Cover.Checked = true;
		CB_Cover.CheckState = (CheckState)1;
		((Control)CB_Cover).Location = new Point(5, 20);
		((Control)CB_Cover).Name = "CB_Cover";
		((Control)CB_Cover).Size = new Size(105, 17);
		((Control)CB_Cover).TabIndex = 4;
		((Control)CB_Cover).Text = "Download Cover";
		((ButtonBase)CB_Cover).UseVisualStyleBackColor = true;
		((Control)CB_RandScr).AutoSize = true;
		((Control)CB_RandScr).Location = new Point(5, 290);
		((Control)CB_RandScr).Name = "CB_RandScr";
		((Control)CB_RandScr).Size = new Size(174, 17);
		((Control)CB_RandScr).TabIndex = 3;
		((Control)CB_RandScr).Text = "Download random Screenshots";
		((ButtonBase)CB_RandScr).UseVisualStyleBackColor = true;
		((Control)CB_RandBG).AutoSize = true;
		((Control)CB_RandBG).Location = new Point(5, 245);
		((Control)CB_RandBG).Name = "CB_RandBG";
		((Control)CB_RandBG).Size = new Size(173, 17);
		((Control)CB_RandBG).TabIndex = 2;
		((Control)CB_RandBG).Text = "Download random Background";
		((ButtonBase)CB_RandBG).UseVisualStyleBackColor = true;
		((Control)B_Cancel).Enabled = false;
		((Control)B_Cancel).Location = new Point(1017, 353);
		((Control)B_Cancel).Name = "B_Cancel";
		((Control)B_Cancel).Size = new Size(91, 30);
		((Control)B_Cancel).TabIndex = 14;
		((Control)B_Cancel).Text = "Cancel";
		((ButtonBase)B_Cancel).UseVisualStyleBackColor = true;
		((Control)B_Cancel).Click += B_Cancel_Click;
		((ContainerControl)this).AutoScaleDimensions = new SizeF(6f, 13f);
		((ContainerControl)this).AutoScaleMode = (AutoScaleMode)1;
		((Form)this).ClientSize = new Size(1120, 601);
		((Control)this).Controls.Add((Control)(object)B_Cancel);
		((Control)this).Controls.Add((Control)(object)GB_OptDownloads);
		((Control)this).Controls.Add((Control)(object)B_Start);
		((Control)this).Controls.Add((Control)(object)TB_Log);
		((Control)this).Controls.Add((Control)(object)GroupStatus);
		((Control)this).Controls.Add((Control)(object)GroupLastGame);
		((Control)this).Font = new Font("Microsoft Sans Serif", 8f, (FontStyle)0, (GraphicsUnit)3);
		((Form)this).FormBorderStyle = (FormBorderStyle)3;
		((Form)this).Icon = (Icon)componentResourceManager.GetObject("$this.Icon");
		((Form)this).MaximizeBox = false;
		((Form)this).MinimizeBox = false;
		((Control)this).Name = "BatchArtDownload";
		((Form)this).ShowInTaskbar = false;
		((Form)this).StartPosition = (FormStartPosition)4;
		((Control)this).Text = "Batch game ART download";
		((Form)this).Shown += BatchArtDownload_Shown;
		((Control)GroupLastGame).ResumeLayout(false);
		((Control)GroupLastGame).PerformLayout();
		((Control)GB_Logo).ResumeLayout(false);
		((ISupportInitialize)ImgLogo).EndInit();
		((ISupportInitialize)ImgCover).EndInit();
		((ISupportInitialize)ImgBack).EndInit();
		((ISupportInitialize)ImgSpine).EndInit();
		((Control)GB_BG).ResumeLayout(false);
		((ISupportInitialize)ImgBackground).EndInit();
		((Control)GB_Screens).ResumeLayout(false);
		((ISupportInitialize)ImgScreen2).EndInit();
		((ISupportInitialize)ImgScreen1).EndInit();
		((Control)GB_Disc).ResumeLayout(false);
		((ISupportInitialize)ImgDisc).EndInit();
		((Control)GroupStatus).ResumeLayout(false);
		((Control)GB_OptDownloads).ResumeLayout(false);
		((Control)GB_OptDownloads).PerformLayout();
		((Control)this).ResumeLayout(false);
		((Control)this).PerformLayout();
	}
}
