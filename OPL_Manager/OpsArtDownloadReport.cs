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

public class OpsArtDownloadReport : BaseForm
{
	private string FileSubmit = "";

	private GameInfo Game;

	private ArtType GameArtType;

	private OPLM_Main mainF = Program.MainFormInst;

	private IContainer components;

	internal Label Label1;

	internal TextBox TB_GameID;

	internal Label Label2;

	internal TextBox TB_ArtType;

	internal Label Label3;

	internal TextBox TB_FileHash;

	internal Label Label4;

	internal TextBox TB_Comments;

	internal Button Button1;

	internal OpenFileDialog OpenFileDialog1;

	internal CheckBox CB_SubmitArt;

	internal PictureBox PictureBox1;

	internal Label Label5;

	internal TextBox TB_GameTitle;

	public OpsArtDownloadReport()
	{
		InitializeComponent();
	}

	public DialogResult ShowDialog(GameInfo _Game, ArtType _Type, List<string> _ServerHashes, int _HashIdx)
	{
		//IL_034c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0269: Unknown result type (might be due to invalid IL or missing references)
		Game = _Game;
		DoGetGameTitle();
		((Control)TB_GameID).Text = Game.ID;
		((Control)TB_FileHash).Text = Path.GetFileName(_ServerHashes[_HashIdx]);
		((Control)TB_ArtType).Text = Enum.GetName(typeof(ArtType), _Type);
		GameArtType = _Type;
		((Control)TB_Comments).Focus();
		FileSubmit = "";
		if (_Type == ArtType.ICO && mainF.SelectedGame.HasICO)
		{
			FileSubmit = mainF.SelectedGame.FileArtICO;
		}
		else if (_Type == ArtType.COV && mainF.SelectedGame.HasCOV)
		{
			FileSubmit = mainF.SelectedGame.FileArtCOV;
		}
		else if (_Type == ArtType.LAB && mainF.SelectedGame.HasLAB)
		{
			FileSubmit = mainF.SelectedGame.FileArtLAB;
		}
		else if (_Type == ArtType.COV2 && mainF.SelectedGame.HasCOV2)
		{
			FileSubmit = mainF.SelectedGame.FileArtCOV2;
		}
		else if (_Type == ArtType.SCR && mainF.SelectedGame.HasSCR)
		{
			FileSubmit = mainF.SelectedGame.FileArtSCR;
		}
		else if (_Type == ArtType.SCR2 && mainF.SelectedGame.HasSCR2)
		{
			FileSubmit = mainF.SelectedGame.FileArtSCR2;
		}
		else if (_Type == ArtType.LGO && mainF.SelectedGame.HasLGO)
		{
			FileSubmit = mainF.SelectedGame.FileArtLGO;
		}
		else if (_Type == ArtType.BG && mainF.SelectedGame.HasBG)
		{
			FileSubmit = mainF.SelectedGame.FileArtBG;
		}
		if (!string.IsNullOrEmpty(FileSubmit))
		{
			string mD = CommonFuncs.GetMD5(FileSubmit);
			if (_ServerHashes.Select((string x) => Path.GetFileNameWithoutExtension(x)).ToList().Contains(mD))
			{
				FileSubmit = "";
			}
			else if (!new PNGClass(File.ReadAllBytes(FileSubmit)).IsPngAnd8Bit)
			{
				MessageBox.Show(Translated.OpsArtDownload_8BitPngRequired);
				FileSubmit = "";
			}
		}
		if (string.IsNullOrEmpty(FileSubmit))
		{
			((Form)this).Size = new Size(((Control)CB_SubmitArt).Location.X, ((Form)this).Size.Height);
			((Control)this).MaximumSize = new Size(((Control)CB_SubmitArt).Location.X + 5, ((Form)this).Size.Height);
			((Control)this).MinimumSize = new Size(((Control)CB_SubmitArt).Location.X + 5, ((Form)this).Size.Height);
		}
		else
		{
			((Control)PictureBox1).BackColor = Color.Gray;
			((Control)PictureBox1).BackgroundImage = Image.FromStream((Stream)new MemoryStream(File.ReadAllBytes(FileSubmit)));
		}
		((Form)this).DialogResult = (DialogResult)2;
		return ((Form)this).ShowDialog();
	}

	private async void DoGetGameTitle()
	{
		GetGameNameByIdResponse getGameNameByIdResponse = await CommonFuncs.SoapAPI.GetGameNameByIdAsync(Game.Type, Game.ID);
		if (!string.IsNullOrEmpty(getGameNameByIdResponse?.Body?.GetGameNameByIdResult))
		{
			((Control)TB_GameTitle).Text = getGameNameByIdResponse.Body.GetGameNameByIdResult;
		}
		else
		{
			((Control)TB_GameTitle).Text = "";
		}
	}

	private async void Button1_Click(object sender, EventArgs e)
	{
		if (((Control)TB_Comments).Text.Length <= 5)
		{
			MessageBox.Show(Translated.OpsArtDownloadReport_Explain, Translated.Global_Information, (MessageBoxButtons)0, (MessageBoxIcon)64);
			return;
		}
		((Control)Button1).Enabled = false;
		ReportArtResponse reportArtResponse;
		if (CB_SubmitArt.Checked && !string.IsNullOrEmpty(FileSubmit) && File.Exists(FileSubmit))
		{
			ArtUploadRequestClass artReplacement = new ArtUploadRequestClass
			{
				GameID = ((Control)TB_GameID).Text,
				FileData = File.ReadAllBytes(FileSubmit),
				FileHash = CommonFuncs.GetMD5(FileSubmit) + Path.GetExtension(FileSubmit),
				FileName = Path.GetFileName(FileSubmit),
				GameType = Game.Type
			};
			reportArtResponse = await CommonFuncs.SoapAPI.ReportArtAsync(mainF.UserID, Game.Type, GameArtType, Game.ID, ((Control)TB_FileHash).Text, ((Control)TB_Comments).Text, artReplacement);
		}
		else
		{
			reportArtResponse = await CommonFuncs.SoapAPI.ReportArtAsync(mainF.UserID, Game.Type, GameArtType, Game.ID, ((Control)TB_FileHash).Text, ((Control)TB_Comments).Text, null);
		}
		if (reportArtResponse == null)
		{
			MessageBox.Show(Translated.OpsArtDownloadReport_SubmitError, Translated.Global_Information);
		}
		else
		{
			MessageBox.Show(Translated.OpsArtDownloadReport_Submited, Translated.Global_Information);
		}
		((Form)this).Close();
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
		//IL_0516: Unknown result type (might be due to invalid IL or missing references)
		//IL_0520: Expected O, but got Unknown
		//IL_0594: Unknown result type (might be due to invalid IL or missing references)
		//IL_059e: Expected O, but got Unknown
		//IL_070d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0717: Expected O, but got Unknown
		//IL_072a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0734: Expected O, but got Unknown
		ComponentResourceManager componentResourceManager = new ComponentResourceManager(typeof(OpsArtDownloadReport));
		Label1 = new Label();
		TB_GameID = new TextBox();
		Label2 = new Label();
		TB_ArtType = new TextBox();
		Label3 = new Label();
		TB_FileHash = new TextBox();
		Label4 = new Label();
		TB_Comments = new TextBox();
		Button1 = new Button();
		OpenFileDialog1 = new OpenFileDialog();
		CB_SubmitArt = new CheckBox();
		PictureBox1 = new PictureBox();
		Label5 = new Label();
		TB_GameTitle = new TextBox();
		((ISupportInitialize)PictureBox1).BeginInit();
		((Control)this).SuspendLayout();
		((Control)Label1).Location = new Point(12, 10);
		((Control)Label1).Name = "Label1";
		((Control)Label1).Size = new Size(52, 20);
		((Control)Label1).TabIndex = 0;
		((Control)Label1).Text = "Game:";
		Label1.TextAlign = (ContentAlignment)64;
		((Control)TB_GameID).Location = new Point(70, 12);
		((Control)TB_GameID).Name = "TB_GameID";
		((TextBoxBase)TB_GameID).ReadOnly = true;
		((Control)TB_GameID).Size = new Size(81, 20);
		((Control)TB_GameID).TabIndex = 7;
		((Control)Label2).Location = new Point(173, 12);
		((Control)Label2).Name = "Label2";
		((Control)Label2).Size = new Size(41, 20);
		((Control)Label2).TabIndex = 2;
		((Control)Label2).Text = "Type:";
		Label2.TextAlign = (ContentAlignment)64;
		((Control)TB_ArtType).Location = new Point(220, 12);
		((Control)TB_ArtType).Name = "TB_ArtType";
		((TextBoxBase)TB_ArtType).ReadOnly = true;
		((Control)TB_ArtType).Size = new Size(85, 20);
		((Control)TB_ArtType).TabIndex = 3;
		((Control)Label3).Location = new Point(12, 38);
		((Control)Label3).Name = "Label3";
		((Control)Label3).Size = new Size(52, 20);
		((Control)Label3).TabIndex = 4;
		((Control)Label3).Text = "Hash:";
		Label3.TextAlign = (ContentAlignment)64;
		((Control)TB_FileHash).Location = new Point(70, 38);
		((Control)TB_FileHash).Name = "TB_FileHash";
		((TextBoxBase)TB_FileHash).ReadOnly = true;
		((Control)TB_FileHash).Size = new Size(235, 20);
		((Control)TB_FileHash).TabIndex = 5;
		((Control)Label4).Location = new Point(18, 102);
		((Control)Label4).Name = "Label4";
		((Control)Label4).Size = new Size(287, 13);
		((Control)Label4).TabIndex = 6;
		((Control)Label4).Text = "Comment/Reason/Explanation:";
		Label4.TextAlign = (ContentAlignment)32;
		((Control)TB_Comments).Location = new Point(18, 118);
		((TextBoxBase)TB_Comments).Multiline = true;
		((Control)TB_Comments).Name = "TB_Comments";
		((Control)TB_Comments).Size = new Size(287, 96);
		((Control)TB_Comments).TabIndex = 1;
		((Control)Button1).Location = new Point(70, 220);
		((Control)Button1).Name = "Button1";
		((Control)Button1).Size = new Size(196, 30);
		((Control)Button1).TabIndex = 8;
		((Control)Button1).Text = "Submit";
		((ButtonBase)Button1).UseVisualStyleBackColor = true;
		((Control)Button1).Click += Button1_Click;
		((FileDialog)OpenFileDialog1).FileName = "OpenFileDialog1";
		((FileDialog)OpenFileDialog1).Filter = "ARTs|*.png";
		((Control)CB_SubmitArt).AutoSize = true;
		((Control)CB_SubmitArt).Location = new Point(323, 10);
		((Control)CB_SubmitArt).Name = "CB_SubmitArt";
		((Control)CB_SubmitArt).Size = new Size(226, 17);
		((Control)CB_SubmitArt).TabIndex = 9;
		((Control)CB_SubmitArt).Text = "Submit this ART of mine as a replacement:";
		((ButtonBase)CB_SubmitArt).UseVisualStyleBackColor = true;
		((Control)PictureBox1).BackgroundImageLayout = (ImageLayout)4;
		((Control)PictureBox1).Location = new Point(323, 38);
		((Control)PictureBox1).Name = "PictureBox1";
		((Control)PictureBox1).Size = new Size(226, 187);
		PictureBox1.TabIndex = 10;
		PictureBox1.TabStop = false;
		((Control)Label5).Font = new Font("Microsoft Sans Serif", 8.25f, (FontStyle)1, (GraphicsUnit)3);
		((Control)Label5).Location = new Point(12, 67);
		((Control)Label5).Name = "Label5";
		((Control)Label5).Size = new Size(52, 20);
		((Control)Label5).TabIndex = 11;
		((Control)Label5).Text = "Title:";
		Label5.TextAlign = (ContentAlignment)64;
		((Control)TB_GameTitle).Font = new Font("Microsoft Sans Serif", 8.25f, (FontStyle)1, (GraphicsUnit)3);
		((Control)TB_GameTitle).Location = new Point(70, 68);
		((Control)TB_GameTitle).Name = "TB_GameTitle";
		((TextBoxBase)TB_GameTitle).ReadOnly = true;
		((Control)TB_GameTitle).Size = new Size(235, 20);
		((Control)TB_GameTitle).TabIndex = 12;
		((ContainerControl)this).AutoScaleDimensions = new SizeF(6f, 13f);
		((ContainerControl)this).AutoScaleMode = (AutoScaleMode)1;
		((Form)this).ClientSize = new Size(559, 262);
		((Control)this).Controls.Add((Control)(object)TB_GameTitle);
		((Control)this).Controls.Add((Control)(object)Label5);
		((Control)this).Controls.Add((Control)(object)PictureBox1);
		((Control)this).Controls.Add((Control)(object)CB_SubmitArt);
		((Control)this).Controls.Add((Control)(object)Button1);
		((Control)this).Controls.Add((Control)(object)TB_Comments);
		((Control)this).Controls.Add((Control)(object)Label4);
		((Control)this).Controls.Add((Control)(object)TB_FileHash);
		((Control)this).Controls.Add((Control)(object)Label3);
		((Control)this).Controls.Add((Control)(object)TB_ArtType);
		((Control)this).Controls.Add((Control)(object)Label2);
		((Control)this).Controls.Add((Control)(object)TB_GameID);
		((Control)this).Controls.Add((Control)(object)Label1);
		((Control)this).Font = new Font("Microsoft Sans Serif", 8f, (FontStyle)0, (GraphicsUnit)3);
		((Form)this).FormBorderStyle = (FormBorderStyle)1;
		((Form)this).Icon = (Icon)componentResourceManager.GetObject("$this.Icon");
		((Form)this).MaximizeBox = false;
		((Form)this).MinimizeBox = false;
		((Control)this).MinimumSize = new Size(575, 300);
		((Control)this).Name = "OpsArtDownloadReport";
		((Form)this).StartPosition = (FormStartPosition)4;
		((Control)this).Text = "ART Report";
		((ISupportInitialize)PictureBox1).EndInit();
		((Control)this).ResumeLayout(false);
		((Control)this).PerformLayout();
	}
}
