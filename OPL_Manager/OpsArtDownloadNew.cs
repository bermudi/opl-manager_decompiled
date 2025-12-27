using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using ImageMagick;
using OPL_Manager.My.Resources;
using OplManagerService;

namespace OPL_Manager;

public class OpsArtDownloadNew : BaseForm
{
	private GameInfo Game;

	private CommonFuncs.ArtSizeClass GameArtSizes;

	private GameART serverResponse;

	private IContainer components;

	internal GroupBox GB_Disc;

	internal PictureBoxWithABigFuckingX Img_Disc;

	internal GroupBox GB_Cover;

	internal Button Cover_Delete;

	internal PictureBoxWithABigFuckingX Img_Cover;

	internal GroupBox GB_BackCover;

	internal PictureBoxWithABigFuckingX Img_CoverBack;

	internal GroupBox GB_Screens;

	internal Button Screen2_Database;

	internal Button Screen1_Database;

	internal Button Screen2_Browse;

	internal PictureBoxWithABigFuckingX Img_Screen2;

	internal Button Screen1__Browse;

	internal PictureBoxWithABigFuckingX Img_Screen1;

	internal GroupBox GB_Bg;

	internal Button Background_Database;

	internal PictureBoxWithABigFuckingX Img_Background;

	internal Button Background_Browse;

	internal Button B_Next;

	internal Button B_Previous;

	internal Button Cover_Database;

	internal Button Disc_Database;

	internal Button Disc_Delete;

	internal Button CoverBack_Database;

	internal Button CoverBack_Delete;

	internal Button Disc_Browse;

	internal Button Cover_Browse;

	internal Button CoverBack_Browse;

	internal Button Background_Delete;

	internal Button Screen2_Delete;

	internal Button Screen1_Delete;

	internal OpenFileDialog OpenFileDialog1;

	internal Label L_Status;

	internal Label L_Index;

	internal Button B_Help;

	internal ToolTip ToolTipMain;

	internal LinkLabel LinkLabelGoogle;

	internal GroupBox GB_Spine;

	internal Button Spine_Browse;

	internal Button Spine_Database;

	internal Button Spine_Delete;

	internal PictureBoxWithABigFuckingX Img_Spine;

	internal CheckBox CB_DontShare;

	internal GroupBox GB_Logo;

	internal Button Logo_Browse;

	internal Button Logo_Delete;

	internal Button Logo_Database;

	internal PictureBoxWithABigFuckingX Img_Logo;

	internal Label L_WarnPNG;

	public OpsArtDownloadNew()
	{
		InitializeComponent();
	}

	private void OpsArtDownloadNew_Shown(object sender, EventArgs e)
	{
		((Control)Img_Screen1).BackColor = Color.Gray;
		((Control)Img_Screen2).BackColor = Color.Gray;
		((Control)Img_Disc).BackColor = Color.Gray;
		((Control)Img_Logo).BackColor = Color.Gray;
		((Control)CB_DontShare).Text = Translated.OpsArtDownload_IgnoreGame;
		ToolTipMain.SetToolTip((Control)(object)CB_DontShare, Translated.OpsArtDownload_IgnoreGameExplain);
		Init();
	}

	private void Init()
	{
		Game = Program.MainFormInst.SelectedGame;
		GameArtSizes = CommonFuncs.ArtSizes(Game.Type);
		((Control)this).Text = Game.Type.ToString() + " | " + Game.Title + " | " + Game.ID;
		((Control)GB_Cover).Text = Translated.GLOBAL_STRING_COVER + CommonFuncs.ArtSizeAsTextString(GameArtSizes.COV);
		((Control)GB_BackCover).Text = Translated.GLOBAL_STRING_BACK_COVER + CommonFuncs.ArtSizeAsTextString(GameArtSizes.COV2);
		((Control)GB_Spine).Text = Translated.GLOBAL_STRING_SPINE + CommonFuncs.ArtSizeAsTextString(GameArtSizes.LAB);
		((Control)GB_Screens).Text = Translated.GLOBAL_STRING_SCREENSHOTS + CommonFuncs.ArtSizeAsTextString(GameArtSizes.SCR);
		((Control)GB_Bg).Text = Translated.GLOBAL_STRING_BACKGROUNDS + CommonFuncs.ArtSizeAsTextString(GameArtSizes.BG);
		((Control)GB_Disc).Text = Translated.GLOBAL_STRING_DISC + CommonFuncs.ArtSizeAsTextString(GameArtSizes.ICO);
		((Control)GB_Logo).Text = Translated.GLOBAL_STRING_LOGO + CommonFuncs.ArtSizeAsTextString(GameArtSizes.LGO);
		((Control)L_WarnPNG).Text = Translated.OpsArtDownload_PngWarning;
		NextPrevButtons();
		CB_DontShare.CheckedChanged -= CB_DontShare_CheckedChanged;
		CB_DontShare.Checked = OplmSettings.ignoredGameList.Contains(Game.ID);
		CB_DontShare.CheckedChanged += CB_DontShare_CheckedChanged;
		((Control)Disc_Database).Enabled = false;
		((Control)Cover_Database).Enabled = false;
		((Control)CoverBack_Database).Enabled = false;
		((Control)Spine_Database).Enabled = false;
		((Control)Logo_Database).Enabled = false;
		((Control)Screen1_Database).Enabled = false;
		((Control)Screen2_Database).Enabled = false;
		((Control)Background_Database).Enabled = false;
		((Control)Img_Disc).AllowDrop = true;
		((Control)Img_Cover).AllowDrop = true;
		((Control)Img_CoverBack).AllowDrop = true;
		((Control)Img_Spine).AllowDrop = true;
		((Control)Img_Logo).AllowDrop = true;
		((Control)Img_Background).AllowDrop = true;
		((Control)Img_Screen1).AllowDrop = true;
		((Control)Img_Screen2).AllowDrop = true;
		((Control)L_Status).Text = Translated.OpsArtDownload_Connecting;
		if ((Game.Type == GameType.POPS) | (Game.Type == GameType.PS2))
		{
			DoArtSearchSingle();
		}
		else
		{
			((Control)L_Status).Text = "-";
		}
		RefreshAll();
	}

	private async void DoArtSearchSingle()
	{
		ArtSearchSingleResponse artSearchSingleResponse = await CommonFuncs.SoapAPI.ArtSearchSingleAsync(Game.Type, Program.MainFormInst.UserID, Game.ID);
		if (artSearchSingleResponse != null)
		{
			((Control)L_Status).Text = Translated.OpsArtDownload_Connected;
			serverResponse = artSearchSingleResponse.Body.ArtSearchSingleResult;
			((Control)Disc_Database).Enabled = !string.IsNullOrEmpty(serverResponse.ICO);
			((Control)Cover_Database).Enabled = !string.IsNullOrEmpty(serverResponse.COV);
			((Control)CoverBack_Database).Enabled = !string.IsNullOrEmpty(serverResponse.COV2);
			((Control)Spine_Database).Enabled = !string.IsNullOrEmpty(serverResponse.LAB);
			((Control)Logo_Database).Enabled = !string.IsNullOrEmpty(serverResponse.LGO);
			((Control)Screen1_Database).Enabled = serverResponse.SCR.Count > 0;
			((Control)Screen2_Database).Enabled = serverResponse.SCR.Count > 0;
			((Control)Background_Database).Enabled = serverResponse.BG.Count > 0;
		}
		else
		{
			((Control)L_Status).Text = Translated.OpsArtDownload_ConnectFail;
		}
	}

	private void RefreshAll()
	{
		((PictureBox)Img_Disc).Image = (Image)(object)((Game.Type == GameType.POPS) ? Resources.ps1_disc_cover_small : Resources.art_disc);
		((PictureBox)Img_Cover).Image = (Image)(object)((Game.Type == GameType.POPS) ? Resources.ps1_front_cover : Resources.art_front);
		((PictureBox)Img_CoverBack).Image = (Image)(object)((Game.Type == GameType.POPS) ? Resources.ps1_back_cover : Resources.art_back);
		((PictureBox)Img_Spine).Image = (Image)(object)((Game.Type == GameType.POPS) ? Resources.ps1_lab_cover : Resources.art_spine);
		((PictureBox)Img_Logo).Image = (Image)(object)Resources.art_logo;
		((PictureBox)Img_Background).Image = (Image)(object)Resources.art_bg;
		((PictureBox)Img_Screen1).Image = (Image)(object)Resources.art_scr1;
		((PictureBox)Img_Screen2).Image = (Image)(object)Resources.art_scr2;
		Game.UpdateGameInfoArts();
		bool flag = false;
		if (Game.HasICO)
		{
			PNGClass pNGClass = new PNGClass(File.ReadAllBytes(Game.FileArtICO));
			((PictureBox)Img_Disc).Image = pNGClass.Bitmap;
			Img_Disc.HasErrorBorder = !pNGClass.IsPngAnd8Bit;
			flag |= !pNGClass.IsPngAnd8Bit;
		}
		if (Game.HasCOV)
		{
			PNGClass pNGClass2 = new PNGClass(File.ReadAllBytes(Game.FileArtCOV));
			((PictureBox)Img_Cover).Image = pNGClass2.Bitmap;
			Img_Cover.HasErrorBorder = !pNGClass2.IsPngAnd8Bit;
			flag |= !pNGClass2.IsPngAnd8Bit;
		}
		if (Game.HasCOV2)
		{
			PNGClass pNGClass3 = new PNGClass(File.ReadAllBytes(Game.FileArtCOV2));
			((PictureBox)Img_CoverBack).Image = pNGClass3.Bitmap;
			Img_CoverBack.HasErrorBorder = !pNGClass3.IsPngAnd8Bit;
			flag |= !pNGClass3.IsPngAnd8Bit;
		}
		if (Game.HasLAB)
		{
			PNGClass pNGClass4 = new PNGClass(File.ReadAllBytes(Game.FileArtLAB));
			((PictureBox)Img_Spine).Image = pNGClass4.Bitmap;
			Img_Spine.HasErrorBorder = !pNGClass4.IsPngAnd8Bit;
			flag |= !pNGClass4.IsPngAnd8Bit;
		}
		if (Game.HasLGO)
		{
			PNGClass pNGClass5 = new PNGClass(File.ReadAllBytes(Game.FileArtLGO));
			((PictureBox)Img_Logo).Image = pNGClass5.Bitmap;
			Img_Logo.HasErrorBorder = !pNGClass5.IsPngAnd8Bit;
		}
		if (Game.HasSCR)
		{
			PNGClass pNGClass6 = new PNGClass(File.ReadAllBytes(Game.FileArtSCR));
			((PictureBox)Img_Screen1).Image = pNGClass6.Bitmap;
			Img_Screen1.HasErrorBorder = !pNGClass6.IsPngAnd8Bit;
			flag |= !pNGClass6.IsPngAnd8Bit;
		}
		if (Game.HasSCR2)
		{
			PNGClass pNGClass7 = new PNGClass(File.ReadAllBytes(Game.FileArtSCR2));
			((PictureBox)Img_Screen2).Image = pNGClass7.Bitmap;
			Img_Screen2.HasErrorBorder = !pNGClass7.IsPngAnd8Bit;
			flag |= !pNGClass7.IsPngAnd8Bit;
		}
		if (Game.HasBG)
		{
			PNGClass pNGClass8 = new PNGClass(File.ReadAllBytes(Game.FileArtBG));
			((PictureBox)Img_Background).Image = pNGClass8.Bitmap;
			Img_Background.HasErrorBorder = !pNGClass8.IsPngAnd8Bit;
			flag |= !pNGClass8.IsPngAnd8Bit;
		}
		((Control)L_WarnPNG).Visible = flag;
	}

	private void OfferArtResizeConvert(ArtType artType, string origImg, Size targetSize, string artSuffixAndExt)
	{
		//IL_004b: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c7: Unknown result type (might be due to invalid IL or missing references)
		//IL_00cd: Invalid comparison between Unknown and I4
		//IL_00a4: Unknown result type (might be due to invalid IL or missing references)
		double num = 0.02;
		double num2 = (double)targetSize.Width / (double)targetSize.Height;
		using MagickImage magickImage = new MagickImage(origImg);
		if (magickImage.Width < targetSize.Width || magickImage.Height < targetSize.Height)
		{
			MessageBox.Show(Translated.OpsArtDownload_ResizeTargetSmaller, Translated.Global_Error);
			return;
		}
		bool flag = targetSize.Width == magickImage.Width && targetSize.Height == magickImage.Height;
		double num3 = (double)magickImage.Width / (double)magickImage.Height;
		if (!(Math.Abs(num2 - num3) < num))
		{
			MessageBox.Show(Translated.OpsArtDownload_ResizeWrongAspect, Translated.Global_Error, (MessageBoxButtons)0, (MessageBoxIcon)16);
		}
		else if ((int)MessageBox.Show(string.Format(Translated.OpsArtDownload_ResizeQuestion, CommonFuncs.ArtSizeAsTextString(targetSize)), Translated.Global_Question, (MessageBoxButtons)4, (MessageBoxIcon)32) != 7)
		{
			if (!flag)
			{
				MagickGeometry magickGeometry = new MagickGeometry(targetSize.Width, targetSize.Height);
				magickGeometry.IgnoreAspectRatio = true;
				magickImage.Resize(magickGeometry);
			}
			QuantizeSettings quantizeSettings = new QuantizeSettings();
			quantizeSettings.Colors = 256;
			quantizeSettings.DitherMethod = DitherMethod.FloydSteinberg;
			magickImage.Quantize(quantizeSettings);
			magickImage.Format = MagickFormat.Png8;
			string text = Path.Join(Path.GetTempPath(), Guid.NewGuid().ToString() + ".png");
			magickImage.Write(text);
			ChangeFile(artType, text);
		}
	}

	private void ChangeFile(ArtType type, string srcFile)
	{
		PNGClass pNGClass = new PNGClass(File.ReadAllBytes(srcFile));
		switch (type)
		{
		case ArtType.ICO:
			if (pNGClass.IsPngAnd8Bit && pNGClass.Bitmap.Size == GameArtSizes.ICO)
			{
				CommonFuncs.DeleteAllArtExtensionsIfExists(Game.FileArtICO);
				File.Copy(srcFile, OplFolders.ART + Game.ArtID + "_ICO.png", overwrite: true);
				RefreshAll();
			}
			else
			{
				OfferArtResizeConvert(type, srcFile, GameArtSizes.ICO, "_ICO.png");
			}
			break;
		case ArtType.COV:
			if (pNGClass.IsPngAnd8Bit && pNGClass.Bitmap.Size == GameArtSizes.COV)
			{
				CommonFuncs.DeleteAllArtExtensionsIfExists(Game.FileArtCOV);
				File.Copy(srcFile, OplFolders.ART + Game.ArtID + "_COV.png", overwrite: true);
				RefreshAll();
			}
			else
			{
				OfferArtResizeConvert(type, srcFile, GameArtSizes.COV, "_COV.png");
			}
			break;
		case ArtType.COV2:
			if (pNGClass.IsPngAnd8Bit && pNGClass.Bitmap.Size == GameArtSizes.COV2)
			{
				CommonFuncs.DeleteAllArtExtensionsIfExists(Game.FileArtCOV2);
				File.Copy(srcFile, OplFolders.ART + Game.ArtID + "_COV2.png", overwrite: true);
				RefreshAll();
			}
			else
			{
				OfferArtResizeConvert(type, srcFile, GameArtSizes.COV2, "_COV2.png");
			}
			break;
		case ArtType.LAB:
			if (pNGClass.IsPngAnd8Bit && pNGClass.Bitmap.Size == GameArtSizes.LAB)
			{
				CommonFuncs.DeleteAllArtExtensionsIfExists(Game.FileArtLAB);
				File.Copy(srcFile, OplFolders.ART + Game.ArtID + "_LAB.png", overwrite: true);
				RefreshAll();
			}
			else
			{
				OfferArtResizeConvert(type, srcFile, GameArtSizes.LAB, "_LAB.png");
			}
			break;
		case ArtType.LGO:
			if (pNGClass.IsPngAnd8Bit && pNGClass.Bitmap.Size == GameArtSizes.LGO)
			{
				CommonFuncs.DeleteAllArtExtensionsIfExists(Game.FileArtLGO);
				File.Copy(srcFile, OplFolders.ART + Game.ArtID + "_LGO.png", overwrite: true);
				RefreshAll();
			}
			else
			{
				OfferArtResizeConvert(type, srcFile, GameArtSizes.LGO, "_LGO.png");
			}
			break;
		case ArtType.SCR:
			if (pNGClass.IsPngAnd8Bit && pNGClass.Bitmap.Size == GameArtSizes.SCR)
			{
				CommonFuncs.DeleteAllArtExtensionsIfExists(Game.FileArtSCR);
				File.Copy(srcFile, OplFolders.ART + Game.ArtID + "_SCR.png", overwrite: true);
				RefreshAll();
			}
			else
			{
				OfferArtResizeConvert(type, srcFile, GameArtSizes.SCR, "_SCR.png");
			}
			break;
		case ArtType.SCR2:
			if (pNGClass.IsPngAnd8Bit && pNGClass.Bitmap.Size == GameArtSizes.SCR)
			{
				CommonFuncs.DeleteAllArtExtensionsIfExists(Game.FileArtSCR2);
				File.Copy(srcFile, OplFolders.ART + Game.ArtID + "_SCR2.png", overwrite: true);
				RefreshAll();
			}
			else
			{
				OfferArtResizeConvert(type, srcFile, GameArtSizes.SCR, "_SCR2.png");
			}
			break;
		case ArtType.BG:
			if (pNGClass.IsPngAnd8Bit && pNGClass.Bitmap.Size == GameArtSizes.BG)
			{
				CommonFuncs.DeleteAllArtExtensionsIfExists(Game.FileArtBG);
				File.Copy(srcFile, OplFolders.ART + Game.ArtID + "_BG.png", overwrite: true);
				RefreshAll();
			}
			else
			{
				OfferArtResizeConvert(type, srcFile, GameArtSizes.BG, "_BG.png");
			}
			break;
		}
	}

	private void B_SCR_1_2_DB_Click(object sender, EventArgs e)
	{
		//IL_00e5: Unknown result type (might be due to invalid IL or missing references)
		//IL_0044: Unknown result type (might be due to invalid IL or missing references)
		//IL_004a: Invalid comparison between Unknown and I4
		if (serverResponse.SCR.Count != 0)
		{
			OpsArtDownloadNewScreenBrowser opsArtDownloadNewScreenBrowser = new OpsArtDownloadNewScreenBrowser();
			try
			{
				string text = ((sender == Screen1_Database) ? "_SCR.png" : "_SCR2.png");
				if ((int)opsArtDownloadNewScreenBrowser.ShowDialog(Game, text, serverResponse.SCR) != 1)
				{
					return;
				}
				using (MemoryStream mem = new MemoryStream(opsArtDownloadNewScreenBrowser.imgBytes))
				{
					if (text == "_SCR.png")
					{
						CommonFuncs.DeleteAllArtExtensionsIfExists(Game.FileArtSCR);
						CommonFuncs.WriteMemoryToFile(mem, OplFolders.ART + Game.ArtID + text);
					}
					else
					{
						CommonFuncs.DeleteAllArtExtensionsIfExists(Game.FileArtSCR2);
						CommonFuncs.WriteMemoryToFile(mem, OplFolders.ART + Game.ArtID + text);
					}
				}
				RefreshAll();
				return;
			}
			finally
			{
				((IDisposable)opsArtDownloadNewScreenBrowser)?.Dispose();
			}
		}
		MessageBox.Show("No Screenshots found!");
	}

	private void Background_Database_Click(object sender, EventArgs e)
	{
		//IL_0096: Unknown result type (might be due to invalid IL or missing references)
		//IL_002a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0030: Invalid comparison between Unknown and I4
		if (serverResponse.BG.Count != 0)
		{
			OpsArtDownloadNewBackground opsArtDownloadNewBackground = new OpsArtDownloadNewBackground();
			try
			{
				if ((int)opsArtDownloadNewBackground.ShowDialog(Game, serverResponse.BG) == 1)
				{
					using (MemoryStream mem = new MemoryStream(opsArtDownloadNewBackground.imgBytes))
					{
						CommonFuncs.DeleteAllArtExtensionsIfExists(Game.FileArtBG);
						CommonFuncs.WriteMemoryToFile(mem, OplFolders.ART + Game.ArtID + "_BG.png");
					}
					RefreshAll();
				}
				return;
			}
			finally
			{
				((IDisposable)opsArtDownloadNewBackground)?.Dispose();
			}
		}
		MessageBox.Show(Translated.OpsArtDownload_NoBgFound, Translated.Global_Information);
	}

	private void Cover_Database_Click(object sender, EventArgs e)
	{
		//IL_008e: Unknown result type (might be due to invalid IL or missing references)
		//IL_002a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0030: Invalid comparison between Unknown and I4
		if (!string.IsNullOrEmpty(serverResponse.COV))
		{
			OpsArtDownloadNewCoverPrompt opsArtDownloadNewCoverPrompt = new OpsArtDownloadNewCoverPrompt();
			try
			{
				if ((int)opsArtDownloadNewCoverPrompt.ShowDialog(Game, serverResponse.COV) == 6)
				{
					CommonFuncs.DeleteFileIfExists(Game.FileArtCOV);
					CommonFuncs.WriteByteArrayToFile(opsArtDownloadNewCoverPrompt.imageBytes, OplFolders.ART + Game.ArtID + "_COV" + serverResponse.ExCOV);
					RefreshAll();
				}
				return;
			}
			finally
			{
				((IDisposable)opsArtDownloadNewCoverPrompt)?.Dispose();
			}
		}
		MessageBox.Show(Translated.OpsArtDownload_NoCovFound, Translated.Global_Information);
	}

	private void CoverBack_Database_Click(object sender, EventArgs e)
	{
		//IL_008e: Unknown result type (might be due to invalid IL or missing references)
		//IL_002a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0030: Invalid comparison between Unknown and I4
		if (!string.IsNullOrEmpty(serverResponse.COV2))
		{
			OpsArtDownloadNewBackPrompt opsArtDownloadNewBackPrompt = new OpsArtDownloadNewBackPrompt();
			try
			{
				if ((int)opsArtDownloadNewBackPrompt.ShowDialog(Game, serverResponse.COV2) == 6)
				{
					CommonFuncs.DeleteFileIfExists(Game.FileArtCOV2);
					CommonFuncs.WriteByteArrayToFile(opsArtDownloadNewBackPrompt.imageBytes, OplFolders.ART + Game.ArtID + "_COV2" + serverResponse.ExCOV2);
					RefreshAll();
				}
				return;
			}
			finally
			{
				((IDisposable)opsArtDownloadNewBackPrompt)?.Dispose();
			}
		}
		MessageBox.Show(Translated.OpsArtDownload_NoCov2Found, Translated.Global_Information);
	}

	private void Spine_Database_Click(object sender, EventArgs e)
	{
		//IL_008e: Unknown result type (might be due to invalid IL or missing references)
		//IL_002a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0030: Invalid comparison between Unknown and I4
		if (!string.IsNullOrEmpty(serverResponse.LAB))
		{
			OpsArtDownloadNewSpinePrompt opsArtDownloadNewSpinePrompt = new OpsArtDownloadNewSpinePrompt();
			try
			{
				if ((int)opsArtDownloadNewSpinePrompt.ShowDialog(Game, serverResponse.LAB) == 6)
				{
					CommonFuncs.DeleteFileIfExists(Game.FileArtLAB);
					CommonFuncs.WriteByteArrayToFile(opsArtDownloadNewSpinePrompt.imageBytes, OplFolders.ART + Game.ArtID + "_LAB" + serverResponse.ExLAB);
					RefreshAll();
				}
				return;
			}
			finally
			{
				((IDisposable)opsArtDownloadNewSpinePrompt)?.Dispose();
			}
		}
		MessageBox.Show(Translated.OpsArtDownload_NoLabFound, Translated.Global_Information);
	}

	private void Logo_Database_Click(object sender, EventArgs e)
	{
		//IL_008e: Unknown result type (might be due to invalid IL or missing references)
		//IL_002a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0030: Invalid comparison between Unknown and I4
		if (!string.IsNullOrEmpty(serverResponse.LGO))
		{
			OpsArtDownloadNewLogoPrompt opsArtDownloadNewLogoPrompt = new OpsArtDownloadNewLogoPrompt();
			try
			{
				if ((int)opsArtDownloadNewLogoPrompt.ShowDialog(Game, serverResponse.LGO) == 6)
				{
					CommonFuncs.DeleteFileIfExists(Game.FileArtLGO);
					CommonFuncs.WriteByteArrayToFile(opsArtDownloadNewLogoPrompt.imageBytes, OplFolders.ART + Game.ArtID + "_LGO" + serverResponse.ExLGO);
					RefreshAll();
				}
				return;
			}
			finally
			{
				((IDisposable)opsArtDownloadNewLogoPrompt)?.Dispose();
			}
		}
		MessageBox.Show(Translated.OpsArtDownload_NoLgoFound, Translated.Global_Information);
	}

	private void Disc_Database_Click(object sender, EventArgs e)
	{
		//IL_008e: Unknown result type (might be due to invalid IL or missing references)
		//IL_002a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0030: Invalid comparison between Unknown and I4
		if (!string.IsNullOrEmpty(serverResponse.ICO))
		{
			OpsArtDownloadNewDiscPrompt opsArtDownloadNewDiscPrompt = new OpsArtDownloadNewDiscPrompt();
			try
			{
				if ((int)opsArtDownloadNewDiscPrompt.ShowDialog(Game, serverResponse.ICO) == 6)
				{
					CommonFuncs.DeleteFileIfExists(Game.FileArtICO);
					CommonFuncs.WriteByteArrayToFile(opsArtDownloadNewDiscPrompt.imageBytes, OplFolders.ART + Game.ArtID + "_ICO" + serverResponse.ExICO);
					RefreshAll();
				}
				return;
			}
			finally
			{
				((IDisposable)opsArtDownloadNewDiscPrompt)?.Dispose();
			}
		}
		MessageBox.Show(Translated.OpsArtDownload_NoIcoFound, Translated.Global_Information);
	}

	private void Disc_Browse_Click(object sender, EventArgs e)
	{
		//IL_006e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0074: Invalid comparison between Unknown and I4
		ArtType type = ArtType.ICO;
		if (sender == Disc_Browse)
		{
			type = ArtType.ICO;
		}
		else if (sender == Cover_Browse)
		{
			type = ArtType.COV;
		}
		else if (sender == CoverBack_Browse)
		{
			type = ArtType.COV2;
		}
		else if (sender == Spine_Browse)
		{
			type = ArtType.LAB;
		}
		else if (sender == Logo_Browse)
		{
			type = ArtType.LGO;
		}
		else if (sender == Screen1__Browse)
		{
			type = ArtType.SCR;
		}
		else if (sender == Screen2_Browse)
		{
			type = ArtType.SCR2;
		}
		else if (sender == Background_Browse)
		{
			type = ArtType.BG;
		}
		if ((int)((CommonDialog)OpenFileDialog1).ShowDialog() == 1)
		{
			ChangeFile(type, ((FileDialog)OpenFileDialog1).FileName);
		}
	}

	private void Cover_Delete_Click(object sender, EventArgs e)
	{
		string f = null;
		if (sender == Cover_Delete)
		{
			f = Game.FileArtCOV;
		}
		else if (sender == CoverBack_Delete)
		{
			f = Game.FileArtCOV2;
		}
		else if (sender == Spine_Delete)
		{
			f = Game.FileArtLAB;
		}
		else if (sender == Logo_Delete)
		{
			f = Game.FileArtLGO;
		}
		else if (sender == Background_Delete)
		{
			f = Game.FileArtBG;
		}
		else if (sender == Disc_Delete)
		{
			f = Game.FileArtICO;
		}
		else if (sender == Screen1_Delete)
		{
			f = Game.FileArtSCR;
		}
		else if (sender == Screen2_Delete)
		{
			f = Game.FileArtSCR2;
		}
		CommonFuncs.DeleteFileIfExistsPrompt(f);
		RefreshAll();
	}

	private void ART_DragEnter(object sender, DragEventArgs e)
	{
		e.Effect = (DragDropEffects)1;
	}

	private void ART_DragDrop(object sender, DragEventArgs e)
	{
		string[] array = (string[])e.Data.GetData(DataFormats.FileDrop);
		if (array.Length <= 1)
		{
			string text = array[0];
			bool flag = false;
			ArtType type = ArtType.ICO;
			if (sender == Img_Disc)
			{
				type = ArtType.ICO;
				flag = (Game.FileArtICO ?? "") == (text ?? "");
			}
			else if (sender == Img_Cover)
			{
				type = ArtType.COV;
				flag = (Game.FileArtCOV ?? "") == (text ?? "");
			}
			else if (sender == Img_CoverBack)
			{
				type = ArtType.COV2;
				flag = (Game.FileArtCOV2 ?? "") == (text ?? "");
			}
			else if (sender == Img_Spine)
			{
				type = ArtType.LAB;
				flag = (Game.FileArtLAB ?? "") == (text ?? "");
			}
			else if (sender == Img_Logo)
			{
				type = ArtType.LGO;
				flag = (Game.FileArtLGO ?? "") == (text ?? "");
			}
			else if (sender == Img_Screen1)
			{
				type = ArtType.SCR;
				flag = (Game.FileArtSCR ?? "") == (text ?? "");
			}
			else if (sender == Img_Screen2)
			{
				type = ArtType.SCR2;
				flag = (Game.FileArtSCR2 ?? "") == (text ?? "");
			}
			else if (sender == Img_Background)
			{
				type = ArtType.BG;
				flag = (Game.FileArtBG ?? "") == (text ?? "");
			}
			if (!flag)
			{
				ChangeFile(type, array[0]);
			}
		}
	}

	private void B_Previous_Click(object sender, EventArgs e)
	{
		if (Program.MainFormInst.CanMoveUp)
		{
			Program.MainFormInst.MoveUp();
			Init();
		}
		NextPrevButtons();
	}

	private void B_Next_Click(object sender, EventArgs e)
	{
		if (Program.MainFormInst.CanMoveDown)
		{
			Program.MainFormInst.MoveDown();
			Init();
		}
		NextPrevButtons();
	}

	private void NextPrevButtons()
	{
		((Control)L_Index).Text = Program.MainFormInst.SelectedGameIndex + 1 + " / " + ((ListView)Program.MainFormInst.GameList).Items.Count;
		((Control)B_Previous).Enabled = Program.MainFormInst.CanMoveUp;
		((Control)B_Next).Enabled = Program.MainFormInst.CanMoveDown;
	}

	protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
	{
		//IL_0000: Unknown result type (might be due to invalid IL or missing references)
		//IL_0003: Invalid comparison between Unknown and I4
		//IL_000f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0012: Invalid comparison between Unknown and I4
		//IL_0020: Unknown result type (might be due to invalid IL or missing references)
		if ((int)keyData == 37)
		{
			B_Previous_Click(null, null);
			return true;
		}
		if ((int)keyData == 39)
		{
			B_Next_Click(null, null);
			return true;
		}
		return ((Form)this).ProcessCmdKey(ref msg, keyData);
	}

	private void B_Help_Click(object sender, EventArgs e)
	{
		//IL_000d: Unknown result type (might be due to invalid IL or missing references)
		MessageBox.Show(Translated.OpsArtDownload_Help, "?", (MessageBoxButtons)0, (MessageBoxIcon)64);
	}

	private void LinkLabelGoogle_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
	{
		CommonFuncs.OpenURL("http://www.google.com/search?tbm=isch&q=" + Game.ID.Replace("_", "-").Replace(".", ""));
	}

	private void CB_DontShare_CheckedChanged(object sender, EventArgs e)
	{
		if (CB_DontShare.Checked)
		{
			OplmSettings.ignoredGameList.Add(Game.ID);
		}
		else
		{
			OplmSettings.ignoredGameList.Remove(Game.ID);
		}
	}

	private void Img_Cover_MouseDown(object sender, MouseEventArgs e)
	{
		//IL_0001: Unknown result type (might be due to invalid IL or missing references)
		//IL_000b: Invalid comparison between Unknown and I4
		//IL_00db: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ef: Unknown result type (might be due to invalid IL or missing references)
		//IL_00fa: Expected O, but got Unknown
		//IL_00f5: Unknown result type (might be due to invalid IL or missing references)
		if ((int)e.Button == 1048576)
		{
			string text = "";
			if (sender == Img_Disc)
			{
				text = Game.FileArtICO;
			}
			else if (sender == Img_Cover)
			{
				text = Game.FileArtCOV;
			}
			else if (sender == Img_CoverBack)
			{
				text = Game.FileArtCOV2;
			}
			else if (sender == Img_Spine)
			{
				text = Game.FileArtLAB;
			}
			else if (sender == Img_Logo)
			{
				text = Game.FileArtLGO;
			}
			else if (sender == Img_Screen1)
			{
				text = Game.FileArtSCR;
			}
			else if (sender == Img_Screen2)
			{
				text = Game.FileArtSCR2;
			}
			else if (sender == Img_Background)
			{
				text = Game.FileArtBG;
			}
			if (!string.IsNullOrEmpty(text))
			{
				((Control)(PictureBox)sender).DoDragDrop((object)new DataObject(DataFormats.FileDrop, (object)new string[1] { text }), (DragDropEffects)1);
			}
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
		//IL_0027: Unknown result type (might be due to invalid IL or missing references)
		//IL_0031: Expected O, but got Unknown
		//IL_0032: Unknown result type (might be due to invalid IL or missing references)
		//IL_003c: Expected O, but got Unknown
		//IL_003d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0047: Expected O, but got Unknown
		//IL_0053: Unknown result type (might be due to invalid IL or missing references)
		//IL_005d: Expected O, but got Unknown
		//IL_005e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0068: Expected O, but got Unknown
		//IL_0069: Unknown result type (might be due to invalid IL or missing references)
		//IL_0073: Expected O, but got Unknown
		//IL_0074: Unknown result type (might be due to invalid IL or missing references)
		//IL_007e: Expected O, but got Unknown
		//IL_008a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0094: Expected O, but got Unknown
		//IL_0095: Unknown result type (might be due to invalid IL or missing references)
		//IL_009f: Expected O, but got Unknown
		//IL_00a0: Unknown result type (might be due to invalid IL or missing references)
		//IL_00aa: Expected O, but got Unknown
		//IL_00ab: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b5: Expected O, but got Unknown
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
		//IL_010e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0118: Expected O, but got Unknown
		//IL_0124: Unknown result type (might be due to invalid IL or missing references)
		//IL_012e: Expected O, but got Unknown
		//IL_012f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0139: Expected O, but got Unknown
		//IL_013a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0144: Expected O, but got Unknown
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
		//IL_01a3: Unknown result type (might be due to invalid IL or missing references)
		//IL_01ad: Expected O, but got Unknown
		//IL_01ae: Unknown result type (might be due to invalid IL or missing references)
		//IL_01b8: Expected O, but got Unknown
		//IL_01b9: Unknown result type (might be due to invalid IL or missing references)
		//IL_01c3: Expected O, but got Unknown
		//IL_01c4: Unknown result type (might be due to invalid IL or missing references)
		//IL_01ce: Expected O, but got Unknown
		//IL_01cf: Unknown result type (might be due to invalid IL or missing references)
		//IL_01d9: Expected O, but got Unknown
		//IL_01da: Unknown result type (might be due to invalid IL or missing references)
		//IL_01e4: Expected O, but got Unknown
		//IL_01f0: Unknown result type (might be due to invalid IL or missing references)
		//IL_01fa: Expected O, but got Unknown
		//IL_01fb: Unknown result type (might be due to invalid IL or missing references)
		//IL_0205: Expected O, but got Unknown
		//IL_0206: Unknown result type (might be due to invalid IL or missing references)
		//IL_0210: Expected O, but got Unknown
		//IL_0211: Unknown result type (might be due to invalid IL or missing references)
		//IL_021b: Expected O, but got Unknown
		//IL_021c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0226: Expected O, but got Unknown
		//IL_0232: Unknown result type (might be due to invalid IL or missing references)
		//IL_023c: Expected O, but got Unknown
		//IL_03ba: Unknown result type (might be due to invalid IL or missing references)
		//IL_03c4: Expected O, but got Unknown
		//IL_0454: Unknown result type (might be due to invalid IL or missing references)
		//IL_045e: Expected O, but got Unknown
		//IL_04e3: Unknown result type (might be due to invalid IL or missing references)
		//IL_04ed: Expected O, but got Unknown
		//IL_058a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0594: Expected O, but got Unknown
		//IL_05fd: Unknown result type (might be due to invalid IL or missing references)
		//IL_0607: Expected O, but got Unknown
		//IL_0614: Unknown result type (might be due to invalid IL or missing references)
		//IL_061e: Expected O, but got Unknown
		//IL_062b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0635: Expected O, but got Unknown
		//IL_0703: Unknown result type (might be due to invalid IL or missing references)
		//IL_070d: Expected O, but got Unknown
		//IL_07a0: Unknown result type (might be due to invalid IL or missing references)
		//IL_07aa: Expected O, but got Unknown
		//IL_083e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0848: Expected O, but got Unknown
		//IL_08dc: Unknown result type (might be due to invalid IL or missing references)
		//IL_08e6: Expected O, but got Unknown
		//IL_0954: Unknown result type (might be due to invalid IL or missing references)
		//IL_095e: Expected O, but got Unknown
		//IL_096b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0975: Expected O, but got Unknown
		//IL_0982: Unknown result type (might be due to invalid IL or missing references)
		//IL_098c: Expected O, but got Unknown
		//IL_0a5e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0a68: Expected O, but got Unknown
		//IL_0afb: Unknown result type (might be due to invalid IL or missing references)
		//IL_0b05: Expected O, but got Unknown
		//IL_0b99: Unknown result type (might be due to invalid IL or missing references)
		//IL_0ba3: Expected O, but got Unknown
		//IL_0c37: Unknown result type (might be due to invalid IL or missing references)
		//IL_0c41: Expected O, but got Unknown
		//IL_0caf: Unknown result type (might be due to invalid IL or missing references)
		//IL_0cb9: Expected O, but got Unknown
		//IL_0cc6: Unknown result type (might be due to invalid IL or missing references)
		//IL_0cd0: Expected O, but got Unknown
		//IL_0cdd: Unknown result type (might be due to invalid IL or missing references)
		//IL_0ce7: Expected O, but got Unknown
		//IL_0e11: Unknown result type (might be due to invalid IL or missing references)
		//IL_0e1b: Expected O, but got Unknown
		//IL_0ea6: Unknown result type (might be due to invalid IL or missing references)
		//IL_0eb0: Expected O, but got Unknown
		//IL_0f3b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0f45: Expected O, but got Unknown
		//IL_0fcf: Unknown result type (might be due to invalid IL or missing references)
		//IL_0fd9: Expected O, but got Unknown
		//IL_1060: Unknown result type (might be due to invalid IL or missing references)
		//IL_106a: Expected O, but got Unknown
		//IL_1100: Unknown result type (might be due to invalid IL or missing references)
		//IL_110a: Expected O, but got Unknown
		//IL_111b: Unknown result type (might be due to invalid IL or missing references)
		//IL_1125: Expected O, but got Unknown
		//IL_1198: Unknown result type (might be due to invalid IL or missing references)
		//IL_11a2: Expected O, but got Unknown
		//IL_11af: Unknown result type (might be due to invalid IL or missing references)
		//IL_11b9: Expected O, but got Unknown
		//IL_11c6: Unknown result type (might be due to invalid IL or missing references)
		//IL_11d0: Expected O, but got Unknown
		//IL_11e1: Unknown result type (might be due to invalid IL or missing references)
		//IL_11eb: Expected O, but got Unknown
		//IL_127e: Unknown result type (might be due to invalid IL or missing references)
		//IL_1288: Expected O, but got Unknown
		//IL_1299: Unknown result type (might be due to invalid IL or missing references)
		//IL_12a3: Expected O, but got Unknown
		//IL_1312: Unknown result type (might be due to invalid IL or missing references)
		//IL_131c: Expected O, but got Unknown
		//IL_1329: Unknown result type (might be due to invalid IL or missing references)
		//IL_1333: Expected O, but got Unknown
		//IL_1340: Unknown result type (might be due to invalid IL or missing references)
		//IL_134a: Expected O, but got Unknown
		//IL_141c: Unknown result type (might be due to invalid IL or missing references)
		//IL_1426: Expected O, but got Unknown
		//IL_14b1: Unknown result type (might be due to invalid IL or missing references)
		//IL_14bb: Expected O, but got Unknown
		//IL_1552: Unknown result type (might be due to invalid IL or missing references)
		//IL_155c: Expected O, but got Unknown
		//IL_15ca: Unknown result type (might be due to invalid IL or missing references)
		//IL_15d4: Expected O, but got Unknown
		//IL_15e1: Unknown result type (might be due to invalid IL or missing references)
		//IL_15eb: Expected O, but got Unknown
		//IL_15f8: Unknown result type (might be due to invalid IL or missing references)
		//IL_1602: Expected O, but got Unknown
		//IL_1613: Unknown result type (might be due to invalid IL or missing references)
		//IL_161d: Expected O, but got Unknown
		//IL_16a5: Unknown result type (might be due to invalid IL or missing references)
		//IL_16af: Expected O, but got Unknown
		//IL_173a: Unknown result type (might be due to invalid IL or missing references)
		//IL_1744: Expected O, but got Unknown
		//IL_17e0: Unknown result type (might be due to invalid IL or missing references)
		//IL_17ea: Expected O, but got Unknown
		//IL_1867: Unknown result type (might be due to invalid IL or missing references)
		//IL_1871: Expected O, but got Unknown
		//IL_18ed: Unknown result type (might be due to invalid IL or missing references)
		//IL_18f7: Expected O, but got Unknown
		//IL_19fd: Unknown result type (might be due to invalid IL or missing references)
		//IL_1a07: Expected O, but got Unknown
		//IL_1ad6: Unknown result type (might be due to invalid IL or missing references)
		//IL_1ae0: Expected O, but got Unknown
		//IL_1b71: Unknown result type (might be due to invalid IL or missing references)
		//IL_1b7b: Expected O, but got Unknown
		//IL_1c0c: Unknown result type (might be due to invalid IL or missing references)
		//IL_1c16: Expected O, but got Unknown
		//IL_1caa: Unknown result type (might be due to invalid IL or missing references)
		//IL_1cb4: Expected O, but got Unknown
		//IL_1d20: Unknown result type (might be due to invalid IL or missing references)
		//IL_1d2a: Expected O, but got Unknown
		//IL_1d37: Unknown result type (might be due to invalid IL or missing references)
		//IL_1d41: Expected O, but got Unknown
		//IL_1d4e: Unknown result type (might be due to invalid IL or missing references)
		//IL_1d58: Expected O, but got Unknown
		//IL_1d76: Unknown result type (might be due to invalid IL or missing references)
		//IL_1d80: Expected O, but got Unknown
		//IL_1ecc: Unknown result type (might be due to invalid IL or missing references)
		//IL_1ed6: Expected O, but got Unknown
		//IL_1f6a: Unknown result type (might be due to invalid IL or missing references)
		//IL_1f74: Expected O, but got Unknown
		//IL_1fff: Unknown result type (might be due to invalid IL or missing references)
		//IL_2009: Expected O, but got Unknown
		//IL_20ac: Unknown result type (might be due to invalid IL or missing references)
		//IL_20b6: Expected O, but got Unknown
		//IL_2121: Unknown result type (might be due to invalid IL or missing references)
		//IL_212b: Expected O, but got Unknown
		//IL_2138: Unknown result type (might be due to invalid IL or missing references)
		//IL_2142: Expected O, but got Unknown
		//IL_214f: Unknown result type (might be due to invalid IL or missing references)
		//IL_2159: Expected O, but got Unknown
		//IL_2177: Unknown result type (might be due to invalid IL or missing references)
		//IL_2181: Expected O, but got Unknown
		//IL_2345: Unknown result type (might be due to invalid IL or missing references)
		//IL_234f: Expected O, but got Unknown
		components = new Container();
		ComponentResourceManager componentResourceManager = new ComponentResourceManager(typeof(OpsArtDownloadNew));
		GB_Disc = new GroupBox();
		Disc_Browse = new Button();
		Disc_Delete = new Button();
		Disc_Database = new Button();
		Img_Disc = new PictureBoxWithABigFuckingX();
		GB_Cover = new GroupBox();
		Cover_Browse = new Button();
		Cover_Database = new Button();
		Cover_Delete = new Button();
		Img_Cover = new PictureBoxWithABigFuckingX();
		GB_BackCover = new GroupBox();
		CoverBack_Browse = new Button();
		CoverBack_Database = new Button();
		CoverBack_Delete = new Button();
		Img_CoverBack = new PictureBoxWithABigFuckingX();
		GB_Screens = new GroupBox();
		Screen2_Delete = new Button();
		Screen1_Delete = new Button();
		Screen2_Database = new Button();
		Screen1_Database = new Button();
		Screen2_Browse = new Button();
		Img_Screen2 = new PictureBoxWithABigFuckingX();
		Screen1__Browse = new Button();
		Img_Screen1 = new PictureBoxWithABigFuckingX();
		GB_Bg = new GroupBox();
		Background_Delete = new Button();
		Background_Database = new Button();
		Img_Background = new PictureBoxWithABigFuckingX();
		Background_Browse = new Button();
		B_Next = new Button();
		B_Previous = new Button();
		OpenFileDialog1 = new OpenFileDialog();
		L_Status = new Label();
		L_Index = new Label();
		B_Help = new Button();
		ToolTipMain = new ToolTip(components);
		LinkLabelGoogle = new LinkLabel();
		GB_Spine = new GroupBox();
		Spine_Browse = new Button();
		Spine_Database = new Button();
		Spine_Delete = new Button();
		Img_Spine = new PictureBoxWithABigFuckingX();
		CB_DontShare = new CheckBox();
		GB_Logo = new GroupBox();
		Logo_Browse = new Button();
		Logo_Delete = new Button();
		Logo_Database = new Button();
		Img_Logo = new PictureBoxWithABigFuckingX();
		L_WarnPNG = new Label();
		((Control)GB_Disc).SuspendLayout();
		((ISupportInitialize)Img_Disc).BeginInit();
		((Control)GB_Cover).SuspendLayout();
		((ISupportInitialize)Img_Cover).BeginInit();
		((Control)GB_BackCover).SuspendLayout();
		((ISupportInitialize)Img_CoverBack).BeginInit();
		((Control)GB_Screens).SuspendLayout();
		((ISupportInitialize)Img_Screen2).BeginInit();
		((ISupportInitialize)Img_Screen1).BeginInit();
		((Control)GB_Bg).SuspendLayout();
		((ISupportInitialize)Img_Background).BeginInit();
		((Control)GB_Spine).SuspendLayout();
		((ISupportInitialize)Img_Spine).BeginInit();
		((Control)GB_Logo).SuspendLayout();
		((ISupportInitialize)Img_Logo).BeginInit();
		((Control)this).SuspendLayout();
		((Control)GB_Disc).Controls.Add((Control)(object)Disc_Browse);
		((Control)GB_Disc).Controls.Add((Control)(object)Disc_Delete);
		((Control)GB_Disc).Controls.Add((Control)(object)Disc_Database);
		((Control)GB_Disc).Controls.Add((Control)(object)Img_Disc);
		((Control)GB_Disc).Location = new Point(436, 280);
		((Control)GB_Disc).Name = "GB_Disc";
		((Control)GB_Disc).Size = new Size(127, 135);
		((Control)GB_Disc).TabIndex = 10;
		GB_Disc.TabStop = false;
		((Control)GB_Disc).Text = "Disc  (64x64)";
		((Control)Disc_Browse).BackgroundImage = (Image)componentResourceManager.GetObject("Disc_Browse.BackgroundImage");
		((Control)Disc_Browse).BackgroundImageLayout = (ImageLayout)4;
		((Control)Disc_Browse).Location = new Point(8, 91);
		((Control)Disc_Browse).Name = "Disc_Browse";
		((Control)Disc_Browse).RightToLeft = (RightToLeft)0;
		((Control)Disc_Browse).Size = new Size(32, 32);
		((Control)Disc_Browse).TabIndex = 21;
		((ButtonBase)Disc_Browse).UseVisualStyleBackColor = true;
		((Control)Disc_Browse).Click += Disc_Browse_Click;
		((Control)Disc_Delete).BackgroundImage = (Image)componentResourceManager.GetObject("Disc_Delete.BackgroundImage");
		((Control)Disc_Delete).BackgroundImageLayout = (ImageLayout)4;
		((Control)Disc_Delete).Location = new Point(84, 91);
		((Control)Disc_Delete).Name = "Disc_Delete";
		((Control)Disc_Delete).Size = new Size(32, 32);
		((Control)Disc_Delete).TabIndex = 20;
		((ButtonBase)Disc_Delete).UseVisualStyleBackColor = true;
		((Control)Disc_Delete).Click += Cover_Delete_Click;
		((Control)Disc_Database).BackgroundImage = (Image)componentResourceManager.GetObject("Disc_Database.BackgroundImage");
		((Control)Disc_Database).BackgroundImageLayout = (ImageLayout)4;
		((Control)Disc_Database).Location = new Point(46, 91);
		((Control)Disc_Database).Name = "Disc_Database";
		((Control)Disc_Database).Size = new Size(32, 32);
		((Control)Disc_Database).TabIndex = 19;
		((ButtonBase)Disc_Database).UseVisualStyleBackColor = true;
		((Control)Disc_Database).Click += Disc_Database_Click;
		((Control)Img_Disc).BackgroundImageLayout = (ImageLayout)3;
		Img_Disc.HasErrorBorder = false;
		((PictureBox)Img_Disc).Image = (Image)componentResourceManager.GetObject("Img_Disc.Image");
		((Control)Img_Disc).Location = new Point(36, 19);
		((Control)Img_Disc).Name = "Img_Disc";
		((Control)Img_Disc).Size = new Size(64, 64);
		((PictureBox)Img_Disc).SizeMode = (PictureBoxSizeMode)1;
		((PictureBox)Img_Disc).TabIndex = 6;
		((PictureBox)Img_Disc).TabStop = false;
		((Control)Img_Disc).DragDrop += new DragEventHandler(ART_DragDrop);
		((Control)Img_Disc).DragEnter += new DragEventHandler(ART_DragEnter);
		((Control)Img_Disc).MouseDown += new MouseEventHandler(Img_Cover_MouseDown);
		((Control)GB_Cover).Controls.Add((Control)(object)Cover_Browse);
		((Control)GB_Cover).Controls.Add((Control)(object)Cover_Database);
		((Control)GB_Cover).Controls.Add((Control)(object)Cover_Delete);
		((Control)GB_Cover).Controls.Add((Control)(object)Img_Cover);
		((Control)GB_Cover).Location = new Point(3, 6);
		((Control)GB_Cover).Name = "GB_Cover";
		((Control)GB_Cover).Size = new Size(155, 268);
		((Control)GB_Cover).TabIndex = 9;
		GB_Cover.TabStop = false;
		((Control)GB_Cover).Text = "Front Cover (140x200)";
		((Control)Cover_Browse).BackgroundImage = (Image)componentResourceManager.GetObject("Cover_Browse.BackgroundImage");
		((Control)Cover_Browse).BackgroundImageLayout = (ImageLayout)3;
		((Control)Cover_Browse).Location = new Point(6, 225);
		((Control)Cover_Browse).Name = "Cover_Browse";
		((Control)Cover_Browse).RightToLeft = (RightToLeft)0;
		((Control)Cover_Browse).Size = new Size(32, 32);
		((Control)Cover_Browse).TabIndex = 19;
		((ButtonBase)Cover_Browse).UseVisualStyleBackColor = true;
		((Control)Cover_Browse).Click += Disc_Browse_Click;
		((Control)Cover_Database).BackgroundImage = (Image)componentResourceManager.GetObject("Cover_Database.BackgroundImage");
		((Control)Cover_Database).BackgroundImageLayout = (ImageLayout)3;
		((Control)Cover_Database).Location = new Point(59, 225);
		((Control)Cover_Database).Name = "Cover_Database";
		((Control)Cover_Database).RightToLeft = (RightToLeft)0;
		((Control)Cover_Database).Size = new Size(32, 32);
		((Control)Cover_Database).TabIndex = 18;
		((ButtonBase)Cover_Database).UseVisualStyleBackColor = true;
		((Control)Cover_Database).Click += Cover_Database_Click;
		((Control)Cover_Delete).BackgroundImage = (Image)componentResourceManager.GetObject("Cover_Delete.BackgroundImage");
		((Control)Cover_Delete).BackgroundImageLayout = (ImageLayout)4;
		((Control)Cover_Delete).Location = new Point(114, 225);
		((Control)Cover_Delete).Name = "Cover_Delete";
		((Control)Cover_Delete).Size = new Size(32, 32);
		((Control)Cover_Delete).TabIndex = 17;
		((ButtonBase)Cover_Delete).UseVisualStyleBackColor = true;
		((Control)Cover_Delete).Click += Cover_Delete_Click;
		Img_Cover.HasErrorBorder = false;
		((PictureBox)Img_Cover).Image = (Image)componentResourceManager.GetObject("Img_Cover.Image");
		((Control)Img_Cover).Location = new Point(6, 19);
		((Control)Img_Cover).Name = "Img_Cover";
		((Control)Img_Cover).Size = new Size(140, 200);
		((PictureBox)Img_Cover).SizeMode = (PictureBoxSizeMode)4;
		((PictureBox)Img_Cover).TabIndex = 3;
		((PictureBox)Img_Cover).TabStop = false;
		((Control)Img_Cover).DragDrop += new DragEventHandler(ART_DragDrop);
		((Control)Img_Cover).DragEnter += new DragEventHandler(ART_DragEnter);
		((Control)Img_Cover).MouseDown += new MouseEventHandler(Img_Cover_MouseDown);
		((Control)GB_BackCover).Controls.Add((Control)(object)CoverBack_Browse);
		((Control)GB_BackCover).Controls.Add((Control)(object)CoverBack_Database);
		((Control)GB_BackCover).Controls.Add((Control)(object)CoverBack_Delete);
		((Control)GB_BackCover).Controls.Add((Control)(object)Img_CoverBack);
		((Control)GB_BackCover).Location = new Point(278, 6);
		((Control)GB_BackCover).Name = "GB_BackCover";
		((Control)GB_BackCover).Size = new Size(152, 268);
		((Control)GB_BackCover).TabIndex = 18;
		GB_BackCover.TabStop = false;
		((Control)GB_BackCover).Text = "Back Cover (242x344)";
		((Control)CoverBack_Browse).BackgroundImage = (Image)componentResourceManager.GetObject("CoverBack_Browse.BackgroundImage");
		((Control)CoverBack_Browse).BackgroundImageLayout = (ImageLayout)4;
		((Control)CoverBack_Browse).Location = new Point(6, 227);
		((Control)CoverBack_Browse).Name = "CoverBack_Browse";
		((Control)CoverBack_Browse).RightToLeft = (RightToLeft)0;
		((Control)CoverBack_Browse).Size = new Size(32, 32);
		((Control)CoverBack_Browse).TabIndex = 20;
		((ButtonBase)CoverBack_Browse).UseVisualStyleBackColor = true;
		((Control)CoverBack_Browse).Click += Disc_Browse_Click;
		((Control)CoverBack_Database).BackgroundImage = (Image)componentResourceManager.GetObject("CoverBack_Database.BackgroundImage");
		((Control)CoverBack_Database).BackgroundImageLayout = (ImageLayout)4;
		((Control)CoverBack_Database).Location = new Point(58, 227);
		((Control)CoverBack_Database).Name = "CoverBack_Database";
		((Control)CoverBack_Database).RightToLeft = (RightToLeft)0;
		((Control)CoverBack_Database).Size = new Size(32, 32);
		((Control)CoverBack_Database).TabIndex = 19;
		((ButtonBase)CoverBack_Database).UseVisualStyleBackColor = true;
		((Control)CoverBack_Database).Click += CoverBack_Database_Click;
		((Control)CoverBack_Delete).BackgroundImage = (Image)componentResourceManager.GetObject("CoverBack_Delete.BackgroundImage");
		((Control)CoverBack_Delete).BackgroundImageLayout = (ImageLayout)4;
		((Control)CoverBack_Delete).Location = new Point(114, 227);
		((Control)CoverBack_Delete).Name = "CoverBack_Delete";
		((Control)CoverBack_Delete).Size = new Size(32, 32);
		((Control)CoverBack_Delete).TabIndex = 19;
		((ButtonBase)CoverBack_Delete).UseVisualStyleBackColor = true;
		((Control)CoverBack_Delete).Click += Cover_Delete_Click;
		Img_CoverBack.HasErrorBorder = false;
		((PictureBox)Img_CoverBack).Image = (Image)componentResourceManager.GetObject("Img_CoverBack.Image");
		((Control)Img_CoverBack).Location = new Point(6, 19);
		((Control)Img_CoverBack).Name = "Img_CoverBack";
		((Control)Img_CoverBack).Size = new Size(140, 200);
		((PictureBox)Img_CoverBack).SizeMode = (PictureBoxSizeMode)4;
		((PictureBox)Img_CoverBack).TabIndex = 3;
		((PictureBox)Img_CoverBack).TabStop = false;
		((Control)Img_CoverBack).DragDrop += new DragEventHandler(ART_DragDrop);
		((Control)Img_CoverBack).DragEnter += new DragEventHandler(ART_DragEnter);
		((Control)Img_CoverBack).MouseDown += new MouseEventHandler(Img_Cover_MouseDown);
		((Control)GB_Screens).Controls.Add((Control)(object)Screen2_Delete);
		((Control)GB_Screens).Controls.Add((Control)(object)Screen1_Delete);
		((Control)GB_Screens).Controls.Add((Control)(object)Screen2_Database);
		((Control)GB_Screens).Controls.Add((Control)(object)Screen1_Database);
		((Control)GB_Screens).Controls.Add((Control)(object)Screen2_Browse);
		((Control)GB_Screens).Controls.Add((Control)(object)Img_Screen2);
		((Control)GB_Screens).Controls.Add((Control)(object)Screen1__Browse);
		((Control)GB_Screens).Controls.Add((Control)(object)Img_Screen1);
		((Control)GB_Screens).Location = new Point(436, 6);
		((Control)GB_Screens).Name = "GB_Screens";
		((Control)GB_Screens).Size = new Size(522, 268);
		((Control)GB_Screens).TabIndex = 19;
		GB_Screens.TabStop = false;
		((Control)GB_Screens).Text = "Screenshots  (250x188)";
		((Control)Screen2_Delete).BackgroundImage = (Image)componentResourceManager.GetObject("Screen2_Delete.BackgroundImage");
		((Control)Screen2_Delete).BackgroundImageLayout = (ImageLayout)4;
		((Control)Screen2_Delete).Location = new Point(404, 222);
		((Control)Screen2_Delete).Name = "Screen2_Delete";
		((Control)Screen2_Delete).Size = new Size(32, 32);
		((Control)Screen2_Delete).TabIndex = 42;
		((ButtonBase)Screen2_Delete).UseVisualStyleBackColor = true;
		((Control)Screen2_Delete).Click += Cover_Delete_Click;
		((Control)Screen1_Delete).BackgroundImage = (Image)componentResourceManager.GetObject("Screen1_Delete.BackgroundImage");
		((Control)Screen1_Delete).BackgroundImageLayout = (ImageLayout)4;
		((Control)Screen1_Delete).Location = new Point(154, 222);
		((Control)Screen1_Delete).Name = "Screen1_Delete";
		((Control)Screen1_Delete).Size = new Size(32, 32);
		((Control)Screen1_Delete).TabIndex = 21;
		((ButtonBase)Screen1_Delete).UseVisualStyleBackColor = true;
		((Control)Screen1_Delete).Click += Cover_Delete_Click;
		((Control)Screen2_Database).BackgroundImage = (Image)componentResourceManager.GetObject("Screen2_Database.BackgroundImage");
		((Control)Screen2_Database).BackgroundImageLayout = (ImageLayout)4;
		((Control)Screen2_Database).Location = new Point(366, 222);
		((Control)Screen2_Database).Name = "Screen2_Database";
		((Control)Screen2_Database).Size = new Size(32, 32);
		((Control)Screen2_Database).TabIndex = 4;
		((ButtonBase)Screen2_Database).UseVisualStyleBackColor = true;
		((Control)Screen2_Database).Click += B_SCR_1_2_DB_Click;
		((Control)Screen1_Database).BackgroundImage = (Image)componentResourceManager.GetObject("Screen1_Database.BackgroundImage");
		((Control)Screen1_Database).BackgroundImageLayout = (ImageLayout)4;
		((Control)Screen1_Database).Location = new Point(116, 222);
		((Control)Screen1_Database).Name = "Screen1_Database";
		((Control)Screen1_Database).Size = new Size(32, 32);
		((Control)Screen1_Database).TabIndex = 1;
		((ButtonBase)Screen1_Database).UseVisualStyleBackColor = true;
		((Control)Screen1_Database).Click += B_SCR_1_2_DB_Click;
		((Control)Screen2_Browse).BackgroundImage = (Image)componentResourceManager.GetObject("Screen2_Browse.BackgroundImage");
		((Control)Screen2_Browse).BackgroundImageLayout = (ImageLayout)4;
		((Control)Screen2_Browse).Location = new Point(328, 222);
		((Control)Screen2_Browse).Name = "Screen2_Browse";
		((Control)Screen2_Browse).Size = new Size(32, 32);
		((Control)Screen2_Browse).TabIndex = 3;
		((ButtonBase)Screen2_Browse).UseVisualStyleBackColor = true;
		((Control)Screen2_Browse).Click += Disc_Browse_Click;
		Img_Screen2.HasErrorBorder = false;
		((PictureBox)Img_Screen2).Image = (Image)componentResourceManager.GetObject("Img_Screen2.Image");
		((PictureBox)Img_Screen2).InitialImage = (Image)componentResourceManager.GetObject("Img_Screen2.InitialImage");
		((Control)Img_Screen2).Location = new Point(262, 28);
		((Control)Img_Screen2).Name = "Img_Screen2";
		((Control)Img_Screen2).Size = new Size(250, 188);
		((PictureBox)Img_Screen2).SizeMode = (PictureBoxSizeMode)1;
		((PictureBox)Img_Screen2).TabIndex = 41;
		((PictureBox)Img_Screen2).TabStop = false;
		((Control)Img_Screen2).DragDrop += new DragEventHandler(ART_DragDrop);
		((Control)Img_Screen2).DragEnter += new DragEventHandler(ART_DragEnter);
		((Control)Img_Screen2).MouseDown += new MouseEventHandler(Img_Cover_MouseDown);
		((Control)Screen1__Browse).BackgroundImage = (Image)componentResourceManager.GetObject("Screen1__Browse.BackgroundImage");
		((Control)Screen1__Browse).BackgroundImageLayout = (ImageLayout)4;
		((Control)Screen1__Browse).Location = new Point(78, 222);
		((Control)Screen1__Browse).Name = "Screen1__Browse";
		((Control)Screen1__Browse).Size = new Size(32, 32);
		((Control)Screen1__Browse).TabIndex = 0;
		((ButtonBase)Screen1__Browse).UseVisualStyleBackColor = true;
		((Control)Screen1__Browse).Click += Disc_Browse_Click;
		Img_Screen1.HasErrorBorder = false;
		((PictureBox)Img_Screen1).Image = (Image)componentResourceManager.GetObject("Img_Screen1.Image");
		((PictureBox)Img_Screen1).InitialImage = (Image)componentResourceManager.GetObject("Img_Screen1.InitialImage");
		((Control)Img_Screen1).Location = new Point(6, 28);
		((Control)Img_Screen1).Name = "Img_Screen1";
		((Control)Img_Screen1).Size = new Size(250, 188);
		((PictureBox)Img_Screen1).SizeMode = (PictureBoxSizeMode)1;
		((PictureBox)Img_Screen1).TabIndex = 40;
		((PictureBox)Img_Screen1).TabStop = false;
		((Control)Img_Screen1).DragDrop += new DragEventHandler(ART_DragDrop);
		((Control)Img_Screen1).DragEnter += new DragEventHandler(ART_DragEnter);
		((Control)Img_Screen1).MouseDown += new MouseEventHandler(Img_Cover_MouseDown);
		((Control)GB_Bg).Controls.Add((Control)(object)Background_Delete);
		((Control)GB_Bg).Controls.Add((Control)(object)Background_Database);
		((Control)GB_Bg).Controls.Add((Control)(object)Img_Background);
		((Control)GB_Bg).Controls.Add((Control)(object)Background_Browse);
		((Control)GB_Bg).Location = new Point(3, 282);
		((Control)GB_Bg).Name = "GB_Bg";
		((Control)GB_Bg).Size = new Size(335, 302);
		((Control)GB_Bg).TabIndex = 20;
		GB_Bg.TabStop = false;
		((Control)GB_Bg).Text = "Background (640x480)";
		((Control)Background_Delete).BackgroundImage = (Image)componentResourceManager.GetObject("Background_Delete.BackgroundImage");
		((Control)Background_Delete).BackgroundImageLayout = (ImageLayout)4;
		((Control)Background_Delete).Location = new Point(203, 262);
		((Control)Background_Delete).Name = "Background_Delete";
		((Control)Background_Delete).Size = new Size(32, 32);
		((Control)Background_Delete).TabIndex = 22;
		((ButtonBase)Background_Delete).UseVisualStyleBackColor = true;
		((Control)Background_Delete).Click += Cover_Delete_Click;
		((Control)Background_Database).BackgroundImage = (Image)componentResourceManager.GetObject("Background_Database.BackgroundImage");
		((Control)Background_Database).BackgroundImageLayout = (ImageLayout)4;
		((Control)Background_Database).Location = new Point(165, 262);
		((Control)Background_Database).Name = "Background_Database";
		((Control)Background_Database).Size = new Size(32, 32);
		((Control)Background_Database).TabIndex = 43;
		((ButtonBase)Background_Database).UseVisualStyleBackColor = true;
		((Control)Background_Database).Click += Background_Database_Click;
		Img_Background.HasErrorBorder = false;
		((PictureBox)Img_Background).Image = (Image)componentResourceManager.GetObject("Img_Background.Image");
		((Control)Img_Background).Location = new Point(6, 19);
		((Control)Img_Background).Name = "Img_Background";
		((Control)Img_Background).Size = new Size(320, 240);
		((PictureBox)Img_Background).SizeMode = (PictureBoxSizeMode)1;
		((PictureBox)Img_Background).TabIndex = 0;
		((PictureBox)Img_Background).TabStop = false;
		((Control)Img_Background).DragDrop += new DragEventHandler(ART_DragDrop);
		((Control)Img_Background).DragEnter += new DragEventHandler(ART_DragEnter);
		((Control)Img_Background).MouseDown += new MouseEventHandler(Img_Cover_MouseDown);
		((Control)Background_Browse).BackgroundImage = (Image)componentResourceManager.GetObject("Background_Browse.BackgroundImage");
		((Control)Background_Browse).BackgroundImageLayout = (ImageLayout)4;
		((Control)Background_Browse).Location = new Point(127, 262);
		((Control)Background_Browse).Name = "Background_Browse";
		((Control)Background_Browse).Size = new Size(32, 32);
		((Control)Background_Browse).TabIndex = 42;
		((ButtonBase)Background_Browse).UseVisualStyleBackColor = true;
		((Control)Background_Browse).Click += Disc_Browse_Click;
		((Control)B_Next).BackgroundImage = (Image)componentResourceManager.GetObject("B_Next.BackgroundImage");
		((Control)B_Next).BackgroundImageLayout = (ImageLayout)3;
		((Control)B_Next).Location = new Point(482, 443);
		((Control)B_Next).Name = "B_Next";
		((Control)B_Next).Size = new Size(48, 48);
		((Control)B_Next).TabIndex = 24;
		((ButtonBase)B_Next).UseVisualStyleBackColor = true;
		((Control)B_Next).Click += B_Next_Click;
		((Control)B_Previous).BackgroundImage = (Image)componentResourceManager.GetObject("B_Previous.BackgroundImage");
		((Control)B_Previous).BackgroundImageLayout = (ImageLayout)3;
		((Control)B_Previous).Location = new Point(419, 443);
		((Control)B_Previous).Name = "B_Previous";
		((Control)B_Previous).Size = new Size(48, 48);
		((Control)B_Previous).TabIndex = 23;
		((ButtonBase)B_Previous).UseVisualStyleBackColor = true;
		((Control)B_Previous).Click += B_Previous_Click;
		((FileDialog)OpenFileDialog1).Filter = "Images|*.png;";
		((Control)L_Status).Font = new Font("Microsoft Sans Serif", 8f, (FontStyle)1, (GraphicsUnit)3);
		((Control)L_Status).Location = new Point(416, 494);
		((Control)L_Status).Name = "L_Status";
		((Control)L_Status).Size = new Size(169, 55);
		((Control)L_Status).TabIndex = 25;
		((Control)L_Status).Text = "-";
		L_Status.TextAlign = (ContentAlignment)32;
		((Control)L_Index).Font = new Font("Microsoft Sans Serif", 12f, (FontStyle)1, (GraphicsUnit)3);
		((Control)L_Index).Location = new Point(419, 418);
		((Control)L_Index).Name = "L_Index";
		((Control)L_Index).Size = new Size(166, 22);
		((Control)L_Index).TabIndex = 26;
		((Control)L_Index).Text = "0 / 0";
		L_Index.TextAlign = (ContentAlignment)32;
		((Control)B_Help).BackgroundImage = (Image)componentResourceManager.GetObject("B_Help.BackgroundImage");
		((Control)B_Help).BackgroundImageLayout = (ImageLayout)3;
		((Control)B_Help).Location = new Point(537, 443);
		((Control)B_Help).Name = "B_Help";
		((Control)B_Help).Size = new Size(48, 48);
		((Control)B_Help).TabIndex = 27;
		((ButtonBase)B_Help).UseVisualStyleBackColor = true;
		((Control)B_Help).Click += B_Help_Click;
		ToolTipMain.ToolTipIcon = (ToolTipIcon)1;
		((Control)LinkLabelGoogle).Location = new Point(6, 598);
		((Control)LinkLabelGoogle).Name = "LinkLabelGoogle";
		((Control)LinkLabelGoogle).Size = new Size(166, 43);
		((Control)LinkLabelGoogle).TabIndex = 28;
		LinkLabelGoogle.TabStop = true;
		((Control)LinkLabelGoogle).Text = "Search Google Images";
		((Label)LinkLabelGoogle).TextAlign = (ContentAlignment)32;
		LinkLabelGoogle.LinkClicked += new LinkLabelLinkClickedEventHandler(LinkLabelGoogle_LinkClicked);
		((Control)GB_Spine).Controls.Add((Control)(object)Spine_Browse);
		((Control)GB_Spine).Controls.Add((Control)(object)Spine_Database);
		((Control)GB_Spine).Controls.Add((Control)(object)Spine_Delete);
		((Control)GB_Spine).Controls.Add((Control)(object)Img_Spine);
		((Control)GB_Spine).Location = new Point(164, 6);
		((Control)GB_Spine).Name = "GB_Spine";
		((Control)GB_Spine).Size = new Size(108, 268);
		((Control)GB_Spine).TabIndex = 21;
		GB_Spine.TabStop = false;
		((Control)GB_Spine).Text = "Spine (18x240)";
		((Control)Spine_Browse).BackgroundImage = (Image)componentResourceManager.GetObject("Spine_Browse.BackgroundImage");
		((Control)Spine_Browse).BackgroundImageLayout = (ImageLayout)4;
		((Control)Spine_Browse).Location = new Point(59, 68);
		((Control)Spine_Browse).Name = "Spine_Browse";
		((Control)Spine_Browse).RightToLeft = (RightToLeft)0;
		((Control)Spine_Browse).Size = new Size(32, 32);
		((Control)Spine_Browse).TabIndex = 20;
		((ButtonBase)Spine_Browse).UseVisualStyleBackColor = true;
		((Control)Spine_Browse).Click += Disc_Browse_Click;
		((Control)Spine_Database).BackgroundImage = (Image)componentResourceManager.GetObject("Spine_Database.BackgroundImage");
		((Control)Spine_Database).BackgroundImageLayout = (ImageLayout)4;
		((Control)Spine_Database).Location = new Point(59, 114);
		((Control)Spine_Database).Name = "Spine_Database";
		((Control)Spine_Database).RightToLeft = (RightToLeft)0;
		((Control)Spine_Database).Size = new Size(32, 32);
		((Control)Spine_Database).TabIndex = 19;
		((ButtonBase)Spine_Database).UseVisualStyleBackColor = true;
		((Control)Spine_Database).Click += Spine_Database_Click;
		((Control)Spine_Delete).BackgroundImage = (Image)componentResourceManager.GetObject("Spine_Delete.BackgroundImage");
		((Control)Spine_Delete).BackgroundImageLayout = (ImageLayout)4;
		((Control)Spine_Delete).Location = new Point(59, 160);
		((Control)Spine_Delete).Name = "Spine_Delete";
		((Control)Spine_Delete).Size = new Size(32, 32);
		((Control)Spine_Delete).TabIndex = 19;
		((ButtonBase)Spine_Delete).UseVisualStyleBackColor = true;
		((Control)Spine_Delete).Click += Cover_Delete_Click;
		Img_Spine.HasErrorBorder = false;
		((PictureBox)Img_Spine).Image = (Image)componentResourceManager.GetObject("Img_Spine.Image");
		((Control)Img_Spine).Location = new Point(21, 19);
		((Control)Img_Spine).Name = "Img_Spine";
		((Control)Img_Spine).Size = new Size(18, 240);
		((PictureBox)Img_Spine).SizeMode = (PictureBoxSizeMode)4;
		((PictureBox)Img_Spine).TabIndex = 3;
		((PictureBox)Img_Spine).TabStop = false;
		((Control)Img_Spine).DragDrop += new DragEventHandler(ART_DragDrop);
		((Control)Img_Spine).DragEnter += new DragEventHandler(ART_DragEnter);
		((Control)Img_Spine).MouseDown += new MouseEventHandler(Img_Cover_MouseDown);
		CB_DontShare.CheckAlign = (ContentAlignment)2;
		((Control)CB_DontShare).Font = new Font("Microsoft Sans Serif", 10f, (FontStyle)1, (GraphicsUnit)3);
		((Control)CB_DontShare).Location = new Point(641, 489);
		((Control)CB_DontShare).Name = "CB_DontShare";
		((Control)CB_DontShare).Size = new Size(306, 84);
		((Control)CB_DontShare).TabIndex = 29;
		((Control)CB_DontShare).Text = "Check Here if you don't want to share this game's ART to the OPLM server. ";
		((ButtonBase)CB_DontShare).TextAlign = (ContentAlignment)2;
		((ButtonBase)CB_DontShare).UseVisualStyleBackColor = true;
		((Control)GB_Logo).Controls.Add((Control)(object)Logo_Browse);
		((Control)GB_Logo).Controls.Add((Control)(object)Logo_Delete);
		((Control)GB_Logo).Controls.Add((Control)(object)Logo_Database);
		((Control)GB_Logo).Controls.Add((Control)(object)Img_Logo);
		((Control)GB_Logo).Location = new Point(644, 280);
		((Control)GB_Logo).Name = "GB_Logo";
		((Control)GB_Logo).Size = new Size(314, 193);
		((Control)GB_Logo).TabIndex = 22;
		GB_Logo.TabStop = false;
		((Control)GB_Logo).Text = "Logo (300x125)";
		((Control)Logo_Browse).BackgroundImage = (Image)componentResourceManager.GetObject("Logo_Browse.BackgroundImage");
		((Control)Logo_Browse).BackgroundImageLayout = (ImageLayout)4;
		((Control)Logo_Browse).Location = new Point(105, 150);
		((Control)Logo_Browse).Name = "Logo_Browse";
		((Control)Logo_Browse).RightToLeft = (RightToLeft)0;
		((Control)Logo_Browse).Size = new Size(32, 32);
		((Control)Logo_Browse).TabIndex = 21;
		((ButtonBase)Logo_Browse).UseVisualStyleBackColor = true;
		((Control)Logo_Browse).Click += Disc_Browse_Click;
		((Control)Logo_Delete).BackgroundImage = (Image)componentResourceManager.GetObject("Logo_Delete.BackgroundImage");
		((Control)Logo_Delete).BackgroundImageLayout = (ImageLayout)4;
		((Control)Logo_Delete).Location = new Point(181, 150);
		((Control)Logo_Delete).Name = "Logo_Delete";
		((Control)Logo_Delete).Size = new Size(32, 32);
		((Control)Logo_Delete).TabIndex = 20;
		((ButtonBase)Logo_Delete).UseVisualStyleBackColor = true;
		((Control)Logo_Delete).Click += Cover_Delete_Click;
		((Control)Logo_Database).BackgroundImage = (Image)componentResourceManager.GetObject("Logo_Database.BackgroundImage");
		((Control)Logo_Database).BackgroundImageLayout = (ImageLayout)4;
		((Control)Logo_Database).Location = new Point(143, 150);
		((Control)Logo_Database).Name = "Logo_Database";
		((Control)Logo_Database).Size = new Size(32, 32);
		((Control)Logo_Database).TabIndex = 19;
		((ButtonBase)Logo_Database).UseVisualStyleBackColor = true;
		((Control)Logo_Database).Click += Logo_Database_Click;
		((Control)Img_Logo).BackgroundImageLayout = (ImageLayout)3;
		Img_Logo.HasErrorBorder = false;
		((PictureBox)Img_Logo).Image = (Image)componentResourceManager.GetObject("Img_Logo.Image");
		((Control)Img_Logo).Location = new Point(6, 19);
		((Control)Img_Logo).Name = "Img_Logo";
		((Control)Img_Logo).Size = new Size(300, 125);
		((PictureBox)Img_Logo).SizeMode = (PictureBoxSizeMode)1;
		((PictureBox)Img_Logo).TabIndex = 6;
		((PictureBox)Img_Logo).TabStop = false;
		((Control)Img_Logo).DragDrop += new DragEventHandler(ART_DragDrop);
		((Control)Img_Logo).DragEnter += new DragEventHandler(ART_DragEnter);
		((Control)Img_Logo).MouseDown += new MouseEventHandler(Img_Cover_MouseDown);
		((Control)L_WarnPNG).CausesValidation = false;
		((Control)L_WarnPNG).Font = new Font("Microsoft Sans Serif", 12f, (FontStyle)0, (GraphicsUnit)3);
		((Control)L_WarnPNG).ForeColor = Color.Red;
		((Control)L_WarnPNG).Location = new Point(346, 576);
		((Control)L_WarnPNG).Name = "L_WarnPNG";
		((Control)L_WarnPNG).Size = new Size(612, 65);
		((Control)L_WarnPNG).TabIndex = 30;
		((Control)L_WarnPNG).Text = "Your ART is not optimized for OPL performance, Please Update Your Art Files";
		L_WarnPNG.TextAlign = (ContentAlignment)32;
		((Control)L_WarnPNG).Visible = false;
		((ContainerControl)this).AutoScaleDimensions = new SizeF(6f, 13f);
		((ContainerControl)this).AutoScaleMode = (AutoScaleMode)1;
		((Form)this).ClientSize = new Size(969, 661);
		((Control)this).Controls.Add((Control)(object)L_WarnPNG);
		((Control)this).Controls.Add((Control)(object)GB_Logo);
		((Control)this).Controls.Add((Control)(object)CB_DontShare);
		((Control)this).Controls.Add((Control)(object)GB_Spine);
		((Control)this).Controls.Add((Control)(object)LinkLabelGoogle);
		((Control)this).Controls.Add((Control)(object)B_Help);
		((Control)this).Controls.Add((Control)(object)L_Index);
		((Control)this).Controls.Add((Control)(object)L_Status);
		((Control)this).Controls.Add((Control)(object)B_Next);
		((Control)this).Controls.Add((Control)(object)B_Previous);
		((Control)this).Controls.Add((Control)(object)GB_Bg);
		((Control)this).Controls.Add((Control)(object)GB_Screens);
		((Control)this).Controls.Add((Control)(object)GB_BackCover);
		((Control)this).Controls.Add((Control)(object)GB_Disc);
		((Control)this).Controls.Add((Control)(object)GB_Cover);
		((Control)this).Font = new Font("Microsoft Sans Serif", 8f, (FontStyle)0, (GraphicsUnit)3);
		((Form)this).KeyPreview = true;
		((Form)this).MaximizeBox = false;
		((Control)this).MaximumSize = new Size(985, 700);
		((Control)this).MinimumSize = new Size(985, 700);
		((Control)this).Name = "OpsArtDownloadNew";
		((Form)this).StartPosition = (FormStartPosition)4;
		((Form)this).Shown += OpsArtDownloadNew_Shown;
		((Control)GB_Disc).ResumeLayout(false);
		((ISupportInitialize)Img_Disc).EndInit();
		((Control)GB_Cover).ResumeLayout(false);
		((ISupportInitialize)Img_Cover).EndInit();
		((Control)GB_BackCover).ResumeLayout(false);
		((ISupportInitialize)Img_CoverBack).EndInit();
		((Control)GB_Screens).ResumeLayout(false);
		((ISupportInitialize)Img_Screen2).EndInit();
		((ISupportInitialize)Img_Screen1).EndInit();
		((Control)GB_Bg).ResumeLayout(false);
		((ISupportInitialize)Img_Background).EndInit();
		((Control)GB_Spine).ResumeLayout(false);
		((ISupportInitialize)Img_Spine).EndInit();
		((Control)GB_Logo).ResumeLayout(false);
		((ISupportInitialize)Img_Logo).EndInit();
		((Control)this).ResumeLayout(false);
	}
}
