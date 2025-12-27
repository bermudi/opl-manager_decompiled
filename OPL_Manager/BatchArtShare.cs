using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.IO.Compression;
using System.Windows.Forms;
using OPL_Manager.My.Resources;
using OplManagerService;

namespace OPL_Manager;

public class BatchArtShare : BaseForm
{
	private readonly string UserID;

	private IContainer components;

	internal Button B_start;

	internal TextBox TB_Share;

	internal ProgressBar ProgressBarStatus;

	internal Label L_StatusArtShareCount;

	internal Label L_Status;

	internal FolderBrowserDialog FolderBrowserDialog1;

	internal SaveFileDialog SaveFileDialog1;

	internal Label L_ART_SHARED;

	internal GroupBox GB_ShareTo;

	internal RadioButton CB_CopyFolder;

	internal RadioButton CB_OPL_DB;

	internal Button B_CopyLog;

	public BatchArtShare()
	{
		InitializeComponent();
	}

	public BatchArtShare(string UserID)
	{
		this.UserID = UserID;
		InitializeComponent();
	}

	private void BatchArtShare_Shown(object sender, EventArgs e)
	{
		((Control)this).Text = Translated.BatchArtShare_Title;
		((Control)L_ART_SHARED).Text = Translated.BatchArtShare_SharedArts;
		((Control)L_Status).Text = Translated.Global_String_Progress + " 000% (0 / 0)";
		((Control)L_StatusArtShareCount).Text = Translated.BatchArtShare_Uploaded + " 0";
		((Control)GB_ShareTo).Text = Translated.BatchArtShare_ShareTo;
		((Control)CB_CopyFolder).Text = Translated.BatchArtShare_ShareToFolder;
		((Control)B_start).Text = Translated.GLOBAL_BUTTON_START;
		((Control)B_CopyLog).Text = Translated.Global_CopyLog;
		CB_CopyFolder.Checked = OplmSettings.Read("BATCH_ART_SHARE_FOLDER", predef: false);
		CB_OPL_DB.Checked = OplmSettings.Read("BATCH_ART_SHARE_OPLMANAGER", predef: false);
		updateStartButton();
		CB_OPL_DB.CheckedChanged += CB_DestinationChanged;
		CB_CopyFolder.CheckedChanged += CB_DestinationChanged;
	}

	private void B_start_Click(object sender, EventArgs e)
	{
		//IL_003d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0043: Invalid comparison between Unknown and I4
		//IL_0052: Unknown result type (might be due to invalid IL or missing references)
		//IL_0080: Unknown result type (might be due to invalid IL or missing references)
		((Control)B_start).Enabled = false;
		((Control)CB_CopyFolder).Enabled = false;
		((Control)CB_OPL_DB).Enabled = false;
		string text = "";
		if (CB_CopyFolder.Checked)
		{
			if ((int)((CommonDialog)FolderBrowserDialog1).ShowDialog() != 1)
			{
				MessageBox.Show(Translated.BatchArtShare_MsgFolderNotSelected, Translated.Global_Error, (MessageBoxButtons)0, (MessageBoxIcon)16);
				updateStartButton();
				return;
			}
			text = FolderBrowserDialog1.SelectedPath;
			if (!Directory.Exists(text))
			{
				MessageBox.Show(Translated.BatchArtShare_MsgFolderNotExists, Translated.Global_Error, (MessageBoxButtons)0, (MessageBoxIcon)16);
				updateStartButton();
				return;
			}
		}
		List<BatchArtShareRequestClass> list = new List<BatchArtShareRequestClass>();
		foreach (GameInfo game in GameProvider.GameList)
		{
			if (OplmSettings.ignoredGameList.Contains(game.ID) | game.ID.StartsWith("FAKE"))
			{
				writeTextBoxShare(Translated.BatchArtShare_MsgIgnoringGame + game.Title + " (" + game.ID + ")");
				continue;
			}
			BatchArtShareRequestClass batchArtShareRequestClass = new BatchArtShareRequestClass
			{
				GameID = game.ID,
				GameType = game.Type
			};
			if (game.HasICO && new PNGClass(game.FileArtICO).IsPngAnd8Bit)
			{
				batchArtShareRequestClass.OrigICO = Path.GetFileName(game.FileArtICO);
				batchArtShareRequestClass.HashedICO = CommonFuncs.GetMD5(game.FileArtICO) + Path.GetExtension(game.FileArtICO);
			}
			if (game.HasCOV && new PNGClass(game.FileArtCOV).IsPngAnd8Bit)
			{
				batchArtShareRequestClass.OrigCOV = Path.GetFileName(game.FileArtCOV);
				batchArtShareRequestClass.HashedCOV = CommonFuncs.GetMD5(game.FileArtCOV) + Path.GetExtension(game.FileArtCOV);
			}
			if (game.HasCOV2 && new PNGClass(game.FileArtCOV2).IsPngAnd8Bit)
			{
				batchArtShareRequestClass.OrigCOV2 = Path.GetFileName(game.FileArtCOV2);
				batchArtShareRequestClass.HashedCOV2 = CommonFuncs.GetMD5(game.FileArtCOV2) + Path.GetExtension(game.FileArtCOV2);
			}
			if (game.HasLAB && new PNGClass(game.FileArtLAB).IsPngAnd8Bit)
			{
				batchArtShareRequestClass.OrigLAB = Path.GetFileName(game.FileArtLAB);
				batchArtShareRequestClass.HashedLAB = CommonFuncs.GetMD5(game.FileArtLAB) + Path.GetExtension(game.FileArtLAB);
			}
			if (game.HasLGO && new PNGClass(game.FileArtLGO).IsPngAnd8Bit)
			{
				batchArtShareRequestClass.OrigLGO = Path.GetFileName(game.FileArtLGO);
				batchArtShareRequestClass.HashedLGO = CommonFuncs.GetMD5(game.FileArtLGO) + Path.GetExtension(game.FileArtLGO);
			}
			if (game.HasSCR && new PNGClass(game.FileArtSCR).IsPngAnd8Bit)
			{
				batchArtShareRequestClass.OrigSCR = Path.GetFileName(game.FileArtSCR);
				batchArtShareRequestClass.HashedSCR = CommonFuncs.GetMD5(game.FileArtSCR) + Path.GetExtension(game.FileArtSCR);
			}
			if (game.HasSCR2 && new PNGClass(game.FileArtSCR2).IsPngAnd8Bit)
			{
				batchArtShareRequestClass.OrigSCR2 = Path.GetFileName(game.FileArtSCR2);
				batchArtShareRequestClass.HashedSCR2 = CommonFuncs.GetMD5(game.FileArtSCR2) + Path.GetExtension(game.FileArtSCR2);
			}
			if (game.HasBG && new PNGClass(game.FileArtBG).IsPngAnd8Bit)
			{
				batchArtShareRequestClass.OrigBG = Path.GetFileName(game.FileArtBG);
				batchArtShareRequestClass.HashedBG = CommonFuncs.GetMD5(game.FileArtBG) + Path.GetExtension(game.FileArtBG);
			}
			if (!string.IsNullOrEmpty(batchArtShareRequestClass.OrigBG) | !string.IsNullOrEmpty(batchArtShareRequestClass.OrigCOV) | !string.IsNullOrEmpty(batchArtShareRequestClass.OrigCOV2) | !string.IsNullOrEmpty(batchArtShareRequestClass.OrigLAB) | !string.IsNullOrEmpty(batchArtShareRequestClass.OrigLGO) | !string.IsNullOrEmpty(batchArtShareRequestClass.OrigICO) | !string.IsNullOrEmpty(batchArtShareRequestClass.OrigSCR) | !string.IsNullOrEmpty(batchArtShareRequestClass.OrigSCR2))
			{
				list.Add(batchArtShareRequestClass);
			}
		}
		writeTextBoxShare(string.Format(Translated.BatchArtShare_MsgCheckingArtForGames, list.Count));
		if (CB_OPL_DB.Checked)
		{
			DoBatchArtShareCheck(list.ToArray());
		}
		else
		{
			CopyToFolder(list, text);
		}
	}

	private void CopyToFolder(List<BatchArtShareRequestClass> cenas, string destFolder)
	{
		//IL_0373: Unknown result type (might be due to invalid IL or missing references)
		//IL_0379: Invalid comparison between Unknown and I4
		//IL_03c5: Unknown result type (might be due to invalid IL or missing references)
		//IL_0392: Unknown result type (might be due to invalid IL or missing references)
		//IL_0398: Invalid comparison between Unknown and I4
		//IL_03ba: Unknown result type (might be due to invalid IL or missing references)
		int num = 0;
		int count = cenas.Count;
		ProgressBarStatus.Minimum = 0;
		ProgressBarStatus.Maximum = count;
		foreach (BatchArtShareRequestClass cena in cenas)
		{
			if (!string.IsNullOrEmpty(cena.OrigICO))
			{
				writeTextBoxShare(Translated.BatchArtShare_LogCopyingToFolder + " " + cena.OrigICO);
				File.Copy(OplFolders.ART + cena.OrigICO, destFolder + "\\" + cena.OrigICO, overwrite: true);
			}
			if (!string.IsNullOrEmpty(cena.OrigCOV))
			{
				writeTextBoxShare(Translated.BatchArtShare_LogCopyingToFolder + " " + cena.OrigCOV);
				File.Copy(OplFolders.ART + cena.OrigCOV, destFolder + "\\" + cena.OrigCOV, overwrite: true);
			}
			if (!string.IsNullOrEmpty(cena.OrigCOV2))
			{
				writeTextBoxShare(Translated.BatchArtShare_LogCopyingToFolder + " " + cena.OrigCOV2);
				File.Copy(OplFolders.ART + cena.OrigCOV2, destFolder + "\\" + cena.OrigCOV2, overwrite: true);
			}
			if (!string.IsNullOrEmpty(cena.OrigLAB))
			{
				writeTextBoxShare(Translated.BatchArtShare_LogCopyingToFolder + " " + cena.OrigLAB);
				File.Copy(OplFolders.ART + cena.OrigLAB, destFolder + "\\" + cena.OrigLAB, overwrite: true);
			}
			if (!string.IsNullOrEmpty(cena.OrigLGO))
			{
				writeTextBoxShare(Translated.BatchArtShare_LogCopyingToFolder + " " + cena.OrigLGO);
				File.Copy(OplFolders.ART + cena.OrigLGO, destFolder + "\\" + cena.OrigLGO, overwrite: true);
			}
			if (!string.IsNullOrEmpty(cena.OrigSCR))
			{
				writeTextBoxShare(Translated.BatchArtShare_LogCopyingToFolder + " " + cena.OrigSCR);
				File.Copy(OplFolders.ART + cena.OrigSCR, destFolder + "\\" + cena.OrigSCR, overwrite: true);
			}
			if (!string.IsNullOrEmpty(cena.OrigSCR2))
			{
				writeTextBoxShare(Translated.BatchArtShare_LogCopyingToFolder + " " + cena.OrigSCR2);
				File.Copy(OplFolders.ART + cena.OrigSCR2, destFolder + "\\" + cena.OrigSCR2, overwrite: true);
			}
			if (!string.IsNullOrEmpty(cena.OrigBG))
			{
				writeTextBoxShare(Translated.BatchArtShare_LogCopyingToFolder + " " + cena.OrigBG);
				File.Copy(OplFolders.ART + cena.OrigBG, destFolder + "\\" + cena.OrigBG, overwrite: true);
			}
			num++;
			((Control)L_Status).Text = Translated.Global_String_Progress + " " + Math.Round((double)(num * 100) / (double)count, 0) + "% (" + num + " / " + count + ")";
			((Control)L_StatusArtShareCount).Text = Translated.BatchArtShare_Uploaded + " " + num;
			ProgressBarStatus.Value = num;
		}
		if ((int)MessageBox.Show(Translated.BatchArtShare_MsgZipOffer, Translated.Global_Question, (MessageBoxButtons)4, (MessageBoxIcon)32) == 6)
		{
			((FileDialog)SaveFileDialog1).InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
			if ((int)((CommonDialog)SaveFileDialog1).ShowDialog() == 1)
			{
				ZipFile.CreateFromDirectory(destFolder, ((FileDialog)SaveFileDialog1).FileName);
			}
			else
			{
				MessageBox.Show(Translated.BatchArtShare_MsgZipError, Translated.Global_Error, (MessageBoxButtons)0, (MessageBoxIcon)16);
			}
		}
		MessageBox.Show(Translated.BatchArtShare_MsgFinished);
		((Control)B_start).Enabled = false;
		((Control)B_CopyLog).Enabled = true;
	}

	private async void DoBatchArtShareCheck(BatchArtShareRequestClass[] request)
	{
		BatchArtShareCheckResponse batchArtShareCheckResponse = await CommonFuncs.SoapAPI.BatchArtShareCheckAsync(UserID, request);
		if (batchArtShareCheckResponse == null)
		{
			MessageBox.Show(Translated.BatchArtDownload_ServerReplyError, Translated.Global_Error);
			((Control)B_start).Enabled = false;
			((Control)B_CopyLog).Enabled = true;
			return;
		}
		if (batchArtShareCheckResponse.Body.BatchArtShareCheckResult.Length == 0)
		{
			MessageBox.Show(Translated.BatchArtShare_MsgNothingToShare, Translated.Global_Information);
			((Control)B_start).Enabled = false;
			((Control)B_CopyLog).Enabled = true;
			return;
		}
		int count = 0;
		int total = batchArtShareCheckResponse.Body.BatchArtShareCheckResult.Length;
		ProgressBarStatus.Minimum = 0;
		ProgressBarStatus.Maximum = total;
		BatchArtShareResponseClass[] batchArtShareCheckResult = batchArtShareCheckResponse.Body.BatchArtShareCheckResult;
		foreach (BatchArtShareResponseClass gameReq in batchArtShareCheckResult)
		{
			ArtUploadRequestClass uploaded = new ArtUploadRequestClass
			{
				FileData = File.ReadAllBytes(OplFolders.ART + gameReq.File),
				FileHash = CommonFuncs.GetMD5(OplFolders.ART + gameReq.File) + Path.GetExtension(gameReq.File),
				FileName = gameReq.File,
				GameID = gameReq.GameID,
				GameType = gameReq.GameType
			};
			bool valueOrDefault = (await CommonFuncs.SoapAPI.ArtUploadAsync(UserID, uploaded))?.Body?.ArtUploadResult == true;
			if (valueOrDefault)
			{
				count++;
			}
			((Control)L_Status).Text = Translated.Global_String_Progress + " " + Math.Round((double)(count * 100) / (double)total, 0) + "% (" + count + " / " + total + ")";
			((Control)L_StatusArtShareCount).Text = Translated.BatchArtShare_Uploaded + " " + count;
			ProgressBarStatus.Value = count;
			writeTextBoxShare(gameReq.File + " : " + (valueOrDefault ? Translated.BatchArtShare_MsgUploadOK : Translated.BatchArtShare_MsgBadOrError));
		}
		MessageBox.Show(Translated.BatchArtShare_MsgFinished);
		((Control)B_start).Enabled = false;
		((Control)B_CopyLog).Enabled = true;
	}

	private void writeTextBoxShare(string text)
	{
		((TextBoxBase)TB_Share).AppendText(text + Environment.NewLine);
		((TextBoxBase)TB_Share).Select(((TextBoxBase)TB_Share).TextLength, 0);
		((TextBoxBase)TB_Share).ScrollToCaret();
	}

	private void CB_DestinationChanged(object sender, EventArgs e)
	{
		updateStartButton();
	}

	private void updateStartButton()
	{
		if (!CB_CopyFolder.Checked & !CB_OPL_DB.Checked)
		{
			((Control)B_start).Enabled = false;
		}
		else
		{
			((Control)B_start).Enabled = true;
		}
		OplmSettings.Write("BATCH_ART_SHARE_FOLDER", CB_CopyFolder.Checked.ToString());
		OplmSettings.Write("BATCH_ART_SHARE_OPLMANAGER", CB_OPL_DB.Checked.ToString());
	}

	private void CB_OPL_DB_CheckedChanged(object sender, EventArgs e)
	{
		updateStartButton();
	}

	private void CB_CopyFolder_CheckedChanged_1(object sender, EventArgs e)
	{
		updateStartButton();
	}

	private void B_CopyLog_Click(object sender, EventArgs e)
	{
		//IL_001a: Unknown result type (might be due to invalid IL or missing references)
		Clipboard.SetText(((Control)TB_Share).Text);
		MessageBox.Show(Translated.Global_LogCopied, Translated.Global_Information);
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
		//IL_0603: Unknown result type (might be due to invalid IL or missing references)
		//IL_060d: Expected O, but got Unknown
		//IL_0620: Unknown result type (might be due to invalid IL or missing references)
		//IL_062a: Expected O, but got Unknown
		ComponentResourceManager componentResourceManager = new ComponentResourceManager(typeof(BatchArtShare));
		B_start = new Button();
		TB_Share = new TextBox();
		ProgressBarStatus = new ProgressBar();
		L_StatusArtShareCount = new Label();
		L_Status = new Label();
		FolderBrowserDialog1 = new FolderBrowserDialog();
		SaveFileDialog1 = new SaveFileDialog();
		L_ART_SHARED = new Label();
		GB_ShareTo = new GroupBox();
		CB_CopyFolder = new RadioButton();
		CB_OPL_DB = new RadioButton();
		B_CopyLog = new Button();
		((Control)GB_ShareTo).SuspendLayout();
		((Control)this).SuspendLayout();
		((Control)B_start).Enabled = false;
		((Control)B_start).Location = new Point(431, 236);
		((Control)B_start).Name = "B_start";
		((Control)B_start).Size = new Size(91, 23);
		((Control)B_start).TabIndex = 1;
		((Control)B_start).Text = "Start!";
		((ButtonBase)B_start).UseVisualStyleBackColor = true;
		((Control)B_start).Click += B_start_Click;
		((Control)TB_Share).Location = new Point(12, 32);
		((TextBoxBase)TB_Share).Multiline = true;
		((Control)TB_Share).Name = "TB_Share";
		((TextBoxBase)TB_Share).ReadOnly = true;
		TB_Share.ScrollBars = (ScrollBars)3;
		((Control)TB_Share).Size = new Size(510, 189);
		((Control)TB_Share).TabIndex = 3;
		((Control)ProgressBarStatus).Location = new Point(174, 236);
		((Control)ProgressBarStatus).Name = "ProgressBarStatus";
		((Control)ProgressBarStatus).Size = new Size(251, 23);
		((Control)ProgressBarStatus).TabIndex = 4;
		((Control)L_StatusArtShareCount).Location = new Point(305, 262);
		((Control)L_StatusArtShareCount).Name = "L_StatusArtShareCount";
		((Control)L_StatusArtShareCount).Size = new Size(120, 24);
		((Control)L_StatusArtShareCount).TabIndex = 22;
		((Control)L_StatusArtShareCount).Text = "Uploaded: 0";
		L_StatusArtShareCount.TextAlign = (ContentAlignment)64;
		((Control)L_Status).Location = new Point(174, 262);
		((Control)L_Status).Name = "L_Status";
		((Control)L_Status).Size = new Size(128, 24);
		((Control)L_Status).TabIndex = 16;
		((Control)L_Status).Text = "Progress: 000% (0 / 0)";
		L_Status.TextAlign = (ContentAlignment)16;
		((FileDialog)SaveFileDialog1).Filter = "Zip file|*.zip";
		((Control)L_ART_SHARED).AutoSize = true;
		((Control)L_ART_SHARED).Location = new Point(215, 16);
		((Control)L_ART_SHARED).Name = "L_ART_SHARED";
		((Control)L_ART_SHARED).Size = new Size(67, 13);
		((Control)L_ART_SHARED).TabIndex = 24;
		((Control)L_ART_SHARED).Text = "ART shared:";
		((Control)GB_ShareTo).Controls.Add((Control)(object)CB_CopyFolder);
		((Control)GB_ShareTo).Controls.Add((Control)(object)CB_OPL_DB);
		((Control)GB_ShareTo).Location = new Point(12, 227);
		((Control)GB_ShareTo).Name = "GB_ShareTo";
		((Control)GB_ShareTo).Size = new Size(156, 68);
		((Control)GB_ShareTo).TabIndex = 29;
		GB_ShareTo.TabStop = false;
		((Control)GB_ShareTo).Text = "Share to...";
		((Control)CB_CopyFolder).AutoSize = true;
		((Control)CB_CopyFolder).Location = new Point(6, 45);
		((Control)CB_CopyFolder).Name = "CB_CopyFolder";
		((Control)CB_CopyFolder).Size = new Size(90, 17);
		((Control)CB_CopyFolder).TabIndex = 31;
		((Control)CB_CopyFolder).Text = "Copy to folder";
		((ButtonBase)CB_CopyFolder).UseVisualStyleBackColor = true;
		((Control)CB_OPL_DB).AutoSize = true;
		CB_OPL_DB.Checked = true;
		((Control)CB_OPL_DB).Location = new Point(6, 19);
		((Control)CB_OPL_DB).Name = "CB_OPL_DB";
		((Control)CB_OPL_DB).Size = new Size(109, 17);
		((Control)CB_OPL_DB).TabIndex = 30;
		CB_OPL_DB.TabStop = true;
		((Control)CB_OPL_DB).Text = "OPL Manager DB";
		((ButtonBase)CB_OPL_DB).UseVisualStyleBackColor = true;
		((Control)B_CopyLog).Enabled = false;
		((Control)B_CopyLog).Location = new Point(431, 266);
		((Control)B_CopyLog).Name = "B_CopyLog";
		((Control)B_CopyLog).Size = new Size(91, 23);
		((Control)B_CopyLog).TabIndex = 30;
		((Control)B_CopyLog).Text = "Copy LOG";
		((ButtonBase)B_CopyLog).UseVisualStyleBackColor = true;
		((Control)B_CopyLog).Click += B_CopyLog_Click;
		((ContainerControl)this).AutoScaleDimensions = new SizeF(6f, 13f);
		((ContainerControl)this).AutoScaleMode = (AutoScaleMode)1;
		((Form)this).ClientSize = new Size(524, 296);
		((Control)this).Controls.Add((Control)(object)B_CopyLog);
		((Control)this).Controls.Add((Control)(object)GB_ShareTo);
		((Control)this).Controls.Add((Control)(object)L_ART_SHARED);
		((Control)this).Controls.Add((Control)(object)L_StatusArtShareCount);
		((Control)this).Controls.Add((Control)(object)L_Status);
		((Control)this).Controls.Add((Control)(object)ProgressBarStatus);
		((Control)this).Controls.Add((Control)(object)TB_Share);
		((Control)this).Controls.Add((Control)(object)B_start);
		((Control)this).Font = new Font("Microsoft Sans Serif", 8f, (FontStyle)0, (GraphicsUnit)3);
		((Form)this).FormBorderStyle = (FormBorderStyle)3;
		((Form)this).Icon = (Icon)componentResourceManager.GetObject("$this.Icon");
		((Form)this).MaximizeBox = false;
		((Control)this).MaximumSize = new Size(540, 335);
		((Control)this).MinimumSize = new Size(540, 335);
		((Control)this).Name = "BatchArtShare";
		((Form)this).ShowInTaskbar = false;
		((Form)this).StartPosition = (FormStartPosition)4;
		((Control)this).Text = "Share Arts";
		((Form)this).Shown += BatchArtShare_Shown;
		((Control)GB_ShareTo).ResumeLayout(false);
		((Control)GB_ShareTo).PerformLayout();
		((Control)this).ResumeLayout(false);
		((Control)this).PerformLayout();
	}
}
